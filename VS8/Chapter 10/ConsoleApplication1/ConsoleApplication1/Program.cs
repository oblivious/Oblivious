using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Debug.Listeners.Add(new ConsoleTraceListener());
            Debug.AutoFlush = true;
            Debug.Indent();
            Debug.WriteLine("Starting application");
            Console.WriteLine("Hello, world!");
            Console.ReadKey();
            Console.Title = "Noodles!";
            Debug.WriteLine("Ending application");
            Debug.Unindent();
        }
    }
}
