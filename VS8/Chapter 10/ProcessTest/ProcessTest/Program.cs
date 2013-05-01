using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ProcessTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Process[] processes = Process.GetProcesses();
            List<SortableProcess> sortableProcesses = new List<SortableProcess>();
            foreach (Process p in processes)
                sortableProcesses.Add(new SortableProcess(p));
            sortableProcesses.Sort();
            foreach (SortableProcess sp in sortableProcesses)
                Console.WriteLine("{0}: {1}", sp.Process.Id, sp.Process.ProcessName);
            Console.WriteLine("Number of processes: " + processes.Length);

            Process current = Process.GetCurrentProcess();
            Console.WriteLine("\nThe current process is: {0}: {1}\n", current.Id, current.ProcessName);

            foreach (Process p in Process.GetProcesses())
            {
                Console.WriteLine("{0}: {1}", p.Id.ToString(), p.ProcessName);
                try
                {
                    foreach (ProcessModule pm in p.Modules)
                    {
                        Console.WriteLine("\t{0}: {1}", pm.ModuleName, pm.ModuleMemorySize.ToString());
                    }
                }
                catch (System.ComponentModel.Win32Exception ex)
                {
                    Console.WriteLine("\t Unable to list modules");
                }
            }
        }
    }

    class SortableProcess : IComparable<SortableProcess>
    {
        private Process process;

        public Process Process
        {
            get { return this.process; }
            set { this.process = value; }
        }

        public SortableProcess(Process p)
        {
            this.process = p;
        }

        #region IComparable<SortableProcess> Members

        public int CompareTo(SortableProcess other)
        {
            return this.Process.ProcessName.CompareTo(other.Process.ProcessName);
        }

        #endregion
    }
}
