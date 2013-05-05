using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace ManagementObjectSearcherTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ManagementObjectSearcher processorSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");

            foreach (ManagementObject obj in processorSearcher.Get())
            {
                Console.WriteLine(obj["Name"]);
                Console.WriteLine("{0} / {1}", obj["CurrentClockSpeed"], obj["MaxClockSpeed"]);
            }

            EventQuery query = new EventQuery();
            //query.QueryString = "SELECT * FROM _InstanceModifiationEvent WITHIN 3600 WHERE TargetInstance ISA 'Win32_DiskDrive'";
            query.QueryString = "SELECT * FROM __InstanceCreationEvent WITHIN 1 WHERE TargetInstance isa \"Win32_Process\"";

            ManagementEventWatcher watcher = new ManagementEventWatcher(query);
            watcher.Options.Timeout = new TimeSpan(0, 0, 30);

            EventArrivedEventHandler eventArrived = new EventArrivedEventHandler(EventReceived);

            watcher.EventArrived += eventArrived;
            watcher.Start();

            Console.WriteLine("Open an application (notepad.exe) to trigger an event.");
            //ManagementBaseObject e = watcher.WaitForNextEvent();

            Console.WriteLine("Actually I rewrote this to use events, it will now monitor all application starts until you hit a key...");
            ConsoleKeyInfo key;
            while (true)
            {
                key = Console.ReadKey();
                if (key.KeyChar.Equals('q'))
                    break;
            }
            /*
            //Display information from the event
            Console.WriteLine("Process {0} has been created, path is: {1}", ((ManagementBaseObject)e["TargetInstance"])["Name"],
                ((ManagementBaseObject)e["TargetInstance"])["ExecutablePath"]);
            */
            watcher.Stop();
        }

        private static void EventReceived(object sender, EventArrivedEventArgs e)
        {
            Console.WriteLine("Process {0} has been created, path is: {1}", ((ManagementBaseObject)(e.NewEvent)["TargetInstance"])["Name"],
                ((ManagementBaseObject)(e.NewEvent)["TargetInstance"])["ExecutablePath"]);
        }
    }
}
