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
    public partial class Form1 : Form
    {


        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;


        public Bitmap CurrentScreenshot { get; set; }
        public List<Process> Processes { get; set; }
        public Process CurrentProcess { get; set; }
        public BackgroundWorker worker { get; set; }
        public Thread analyseScreenshotThread { get; set; }

        public IntPtr ProcessHandlePointer { get; set; }

        public Int32 X { get; set; }
        public Int32 Y { get; set; }

        public Form1()
        {
            InitializeComponent();
            CurrentScreenshot = null;
            Processes = new List<Process>();
            Initialize();
        }

        public void Initialize()
        {
            // Filtern nach name (sortby c=> c.name) etc.
            //Processes = Process.GetProcesses().OrderBy(c => c.ProcessName).ToList();
            Processes = Process.GetProcesses().Where(c => c.ProcessName.Contains("notepad")).ToList();
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
                Bitmap btmp = Helper.createBitmap(ProcessHandlePointer, 632, 410);
                pictureBoxOriginal.Image = btmp;
                CurrentScreenshot = btmp;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                Thread.Sleep(1);
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

            RECT rect = new RECT();
            if (Helper.GetWindowRect(ProcessHandlePointer, ref rect))
            {
                int left = rect.left;
                int right = rect.right;
                int bottom = rect.bottom;
                int top = rect.top;

                MoveWindow(ProcessHandlePointer, 200, 200, 640, 480, true);
            }
            else
            {
                return;
            }

            textBoxCurrentProcess.Text = "Current Process: " + CurrentProcess.ProcessName;
        }




        /// <summary>
        /// Analysiert den aktuellen Screenshot
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
                Image<Bgr, byte> source = new Image<Bgr, byte>(CurrentScreenshot);
                //Image<Bgr, byte> template = new Image<Bgr, byte>("C:/Users/ekaufmann/Desktop/screenys/theme.bmp"); // Image A
                Image<Bgr, byte> template = new Image<Bgr, byte>(Properties.Resources.theme); // Image A
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
                pictureBoxFoundImage.Image = imageToShow.ToBitmap();
            }
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
    }
}
