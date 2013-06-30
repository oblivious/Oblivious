using System;

namespace CodeProjectReflection
{
    internal class GetConstructorsDemo
    {
        internal static void Run()
        {
            // Get name of type 
            var t = typeof (Car);
            GetConstructorsInfo(t);
        }

        // Display method names of type. 
        private static void GetConstructorsInfo(Type t)
        {
            Console.WriteLine("***** ConstructorsInfo *****");
            var ci = t.GetConstructors();
            foreach (var c in ci)
                Console.WriteLine(c.ToString());
            Console.WriteLine("");
        }
    }
}