using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static Tron.Helper;

namespace Tron
{
    public partial class Form1 : Form
    {

        public List<Process> Processes { get; set; }
        public Process CurrentProcess { get; set; }

        public Form1()
        {
            InitializeComponent();
            Processes = new List<Process>();

            this.Initialize();
        }

        public void Initialize()
        {
            // Filtern nach name (sortby c=> c.name) etc.
            //Processes = Process.GetProcesses().OrderBy(c=> c.ProcessName).ToList();

            Processes = Process.GetProcesses().Where(c => c.ProcessName.Contains("notepad")).ToList();
            this.bindingSource1.DataSource = this.Processes;
            this.listBoxProcess.DisplayMember = "ProcessName";
        }

        private void btnStartConnect_Click(object sender, EventArgs e)
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


                    // currentSize
                    Size size = new Size(rect.right - rect.left,
                             rect.bottom - rect.top);

                    rect.left = 0;
                    rect.right = 640;

                    rect.bottom = 480;
                    rect.top = 0;

                    Size size2 = new Size(rect.right - rect.left,
                             rect.bottom - rect.top);

                    MoveWindow(ptr1, 100, 100, size2.Width, size2.Height, true);

                }
                this.pictureBox3.Image = Helper.createBitmap(ptr1, Size.Width, Size.Height);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void listBoxProcess_DoubleClick(object sender, EventArgs e)
        {
            CurrentProcess = (Process)listBoxProcess.SelectedItem;
            this.textBoxCurrentProcess.Text = "Current Process: " + CurrentProcess.ProcessName;
        }
    }
}
