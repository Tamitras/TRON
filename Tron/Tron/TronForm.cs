using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using static Tron.Helper;
using System.Threading.Tasks;
using WindowsInput;
using CoreAudioApi;
using Tron.Classes;

namespace Tron
{
    public partial class TronForm : Form
    {
        public Boolean TemplateFound { get; set; }

        /// <summary>
        /// Offsize zwischen Screen und Window (links)
        /// </summary>
        private Int32 OffSizeLeft { get; set; }

        /// <summary>
        /// Offsize zwischen Screen und Window (Oben)
        /// </summary>
        private Int32 OffSizeTop { get; set; }
        public Double CurrentCoeff { get; set; }

        /// <summary>
        /// Liste mit Templates
        /// z.B. Lebensbalken des Spielers
        /// </summary>
        public List<Tuple<Bitmap, String>> Templates { get; set; }

        /// <summary>
        /// Global Mouse Hook Interface-Events
        /// </summary>
        private IKeyboardMouseEvents m_GlobalHook;


        public Bitmap CurrentScreenshotWindow { get; set; }
        public Bitmap CurrentScreenshotBobber { get; set; }
        public List<Process> Processes { get; set; }
        public Process CurrentProcess { get; set; }

        public IntPtr ProcessHandlePointer { get; set; }

        public Int32 FoundTemplateX { get; set; }
        public Int32 FoundTemplateY { get; set; }

        public Int32 CalcedTemplateX { get; set; }
        public Int32 CalcedTemplateY { get; set; }

        Int32 CurrentMousePosX { get; set; }
        Int32 CurrentMousePosY { get; set; }
        public int CoeffType { get; private set; }

        ///  average
        private int _aFishWait;

        /// <summary>
        /// 
        /// </summary>
        private readonly Ears _ears;


        /// <summary>
        /// Konstruktor
        /// </summary>
        public TronForm()
        {
            InitializeComponent();

            Templates = new List<Tuple<Bitmap, String>>();
            m_GlobalHook = Hook.GlobalEvents();
            m_GlobalHook.MouseMove += M_GlobalHook_MouseMove;
            CurrentScreenshotWindow = null;
            Processes = new List<Process>();
            Initialize();
        }

        private void M_GlobalHook_MouseMove(object sender, MouseEventArgs e)
        {
            CurrentMousePosX = e.X;
            CurrentMousePosY = e.Y;

            labelMousePosX.Text = CurrentMousePosX.ToString();
            labelMousePosY.Text = CurrentMousePosY.ToString();
        }

        private void WriteToLog(String text)
        {
            BeginInvoke(new MethodInvoker(delegate ()
            {
                this.textBoxLog.Text += DateTime.Now.ToLongTimeString() + ": " + text + Environment.NewLine;
            }));
        }

        /// <summary>
        /// Initialisiert die Anwendung
        /// </summary>
        public void Initialize()
        {
            // Filtern nach name (sortby c=> c.name) etc.
            //Processes = Process.GetProcesses().OrderBy(c => c.ProcessName).ToList();
            //Processes = Process.GetProcesses().Where(c => c.ProcessName.Contains("notepad")).ToList();
            Processes = Process.GetProcesses().Where(c => c.ProcessName.Contains("Wow")).ToList();

            if (Processes.Any())
            {
                bindingSource1.DataSource = Processes;
                listBoxProcess.DisplayMember = "ProcessName";
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Liefert den aktuellen Bildausschnitt
        /// </summary>
        public Bitmap GetCurrentScreentshot()
        {
            try
            {
                //Bitmap btmp = Helper.createBitmap(ptr1, 640, 480);
                // best capture for 640x480 resolution

                return Helper.createBitmap(ProcessHandlePointer, 640, 480);
                //return Helper.createBitmap(ProcessHandlePointer, 1920, 1080);
            }
            catch (Exception ex)
            {
                this.WriteToLog(ex.Message);
                return null;
            }
        }


        /// <summary>
        /// Aktualisiert die PicutreBox
        /// </summary>
        /// <param name="res"></param>
        private void RefreshUIFoundTemplate(Bitmap res)
        {
            try
            {
                if (this.pictureBoxFoundImage.InvokeRequired)
                {
                    Invoke(new MethodInvoker(delegate ()
                    {
                        RefreshUIFoundTemplate(res);
                    }));
                }
                else
                {
                    pictureBoxFoundImage.Image = res;
                }
            }
            catch (Exception ex)
            {
                this.WriteToLog(ex.Message);
            }
        }

        private void RefreshDebug()
        {
            try
            {
                if (this.lblCoeff.InvokeRequired)
                {
                    Invoke(new MethodInvoker(delegate ()
                    {
                        RefreshDebug();
                    }));
                }
                else
                {
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Aktualisiert die PicutreBox
        /// </summary>
        /// <param name="res"></param>
        private void RefreshUILiveTemplate(Bitmap res)
        {
            try
            {
                if (this.pictureBoxLiveImage.InvokeRequired)
                {
                    Invoke(new MethodInvoker(delegate ()
                    {
                        RefreshUILiveTemplate(res);
                    }));
                }
                else
                {
                    pictureBoxLiveImage.Image = res;
                    lblFoundX.Text = FoundTemplateX.ToString();
                    lblFoundY.Text = FoundTemplateY.ToString();

                    labelMousePosX.Text = CurrentMousePosX.ToString();
                    labelMousePosY.Text = CurrentMousePosY.ToString();

                    lblCalcedPosX.Text = CalcedTemplateX.ToString();
                    lblCalcedPosY.Text = CalcedTemplateY.ToString();
                }
            }
            catch (Exception ex)
            {
                this.WriteToLog(ex.Message);
            }
        }

        /// <summary>
        /// Analysiert den aktuellen Screenshot und vergleicht diesen mit einem template
        /// </summary>
        private async void AnalyseScreenshot()
        {
            try
            {
                Bitmap temp = (Bitmap)CurrentScreenshotWindow.Clone();

                Image<Bgr, byte> source = new Image<Bgr, byte>(temp);
                //Image<Bgr, byte> template = new Image<Bgr, byte>("C:/Users/ekaufmann/Desktop/screenys/theme.bmp"); // Image A
                Image<Bgr, byte> template = new Image<Bgr, byte>(Properties.Resources.hilfe_large); // Image A

                Boolean bitten = false;
                Image<Bgr, byte> res = this.FindTemplate(template, source, out bitten);

                var x = (CurrentScreenshotWindow.Width / 2) - 200;
                var y = (CurrentScreenshotWindow.Height / 2) - 200;

                if (TemplateFound)
                {
                    Rectangle cropRect = new Rectangle(x, y, 400, 400);
                    var croppedRes = this.CropImage(res.Bitmap, cropRect);

                    await Task.Run(() =>
                    {
                        //RefreshUIFoundTemplate(res.ToBitmap());
                        RefreshUIFoundTemplate(croppedRes);
                    });
                }

            }
            catch (Exception ex)
            {
                this.WriteToLog(ex.Message);
            }
        }

        private Bitmap CropImage(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            return bmpImage.Clone(cropArea, bmpImage.PixelFormat);
        }

        /// <summary>
        /// Asynchroner Task zum analysieren des Processes
        /// Erstellt Screenshots vom Process, alle 10MS
        /// </summary>
        private async void StartGetCurrentScreentshotAsync()
        {
            try
            {
                _ = Task.Run(() =>
                  {
                      while (true)
                      {
                          CurrentScreenshotWindow = CurrentScreenshotFromScreen();
                          RefreshUILiveTemplate(CurrentScreenshotWindow);
                          AnalyseScreenshot();

                          Task.Delay(500);
                      }
                  });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private Bitmap CurrentScreenshotFromScreen()
        {
            //Bitmap bitmap = new Bitmap(1000, 800);

            var rect = new Helper.Rect();
            Helper.GetWindowRect(CurrentProcess.MainWindowHandle, ref rect);


            var width = rect.right - rect.left;
            var height = rect.bottom - rect.top;

            //Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Bitmap bitmap = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(bitmap as Image);

            //graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
            //graphics.CopyFromScreen(rect.left, rect.top, width, height, bitmap.Size);
            graphics.CopyFromScreen(rect.left, rect.top, 0, 0, bitmap.Size);
            graphics.Dispose();

            return bitmap;
        }

        private Bitmap CurrentScreenshotFromBobber()
        {
            Rectangle cropRect = new Rectangle(CalcedTemplateY, CalcedTemplateX, 180, 180);
            Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);

            using (Graphics g = Graphics.FromImage(target))
            {
                g.DrawImage(CurrentScreenshotWindow, new Rectangle(0, 0, target.Width, target.Height),
                                 cropRect,
                                 GraphicsUnit.Pixel);
            }

            return target;
        }


        private void MoveMouseAndSimulateClick()
        {
            InvokeIfRequired(this, (MethodInvoker)delegate ()
            {
                Int32 oldMousePosX = CurrentMousePosX;
                Int32 oldMousePosY = CurrentMousePosY;

                Helper.SetCursorPos((int)CalcedTemplateX, (int)CalcedTemplateY);
                System.Threading.Thread.Sleep(1000);
                Helper.mouse_event(Helper.MOUSEEVENTF_RIGHTDOWN | Helper.MOUSEEVENTF_RIGHTUP, CalcedTemplateX, CalcedTemplateY, 0, 0);

                Thread.Sleep(1000);
            });

        }


        private void InvokeIfRequired(Control target, Delegate methodToInvoke)
        {
            /* Mit Hilfe von InvokeRequired wird geprüft ob der Aufruf direkt an die UI gehen kann oder
             * ob ein Invokeing hier von Nöten ist
             */
            if (target.InvokeRequired)
            {
                // Das Control muss per Invoke geändert werden, weil der aufruf aus einem Backgroundthread kommt
                target.Invoke(methodToInvoke);
            }
            else
            {
                // Die Änderung an der UI kann direkt aufgerufen werden.
                methodToInvoke.DynamicInvoke();
            }
        }

        private void BtnStartConnect_Click(object sender, EventArgs e)
        {
            StartGetCurrentScreentshotAsync();
        }

        private void listBoxProcess_DoubleClick(object sender, EventArgs e)
        {
            CurrentProcess = (Process)listBoxProcess.SelectedItem;
            ProcessHandlePointer = CurrentProcess.MainWindowHandle;
            textBoxLog.Text += "Ausgewählter Process: " + CurrentProcess.ProcessName + ", " + CurrentProcess.MainWindowTitle;
            btnStartConnect.Enabled = true;
        }


        private Image<Bgr, byte> FindTemplate(Image<Bgr, byte> template, Image<Bgr, byte> source, out Boolean bitten)
        {
            Image<Bgr, byte> imageToShow = source.Copy();
            bitten = false;
            switch (CoeffType)
            {
                case 0:
                    using (Image<Gray, float> result = source.MatchTemplate(template, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed))
                    {
                        CalculateTemplateCoeff(template, imageToShow, result);
                    }
                    break;
                case 1:
                    using (Image<Gray, float> result = source.MatchTemplate(template, Emgu.CV.CvEnum.TemplateMatchingType.Ccoeff))
                    {
                        CalculateTemplateCoeff(template, imageToShow, result);
                    }
                    break;
                case 2:
                    using (Image<Gray, float> result = source.MatchTemplate(template, Emgu.CV.CvEnum.TemplateMatchingType.Ccorr))
                    {
                        CalculateTemplateCoeff(template, imageToShow, result);
                    }
                    break;
                case 3:
                    using (Image<Gray, float> result = source.MatchTemplate(template, Emgu.CV.CvEnum.TemplateMatchingType.CcorrNormed))
                    {
                        CalculateTemplateCoeff(template, imageToShow, result);
                    }
                    break;
                case 4:
                    using (Image<Gray, float> result = source.MatchTemplate(template, Emgu.CV.CvEnum.TemplateMatchingType.Sqdiff))
                    {
                        CalculateTemplateCoeff(template, imageToShow, result);
                    }
                    break;
                case 5:
                    using (Image<Gray, float> result = source.MatchTemplate(template, Emgu.CV.CvEnum.TemplateMatchingType.SqdiffNormed))
                    {
                        CalculateTemplateCoeff(template, imageToShow, result);
                    }
                    break;

                default:
                    break;
            }



            return imageToShow;
        }

        private void CalculateTemplateCoeff(Image<Bgr, byte> template, Image<Bgr, byte> imageToShow, Image<Gray, float> result)
        {
            double[] minValues, maxValues;
            Point[] minLocations, maxLocations;
            result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);

            CurrentCoeff = maxValues[0];

            BeginInvoke(new MethodInvoker(delegate ()
            {
                this.lblCurrentCoeff.Text = CurrentCoeff.ToString();
            }
            ));

            // You can try different values of the threshold. I guess somewhere between 0.75 and 0.95 would be good.
            if (maxValues[0] > 0.95)
            {
                FoundTemplateX = maxLocations[0].X;
                FoundTemplateY = maxLocations[0].Y;

                // This is a match. Do something with it, for example draw a rectangle around it.
                Rectangle match = new Rectangle(maxLocations[0], template.Size);
                imageToShow.Draw(match, new Bgr(Color.Yellow), 2);
                imageToShow.Draw($"Coeff: {CurrentCoeff}", new Point(match.X -70, match.Y -50), Emgu.CV.CvEnum.FontFace.HersheyComplex, 0.4, new Bgr(Color.Blue));
                imageToShow.Draw($"X: {FoundTemplateX}", new Point(match.X - 70, match.Y -65), Emgu.CV.CvEnum.FontFace.HersheyComplex, 0.4, new Bgr(Color.Blue));
                imageToShow.Draw($"Y: {FoundTemplateY}", new Point(match.X - 70, match.Y - 80), Emgu.CV.CvEnum.FontFace.HersheyComplex, 0.4, new Bgr(Color.Blue));

                var rect = new Helper.Rect();
                Helper.GetWindowRect(CurrentProcess.MainWindowHandle, ref rect);

                OffSizeLeft = rect.left;
                OffSizeTop = rect.top;
                int width = rect.right - rect.left;
                int height = rect.bottom - rect.top;

                CalcedTemplateX = maxLocations[0].X + (template.Size.Width / 2) + OffSizeLeft;
                CalcedTemplateY = maxLocations[0].Y + (template.Size.Height) + OffSizeTop;

                var center = new PointF(match.Left + match.Width / 2, match.Top + match.Height / 2);
                Ellipse ellipse = new Ellipse(center, new SizeF(1, 1), 0);
                imageToShow.Draw(ellipse, new Bgr(Color.Magenta), 3);

                TemplateFound = true;
            }
            else
            {
                TemplateFound = false;
            }

        }

        private void TrackBarEmguType_Scroll(object sender, EventArgs e)
        {
            CoeffType = this.trackBarEmguType.Value;
        }

        private void BtnRightClick_Click(object sender, EventArgs e)
        {
            MoveMouseAndSimulateClick();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // offset 18-25 allowed
            Helper.LeftMouseClick(this.FoundTemplateX, this.FoundTemplateY - 25, CurrentProcess.MainWindowHandle);
        }

        private void btnEsc_Click(object sender, EventArgs e)
        {
            PostMessage(CurrentProcess.MainWindowHandle, WM_KEYDOWN, (IntPtr)Keys.Escape, IntPtr.Zero);
        }
    }
}
