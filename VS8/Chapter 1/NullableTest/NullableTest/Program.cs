using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NullableTest
{
    class Program
    {
        static void Main(string[] args)
        {
            bool? b = null;
            if (b.HasValue)
                Console.WriteLine("1: b has a value");
            b = false;
            if (b.HasValue)
                Console.WriteLine("2: b has a value");

            Point p = new Point(20, 30);
            Console.WriteLine("p has coordinates {0},{1}", p.X, p.Y);

            Console.WriteLine(2 - 1 + 3 - 4); //addition first, the substraction, right to left = 2. left to right = -6
        }
    }
}
