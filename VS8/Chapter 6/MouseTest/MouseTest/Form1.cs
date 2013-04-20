using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MouseTest
{
    public partial class Form1 : Form
    {
        private List<Point> points;
        private const int MAX_SIZE = 20;
        private int counter = 0;

        public Form1()
        {
            InitializeComponent();
            points = new List<Point>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            points.Add(new Point(e.X, e.Y));
            counter++;
            if (counter >= MAX_SIZE)
            {
                counter = 0;
                points.Clear();
            }
            this.Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (points.Count > 2)
            {
                Graphics g = this.CreateGraphics();
                Point[] p = points.ToArray();
                g.FillPolygon(Brushes.Blue, p);
            }
        }
    }
}
