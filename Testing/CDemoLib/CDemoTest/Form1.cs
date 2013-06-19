using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CDemoLib;

namespace CDemoTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Text = "CDemo instances: " + CDemo.InstanceCount;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CDemo cd;
            int ct;
            for (ct = 0; ct < 10000; ct++)
                cd = new CDemo();
        }
    }
}
