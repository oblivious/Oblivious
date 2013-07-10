using System;
using System.Threading;

namespace WaitCallbackTest
{
    class Program
    {
        static void Main( string[] args )
        {
            ThreadPool.QueueUserWorkItem(ThreadProc);

            Console.WriteLine("Main thread does some work, then sleeps.");

            Thread.Sleep(1000);
        }

        static void ThreadProc( Object stateInfo )
        {
            Console.WriteLine("Hello from the thread pool.");
        }
    }
}
