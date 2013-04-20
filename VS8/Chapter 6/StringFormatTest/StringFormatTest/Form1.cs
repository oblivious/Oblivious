using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StringFormatTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = this.CreateGraphics();

            Rectangle r = new Rectangle(new Point(40, 40), new Size(80, 80));

            StringFormat f1 = new StringFormat(StringFormatFlags.NoClip);
            StringFormat f2 = new StringFormat(f1);

            f1.LineAlignment = StringAlignment.Near;
            f1.Alignment = StringAlignment.Center;
            f2.LineAlignment = StringAlignment.Center;
            f2.Alignment = StringAlignment.Far;
            f2.FormatFlags = StringFormatFlags.DirectionVertical;

            g.DrawRectangle(Pens.Black, r);
            g.DrawString("Format1", this.Font, Brushes.Red, (RectangleF)r, f1);
            g.DrawString("Format2", this.Font, Brushes.Red, (RectangleF)r, f2);
        }
    }
}
