using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Drawing_Tests
{
    public partial class DrawingTest : Form
    {
        public DrawingTest()
        {
            InitializeComponent();
        }

        private void btnGraphicsClass_Click(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            g.SmoothingMode = SmoothingMode.HighQuality;//.AntiAlias;

            int baseColour = 0x8A2BE2;
            int penWidth = 1;
            Pen p = new Pen(Color.FromArgb(255, Color.FromArgb(baseColour)), penWidth);

            //g.DrawLine(p, 110, 110, 300, 300);

            int xPoint = 50;
            int yPoint = 50;
            int xStart = 200;
            int yStart = 200;
            int sizeX = 200;
            int sizeY = 200;

            Point point = new Point(50, 50);
            Size size = new Size(sizeX, sizeY);
            double angle = 90;
            int difference = 0;

            Rectangle rect = new Rectangle(point, size);
            while (sizeY >= 0)
            {
                g.DrawEllipse(p, rect);

                int opposite = (int)(100 * Math.Sin(angle * (Math.PI / 180)));
                difference = 100 - opposite;
                angle -= 6;
                //p.Width = penWidth + (difference / 40);

                baseColour = (int)(((long)baseColour + 0x020202) % Int32.MaxValue);
                p.Color = Color.FromArgb(255, Color.FromArgb(baseColour));

                rect.Y = yPoint + difference;
                sizeY = yStart - (difference * 2);
                rect.Height = sizeY > 0 ? sizeY : 0;
            }

            rect.Y = 50;
            sizeY = 200;
            baseColour = 0x8A2BE2;
            p.Color = Color.FromArgb(255, Color.FromArgb(baseColour));
            p.Width = penWidth;

            angle = 90;
            difference = 0;

            rect = new Rectangle(point, size);
            while (sizeX >= 0)
            {
                g.DrawEllipse(p, rect);

                int opposite = (int)(100 * Math.Sin(angle * (Math.PI / 180)));
                difference = 100 - opposite;
                angle -= 6;
                //p.Width = penWidth + (difference / 40);

                baseColour = (int)(((long)baseColour + 0x020202) % Int32.MaxValue);
                p.Color = Color.FromArgb(255, Color.FromArgb(baseColour));

                rect.X = xPoint + difference;
                sizeX = xStart - (difference * 2);
                rect.Width = sizeX > 0 ? sizeX : 0;
            }

            Pen o = new Pen(Color.Red, 3);
            g.DrawLines(o, new Point[]
            {
                new Point(250, 50),
                new Point(300, 200),
                new Point(350, 50),
                new Point(400, 200)
            });

            o.Brush = Brushes.Coral;
            g.DrawPie(o, 325, 25, 200, 200, -30, 50);

            g.FillPie(Brushes.CornflowerBlue, 425, 100, 200, 200, 150, 50);

            Brush b = new LinearGradientBrush(
                          new Rectangle(0, 0, 150, 150),
                          Color.Magenta,
                          Color.SlateBlue,
                          LinearGradientMode.Horizontal
                      );

            g.FillPie(b, 325, 175, 200, 200, -30, 50);

            g.FillRectangle(b, 50, 250, 100, 100);

            Point[] points = new Point[]
            {
                new Point(200, 250), new Point(325, 200), new Point(400, 325),
                new Point(425, 420), new Point(325, 375), new Point(175, 300)
            };

            p.DashStyle = DashStyle.Dash;
            p.Width = 2;
            p.Color = Color.Green;
            g.DrawPolygon(p, points);


            //Close curve stuff (just because)
            // Create pens.
            Pen redPen = new Pen(Color.Red, 3);
            Pen greenPen = new Pen(Color.Green, 3);

            // Create points that define curve.
            Point point1 = new Point(100, 75);
            Point point2 = new Point(100, 25);
            Point point3 = new Point(200, 5);
            Point point4 = new Point(250, 50);
            Point point5 = new Point(300, 100);
            Point point6 = new Point(350, 200);
            Point point7 = new Point(250, 250);
            Point[] curvePoints =
             {
                 point1,
                 point2,
                 point3,
                 point4,
                 point5,
                 point6,
                 point7
             };

            // Draw lines between original points to screen.
            g.DrawLines(redPen, curvePoints);

            // Draw closed curve to screen.
            g.DrawClosedCurve(greenPen, curvePoints, 0.2f, FillMode.Alternate);
        }

        private void btnMoreGraphicsClass_Click(object sender, EventArgs e)
        {
            Bitmap b = new Bitmap("fullmoon.jpg");
            Graphics g = Graphics.FromImage(b);
            g.DrawLine(Pens.Aquamarine, 10, 10, 200, 200);
            g.DrawPie(Pens.Chartreuse, 50, 50, 200, 200, -30, 50);

            Graphics tab = this.CreateGraphics();
            tab.DrawImage(b, 25, 25);
        }
    }
}
