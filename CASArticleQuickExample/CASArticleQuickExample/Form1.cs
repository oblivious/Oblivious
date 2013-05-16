using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CASArticleQuickExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void writeButton_Click(object sender, EventArgs e)
        {
            StreamWriter myFile = new StreamWriter(@"c:\Security.txt");
            myFile.WriteLine("Trust No One");
            myFile.Close();
        }

        private void readButton_Click(object sender, EventArgs e)
        {
            StreamReader myFile = new StreamReader(@"c:\Security.txt");
            MessageBox.Show(myFile.ReadLine());
            myFile.Close();
        }
    }
}
