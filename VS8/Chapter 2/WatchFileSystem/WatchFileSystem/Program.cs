using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WatchFileSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            using (FileSystemWatcher fsw = new FileSystemWatcher(Environment.GetEnvironmentVariable("USERPROFILE")))
            {
                fsw.IncludeSubdirectories = true;
                fsw.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite;

                fsw.Changed += new FileSystemEventHandler(fsw_Changed);
                fsw.Created += new FileSystemEventHandler(fsw_Changed);
                fsw.Deleted += new FileSystemEventHandler(fsw_Changed);
                fsw.Renamed += new RenamedEventHandler(fsw_Renamed);

                fsw.EnableRaisingEvents = true;

                Console.WriteLine("Press a key to end the program");
                Console.ReadKey();
            }
        }

        static void fsw_Changed(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine(e.ChangeType + ": " + e.FullPath);
        }

        static void fsw_Renamed(object sender, RenamedEventArgs e)
        {
            Console.WriteLine(e.ChangeType + ": " + e.FullPath);
        }
    }
}
