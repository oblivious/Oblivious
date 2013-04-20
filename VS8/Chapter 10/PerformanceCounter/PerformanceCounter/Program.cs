using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace PerformanceCounters
{
    class Program
    {
        static void Main(string[] args)
        {
            PerformanceCounter pc = new PerformanceCounter("Processor", "% Processor Time", "_Total");

            pc.NextValue();

            Thread.Sleep(1000);

            Console.WriteLine(pc.NextValue());

            pc.Dispose();

            pc = new PerformanceCounter("IPv4", "Datagrams/sec");

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(pc.NextValue());
                Thread.Sleep(100);
            }
            Console.ReadKey();
        }
    }
}
