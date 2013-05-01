using System;
using System.Reflection;
using System.Text;

namespace ReflectionSBTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a Type instance
            // Typically, this would be done by loading an external assembly,
            // and then calling Assembly.GetType()
            //Type t = typeof(StringBuilder);

            // Far more difficult to figure out than I would have expected.
            // Did you know the mscorlib.dll doesn't show up in the reference list? I guess it's implicit. 
            Assembly ass = Assembly.LoadFrom(@"C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll");
            Type t = ass.GetType("System.Text.StringBuilder", true);

            // Create a ConstructorInfo instance that will allow us to create an 
            // instance of the Type we just loaded.
            // GetConstructor requires a list of parameters in a Type array
            // that match those required by the constructor.
            // This example represents the StringBuilder constructor that 
            // requires a single parameter.
            ConstructorInfo ci = t.GetConstructor(new Type[] { Type.GetType("System.String") });

            // Create an instance of the type by calling ConstructorInfo.Invoke.
            // Provide the parameters required by the constructor: a single string.
            // This creates a StringBuilder instance.
            Object sb = ci.Invoke(new Object[] { "Hello, " });

            // Casting to a regular type just to check it can actually be done...
            StringBuilder other = (StringBuilder)sb;
            Console.WriteLine(other);

            // Create a MethodInfo instance representing the StringBuilder.Append method.
            // GetMethod requires the first parameter to be the name of the method.
            // The second parameter is a Type array representing the parameters required
            // by the method. We're using the Append overload that requires a single string.
            MethodInfo sbAppend = t.GetMethod("Append", new Type[] { typeof(string) });

            // Call StringBuilder.Append and provide a single parameter: the string "world!".
            Object result = sbAppend.Invoke(sb, new Object[] { "world!" });

            Console.WriteLine(result);


            // Everything was done using reflection.


            PropertyInfo pi = t.GetProperty("Length");

            int length = (int)pi.GetValue(sb, null);

            Console.WriteLine("Length: {0}", length);


            t = typeof(Console);

            foreach (MemberInfo m in t.GetMembers(BindingFlags.NonPublic | BindingFlags.Static))
                Console.WriteLine("{0}: {1}", m.Name, m.MemberType);

            Console.WriteLine();

            // Assembly Attributes
            Assembly asm = Assembly.GetExecutingAssembly();

            foreach (Attribute attr in asm.GetCustomAttributes(false))
            {
                if (attr.GetType() == typeof(AssemblyCopyrightAttribute))
                {
                    Console.WriteLine("Copyright: {0}", ((AssemblyCopyrightAttribute)attr).Copyright);
                }

                if (attr.GetType() == typeof(AssemblyCompanyAttribute))
                {
                    Console.WriteLine("Company: {0}", ((AssemblyCompanyAttribute)attr).Company);
                }

                if (attr.GetType() == typeof(AssemblyDescriptionAttribute))
                {
                    Console.WriteLine("Description: {0}", ((AssemblyDescriptionAttribute)attr).Description);
                }
            }

            object[] descs = asm.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            Console.WriteLine("Number returned: " + descs.Length);
            AssemblyDescriptionAttribute desc = (AssemblyDescriptionAttribute)descs[0];
            Console.WriteLine("Description: " + desc.Description);
        }
    }
}
