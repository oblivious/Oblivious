using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "\tinput $&^&$INPUT%^&$12345&\n\r";
            string output = Regex.Replace(input, @"[^0-9a-zA-Z]", string.Empty);

            Console.WriteLine("Input: {0}", input);
            Console.WriteLine("Output: {0}", output);
        }
    }
}
