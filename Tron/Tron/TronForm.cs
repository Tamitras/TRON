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

namespace Tron
{
    public partial class TronForm : Form
    {

        public Boolean TemlateFound_Head { get; set; }

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


        public Bitmap CurrentScreenshot { get; set; }
        public List<Process> Processes { get; set; }
        public Process CurrentProcess { get; set; }

        public IntPtr ProcessHandlePointer { get; set; }

        public Int32 FoundTemplateX { get; set; }
        public Int32 FoundTemplateY { get; set; }

        public Int32 CalcedTemplateX { get; set; }
        public Int32 CalcedTemplateY { get; set; }

        Int32 CurrentMousePosX { get; set; }
        Int32 CurrentMousePosY { get; set; }


        /// <summary>
        /// Konstruktor
        /// </summary>
        public TronForm()
        {
            InitializeComponent();

            Templates = new List<Tuple<Bitmap, String>>();
            m_GlobalHook = Hook.GlobalEvents();
            m_GlobalHook.MouseMove += M_GlobalHook_MouseMove;
            CurrentScreenshot = null;
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
            //this.textBoxLog.Text += DateTime.Now.ToLongTimeString() + ": " + text + Environment.NewLine;
        }

        /// <summary>
        /// Initialisiert die Anwendung
        /// </summary>
        public void Initialize()
        {
            // Filtern nach name (sortby c=> c.name) etc.
            //Processes = Process.GetProcesses().OrderBy(c => c.ProcessName).ToList();
            //Processes = Process.GetProcesses().Where(c => c.ProcessName.Contains("notepad")).ToList();
            Processes = Process.GetProcesses().Where(c => c.ProcessName.Contains("talon")).ToList();
            bindingSource1.DataSource = Processes;
            listBoxProcess.DisplayMember = "ProcessName";
        }


        /// <summary>
        /// 
        /// </summary>
        public Bitmap AnalyzeCurrentProgress()
        {
            try
            {
                //Bitmap btmp = Helper.createBitmap(ptr1, 640, 480);
                // best capture for 640x480 resolution

                return Helper.createBitmap(ProcessHandlePointer, 803, 603);

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
        private void RefreshUI(Bitmap res)
        {
            try
            {
                if (this.pictureBoxFoundImage.InvokeRequired)
                {
                    Invoke(new MethodInvoker(delegate ()
                    {
                        RefreshUI(res);
                    }));
                }
                else
                {
                    pictureBoxFoundImage.Image = res;
                    lblCurrentCoeff.Text = CurrentCoeff.ToString();
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
                Image<Bgr, byte> source = new Image<Bgr, byte>(CurrentScreenshot);
                //Image<Bgr, byte> template = new Image<Bgr, byte>("C:/Users/ekaufmann/Desktop/screenys/theme.bmp"); // Image A
                Image<Bgr, byte> template = new Image<Bgr, byte>(Properties.Resources.templateHead); // Image A
                Image<Bgr, byte> res = FindTemplate(template, source);

                await Task.Run(() =>
                {
                    RefreshUI(res.ToBitmap());
                });

            }
            catch (Exception ex)
            {
                this.WriteToLog(ex.Message);
            }
        }

        /// <summary>
        /// Asynchroner Task zum analysieren des Processes
        /// Macht Screenshots vom Process
        /// </summary>
        private async void StartAnalyseProcessAsync()
        {
            await Task.Run(async () =>
            {
                while (true)
                {
                    CurrentScreenshot = AnalyzeCurrentProgress();
                    await Task.Delay(10);
                }
            });

        }

        /// <summary>
        /// Asynchroner Task zum Analysieren des aktuellen Screenshots
        /// </summary>
        private async void StartAnalyseScreenshotAsync()
        {
            await Task.Run(async () =>
            {
                while (true)
                {
                    AnalyseScreenshot();
                    await Task.Delay(20);
                }
            });
        }

        private async void SimulateClickAsync()
        {
            await Task.Run(async () =>
            {
                while (true)
                {
                    
                    if(TemlateFound_Head)
                    {
                        MoveMouseAndSimulateClick();
                        
                        await Task.Delay(5000);
                    }
                }
            });
        }

        private void MoveMouseAndSimulateClick()
        {
            InvokeIfRequired(this, (MethodInvoker)delegate ()
            {
                Int32 oldMousePosX = CurrentMousePosX;
                Int32 oldMousePosY = CurrentMousePosY;

                //Helper.SetCursorPos((int)CalcedTemplateX, (int)CalcedTemplateY);
                System.Threading.Thread.Sleep(100);
                //User32.mouse_event(Helper.MOUSEEVENTF_LEFTDOWN | User32.MOUSEEVENTF_LEFTUP, CalcedTemplateX, CalcedTemplateY, 0, 0);
                //User32.mouse_event(Helper.MOUSEEVENTF_LEFTDOWN | User32.MOUSEEVENTF_LEFTUP, FoundTemplateX, FoundTemplateY, 0, 0);

                //User32.mouse_event(Helper.MOUSEEVENTF_LEFTDOWN, CalcedTemplateX, CalcedTemplateY, 0, 0);
                //System.Threading.Thread.Sleep(200);
                //User32.mouse_event(Helper.MOUSEEVENTF_LEFTUP, CalcedTemplateX, CalcedTemplateY, 0, 0);
                //System.Threading.Thread.Sleep(500);
                //Helper.SetCursorPos(oldMousePosX, oldMousePosY);
                ////WriteToLog("SimulatedMouseEvent");
                ///

                IntPtr hWnd = CurrentProcess.MainWindowHandle;



                if ((int)hWnd > 0)
                {
                    SetForegroundWindow((int)hWnd);
                    Thread.Sleep(100);
                    //Helper.MouseMove(-9999, -9999);
                    Thread.Sleep(100);
                    Helper.MouseMove(CalcedTemplateX, CalcedTemplateY);
                    //Helper.MouseMove(-641, 500);
                    Thread.Sleep(150);
                    Helper.ClickLeftMouseButtonDown();
                    Thread.Sleep(150);
                    Helper.ClickLeftMouseButtonUp();

                    //    InputSimulator temp = new InputSimulator();
                    //    temp.Mouse.MoveMouseBy(100, 100);
                    //    temp.Mouse.Sleep(1);
                }
                else
                {
                    MessageBox.Show("Window Not Found!");
                }
                

                Thread.Sleep(50);
                //Helper.SetCursorPos(oldMousePosX, oldMousePosY);
            });
           
        }




        /// <summary>
        /// -------------------------------------------------------------------------------
        /// ------------------------
        /// -------------------------------------------------------------------------------
        /// </summary>


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
            StartAnalyseProcessAsync();
            StartAnalyseScreenshotAsync();
            SimulateClickAsync();
        }

        private void listBoxProcess_DoubleClick(object sender, EventArgs e)
        {
            CurrentProcess = (Process)listBoxProcess.SelectedItem;
            ProcessHandlePointer = CurrentProcess.MainWindowHandle;
            textBoxLog.Text += "Ausgewählter Process: " + CurrentProcess.ProcessName + ", " + CurrentProcess.MainWindowTitle;
        }


        private Image<Bgr, byte> FindTemplate(Image<Bgr, byte> template, Image<Bgr, byte> source)
        {
            Image<Bgr, byte> imageToShow = source.Copy();
            using (Image<Gray, float> result = source.MatchTemplate(template, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed))
            {
                double[] minValues, maxValues;
                Point[] minLocations, maxLocations;
                result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);


                CurrentCoeff = maxValues[0];
                // You can try different values of the threshold. I guess somewhere between 0.75 and 0.95 would be good.
                if (maxValues[0] > 0.77)
                {
                    // This is a match. Do something with it, for example draw a rectangle around it.
                    Rectangle match = new Rectangle(maxLocations[0], template.Size);
                    imageToShow.Draw(match, new Bgr(Color.Red), 3);
                    FoundTemplateX = maxLocations[0].X;
                    FoundTemplateY = maxLocations[0].Y;


                    var rect = new Helper.Rect();
                    Helper.GetWindowRect(CurrentProcess.MainWindowHandle, ref rect);

                    OffSizeLeft = rect.left;
                    OffSizeTop = rect.top;
                    int width = rect.right - rect.left;
                    int height = rect.bottom - rect.top;

                    CalcedTemplateX = maxLocations[0].X + (template.Size.Width / 2) + OffSizeLeft;
                    CalcedTemplateY = maxLocations[0].Y + (template.Size.Height) + OffSizeTop;

                    TemlateFound_Head = true;
                }
                else
                {
                    TemlateFound_Head = false;
                }
            }

            return imageToShow;
        }

      


        private void btnRightClick_Click(object sender, EventArgs e)
        {
            // these are the pointer choords
            //var w = (Y << 16) | X;
            //Helper.SendMessage((int)ProcessHandlePointer, Helper.WM_RBUTTONDBLCLK, 0x00000001, w);

        }


        ///// <summary>
        ///// Startet TalonRo in neuem Process und setzt diesen als ChildProcess
        ///// </summary>
        //private void StartAppInNewProcess()
        //{
        //    Process p = Process.Start(@"C:\Games\TalonRO\TalonPatch.exe");
        //    Thread.Sleep(500);
        //    p.WaitForInputIdle();
        //    SetParent(p.MainWindowHandle, this.Handle);
        //}

    }
}
