using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary1;

namespace TypeForwardedToTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Class1 myClass = new Class1();
                Console.WriteLine("Class1: " + myClass.WriteOut1());
                MyResidentClassInLib1 myClass2 = new MyResidentClassInLib1();
                Console.WriteLine("Class2: " + myClass2.WriteOut2());
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occurred: " + e.ToString());
            }
        }
    }
}
