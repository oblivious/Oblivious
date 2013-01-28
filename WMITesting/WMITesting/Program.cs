using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace WMITesting
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                /*
                ManagementClass mc = new ManagementClass("Win32_ComputerSystem");

                ManagementObjectCollection moc = mc.GetInstances();

                if (moc.Count != 0)
                {
                    foreach (ManagementObject mo in moc)
                    {
                        Console.WriteLine("\nMachine Make: {0}\nMachine Model: {1}\nSystem Type: {2}\nHostName: {3}\nUserName: {4}",
                            mo["Manufacturer"].ToString(),
                            mo["Model"].ToString(),
                            mo["SystemType"].ToString(),
                            mo["DNSHostName"].ToString(),
                            mo["UserName"].ToString());
                    }
                }
                */
                /*
                //
                // Accessing TMP-related information
                //
                bool isTpmPresent;
                uint status = 0;
                object[] wmiParams = null;
                // create management class object
                ManagementClass mc2 = new ManagementClass("/root/CIMv2/Security/MicrosoftTpm:Win32_Tpm");
                Console.WriteLine("ManagementClass" + (mc2 == null ? " is null" : " is valid"));
                // collection to store all management objects
                ManagementObjectCollection moc2 = mc2.GetInstances();
                //Retrieve single onstance of WMI management object
                ManagementObjectCollection.ManagementObjectEnumerator moe2 = moc2.GetEnumerator();
                moe2.Reset();
                moe2.MoveNext();
                ManagementObject mo2 = (ManagementObject)moe2.Current;

                if (mo2 == null)
                {
                    isTpmPresent = false;
                    Console.WriteLine("\nTPM Present: {0}\n", isTpmPresent.ToString());
                }
                else
                {
                    isTpmPresent = true;
                    Console.WriteLine("\nTPM Present: {0}\n", isTpmPresent.ToString());
                }

                if (isTpmPresent)
                {
                    wmiParams = new object[1];
                    wmiParams[0] = false;
                    status = (uint)mo2.InvokeMethod("IsActivated", wmiParams);
                    if (status != 0)
                        Console.WriteLine("The WMI method call {0} returned arror status {1}", "IsActivated", status);
                    else
                        Console.WriteLine("TPM Status: {0}", status);
                }*/
                //
                // Accessing TPM-related information
                //

                ManagementScope scope = new ManagementScope(@"//./root/CIMV2/Security/MicrosoftTpm");

                ObjectQuery oq = new ObjectQuery("SELECT * FROM Win32_Tpm");

                ManagementObjectSearcher mos = new ManagementObjectSearcher(scope, oq);

                foreach (ManagementObject mo1 in mos.Get())
                {
                    Console.WriteLine("In loop");
                    Console.WriteLine(mo1.InvokeMethod("IsActivated", new object[] { false }));
                }

                /*
                bool isTpmPresent;
                UInt32 status = 0;
                object[] wmiParams = null;
                // create management class object
                ManagementClass mc = new ManagementClass(@"root/CIMV2/Security/MicrosoftTpm:Win32_Tpm");
                //collection to store all management objects
                ManagementObjectCollection moc = mc.GetInstances();
                // Retrieve single instance of WMI management object
                ManagementObjectCollection.ManagementObjectEnumerator moe = moc.GetEnumerator();

                foreach (ManagementBaseObject mi in moc)
                {
                    Console.WriteLine(mi.ClassPath.ClassName);
                }

                //moe.MoveNext();
                ManagementObject mo = (ManagementObject)moe.Current;
                if (null == mo)
                {
                    isTpmPresent = false;
                    Console.WriteLine("\nTPM Present: {0}\n", isTpmPresent.ToString());
                }
                else
                {
                    isTpmPresent = true;
                    Console.WriteLine("\nTPM Present: {0}\n", isTpmPresent.ToString());
                }
                if (isTpmPresent) // Query if TPM is in activated state
                {
                    wmiParams = new object[1];
                    wmiParams[0] = false;
                    status = (UInt32)mo.InvokeMethod("IsActivated", wmiParams);
                    if (0 != status)
                    {
                        Console.WriteLine("The WMI method call {0} returned error status {1}", "IsActivated", status);
                    }
                    else
                    {
                        Console.WriteLine("TPM Status: {0}", status);
                    }
                }
                 * */
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }
}
