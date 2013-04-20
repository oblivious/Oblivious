using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ThreadpoolIntro
{
    class Program
    {
        static void Main(string[] args)
        {
            int workers, completion;
            ThreadPool.GetAvailableThreads(out workers, out completion);
            Console.WriteLine("Worker threads available: {0}, Completion port threads: {1}", workers, completion);
            ThreadPool.QueueUserWorkItem(ThreadProc, "bite me 1");
            ThreadPool.QueueUserWorkItem(ThreadProc, "bite me 2");
            ThreadPool.QueueUserWorkItem(ThreadProc, "bite me 3");
            ThreadPool.QueueUserWorkItem(ThreadProc, "bite me 4");

            Thread.Sleep(100);
            if (Thread.CurrentThread.IsBackground)
                Console.WriteLine("I am a background thread! ");
            else
                Console.WriteLine("I am a foreground thread! ");

            ThreadPool.GetAvailableThreads(out workers, out completion);
            Console.WriteLine("Worker threads available: {0}, Completion port threads: {1}", workers, completion);

            Console.WriteLine("Main thread does some work, then sleeps.");

            Thread.Sleep(1000);

            Console.WriteLine("Main thread exits.");
        }

        static void ThreadProc(Object stateInfo)
        {
            Console.WriteLine("Hello from the thread pool. " + stateInfo ?? string.Empty);
            if (Thread.CurrentThread.IsBackground)
                Console.WriteLine("I am a background thread! " + stateInfo ?? string.Empty);
            else
                Console.WriteLine("I am a foreground thread! ");
            Thread.Sleep(500);
        }
    }
}
