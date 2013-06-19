using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication12
{
    class Program
    {
        static void Main(string[] args)
        {
            Chinese hello = new Chinese()
            {
                YourMother = new Noodles(){MyMother = "hello"}
            };

            Console.WriteLine(hello.YourMother.MyMother);
            Console.WriteLine("ok");
            hello.YourMother = null;
            Console.WriteLine(hello.YourMother.MyMother ?? "fail");
        }
    }

    class Noodles
    {
        public string MyMother { get; set; }
    }

    class Chinese
    {
        public Noodles YourMother { get; set; }
    }
}
