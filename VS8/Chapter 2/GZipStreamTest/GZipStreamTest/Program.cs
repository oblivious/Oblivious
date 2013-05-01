using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace GZipStreamTest
{
    class Program
    {
        static void Main(string[] args)
        {
            GZipStream gzOut = new GZipStream(File.Create("data.zip"), CompressionMode.Compress);

            StreamWriter sw = new StreamWriter(gzOut);

            for (int i = 0; i < 1000; i++)
                sw.Write("Hello, World! ");

            sw.Close();
            gzOut.Close();

            GZipStream gzIn = new GZipStream(File.OpenRead("data.zip"), CompressionMode.Decompress);

            StreamReader sr = new StreamReader(gzIn);

            Console.WriteLine(sr.ReadToEnd());

            sr.Close();
            gzIn.Close();
        }
    }
}
