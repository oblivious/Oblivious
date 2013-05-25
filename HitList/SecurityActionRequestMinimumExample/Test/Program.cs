using System;
using System.Security.Permissions;
using System.IO;
//[assembly: FileIOPermission(SecurityAction.RequestMinimum, Unrestricted=true)]
namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test is loaded.");
            File.Create("test.txt");
        }
    }
}