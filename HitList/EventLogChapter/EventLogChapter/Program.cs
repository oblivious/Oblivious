using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace EventLogChapter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowWidth = 160;
            Console.WindowHeight = 50;
            EventLogReading.Run();
        }
    }
}
