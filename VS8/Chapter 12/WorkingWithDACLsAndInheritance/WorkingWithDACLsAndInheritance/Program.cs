using System;
using System.Text;
using System.IO;
using System.Security.AccessControl;
using System.Security.Policy;
using System.Security.Principal;

namespace WorkingWithDACLsAndInheritance
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectorySecurity ds = new DirectorySecurity();
            ds.AddAccessRule(new FileSystemAccessRule("Guest", FileSystemRights.Read, AccessControlType.Allow));
            string newFolder = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Guest");
            Directory.CreateDirectory(newFolder, ds);

            string newFile = System.IO.Path.Combine(newFolder, "Data.dat");
            File.Create(newFile);
        }
    }
}
