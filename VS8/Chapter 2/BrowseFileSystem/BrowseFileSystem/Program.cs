using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BrowseFileSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Drives: ");
            foreach (DriveInfo di in DriveInfo.GetDrives())
                Console.WriteLine("  {0} ({1})", di.Name, di.DriveType);

            bool waitingForDriveLetter = true;
            DirectoryInfo dir = null;
            while (waitingForDriveLetter)
            {
                Console.Write("\nPress a drive letter to view files and folders, 'q' to quit: ");
                ConsoleKeyInfo drive = Console.ReadKey();
                Console.WriteLine();
                string driveLetter = drive.Key.ToString().ToUpperInvariant();
                if (driveLetter.ToLowerInvariant().Equals("q"))
                    return;
                if (!char.IsLetter(drive.KeyChar))
                {
                    Console.WriteLine("\nPlease enter a letter");
                    continue;
                }
                dir = new DirectoryInfo(driveLetter + @":\");
                if (dir.Exists)
                {
                    foreach (DriveInfo di in DriveInfo.GetDrives())
                    {
                        if (di.Name.Equals(driveLetter + @":\") && di.IsReady)
                            waitingForDriveLetter = false;
                    }
                }
                else
                    Console.WriteLine("Directory \"" + driveLetter + ":\\\" does not exist");
            }
            Console.WriteLine("\nFolders:");
            foreach (DirectoryInfo dirInfo in dir.GetDirectories())
                Console.WriteLine("  \\" + dirInfo.Name);

            Console.WriteLine("\nFiles:");
            foreach (FileInfo fi in dir.GetFiles())
                Console.WriteLine("  " + fi.Name);
        }
    }
}
