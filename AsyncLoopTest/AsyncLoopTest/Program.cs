using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AsyncLoopTest
{
    class Program
    {
        private delegate void MyDelegate();

        private static MyDelegate deli;

        private static int counter = 0;

        private static IAsyncResult result;

        static void Main(string[] args)
        {
            deli = new MyDelegate(MyMethod);
            Console.WriteLine("Invoking the method...");
            result = deli.BeginInvoke(MyMethodEnds, null);
            Console.WriteLine("Reached Console.ReadKey()");
            Console.ReadKey();
        }

        private static void MyMethod()
        {
            Console.WriteLine("In MyMethod, ++counter = {0}", ++counter);
            Thread.Sleep(500);
        }

        private static void MyMethodEnds(object context)
        {
            Console.WriteLine("In MyMethodEnds, calling BeginInvoke again. counter = {0}", counter);
            Console.WriteLine("Disposing of the wait handle...");
            result.AsyncWaitHandle.Dispose();
            Thread.Sleep(500);
            if (counter < 10)
            {
                result = deli.BeginInvoke(MyMethodEnds, null);
                Console.WriteLine("Called BeginInvoke, counter = {0}", counter);
            }
            else
                Console.WriteLine("Reached threshold, counter = {0}", counter);
        }
    }
}
