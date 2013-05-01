using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace StringWriterReader
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            sw.Write("Hello, ");
            sw.Write("World!");
            sw.Close();

            StringReader sr = new StringReader(sb.ToString());
            Console.WriteLine(sr.ReadToEnd());
            sr.Close();
        }
    }
}
