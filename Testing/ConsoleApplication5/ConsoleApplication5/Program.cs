using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication5
{
    class Program
    {
        static void Main(string[] args)
        {
            TimeSpan addedTime = new TimeSpan(5,0,0);
            DateTime time1 = DateTime.Now;
            DateTime time2 = DateTime.Now + addedTime;
            Console.WriteLine((time2 - time1).ToString());
            Console.WriteLine((time1 - time2).ToString());
        }
    }
}
