using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
            String noodles = null;
            Console.WriteLine(noodles ?? "Didn't throw an exception, woohoo!");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }


    class Nullable
    {
        public string MyField { get; set; }
        public InternalNullable Property { get; set; }

        public Nullable()
        {
        }
    }

    class InternalNullable
    {
        public string One { get; set; }
        public string Two { get; set; }
    }
}
