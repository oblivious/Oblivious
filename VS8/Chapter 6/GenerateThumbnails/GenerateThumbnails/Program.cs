using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace GenerateThumbnails
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo dir = new DirectoryInfo(@"C:\Users\Public\Pictures");

            string thumbnailPath = dir.FullName + @"\Thumbnails";
            if (!Directory.Exists(thumbnailPath))
                Directory.CreateDirectory(thumbnailPath);

            FileInfo[] files = dir.GetFiles("*.jpg");
            Console.WriteLine("Found " + files.Count() + " files.");

            foreach (FileInfo file in files)
            {
                Bitmap b = new Bitmap(Image.FromFile(file.FullName));
                int width = b.Width;
                int height = b.Height;
                int size;
                Point p;
                if (height > width)
                {
                    size = width;
                    p = new Point(0, (height - width) / 2);
                }
                else
                {
                    size = height;
                    p = new Point((width - height) / 2, 0);
                }

                Rectangle cropRect = new Rectangle(p, new Size(size, size));
                Bitmap c = new Bitmap(size, size);

                using(Graphics g = Graphics.FromImage(c))
                {
                   g.DrawImage(b, new Rectangle(0, 0, c.Width, c.Height), 
                                    cropRect,                        
                                    GraphicsUnit.Pixel);
                }
                Bitmap d = new Bitmap(80, 80);
                Graphics h = Graphics.FromImage(d);
                h.CompositingQuality = CompositingQuality.HighQuality;
                h.InterpolationMode = InterpolationMode.HighQualityBicubic;
                h.DrawImage(c, 0, 0, d.Width, d.Height);
                c.Save(thumbnailPath + @"\" + file.Name.Substring(0, file.Name.Length - file.Extension.Length) +
                    "_80x80" + file.Extension, ImageFormat.Jpeg);
            }
        }
    }
}
