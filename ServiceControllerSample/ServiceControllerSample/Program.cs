using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;
using System.Diagnostics;
using System.Threading;

namespace ServiceControllerSample
{
    class Program
    {
        public enum SimpleServiceCustomCommands
        {
            StopWorker = 128,
            RestartWorker,
            CheckWorker
        }

        static void Main(string[] args)
        {
            ServiceController[] scServices = ServiceController.GetServices();
            foreach (ServiceController scTemp in scServices)
            {
                if (scTemp.ServiceName.Equals("TestService"))
                {
                    ServiceController sc = new ServiceController("TestService");
                    Console.WriteLine("Status = " + sc.Status);
                    Console.WriteLine("Can Pause and Continue = " + sc.CanPauseAndContinue);
                    Console.WriteLine("Can Shutdown = " + sc.CanShutdown);
                    Console.WriteLine("Can Stop = " + sc.CanStop);
                    if (sc.Status == ServiceControllerStatus.Stopped)
                    {
                        sc.Start();
                        while (sc.Status == ServiceControllerStatus.Stopped)
                        {
                            Thread.Sleep(1000);
                            sc.Refresh();
                        }
                    }

                    sc.ExecuteCommand((int)SimpleServiceCustomCommands.StopWorker);
                    sc.ExecuteCommand((int)SimpleServiceCustomCommands.RestartWorker);

                    sc.Stop();
                    while (sc.Status != ServiceControllerStatus.Stopped)
                    {
                        Thread.Sleep(1000);
                        sc.Refresh();
                    }

                    Console.WriteLine("Status = " + sc.Status);
                    sc.Start();
                    while (sc.Status == ServiceControllerStatus.Stopped)
                    {
                        Thread.Sleep(1000);
                        sc.Refresh();
                    }
                    Console.WriteLine("Status = " + sc.Status);

                    Console.Write(scTemp.ServiceName.PadRight(30));
                    Console.WriteLine(("Status: " + scTemp.Status));

                    EventLog el = new EventLog();
                    el.Source = "MySource";
                    Console.WriteLine("Log: " + el.Log);
                    Console.WriteLine("LogName: " + el.LogDisplayName);

                    EventLogEntryCollection elec = el.Entries;
                    foreach (EventLogEntry ele in elec)
                    {
                        Console.Write(ele.Source + ": ");
                        Console.WriteLine(ele.Message);
                    }

                    ServiceControllerPermission permission = new ServiceControllerPermission();
                    Console.WriteLine("ServiceControllerPermission.Any: " + ServiceControllerPermission.Any);
                    Console.WriteLine("ServicecontrollerPermissions.Local: " + ServiceControllerPermission.Local);
                }
            }
        }
    }
}
