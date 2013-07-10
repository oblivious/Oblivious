using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace CodeProjectWMI
{
    class TackingOnManagementEventWatcher
    {
        public static void Run()
        {
            WqlEventQuery query = new WqlEventQuery("__InstanceCreationEvent", new TimeSpan(0, 0, 1), "TargetInstance isa \"Win32_Process\"");

            ManagementEventWatcher watcher = new ManagementEventWatcher();

            watcher.Query = query;

            watcher.Options.Timeout = new TimeSpan(0,0,5);

            Console.WriteLine("Open application (motepad.exe) to trigger an event.");

            ManagementBaseObject e = watcher.WaitForNextEvent();

            Console.WriteLine("Process {0} has been created, path is: {1}",
                ((ManagementBaseObject)e["TargetInstance"])["Name"], ((ManagementBaseObject)e["TargetInstance"])["ExecutablePath"]);

            watcher.Stop();
        }
    }
}
