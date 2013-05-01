using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace ReadingBooties
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader(@"C:\bootie.txt");
            string input = sr.ReadToEnd();
            sr.Close();

            Match m = Regex.Match(input, @"^timeout\s(.*)$", RegexOptions.Multiline);
            Console.WriteLine(m.Groups[1]);
        }
    }
}
