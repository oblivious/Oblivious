using System;
using System.Reflection;

namespace CodeProjectReflection
{
    class GetMethodsDemo
    {
        internal static void Run()
        {
            var t = typeof(Car);
            GetMethod(t);
            GetMethods(t);
        }

        private static void GetMethods(Type type)
        {
            Console.WriteLine("***** Methods *****");
            var mi = type.GetMethods(BindingFlags.DeclaredOnly |
                                     BindingFlags.Instance |
                                     BindingFlags.Public);
            foreach (var m in mi)
            {
                Console.WriteLine("->{0}", m.Name);
            }
            Console.WriteLine();
        }

        private static void GetMethod(Type type)
        {
            Console.WriteLine("***** Method *****");
            var mi = type.GetMethod("IsMoving");
            Console.WriteLine("->{0}", mi.Name);
            Console.WriteLine();
        }
    }
}
