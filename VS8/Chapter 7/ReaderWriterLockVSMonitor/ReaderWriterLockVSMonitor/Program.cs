using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ReaderWriterLockVSMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime start = DateTime.Now;
            MemFile m = new MemFile();

            Thread t1 = new Thread(new ThreadStart(m.ReadFile));
            Thread t2 = new Thread(new ThreadStart(m.WriteFile));
            Thread t3 = new Thread(new ThreadStart(m.ReadFile));
            Thread t4 = new Thread(new ThreadStart(m.ReadFile));

            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();

            t1.Join();
            t2.Join();
            t3.Join();
            t4.Join();

            Console.WriteLine("Time consumed: " + (DateTime.Now - start).TotalSeconds + " seconds.");

            start = DateTime.Now;
            MemFile2 m2 = new MemFile2();

            Thread t5 = new Thread(new ThreadStart(m2.ReadFile));
            Thread t6 = new Thread(new ThreadStart(m2.WriteFile));
            Thread t7 = new Thread(new ThreadStart(m2.ReadFile));
            Thread t8 = new Thread(new ThreadStart(m2.ReadFile));

            t5.Start();
            t6.Start();
            t7.Start();
            t8.Start();

            t5.Join();
            t6.Join();
            t7.Join();
            t8.Join();

            Console.WriteLine("Time consumed: " + (DateTime.Now - start).TotalSeconds + " seconds.");
        }
    }

    class MemFile
    {
        string file = "Hello, World!";
        public void ReadFile()
        {
            lock (this)
            {
                for (int i = 1; i <= 3; i++)
                {
                    Console.WriteLine(file);
                    Thread.Sleep(1000);
                }
            }
        }

        public void WriteFile()
        {
            lock (this)
            {
                file += " It's a nice day!";
            }
        }
    }

    class MemFile2
    {
        string file = "Hello, World!";
        ReaderWriterLock rwl = new ReaderWriterLock();

        public void ReadFile()
        {
            rwl.AcquireReaderLock(10000);
            for (int i = 1; i <= 3; i++)
            {
                Console.WriteLine(file);
                Thread.Sleep(1000);
            }
            rwl.ReleaseReaderLock();
        }

        public void WriteFile()
        {
            rwl.AcquireWriterLock(10000);
            file += " It's a nice day!";
            rwl.ReleaseWriterLock();
        }
    }
}
