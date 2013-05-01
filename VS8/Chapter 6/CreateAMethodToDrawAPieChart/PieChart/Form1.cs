using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;

namespace PieChart
{
    public partial class Form1 : Form
    {
        private const int PIE_HEIGHT = 100;

        public Form1()
        {
            InitializeComponent();
        }

        private void Draw(object sender, PaintEventArgs e)
        {
            Draw();
        }

        private void Draw()
        {
            ArrayList data = new ArrayList();
            data.Add(new PieChartElement("East", (float)50.75));
            data.Add(new PieChartElement("West", (float)22));
            data.Add(new PieChartElement("North", (float)72.32));
            data.Add(new PieChartElement("South", (float)12));
            data.Add(new PieChartElement("Central", (float)44));

            chart.Image = drawPieChart(data, new Size(chart.Width, chart.Height));
        }

        private Image drawPieChart(ArrayList elements, Size s)
        {
            Bitmap bm = new Bitmap(s.Width, s.Height);

            Graphics g = Graphics.FromImage(bm);
            g.SmoothingMode = SmoothingMode.HighQuality;

            Color[] colours = { Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Indigo, 
                                 Color.Violet, Color.DarkRed, Color.DarkOrange, Color.DarkSalmon, Color.DarkGreen, 
                                 Color.DarkBlue, Color.Lavender, Color.LightBlue, Color.Coral };

            if (elements.Count > colours.Length)
                throw new ArgumentException("Pie chart must have " + colours.Length.ToString() + " or fewer elements");

            int colourNum = 0;

            float total = 0;
            foreach (PieChartElement e in elements)
            {
                if (e.value < 0)
                {
                    throw new ArgumentException("All elements must have positive values");
                }
                total += e.value;
            }

            Rectangle rect = new Rectangle(1, 1, s.Width - 2, s.Height - (PIE_HEIGHT + 2) );

            Rectangle rect2 = new Rectangle(1, 1 + PIE_HEIGHT, s.Width - 2, s.Height - (PIE_HEIGHT + 2));

            Pen p = new Pen(Color.Black, 1);

            float startAngle = 0;

            g.DrawArc(p, rect2, 0, 180);
            int temp = (s.Height - (PIE_HEIGHT + 1)) / 2;
            g.DrawLine(p, 1, temp, 1, temp + PIE_HEIGHT);
            g.DrawLine(p, s.Width - 1, temp, s.Width - 1, temp + PIE_HEIGHT);

            startAngle = 0;

            foreach (PieChartElement e in elements)
            {
                float sweepAngle = (e.value / total) * 360;

                g.FillPie(new LinearGradientBrush(rect, colours[colourNum++], Color.White, 45f), rect, startAngle, sweepAngle);
                g.DrawPie(p, rect, startAngle, sweepAngle);

                startAngle += sweepAngle;
            }
            return bm;
        }
    }
}