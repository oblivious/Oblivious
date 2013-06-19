using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication6
{
    class Program
    {
        static void Main(string[] args)
        {
            int versionNumber = 1;
            int version;
            Console.WriteLine(versionNumber.ToString("00"));
            ByRef(ref versionNumber);
            Console.WriteLine(versionNumber.ToString("00"));
            ByOut(out version);
            Console.WriteLine(version.ToString("00"));

            Console.WriteLine();
        }
        private static void ByRef(ref int someInt)
        {
            someInt++;
        }
        private static void ByOut(out int someInt)
        {
            someInt = 3;
            someInt++;
        }
    }
}
