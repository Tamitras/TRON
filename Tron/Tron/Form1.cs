using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tron
{
    public partial class Form1 : Form
    {

        public List<Process> Processes { get; set; }

        public Form1()
        {
            InitializeComponent();

            Processes = new List<Process>();
        }

        public void Initialize()
        {
            // Filtern nach name (sortby c=> c.name) etc.
            Processes = Process.GetProcesses().ToList();
        }

        private void btnStartConnect_Click(object sender, EventArgs e)
        {

        }
    }
}
