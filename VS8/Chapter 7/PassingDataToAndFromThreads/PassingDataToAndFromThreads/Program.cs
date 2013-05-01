using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PassingDataToAndFromThreads
{
    class Program
    {
        static void Main(string[] args)
        {
            Multiply m = new Multiply("Hello, World!", 13, new ResultDelegate(ResultCallback));

            Thread t = new Thread(new ThreadStart(m.ThreadProc));
            t.Start();

            Console.WriteLine("Main thread does some work, then waits.");

            t.Join();
            Console.WriteLine("Thread completed.");
            Console.WriteLine("M's Value is: " + m.Value);
            Console.ReadKey();
        }

        public static void ResultCallback(int retValue)
        {
            Console.WriteLine("Returned value: {0}", retValue);
        }
    }

    public class Multiply
    {
        private string greeting;
        private int value;

        public string Value { get; set; }

        private ResultDelegate callback;

        public Multiply(string greeting, int value, ResultDelegate callback)
        {
            this.greeting = greeting;
            this.value = value;
            this.callback = callback;
            this.Value = "I am the original value...";
        }

        public void ThreadProc()
        {
            Console.WriteLine(this.greeting);
            this.Value = "I was returned from the thread, without needing a delegate...";
            if (callback != null)
                callback(value * 2);
        }
    }

    public delegate void ResultDelegate(int value);
}
