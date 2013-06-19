using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace TelcelLogTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            //LogWriter logWriter = new LogWriter();
            try
            {
                using (var log = new StreamWriter(@"C:\Log\Log.txt", true))
                {
                    log.WriteLine(string.Format("{0}: {1}", DateTime.Now, "Starting run..."));
                }
                Console.WriteLine("Entering loop...");
                for (int i = 0; i < 100; i++)
                {
                    ThreadPool.QueueUserWorkItem((new LogWriter()).WriteToLog, i);
                }
                Console.WriteLine("Exited loop");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception thrown: " + e.ToString());
            }
            Console.ReadKey();
        }
    }

    class LogWriter
    {
        private static readonly object syncRoot = new object();

        public void WriteToLog(object state)
        {
            try
            {
                lock (syncRoot)
                {
                    int i = (int)state;
                    using (var log = new StreamWriter(@"C:\Log\Log.txt", true))
                    {
                        log.WriteLine(string.Format("{0}: {1}", DateTime.Now, "Received value: " + i));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception thrown: " + e.ToString());
            }
        }
    }
}
