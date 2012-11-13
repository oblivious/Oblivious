using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.Remoting.Messaging;

namespace AsyncTest
{
    // The delegate must have the same signature as 
    // the method it will call asynchronously.
    public delegate string AsyncMethodCaller(int callDuration, out int threadId);

    // Calling synchronous methods asynchronously, from:
    // http://msdn.microsoft.com/en-us/library/2e08f6yc%28v=vs.100%29.aspx
    class Program
    {
        static void Main()
        {
            // The asynchronous method puts the thread id here.
            int threadId;

            // Create an instance of the test class
            AsyncDemo ad = new AsyncDemo();

            // Create the delegate.
            AsyncMethodCaller caller = new AsyncMethodCaller(ad.TestMethod);
            IAsyncResult result;

            #region Using Begin and End Invoke
            Console.WriteLine("Beginning solely end invoke method...");
            // Initiate the asynchronous call.
            result = caller.BeginInvoke(1111, out threadId, null, null);

            Thread.Sleep(0);
            Console.WriteLine("Main thread {0} does some work.", Thread.CurrentThread.ManagedThreadId);

            // Call EndInvoke to wait for the asynchronous call to complete,
            // and to retrieve the results.
            string returnValue = caller.EndInvoke(out threadId, result);

            Console.WriteLine("The call executed on thread {0}, has completed using EndInvoke (blocking) with return value \"{1}\".", threadId, returnValue);
            Console.WriteLine();
            #endregion

            ad = new AsyncDemo();
            caller = new AsyncMethodCaller(ad.TestMethod);

            #region Using a Wait Handle
            Console.WriteLine("Beginning wait handle method...");
            result = caller.BeginInvoke(1234, out threadId, null, null);

            Thread.Sleep(0);
            Console.WriteLine("Main thread {0} does some more work.", Thread.CurrentThread.ManagedThreadId);

            // Wait for the WaitHandle to become signalled.
            result.AsyncWaitHandle.WaitOne();

            // Perform additional processing here.
            // Call EndInvoke to retrieve the results.
            returnValue = caller.EndInvoke(out threadId, result);

            // Close the wait handle.
            result.AsyncWaitHandle.Close();

            Console.WriteLine("The called executed on thread {0}, has completed using a Wait Handle with return value \"{1}\".", threadId, returnValue);
            Console.WriteLine();
            #endregion

            ad = new AsyncDemo();
            caller = new AsyncMethodCaller(ad.TestMethod);

            #region Using Polling
            Console.WriteLine("Beginning polling method...");
            result = caller.BeginInvoke(1010, out threadId, null, null);

            // Poll while simulating work.
            while (result.IsCompleted == false)
            {
                Thread.Sleep(250);
                Console.Write(".");
            }

            returnValue = caller.EndInvoke(out threadId, result);

            Console.WriteLine("\nThe call executed on thread {0}, has completed using polling with return value \"{1}\".", threadId, returnValue);
            Console.WriteLine();
            #endregion

            ad = new AsyncDemo();
            caller = new AsyncMethodCaller(ad.TestMethod);

            #region Using an AsyncCallBack method
            Console.WriteLine("Beginning asynchronous callback method...");
            /*
            AsyncCallback callback = new AsyncCallback(ad.AsyncCallBackMethod);
            result = caller.BeginInvoke(1234, out threadId, callback, null);

            callback(result);
            */
            result = caller.BeginInvoke(1234, out threadId, new AsyncCallback(ad.AsyncCallBackMethod),
                "The call executed on thread {0}, with return value \"{1}\".");

            Console.WriteLine("The main thread {0} continues to execute...", Thread.CurrentThread.ManagedThreadId);

            // The callback is made on a threadpool thread. Threadpool threads
            // are background threads, which do not keep the application running
            // if the main thread ends. Comment out the next line to demonstrate
            // this.
            Thread.Sleep(4000);

            Console.WriteLine("The main thread ends.");
            Console.WriteLine();
            #endregion

            #region My own test
            Console.WriteLine("Running my own test...");
            AsyncThingy thingy = new AsyncThingy();
            thingy.DoAsyncStuff();
            Console.WriteLine("Main thread {0} is back.", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("The asynchronous method should write something shortly, or stuff will explode");
            Thread.Sleep(6000);
            #endregion
        }
    }

    public class AsyncDemo
    {
        // The method to be executed asynchronously.
        public string TestMethod(int callDuration, out int threadId)
        {
            threadId = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine("Test method begins. It is executing on thread {0}", threadId);
            Thread.Sleep(callDuration);
            Console.WriteLine("Test method has finished sleeping.");
            return String.Format("My call time was {0}.", callDuration.ToString());
        }

        public void AsyncCallBackMethod(IAsyncResult ar)
        {
            // Retrieve the delegate.
            AsyncResult result = (AsyncResult)ar;
            AsyncMethodCaller caller = (AsyncMethodCaller)result.AsyncDelegate;

            // Retrieve the format string that was passed as state
            // information.
            string formatString = (string)ar.AsyncState;

            int threadId = 0;

            string returnValue = caller.EndInvoke(out threadId, ar);

            Console.WriteLine(formatString, threadId, returnValue);

            Console.WriteLine("The test method has returned. This is being written from thread {0} by the async callback method.",
                Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Result Async State: " + (string)ar.AsyncState);
        }
    }

    public class AsyncThingy
    {
        public AsyncDemo myAd = new AsyncDemo();
        public int threadId;

        AsyncMethodCaller caller;
        IAsyncResult result;

        public AsyncThingy()
        {
            myAd = new AsyncDemo();
            caller = new AsyncMethodCaller(myAd.TestMethod);
        }

        public void DoAsyncStuff()
        {
            result = caller.BeginInvoke(5000, out threadId, null, null);
        }
    }
}
