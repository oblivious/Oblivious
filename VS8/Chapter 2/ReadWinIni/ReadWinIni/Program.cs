using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ReadWinIni
{
    class Program
    {
        static void Main(string[] args)
        {
            TextReader tr = File.OpenText(@"C:\windows\win.ini");
            Console.WriteLine("win.ini contents:");
            Console.Write(tr.ReadToEnd());
            tr.Close();
            tr.Dispose();

            StreamReader sr = new StreamReader(@"C:\windows\win.ini");
            string input;
            while ((input = sr.ReadLine()) != null)
                Console.WriteLine(input);
            sr.Close();
            sr.Dispose();

            TextWriter tw = File.CreateText("output.txt");
            tw.WriteLine("Hello, world!");
            tw.Close();
        }
    }
}
