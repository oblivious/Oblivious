using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ThreadAbortion
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread DoWorkThread = new Thread(new ThreadStart(DoWork));
            DoWorkThread.Start();
            Thread.Sleep(1000);
            DoWorkThread.Abort();
            Console.WriteLine("The Main() thread is ending.");
            Thread.Sleep(6000);
        }

        static void DoWork()
        {
            Console.WriteLine("DoWork is running on another thread.");
            try
            {
                Thread.Sleep(5000);
                Console.WriteLine("DoWork has finished running without error.");
            }
            catch (ThreadAbortException)
            {
                Console.WriteLine("DoWork was aborted... oh the humanity.");
            }
            finally
            {
                Console.WriteLine("Use finally to close all open resources.");
            }
            Console.WriteLine("DoWork has reached the end of its tether.");
        }
    }
}
