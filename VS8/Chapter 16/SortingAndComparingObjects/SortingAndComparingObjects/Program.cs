using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Globalization;

namespace SortingAndComparingObjects
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] words = new string[] { "AEble", "Æble" };

            Console.WriteLine("Sorting...");
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            SortWords(words);
            Thread.CurrentThread.CurrentCulture = new CultureInfo("da-DK");
            SortWords(words);

            Console.WriteLine("Searching...");
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            FindAE(words);
            Thread.CurrentThread.CurrentCulture = new CultureInfo("da-DK");
            FindAE(words);
        }

        static void SortWords(string[] words)
        {
            Console.WriteLine(Thread.CurrentThread.CurrentCulture);
            Array.Sort(words);
            foreach (string s in words)
                Console.WriteLine(s);
            Console.WriteLine();
        }

        static void FindAE(string[] words)
        {
            Console.WriteLine(Thread.CurrentThread.CurrentCulture + ":");
            Array.Sort(words);
            foreach (string s in words)
            {
                Console.WriteLine("    AE in {0}: {1}", s, s.IndexOf("AE"));
                Console.WriteLine("    Æ in {0}: {1}", s, s.IndexOf("Æ"));
            }
            Console.WriteLine();
        }
    }
}
