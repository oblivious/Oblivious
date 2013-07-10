using System;
using System.Management;

namespace ManagementScopeClass
{
    class Program
    {
        static void Main( string[] args )
        {
            try
            {
                Console.BufferHeight = 5000;
                Console.WindowWidth = 162;
                Console.WindowHeight = 56;
                /*
                ManagementObjectSearcher searcher1 = new ManagementObjectSearcher(
                    "SELECT * FROM Win32_Service WHERE State = 'Stopped'");
                foreach(var mo in searcher1.Get())
                {
                    Console.WriteLine(mo["DisplayName"]);
                }
                return;*/
                ManagementScope scope = new ManagementScope(@"\\DUB247-PC\root\cimv2");
                scope.Connect();

                ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_UserAccount WHERE Disabled = TRUE");
                //ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_Process");
                //ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

                ManagementObjectCollection queryCollection = searcher.Get();

                foreach (ManagementObject m in queryCollection)
                {
                    Console.WriteLine("Caption: {0,-30} Full Name: {1}", m["Caption"], m["FullName"]);
                    /*
                    //Console.WriteLine("Site Name: {0}", m.Site.Name);
                    Console.WriteLine("Path: {0}:", m.Path.Path);
                    Console.WriteLine("ClassPath: {0}", m.ClassPath.Path);
                    Console.WriteLine("Computer Name: {0}", m["csname"]);
                    Console.WriteLine("Windows Directory: {0}", m["WindowsDirectory"]);
                    Console.WriteLine("Operating System: {0}", m["Caption"]);
                    Console.WriteLine("Version: {0}", m["Version"]);
                    Console.WriteLine("Manufacturer: {0}", m["Manufacturer"]);*/
                    /*
                    string[] roles = (string[])m["Roles"];
                    foreach ( var role in roles )
                    {
                        Console.WriteLine("    " + role);
                    }
                    
                    Console.WriteLine("\nStandard \"Properties\":");
                    PropertyDataCollection properties = m.Properties;
                    foreach (var pd in properties)
                    {
                        Console.WriteLine("  Property Name: {0,-42} Type: {1,-10} Value: {2}", pd.Name, pd.Type, pd.Value);
                    }

                    Console.WriteLine("\nSystem Properties:");
                    PropertyDataCollection systemProperties = m.SystemProperties;
                    foreach (var pd in systemProperties)
                    {
                        Console.WriteLine("  Property Name: {0,-42} Type: {1,-10} Value: {2}", pd.Name, pd.Type, pd.Value);
                    }*/
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
            }
        }
    }
}
