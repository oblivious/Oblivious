using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OverrideToStringTestProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            DerivedClass derived = new DerivedClass();
            Console.WriteLine(derived.ToString());
            Console.WriteLine(derived.ToOtherFormat());

            Console.WriteLine("\nBase ValidLength: {0}", BaseClass.ValidLength);
            Console.WriteLine("\nBase ValidLengthIncludingHeaders: {0}", BaseClass.ValidLengthIncludingHeaders);
            Console.WriteLine("\nDerived ValidLength: {0}", DerivedClass.ValidLength);
            Console.WriteLine("\nDerived ValidLengthIncludingHeaders: {0}", DerivedClass.ValidLengthIncludingHeaders);
            Console.WriteLine("\nDerived ValidLength: {0}", ThirdLevelClass.ValidLength);
            Console.WriteLine("\nDerived ValidLengthIncludingHeaders: {0}", ThirdLevelClass.ValidLengthIncludingHeaders);

            Console.WriteLine(BaseClass.Length);
            Console.WriteLine(DerivedClass.Length);
            Console.WriteLine(ThirdLevelClass.Length);
        }
    }
}
