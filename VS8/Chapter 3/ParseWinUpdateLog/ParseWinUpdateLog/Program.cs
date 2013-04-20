using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace ParseWinUpdateLog
{
    class Program
    {
        static void Main(string[] args)
        {
            string windir = Environment.GetEnvironmentVariable("windir");
            StreamReader reader = new StreamReader(windir + @"\WindowsUpdate2.log");
            StreamWriter writer = new StreamWriter(@"WindowsUpdate3.log");
            string input = reader.ReadToEnd();
            reader.Close();
            //@"^(\d{4}-\d{2}-\d{2})\t(\d{2}\:\d{2}\:\d{2}\:\d{3}).+\[(Exit\scode[^\]]+)\]$"
            foreach (Match m in Regex.Matches(input, @"^(\d{4}-\d{2}-\d{2})\t(\d{2}\:\d{2}\:\d{2}\:\d{3}).+\[(Exit\scode.+)\].*$", RegexOptions.Multiline))
            {
                //Console.WriteLine("{0}\t{1}\t{2}", m.Groups[1], m.Groups[2], m.Groups[3]);
            }
            string output = Regex.Replace(input, @"^(?<year>\d{4})-(?<month>\d{2})-(?<day>\d{2})(.+)$", 
                                @"${month}-${day}-${year}$d{1}", RegexOptions.Multiline);
            writer.Write(output);
            writer.Close();
            reader.Dispose();
            writer.Dispose();
        }
    }
}
