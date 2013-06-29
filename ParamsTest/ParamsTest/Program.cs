using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParamsTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(FormatThisString("{0},{1},{2}", 1, 2, 3));
            Console.WriteLine("Without parameters");
        }

        public static string FormatThisString(string input, params object[] parameters)
        {
            return String.Format(input, parameters);
        }
    }
}
