using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnumStringTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(MyEnum.Rice + ", " + (int)MyEnum.Noodles + ", " + (int)MyEnum.Bacon);
            int i = 1;
            Console.WriteLine((MyEnum)i);
            Console.WriteLine((MyEnum)4);
            string test = ((MyEnum)0).ToString();
            Console.WriteLine(test);
            test = ((MyEnum)4).ToString();
            Console.WriteLine(test);
        }
    }

    enum MyEnum : int
    {
        Rice,
        Noodles,
        Bacon
    }
}
