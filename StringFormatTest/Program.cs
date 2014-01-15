using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StringFormatTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "TelcelOutgoingEchoResponse";
            Console.WriteLine(String.Format("{0,30}", input));
            Console.WriteLine(String.Format("{0,30}", new SomeClass().TypeName));
        }
    }

    class SomeClass
    {
        public string TypeName { get { return this.GetType().Name; } }
    }
}
