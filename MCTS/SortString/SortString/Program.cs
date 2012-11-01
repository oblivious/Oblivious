using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortString
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "Microsoft .NET Framework 2.0 Application Development Foundation";
            string[] sa = s.Split(' ');

            Array.Sort(sa);

            s = String.Join(" ", sa);
            Console.WriteLine(s);
        }
    }
}
