using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RespondToEvent
{
    public partial class Form1 : Form
    {
        private bool reverse = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Loading...";
            timer1.Interval = 100;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!reverse)
            {
                if (progressBar1.Value < 100)
                    progressBar1.Value += 1;
                if (progressBar1.Value == 80)
                    this.Text = "Nearly there!";
                if (progressBar1.Value == 100)
                    reverse = true;
            }
            else
            {
                if (progressBar1.Value > 0)
                    progressBar1.Value -= 1;
                if (progressBar1.Value == 90)
                    this.Text = "Aww...";
                else if (progressBar1.Value == 50)
                    this.Text = "That's too bad.";
                else if (progressBar1.Value == 0)
                {
                    this.Text = "Loading...";
                    reverse = false;
                }
            }
        }
    }
}
