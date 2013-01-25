using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace StreamTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            //Files and streams testing
            FileStream stream = File.Create("pasta.txt");
            Console.WriteLine("File created...");
            Console.ReadKey();
            FileInfo info = new FileInfo("bacon.txt");
            Console.WriteLine("Got file info for: " + info.FullName);
            FileStream writer = info.OpenWrite();
            writer.Write(new byte[] { Byte.Parse("65"), Byte.Parse("66"), Byte.Parse("67") }, 0, 3);
            writer.Flush();
            writer.Close();
            Stream steam = new FileStream("noodles.txt", FileMode.Create);//new Stream(new byte[]{Byte.Parse("1"), Byte.Parse("2"),Byte.Parse("3"),Byte.Parse("4")});
            AppendToStream(steam, new byte[]{Byte.Parse("65"),Byte.Parse("66"),Byte.Parse("67")});
            DumpStream(steam);
            steam.Flush();
            steam.Close();
            Console.ReadKey();
            info.Delete();
            info = new FileInfo("noodles.txt");
            info.Delete();
            stream.Write(Encoding.ASCII.GetBytes("Some pasta"), 0, 10);
            stream.Close();
            Console.ReadKey();
            File.Delete("pasta.txt");


            //Directory Testing
            DirectoryInfo dirInfo = Directory.CreateDirectory("bleh");
            Console.WriteLine("Created directory: " + dirInfo.FullName);

            string[] files = Directory.GetFileSystemEntries(System.Environment.CurrentDirectory);
            foreach (string s in files)
                Console.WriteLine(s);
            string[] drives = Directory.GetLogicalDrives();
            foreach (string s in drives)
                Console.WriteLine(s);
            dirInfo = Directory.GetParent(Directory.GetCurrentDirectory());
            Console.WriteLine(dirInfo.FullName);
            Console.WriteLine(dirInfo.Parent.FullName);
        }

        static void DumpStream(Stream theStream)
        {
            theStream.Position = 0;
            while (theStream.Position != theStream.Length)
            {
                Console.WriteLine("{0:x2}", theStream.ReadByte());
            }
        }

        static void AppendToStream(Stream theStream, byte[] data)
        {
            theStream.Position = theStream.Length;
            theStream.Write(data, 0, data.Length);
        }
    }
}
