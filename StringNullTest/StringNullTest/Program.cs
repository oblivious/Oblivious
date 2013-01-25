using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StringNullTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string x = null;
            string y = "something";
            Console.WriteLine(x + y);
        }
    }
}
