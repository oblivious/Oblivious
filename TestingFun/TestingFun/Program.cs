using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestingFun
{
    class Program
    {
        static void Main(string[] args)
        {
            int val = 0;

            Console.WriteLine(val);
            Console.WriteLine(val++);
            Console.WriteLine(++val);

            val = 0;

            int nul = val++;
            Console.WriteLine(val + " " + nul);

            val = 0;
            nul = 0;
            nul = ++val;
            Console.WriteLine(val + " " + nul);
        }
    }
}
