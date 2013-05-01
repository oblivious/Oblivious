using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Win32;

namespace AccessControlListsTest
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectorySecurity ds = new DirectorySecurity(@"C:\Program Files", AccessControlSections.Access);
            AuthorizationRuleCollection arc = ds.GetAccessRules(true, true, typeof(NTAccount));

            Console.WriteLine("Identity Reference".PadRight(28)+ ": " + 
                    "Access Control Type".PadRight(20) + " " + "File System Rights");
            foreach (FileSystemAccessRule fsar in arc)
            {
                Console.WriteLine(fsar.IdentityReference.ToString().PadRight(28) + ": " + 
                    fsar.AccessControlType.ToString().PadRight(20) + " " + fsar.FileSystemRights);
            }

            Console.WriteLine();

            RegistrySecurity rs = Registry.LocalMachine.GetAccessControl();
            arc = rs.GetAccessRules(true, true, typeof(NTAccount));

            Console.WriteLine("Identity Reference".PadRight(28) + ": " +
                    "Access Control Type".PadRight(20) + " " + "Registry Rights");
            foreach (RegistryAccessRule rar in arc)
            {
                Console.WriteLine(rar.IdentityReference.ToString().PadRight(28) + ": " +
                    rar.AccessControlType.ToString().PadRight(20) + " " + rar.RegistryRights);
            }
        }
    }
}
