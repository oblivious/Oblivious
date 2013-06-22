using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackTraceAcceptsIntegerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                throw new Exception("Some exception");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace(0));
            }

        }
    }
}
