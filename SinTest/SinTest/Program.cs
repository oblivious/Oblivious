using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SinTest
{
    class Program
    {
        static void Main(string[] args)
        {
            int angle = 90;
            while (angle >= 0)
            {
                double sin = Math.Sin(angle * (Math.PI / 180));
                int opposite = (int)(100 * sin);
                int difference = 100 - opposite;
                angle -= 10;

                Console.WriteLine("Angle: {0}, Sin: {1}, Opposite: {2}, Difference: {3}",
                    angle.ToString().PadLeft(4),
                    sin.ToString("0.00").PadLeft(6),
                    opposite.ToString().PadLeft(4),
                    difference.ToString().PadLeft(4));
            }
        }
    }
}
