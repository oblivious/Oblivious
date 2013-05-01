using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace EnumeratingDrives
{
    /// <summary>
    /// DriveInfo enumeration, files and folder mucking about and other such nonsense.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            foreach (DriveInfo di in DriveInfo.GetDrives())
            {
                Console.WriteLine("\n  {0} ({1})", di.Name, di.DriveType);
                if (di.IsReady)
                {
                    Console.WriteLine("  -Drive Format: " + di.DriveFormat);
                    Console.WriteLine("  -Drive Free Space: " + di.AvailableFreeSpace);
                }
                else
                    Console.WriteLine("  -Drive is not ready.");
            }
            Console.WriteLine();

            DirectoryInfo dir = new DirectoryInfo(@"C:\Windows");

            Console.WriteLine("Folders:");
            foreach (DirectoryInfo dirInfo in dir.GetDirectories())
                Console.WriteLine(dirInfo.Name);
            Console.WriteLine("\nFiles:");
            foreach (FileInfo fi in dir.GetFiles())
                Console.WriteLine(fi.Name);

            DirectoryInfo newDir = new DirectoryInfo(@"C:\deleteme");
            if (newDir.Exists)
            {
                Console.WriteLine("The folder already exists, deleting...");
                newDir.Delete();
            }
            else
            {
                Console.WriteLine("The folder does not exist, creating...");
                newDir.Create();
            }

            // Create, copy and move files.
            if (File.Exists("mynewfile.txt"))
                File.Delete("mynewfile.txt");
            if (File.Exists("newfile2.txt"))
                File.Delete("newfile2.txt");
            if (File.Exists("newfile3.txt"))
                File.Delete("newfile3.txt");

            StreamWriter stream1 = File.CreateText("mynewfile.txt");
            File.Copy("mynewfile.txt", "newfile2.txt");
            File.Move("newfile2.txt", "newfile3.txt");
            stream1.Close();
            stream1.Dispose();
            /*
            if (File.Exists("mynewfile.txt"))
                File.Delete("mynewfile.txt");
            if (File.Exists("newfile2.txt"))
                File.Delete("newfile2.txt");
            if (File.Exists("newfile3.txt"))
                File.Delete("newfile3.txt");
            */
            // Alternatively, use FileInfo.
            FileInfo fi1 = new FileInfo("mynewfile.txt");
            FileInfo fi2 = new FileInfo("newfile2.txt");
            FileInfo fi3 = new FileInfo("newfile3.txt");
            if (fi1.Exists)
                fi1.Delete();
            fi1.CreateText();
            if (fi2.Exists)
                fi2.Delete();
            fi1.CopyTo("newfile2.txt");
            if (fi3.Exists)
                fi3.Delete();
            fi2.MoveTo("newfile3.txt");
        }
    }
}
