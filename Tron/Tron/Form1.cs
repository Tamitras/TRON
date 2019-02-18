using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using static Tron.Helper;

namespace Tron
{
    public partial class Form1 : Form
    {

        public List<Process> Processes { get; set; }
        public Process CurrentProcess { get; set; }
        public BackgroundWorker worker;

        public Form1()
        {
            InitializeComponent();
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
        }

        public Bitmap AnalyzeCurrentProgress()
        {
            try
            {
                IntPtr ptr1 = CurrentProcess.MainWindowHandle;

                RECT rect = new RECT();
                if (Helper.GetWindowRect(ptr1, ref rect))
                {
                    int left = rect.left;
                    int right = rect.right;
                    int bottom = rect.bottom;
                    int top = rect.top;

                    MoveWindow(ptr1, 200, 200, 640, 480, true);
                }
                else
                {
                    return null;
                }

                //Bitmap btmp = Helper.createBitmap(ptr1, 640, 480);
                Bitmap btmp = Helper.createBitmap(ptr1, 632, 410);

                pictureBox3.Image = btmp;

                //btmp.Save(@"C:\Users\ekaufmann\Desktop\screenys\test.jpg", btmp.RawFormat);
                //btmp.Save(@"C:\Users\ekaufmann\Desktop\screenys\test.bmp", btmp.RawFormat);

                Image<Bgr, byte> source = new Image<Bgr, byte>("C:/Users/ekaufmann/Desktop/screenys/test.bmp"); // Image B
                Image<Bgr, byte> template = new Image<Bgr, byte>("C:/Users/ekaufmann/Desktop/screenys/theme.bmp"); // Image A
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
                    }
                    else
                    {
                        pictureBox1.Image = null;
                    }
                }

                // Show imageToShow in an ImageBox (here assumed to be called imageBox1)               
                //pictureBox1.Image =
                return imageToShow.ToBitmap();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }



        private void btnStartConnect_Click(object sender, EventArgs e)
        {
            if (worker.IsBusy)
            {
                worker.CancelAsync();
                btnStartConnect.Text = "Starten";
            }
            else
            {

                worker.RunWorkerAsync(this);
                btnStartConnect.Text = "Stoppen";
            }
        }

        private void listBoxProcess_DoubleClick(object sender, EventArgs e)
        {
            CurrentProcess = (Process)listBoxProcess.SelectedItem;
            textBoxCurrentProcess.Text = "Current Process: " + CurrentProcess.ProcessName;
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!worker.CancellationPending)
            {
                Bitmap temp = AnalyzeCurrentProgress();
                SetBitmap(temp);
                Thread.Sleep(1);
                //Thread.Sleep(100000);
            }
        }

        private void SetBitmap(Bitmap bmp)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate ()
                {
                    SetBitmap(bmp);
                }));
            }
            else
            {
                this.pictureBox1.Image = bmp;
            }
        }

    }
}
