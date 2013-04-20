using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Performance
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Performance: Allocating memory...");
            string[] stringyThingy = new string[10000000];
            for (int i = 0; i < 10000000; i++)
                stringyThingy[i] = "Stringy Thingy!";
            Console.WriteLine("Performance: Memory allocated.");
            Console.ReadKey();
        }
    }
}
