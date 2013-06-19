using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RegexTest
{
    class Program
    {
        static void Main(string[] args)
        {
            if (Regex.IsMatch(":", @"^:$"))
            {
                Console.WriteLine("Is a match.");
            }
        }
    }
}
