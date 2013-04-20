using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace FileSystemsWatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Creating file system watcher for: " + Environment.GetEnvironmentVariable("USERPROFILE"));
            Console.ReadKey();
            // Create an instance of FileSystemWatcher...
            using (FileSystemWatcher fsw = new FileSystemWatcher(Environment.GetEnvironmentVariable("USERPROFILE")))
            {

                // Set the FileSystemWatcher properties
                fsw.IncludeSubdirectories = true;
                fsw.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite;
                
                // Add the changed event handler
                fsw.Changed += new FileSystemEventHandler(fsw_Changed);
                fsw.Renamed += new RenamedEventHandler(fsw_Renamed);

                fsw.EnableRaisingEvents = true;

                Console.WriteLine("Press 'q' to quit...");
                while (true)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    if (Char.ToLowerInvariant(keyInfo.KeyChar).Equals('q'))
                        return;
                    Thread.Sleep(1000);
                }
            }
        }

        private static void fsw_Changed(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("\t-" + e.ChangeType + ": " + e.FullPath);
        }

        private static void fsw_Renamed(object sender, RenamedEventArgs e)
        {
            // Write the path of a changed file to the console
            Console.WriteLine(e.ChangeType + " from " + e.OldFullPath + " to " + e.Name);
        }
    }
}
