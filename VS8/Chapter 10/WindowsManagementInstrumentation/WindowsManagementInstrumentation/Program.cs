using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace WindowsManagementInstrumentation
{
    class Program
    {
        static void Main(string[] args)
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectCollection queryCollection = searcher.Get();

            foreach (ManagementObject m in queryCollection)
            {
                Console.WriteLine("Computer Name : {0}", m["csname"]);
                Console.WriteLine("Windows Directory : {0}", m["WindowsDirectory"]);
                Console.WriteLine("Operating System: {0}", m["Caption"]);
                Console.WriteLine("Version: {0}", m["Version"]);
                Console.WriteLine("Manufacturer: {0}", m["Manufacturer"]);
            }

            //-----------------------------------------------------------------------------------------
            //Process creation event (waiting for)

            WqlEventQuery query = new WqlEventQuery("__InstanceCreationEvent", new TimeSpan(0, 0, 1), "TargetInstance isa \"Win32_Process\"");
            ManagementEventWatcher watcher = new ManagementEventWatcher(query);
            ManagementBaseObject e = watcher.WaitForNextEvent();

            Console.WriteLine("Process {0} has been created, path is: {1}",
                ((ManagementBaseObject)e["TargetInstance"])["Name"],
                ((ManagementBaseObject)e["TargetINstance"])["ExecutablePath"]);

            watcher.Stop();

            //-----------------------------------------------------------------------------------------
            //Responding to WMI Events with an Event Handler

            ManagementEventWatcher mew = null;

            EventReceiver receiver = new EventReceiver();

            mew = GetWatcher(new EventArrivedEventHandler(receiver.OnEventArrived));

            mew.Start();

            Console.ReadKey();

            mew.Stop();
        }

        public static ManagementEventWatcher GetWatcher(EventArrivedEventHandler handler)
        {
            WqlEventQuery query = new WqlEventQuery("__InstanceCreationEvent", new TimeSpan(0, 0, 1), "TargetInstance isa \"Win32_Process\"");
            ManagementEventWatcher watcher = new ManagementEventWatcher(query);
            watcher.EventArrived += new EventArrivedEventHandler(handler);
            return watcher;
        }
    }

    class EventReceiver
    {
        public void OnEventArrived(object sender, EventArrivedEventArgs e)
        {
            ManagementBaseObject evt = e.NewEvent;

            Console.WriteLine("Process {0} has been created, path is: {1}",
                ((ManagementBaseObject)evt["TargetInstance"])["Name"],
                ((ManagementBaseObject)evt["TargetInstance"])["ExecutablePath"]);
 
        }
    }
}
