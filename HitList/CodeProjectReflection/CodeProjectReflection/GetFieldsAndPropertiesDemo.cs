using System;

namespace CodeProjectReflection
{
    class GetFieldsAndPropertiesDemo
    {
        internal static void Run()
        {
            var t = typeof (Car);
            GetFields(t);
            GetProperties(t);
        }

        private static void GetFields(Type t)
        {
            Console.WriteLine("***** Fields *****");
            var fi = t.GetFields();
            foreach (var field in fi)
                Console.WriteLine("->{0}", field.Name);
            Console.WriteLine();
        }

        private static void GetProperties(Type t)
        {
            Console.WriteLine("***** Properties *****");
            var pi = t.GetProperties();
            foreach (var prop in pi)
                Console.WriteLine("->{0}", prop.Name);
            Console.WriteLine();
        }
    }
}
