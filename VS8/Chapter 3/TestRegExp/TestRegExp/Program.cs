using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TestRegExp
{
    class Program
    {
        static void Main(string[] args)
        {


            string inny = "Company Name: Contoso, Inc.";
            Match m = Regex.Match(inny, @"Company Name: (.*$)");
            Console.WriteLine(m.Groups[1]);

            bool waitingForKey = true;
            while (waitingForKey)
            {
                Console.Write("\nContinue? y/n: ");
                string key = Console.ReadKey().Key.ToString().ToLowerInvariant();
                if (key.Equals("n"))
                    return;
                else if (key.Equals("y"))
                    waitingForKey = false;
                else
                    Console.Write("\nThat is not a valid selection.");
            }
            Console.WriteLine();

            Console.Write("Enter regular expression: ");
            string regularExpression = Console.ReadLine();

            Console.Write("Enter input for comparison: ");
            string input = Console.ReadLine();

            if (Regex.IsMatch(input, regularExpression))
                Console.WriteLine("Input matches regular expression.");
            else
                Console.WriteLine("Input does not match regular expression.");
        }

        private static void DumpHrefs(string inputString)
        {
            Regex r;
            Match m;

            /* Tres Bizarre. "href" followed by whitespace, followed by "=" followed by whitespace */
            r = new Regex(@"href\s*=\s*(?:""(?<1>[^""]*)""|(?<1>\S+))", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            for (m = r.Match(inputString); m.Success; m = m.NextMatch())
            {
                Console.WriteLine("Found href " + m.Groups[1] + " at " + m.Groups[1].Index);
            }
        }
    }
}
