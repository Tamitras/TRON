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
using static Tron.Helper;

namespace Tron
{
    public partial class TronForm : Form
    {

        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hwc, IntPtr hwp);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;


        const short SWP_NOMOVE = 0X2;
        const short SWP_NOSIZE = 1;
        const short SWP_NOZORDER = 0X4;
        const int SWP_SHOWWINDOW = 0x0040;


        public Bitmap CurrentScreenshot { get; set; }
        public List<Process> Processes { get; set; }
        public Process CurrentProcess { get; set; }
        public BackgroundWorker worker { get; set; }
        public Thread analyseScreenshotThread { get; set; }

        public IntPtr ProcessHandlePointer { get; set; }

        public Int32 X { get; set; }
        public Int32 Y { get; set; }

        public TronForm()
        {
            InitializeComponent();
            CurrentScreenshot = null;
            Processes = new List<Process>();
            Initialize();
        }

        /// <summary>
        /// Initialisiert die Anwendung
        /// </summary>
        public void Initialize()
        {
            // Filtern nach name (sortby c=> c.name) etc.
            Processes = Process.GetProcesses().OrderBy(c => c.ProcessName).ToList();
            //Processes = Process.GetProcesses().Where(c => c.ProcessName.Contains("notepad")).ToList();
            //Processes = Process.GetProcesses().Where(c => c.ProcessName.Contains("talon")).ToList();
            bindingSource1.DataSource = Processes;
            listBoxProcess.DisplayMember = "ProcessName";

            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;

            // Startet den Worker
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            // In Methode einbinden

            // Thread wird initialisiert
            analyseScreenshotThread = new Thread(delegate ()
            {
                while (true)
                {
                    AnalyseScreenshot();
                    Thread.Sleep(100);
                }

            });

        }

        /// <summary>
        /// 
        /// </summary>
        public void AnalyzeCurrentProgress()
        {
            try
            {
                //Bitmap btmp = Helper.createBitmap(ptr1, 640, 480);

                // best capture for 640x480 resolution
                CurrentScreenshot = Helper.createBitmap(ProcessHandlePointer, 803, 603);
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                //MessageBox.Show("Error on AnalyzeCurrentProgress: " + ex.Message);
                return;
            }
        }


        /// <summary>
        /// Worker Thread, ist mit Screenshotten beschäftigt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!worker.CancellationPending)
            {
                AnalyzeCurrentProgress();
                Thread.Sleep(10);
            }
        }


        private void btnStartConnect_Click(object sender, EventArgs e)
        {

            if (worker.IsBusy)
            {
                worker.CancelAsync();
                btnStartConnect.Text = "Starten";
                analyseScreenshotThread.Abort();
            }
            else
            {

                analyseScreenshotThread.Start();
                worker.RunWorkerAsync(this);
                btnStartConnect.Text = "Stoppen";
            }
        }

        private void listBoxProcess_DoubleClick(object sender, EventArgs e)
        {
            CurrentProcess = (Process)listBoxProcess.SelectedItem;
            ProcessHandlePointer = CurrentProcess.MainWindowHandle;

            //RECT rect = new RECT();
            //if (Helper.GetWindowRect(ProcessHandlePointer, ref rect))
            //{
            //    int left = rect.left;
            //    int right = rect.right;
            //    int bottom = rect.bottom;
            //    int top = rect.top;

            //    //if (ProcessHandlePointer != IntPtr.Zero)
            //    //{
            //    //    //Helper.SetWindowPos(ProcessHandlePointer, 0, 0, 0, 0, 0, SWP_NOZORDER | SWP_NOSIZE | SWP_SHOWWINDOW);
            //    //    try
            //    //    {
            //    //        Helper.SetWindowPos(ProcessHandlePointer, 0, 200, 200, 640, 480, SWP_SHOWWINDOW);
            //    //    }
            //    //    catch (Exception)
            //    //    {
            //    //        MessageBox.Show("Window konnte nicht verschoben werden");
            //    //    }

            //    //}

            //    //IntPtr handle;

            //    //try
            //    //{
            //    //    // Find the handle to the Start Bar
            //    //    handle = Helper.FindWindow(CurrentProcess.MainWindowTitle, null);

            //    //    IntPtr parentPointer = Helper.GetParentFromChild(handle);

            //    //    // If the handle is found then hide the start bar
            //    //    if (parentPointer != IntPtr.Zero)
            //    //    {
            //    //        // Hide the start bar
            //    //        Helper.SetWindowPos(parentPointer, 0, 200, 200, 640, 480, SWP_SHOWWINDOW);
            //    //    }
            //    //}
            //    //catch
            //    //{
            //    //    MessageBox.Show("Could not hide Start Bar.");
            //    //}



            //    //if (MoveWindow(ProcessHandlePointer, 200, 200, 640, 480, true))
            //    //{
            //    //    this.textBoxCurrentProcess.Text += Environment.NewLine + "Verschoben fehlerhaft";
            //    //}
            //    //else
            //    //{
            //    //    this.textBoxCurrentProcess.Text += Environment.NewLine + "Verschoben erfolgreich";
            //    //}
            //}
            //else
            //{
            //    return;
            //}

            textBoxCurrentProcess.Text += "Ausgewählter Process: " + CurrentProcess.ProcessName + ", " + CurrentProcess.MainWindowTitle;
        }


        /// <summary>
        /// Analysiert den aktuellen Screenshot und vergleicht diesen mit einem template
        /// </summary>
        private void AnalyseScreenshot()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate ()
                {
                    AnalyseScreenshot();

                }));
            }
            else
            {
                if (null != CurrentScreenshot)
                {
                    Image<Bgr, byte> source = new Image<Bgr, byte>(CurrentScreenshot);
                    //Image<Bgr, byte> template = new Image<Bgr, byte>("C:/Users/ekaufmann/Desktop/screenys/theme.bmp"); // Image A
                    Image<Bgr, byte> template = new Image<Bgr, byte>(Properties.Resources.templateHead); // Image A
                    //Image<Bgr, byte> imageToShow = source.Copy();


                    Image<Bgr, byte>  res = FindTemplate(template, source);
                    pictureBoxFoundImage.Image = res.ToBitmap();
                }
            }
        }

        private Image<Bgr, byte> FindTemplate(Image<Bgr, byte> template, Image<Bgr, byte> source)
        {
            Image<Bgr, byte> imageToShow = source.Copy();
            using (Image<Gray, float> result = source.MatchTemplate(template, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed))
            {
                double[] minValues, maxValues;
                Point[] minLocations, maxLocations;
                result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);

                // You can try different values of the threshold. I guess somewhere between 0.75 and 0.95 would be good.
                if (maxValues[0] > 0.9) 
                {
                    // This is a match. Do something with it, for example draw a rectangle around it.
                    Rectangle match = new Rectangle(maxLocations[0], template.Size);
                    imageToShow.Draw(match, new Bgr(Color.Red), 3);
                    X = maxLocations[0].X;
                    Y = maxLocations[0].Y;
                }
            }

            return imageToShow;
        }
            

        private void btnRightClick_Click(object sender, EventArgs e)
        {
            //IntPtr res =  Helper.SendMessage(ProcessHandlePointer, Helper.WM_RBUTTONDOWN, 0, Helper.MAKELPARAM(X, Y));
            //res = Helper.SendMessage(ProcessHandlePointer, Helper.WM_RBUTTONUP, 0, Helper.MAKELPARAM(X, Y));


            //IntPtr returnValue = Helper.SendMessage(out hwndMessageWasSentTo, msg, wParam, lParam); 
            //if (res != IntPtr.Zero)
            //    Console.WriteLine("Message successfully sent to hwnd: " + hwndMessageWasSentTo.ToString() + " and return value was: " + returnValue.ToString());
            //else
            //    Console.WriteLine("No windows found in process.  SendMessage was not called.");

            //mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, (uint)X, (uint)Y, 0, 0);




            //https://stackoverflow.com/questions/10355286/programmatically-mouse-click-in-another-window/24357790

            // these are the pointer choords
            var w = (Y << 16) | X;
            Helper.SendMessage((int)ProcessHandlePointer, Helper.WM_RBUTTONDBLCLK, 0x00000001, w);

        }

        private void btnStartNewProcess_Click(object sender, EventArgs e)
        {
            try
            {
                StartAppInNewProcess();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        /// <summary>
        /// Startet TalonRo in neuem Process und setzt diesen als ChildProcess
        /// </summary>
        private void StartAppInNewProcess()
        {
            Process p = Process.Start(@"C:\Games\TalonRO\TalonPatch.exe");
            Thread.Sleep(500);
            p.WaitForInputIdle();
            SetParent(p.MainWindowHandle, this.Handle);
        }
    }
}
