using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace GoofingWithGraphics
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            g.SmoothingMode = SmoothingMode.AntiAlias;

            g.DrawLine(new Pen(Color.Red, 3), -1, -1, 99, 99);

            g.DrawPie(new Pen(Color.Blue, 3), -1, -1, 99, 99, -30, 60);

            Point[] points = new Point[]
            {
                new Point(110, 10),
                new Point(110, 100),
                new Point(150, 65),
                new Point(200, 100),
                new Point(185, 40)
            };

            g.DrawPolygon(new Pen(Color.ForestGreen, 2), points);

            Pen p = new Pen(Color.Red, 2);

            g.DrawLine(p, 50, 125, 350, 125);

            p.DashStyle = DashStyle.Dash;
            g.DrawLine(p, 50, 135, 350, 135);

            p.DashStyle = DashStyle.DashDot;
            g.DrawLine(p, 50, 145, 350, 145);

            p.DashStyle = DashStyle.DashDotDot;
            g.DrawLine(p, 50, 155, 350, 155);

            p.DashStyle = DashStyle.Solid;
            g.DrawLine(p, 50, 165, 350, 165);

            p.DashStyle = DashStyle.Custom;
            p.DashOffset = 5f;
            p.DashPattern = new float[] { 1.0f, 1.0f, 2.0f, 1.0f, 3.0f, 1.0f };
            g.DrawLine(p, 50, 175, 350, 175);

            p = new Pen(Color.Purple, 7);

            p.StartCap = LineCap.ArrowAnchor;
            p.EndCap = LineCap.DiamondAnchor;
            g.DrawLine(p, 250, 25, 350, 25);

            Brush b = new SolidBrush(Color.Maroon);

            points = new Point[]
            {
                new Point(10, 185),
                new Point(10, 275),
                new Point(50, 240),
                new Point(100, 275),
                new Point(85, 215)
            };

            g.FillPolygon(b, points);

            p = new Pen(Color.Maroon, 2);
            b = new LinearGradientBrush(new Point(100, 180), new Point(200, 280), Color.White, Color.Red);

            points = new Point[]
            {
                new Point(110, 185),
                new Point(110, 275),
                new Point(150, 240),
                new Point(200, 275),
                new Point(185, 215)
            };

            g.FillPolygon(b, points);
            g.DrawPolygon(p, points);

            g.DrawIcon(SystemIcons.Question, 240, 210);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            g.Clear(Color.Maroon);
        }
    }
}
