using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EmailStringSanitationTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "<Donal O Byrne> dobyrne@ezetop.com; giglesias@ezetop.com";
            Console.WriteLine("1: " + input);
            input = Regex.Replace(input, @"<[^>]+?>", string.Empty);
            Console.WriteLine("2: " + input);
            input = Regex.Replace(input, @"\s+", ","); //Replace any sequences of whitespace characters with a single comma.
            Console.WriteLine("3: " + input);
            input = Regex.Replace(input, @"(,|;)+", ","); //Replace any multiples of ',' or ';' with a single comma.
            Console.WriteLine("4: " + input);
            input = Regex.Replace(input, @"^,", ""); //Remove any comma from the start of the string.
            Console.WriteLine("5: " + input);
            input = Regex.Replace(input, @",$", ""); //Remove any comma from the start of the string.
            Console.WriteLine("6: " + input);
        }
    }
}
