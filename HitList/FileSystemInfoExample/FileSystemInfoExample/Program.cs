using System;
using System.Collections.Generic;
using System.IO;

namespace FileSystemInfoExample
{
    class Program
    {
        static void Main( string[] args )
        {
            Console.WindowWidth = 150;
            Console.WindowHeight = 40;

            int i = 2;
            Console.WriteLine(i.ToString("D"));

            string[] entries = Directory.GetDirectories(@"C:\");
            List<DirectoryInfo> list = new List<DirectoryInfo>();
            foreach ( var e in entries )
            {
                list.Add(new DirectoryInfo(e));
            }

            list.Sort((x, y) => x.CreationTime.CompareTo(y.CreationTime));

            foreach ( var entry in list )
            {
                DisplayFileSystemInfoAttributes(entry);
            }

            foreach ( string entry in Directory.GetFiles(@"C:\") )
            {
                DisplayFileSystemInfoAttributes(new FileInfo(entry));
            }

            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach ( var drive in drives )
            {
                if (drive.IsReady)
                    Console.WriteLine("Name: {0,-20} VolumeLabel: {1, -20} TotalSize: {2,-20}", drive.Name, drive.VolumeLabel, drive.TotalSize);
            }
        }

        static void DisplayFileSystemInfoAttributes(FileSystemInfo fsi)
        {
            string entryType = "File";

            if ( ( fsi.Attributes & FileAttributes.Directory ) == FileAttributes.Directory )
            {
                entryType = "Directory";
            }

            // Show this entry's type, name and creation date.
            Console.WriteLine("{0,9} entry {1,-40} was created on {2:D}", entryType, fsi.FullName, fsi.CreationTime);
        }
    }
}
