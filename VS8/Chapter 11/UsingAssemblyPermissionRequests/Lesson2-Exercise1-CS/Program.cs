using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Permissions;

// TODO: Add CAS declarations
[assembly: FileIOPermission(SecurityAction.RequestMinimum, Unrestricted=true)]
[assembly: FileIOPermission(SecurityAction.RequestOptional, ViewAndModify=@"C:\Hello.txt")]
namespace Lesson2_Exercise1_CS
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a file
            TextWriter tw = new StreamWriter(@"C:\Hello.txt");
            tw.WriteLine("Hello, world!");
            tw.Close();

            // Display the text of the file
            TextReader tr = new StreamReader(@"C:\Hello.txt");
            Console.WriteLine(tr.ReadToEnd());
            tr.Close();
        }
    }
}
