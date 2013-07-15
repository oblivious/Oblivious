using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace StaticStuff
{
    class Program
    {
        static void Main(string[] args)
        {
            Foo foo1 = new Foo();
            Console.WriteLine(Foo.Bob);
            Console.WriteLine(foo1.NonStaticBob);

            Console.ReadKey();

            Foo foo2 = new Foo();
            Console.WriteLine(Foo.Bob);
            Console.WriteLine(foo2.NonStaticBob);

            Console.ReadKey();

            ConfigurationManager.RefreshSection("appSettings");

            Foo foo3 = new Foo();
            Console.WriteLine(Foo.Bob);
            Console.WriteLine(foo3.NonStaticBob);

            Console.ReadKey();
        }
    }

    class Foo
    {
        public string NonStaticBob { get; private set; }

        public Foo()
        {
            Bob = ConfigurationManager.AppSettings["Bob"];
            NonStaticBob = ConfigurationManager.AppSettings["Bob"];
        }
        public static string Bob = ConfigurationManager.AppSettings["Bob"];
    }
}
