using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace AssemblyNameExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowWidth = 150;
            Console.WindowHeight = 40;

            AssemblyName myAssemblyName = new AssemblyName("Example, Version=1.0.0.2001, Culture=en-US, PublicKeyToken=null");
            Console.WriteLine("Name: {0}", myAssemblyName.Name);
            Console.WriteLine("Version: {0}", myAssemblyName.Version);
            Console.WriteLine("CultureInfo: {0}", myAssemblyName.CultureInfo);
            Console.WriteLine("FullName: {0}", myAssemblyName.FullName);

            Console.WriteLine("\nThat was just the Constructor example, now on to the meat and potatoes:\n");

            int indent = 0;

            Assembly a = Assembly.GetExecutingAssembly();

            Display(indent, "Assembly identity={0}", a.FullName);
            Display(indent + 1, "Codebase={0}", a.CodeBase);

            Display(indent, "Referenced assemblies:");
            foreach (AssemblyName an in a.GetReferencedAssemblies())
            {
                Display(indent + 1, "Name={0}, Version={1}, Culture={2}, PublicKeyToken={3}", an.Name, an.Version, an.CultureInfo, (BitConverter.ToString(an.GetPublicKeyToken())));
            }
            Console.WriteLine();

            Console.WriteLine("AppDomain.CurrentDomain.GetAssemblies():");
            foreach (Assembly b in AppDomain.CurrentDomain.GetAssemblies())
            {
                Display(indent, "Assembly: {0}", b);

                // Display information about each module of this assembly. 
                foreach (Module m in b.GetModules(true))
                {
                    Display(indent + 1, "Module: {0}, Scope: {1}", m.Name, m.ScopeName);
                }
            }
        }

        private static void Display(int indent, string format, params object[] param)
        {
            Console.Write(new string(' ', indent * 2));
            Console.WriteLine(format, param);
        }
    }
}
