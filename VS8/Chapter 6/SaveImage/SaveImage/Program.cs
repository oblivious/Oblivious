using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace SaveImage
{
    class Program
    {
        static void Main(string[] args)
        {
            Bitmap bm = new Bitmap(600, 600);
            Graphics g = Graphics.FromImage(bm);

            Brush b = new LinearGradientBrush(new Point(1, 1), new Point(600, 600), Color.White, Color.Red);
            Point[] points = new Point[]
            {
                new Point(10, 10),
                new Point(77, 500),
                new Point(590, 100),
                new Point(250, 590),
                new Point(300, 410)
            };

            g.FillPolygon(b, points);
            bm.Save("bm.jpg", ImageFormat.Jpeg);
        }
    }
}
