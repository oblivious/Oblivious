using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AutoResetEventWaitAll
{
    class Program
    {
        static AutoResetEvent[] waitHandles = new AutoResetEvent[]
        {
            new AutoResetEvent(false),
            new AutoResetEvent(false),
            new AutoResetEvent(false)
        };

        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(DoTask), new ThreadInfo(3000, waitHandles[0]));
            ThreadPool.QueueUserWorkItem(new WaitCallback(DoTask), new ThreadInfo(2000, waitHandles[1]));
            ThreadPool.QueueUserWorkItem(new WaitCallback(DoTask), new ThreadInfo(1000, waitHandles[2]));
            WaitHandle.WaitAll(waitHandles);
            Console.WriteLine("Main thread is complete.");
            Console.ReadKey();
        }

        static void DoTask(Object state)
        {
            ThreadInfo ti = (ThreadInfo)state;
            Thread.Sleep(ti.ms);
            Console.WriteLine("Waited for " + ti.ms.ToString() + " ms.");
            ti.are.Set();
        }
    }

    class ThreadInfo
    {
        public AutoResetEvent are;
        public int ms;

        public ThreadInfo(int ms, AutoResetEvent are)
        {
            this.ms = ms;
            this.are = are;
        }
    }
}
