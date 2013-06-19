using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApplication1
{
    class Program
    {
        private const long TICKS_WHEN_WRITTEN = 634738880794924912;
        static void Main(string[] args)
        {
            Console.WriteLine(DateTime.Now.Ticks);

            DateTime startTime = DateTime.Now;
            Console.WriteLine(DateTime.Now.ToString());
            long startValue = (((DateTime.Now.Ticks - TICKS_WHEN_WRITTEN) / 30000000) % 30000000);
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine((((DateTime.Now.Ticks - TICKS_WHEN_WRITTEN) / 30000000) % 30000000)); //% 100000000000000) / 4000000);
                Thread.Sleep(100);
            }
            long endValue = (((DateTime.Now.Ticks - TICKS_WHEN_WRITTEN) / 30000000) % 30000000);
            long difference = endValue - startValue;
            DateTime endTime = DateTime.Now;
            Console.WriteLine(DateTime.Now.ToString());

            TimeSpan timeUntilRollover = new TimeSpan();
            long differenceTotal = 0;
            while (differenceTotal < 30000000)
            {
                timeUntilRollover += (endTime - startTime);
                differenceTotal += difference;
            }
            Console.WriteLine("Time until rollover: " +
                timeUntilRollover.Days + " days, " +
                timeUntilRollover.Hours + " hours, " +
                timeUntilRollover.Minutes + " minutes, " +
                timeUntilRollover.Seconds + " seconds");
        }
    }
}
