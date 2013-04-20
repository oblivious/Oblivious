using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MemoryStreamTest
{
    class Program
    {
        static void Main(string[] args)
        {
            MemoryStream ms = new MemoryStream();

            StreamWriter sw = new StreamWriter(ms);

            sw.WriteLine("Hello, World!");

            sw.Flush();

            FileStream fs = File.Create("memory.txt");
            ms.WriteTo(fs);

            sw.Close();
            ms.Close();
            fs.Close();
        }
    }
}
