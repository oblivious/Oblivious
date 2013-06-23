using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Q015_Sort
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputLines = new List<string>();
            string inputText;
            do
            {
                inputText = Console.ReadLine();
                if (!String.IsNullOrEmpty(inputText))
                    inputLines.Add(inputText);
            } while (!String.IsNullOrEmpty(inputText));

            foreach (var words in inputLines.Select(s => s.Split(' ')).Where(words => words.Length > 0))
            {
                Array.Sort(words);
                foreach (var w in words)
                    Console.Write(w + " ");
                Console.WriteLine();
            }
        }
    }
}
