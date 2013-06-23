using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CallbackThreadTest
{
    public delegate void SomeEvent(object sender, EventArgs e);

    public delegate void MyDelegate();

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("In Main");
            Console.WriteLine("Managed Thread Id: {0}", Thread.CurrentThread.ManagedThreadId);

            Console.WriteLine("\nTesting ThreadPool:");
            var threadpoolTester = new ThreadpoolTester();
            ThreadPool.QueueUserWorkItem(threadpoolTester.TestCallBack);

            Thread.Sleep(500);

            Console.WriteLine("\nTesting Events:");
            var eventTester = new EventTester();
            eventTester.MyEvent += EventMethod;
            eventTester.RaiseEvent();

            Console.WriteLine("\nTesting Delegate Callbacks Sync:");
            MyDelegate myDelegate1 = MyMethod;
            myDelegate1.Invoke();

            Console.WriteLine("\nTesting Delegate Callbacks Async:");
            MyDelegate myDelegate2 = MyMethod;
            IAsyncResult result = myDelegate2.BeginInvoke(MyMethodCallback, null);
            result.AsyncWaitHandle.WaitOne();
            myDelegate2.EndInvoke(result);

            Thread.Sleep(500);

            Console.WriteLine("\nExiting Main");
            Console.ReadKey();
        }

        private static void EventMethod(object sender, EventArgs e)
        {
            Console.WriteLine("   In EventMethod");
            Console.WriteLine("   Managed Thread Id: {0}", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("   Exiting EventMethod");
        }

        private static void MyMethod()
        {
            Console.WriteLine("   In MyMethod");
            Console.WriteLine("   Managed Thread Id: {0}", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("   Exiting MyMethod");
        }

        private static void MyMethodCallback(IAsyncResult result)
        {
            Console.WriteLine("   In MyMethodCallback");
            Console.WriteLine("   Managed Thread Id: {0}", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("   Exiting MyMethodCallback");
        }
    }

    internal class ThreadpoolTester
    {
        public void TestCallBack(object o)
        {
            Console.WriteLine("   In TestCallBack");
            Console.WriteLine("   Managed Thread Id: {0}", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("   Exiting TestCallBack");
        }
    }

    internal class EventTester
    {
        public event SomeEvent MyEvent;

        public void RaiseEvent()
        {
            if (MyEvent != null)
                MyEvent(this, new EventArgs());
        }
    }
}
