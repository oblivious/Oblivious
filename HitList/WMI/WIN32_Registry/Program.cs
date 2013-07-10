using System;
using System.Collections.Generic;
using System.Text;

namespace baileySoft.Wmi
{
    class Program
    {
        static void Main(string[] args)
        {
            //Instantiate Remote Client
            //This is commented out since you must specify your details in order for it to work.
            //Registry.RegistryObject SysRegistry =
            //    new Registry.RegistryRemote("neal.bailey",
            //                                "S3cr3tPa$$",
            //                                "BAILEYSOFT",
            //                                "192.168.2.1");

            //Instantiate Local Client
            Registry.RegistryObject SysRegistry = new Registry.RegistryLocal();
            string registryKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion";
           
            //Enumerate Keys
            Console.WriteLine("");
            Console.WriteLine("SubKeys Under: " + registryKey);
            
            foreach (string subKey in SysRegistry.EnumerateKeys(
                        baileySoft.Wmi.Registry.baseKey.HKEY_LOCAL_MACHINE, registryKey))
            {
                Console.WriteLine(subKey);
            }

            //Enumerate Values
            registryKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
            Console.WriteLine("");
            Console.WriteLine("Values Under: " + registryKey);
         
            foreach (string subKey in SysRegistry.EnumerateValues(
                        baileySoft.Wmi.Registry.baseKey.HKEY_LOCAL_MACHINE, registryKey))
            {
                Console.WriteLine(subKey);
            }
            
            //Get Value Data
            registryKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
            Console.WriteLine("");
            Console.WriteLine("Getting Value Data For Value Daemon Tools");
            Console.WriteLine(SysRegistry.GetValue(baileySoft.Wmi.Registry.baseKey.HKEY_LOCAL_MACHINE,
                              registryKey, "DAEMON Tools",
                              baileySoft.Wmi.Registry.valueType.STRING));

           
            //Create Key
            registryKey = @"SOFTWARE\MyWmiApp";
            Console.WriteLine("");
            Console.WriteLine("Creating Key: " + registryKey);
            SysRegistry.CreateKey(baileySoft.Wmi.Registry.baseKey.HKEY_LOCAL_MACHINE, registryKey);

            //Create Value
            registryKey = @"SOFTWARE\MyWmiApp";
            Console.WriteLine("");
            Console.WriteLine("Creating Value: SomeValue");
            SysRegistry.CreateValue(baileySoft.Wmi.Registry.baseKey.HKEY_LOCAL_MACHINE, 
                                                        registryKey, "SomeValue", "Value01");


            //Set Value
            registryKey = @"SOFTWARE\MyWmiApp";
            Console.WriteLine("");
            Console.WriteLine("Setting SomeValue: Value02");
            SysRegistry.SetValue(baileySoft.Wmi.Registry.baseKey.HKEY_LOCAL_MACHINE, 
                  registryKey, "SomeValue", "Value02", baileySoft.Wmi.Registry.valueType.STRING);

            //Deleting Value
            registryKey = @"SOFTWARE\MyWmiApp";
            Console.WriteLine("");
            Console.WriteLine("Deleting Value: SomeValue");
            SysRegistry.DeleteValue(baileySoft.Wmi.Registry.baseKey.HKEY_LOCAL_MACHINE, 
                                                         registryKey, "Value01");

            //Deleting Key
            registryKey = @"SOFTWARE\MyWmiApp";
            Console.WriteLine("");
            Console.WriteLine("Deleting Key: " + registryKey);
            SysRegistry.DeleteKey(baileySoft.Wmi.Registry.baseKey.HKEY_LOCAL_MACHINE, registryKey);

            Console.WriteLine("");
            Console.WriteLine("Getting System Registry Settings");
            Console.WriteLine("Caption: " + SysRegistry.Caption);
            Console.WriteLine("Current Size: " + SysRegistry.CurrentSize);
            Console.WriteLine("Description: " + SysRegistry.Description);
            Console.WriteLine("Install Date: " + SysRegistry.InstallDate);
            Console.WriteLine("Max Size: " + SysRegistry.MaximumSize);
            Console.WriteLine("Name: " + SysRegistry.Name);
            Console.WriteLine("Proposed Size: " + SysRegistry.ProposedSize);
            Console.WriteLine("Status: " + SysRegistry.Status);
            Console.ReadLine();
        }
    }
}
