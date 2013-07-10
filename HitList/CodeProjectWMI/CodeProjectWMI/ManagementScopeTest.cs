using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace CodeProjectWMI
{
    class ManagementScopeTest
    {
        public static void Run()
        {
            ManagementScope scope = new ManagementScope(@"\\.\root\cimv2");

            scope.Connect();

            WqlObjectQuery query = new WqlObjectQuery("SELECT * FROM Win32_NTLogEvent WHERE LogFile = 'Application'");

            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

            ManagementObjectCollection queryCollection = searcher.Get();

            foreach(var m in queryCollection)
            {
                Console.WriteLine("\nStandard \"Properties\":");
                PropertyDataCollection properties = m.Properties;
                foreach(var pd in properties)
                {
                    Console.WriteLine("  Property Name: {0,-42} Type: {1,-10} Value: {2}", pd.Name, pd.Type, pd.Value);
                }
            }
        }
    }
}
