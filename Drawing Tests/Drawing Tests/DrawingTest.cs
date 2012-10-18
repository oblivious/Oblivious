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
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int baseColour = 0x8A2BE2;
            Pen p = new Pen(Color.FromArgb(255, Color.FromArgb(baseColour)), 2);

            //g.DrawLine(p, 110, 110, 300, 300);
            
            int sizeX = 200;
            int sizeY = 200;

            Point point = new Point(50, 50);
            Size size = new Size(sizeX, sizeY);
            int decrement = 22;

            Rectangle rect = new Rectangle(point, size);
            while (sizeY > 0)
            {
                g.DrawEllipse(p, rect);
                baseColour = (int)(((long)baseColour + 0x111100) % Int32.MaxValue);
                p.Color = Color.FromArgb(255, Color.FromArgb(baseColour));
                rect.Y += decrement;
                sizeY -= (decrement * 2);
                rect.Height = sizeY > 0 ? sizeY : 0;
            }

            rect.Y = 50;
            sizeY = 200;
            baseColour = 0x8A2BE2;
            p.Color = Color.FromArgb(255, Color.FromArgb(baseColour));

            rect = new Rectangle(point, size);
            while (sizeX > 0)
            {
                g.DrawEllipse(p, rect);
                baseColour = (int)(((long)baseColour + 0x111100) % Int32.MaxValue);
                p.Color = Color.FromArgb(255, Color.FromArgb(baseColour));
                rect.X += decrement;
                sizeX -= (decrement * 2);
                rect.Width = sizeX > 0 ? sizeX : 0;
            }
        }
    }
}
