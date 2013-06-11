using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using System.Threading;

namespace GenericPrincipalThreadTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Creating a Generic Principal");
            GenericIdentity identity = new GenericIdentity("Doodad", "Asskicking");
            GenericPrincipal principal = new GenericPrincipal(identity, new string[] {"Buttkisser", "Lamewad"} );

            Thread.CurrentPrincipal = principal;

            Console.WriteLine("\nQueueing method using threadpool thread...");
            ThreadPool.QueueUserWorkItem(MyMethod);

            Console.WriteLine("\nTrying to change the principal!");
            identity = new GenericIdentity("Hickey", "Buttmunch");
            principal = new GenericPrincipal(identity, new string[] { "Buttkisser", "Lamewad" });
            Thread.CurrentPrincipal = principal;

            Console.WriteLine("\nQueueing method using Thread!");
            Thread thread = new Thread(new ParameterizedThreadStart(MyMethod));
            thread.Start();

            Thread.Sleep(200);
            Console.WriteLine("\n\nSlept a bit because I was soo tired...");
            Console.Read();
        }

        static void MyMethod(object context)
        {
            Console.WriteLine("\nIn the method of thread {0}:", Thread.CurrentThread.ManagedThreadId);
            GenericPrincipal innerPrincipal = (GenericPrincipal)Thread.CurrentPrincipal;
            Console.WriteLine("({0})Inner Principal Name: {1}", Thread.CurrentThread.ManagedThreadId, innerPrincipal.Identity.Name);
            Console.WriteLine("({0})Inner Principal Auth Type: {1}", Thread.CurrentThread.ManagedThreadId, innerPrincipal.Identity.AuthenticationType);
        }
    }
}
