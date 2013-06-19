using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            string sample = "*" + 12345678 + "*" + 5060000000 + "*" + 50 + "*0#";
            string[] tokens = sample.Split('*');
            Console.WriteLine(tokens[0]);
            Console.WriteLine(tokens[1]);
            Console.WriteLine(tokens[2]);
            Console.WriteLine(tokens[3]);

            DateTime startTime = DateTime.Now;
            Thread.Sleep(5000);
            Console.WriteLine(DateTime.Now - startTime);
        }
    }
}
