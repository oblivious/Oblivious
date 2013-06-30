using System;

namespace CodeProjectReflection
{
    class GetInterfacesDemo
    {
        internal static void Run()
        {
            var t = typeof (Car);
            GetInterfaces(t);
        }

        private static void GetInterfaces(Type t)
        {
            Console.WriteLine("***** Interfaces *****");
            var interfaces = t.GetInterfaces();
            foreach (var i in interfaces)
                Console.WriteLine("->{0}", i.Name);
        }
    }
}
