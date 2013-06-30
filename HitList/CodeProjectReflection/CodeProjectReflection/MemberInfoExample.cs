using System;
using System.Globalization;
using System.Reflection;

namespace CodeProjectReflection
{
    class MemberInfoExample
    {
        internal static void Run()
        {
            Console.Write("Press 'y' if it wants the hose again: ");
            var input = Console.ReadKey().KeyChar;
            if (!input.ToString(CultureInfo.InvariantCulture).ToLowerInvariant().Equals("y"))
            {
                Console.WriteLine();
                return;
            }

            var indent = 0;

            var a = Assembly.GetExecutingAssembly();

            Display(indent, "Assembly identity = {0}", a.FullName);
            Display(indent + 1, "Codebase = {0}", a.CodeBase);

            // Display the set of assemblies our assemblies reference.
            Display(indent, "Referenced assemblies: ");
            foreach (var an in a.GetReferencedAssemblies())
            {
                Display(indent + 1, "Name = {0}, Version = {1}, Culture = {2}, PublicKey token = {3}",
                    an.Name, an.Version, an.CultureInfo, an.GetPublicKeyToken());
            }

            Console.WriteLine("\nAppDomain.CurrentDomain.GetAssemblies():");

            foreach (var b in AppDomain.CurrentDomain.GetAssemblies())
            {
                Display(indent, "Assembly: {0}", b);

                foreach (var m in b.GetModules(true))
                {
                    Display(indent + 1, "Module: {0}", m.Name);
                }

                indent += 1;

                foreach (var t in b.GetExportedTypes())
                {
                    Console.WriteLine();
                    Display(indent, "Type: {0}", t);

                    indent += 1;
                    foreach (var mi in t.GetMembers())
                    {
                        Display(indent, "Member: {0}", mi.Name);

                        DisplayAttributes(indent, mi);

                        switch (mi.MemberType)
                        {
                            case MemberTypes.Method:
                                foreach (var pi in ((MethodInfo) mi).GetParameters())
                                {
                                    Display(indent + 1, "Parameter: Type = {0}, Name = {1}", pi.ParameterType, pi.Name);
                                }
                                break;
                            case MemberTypes.Property:
                                foreach (var am in ((PropertyInfo) mi).GetAccessors())
                                {
                                    Display(indent + 1, "Accessor method: {0}", am);
                                }
                                break;
                        }
                    }
                    indent -= 1;
                }
                indent -= 1;
            }
        }

        private static void DisplayAttributes(int indent, MemberInfo mi)
        {
            var attributes = mi.GetCustomAttributes(false);
            if (attributes.Length == 0)
                return;
            Display(indent + 1, "Attributes:");
            foreach (var o in attributes)
                Display(indent + 2, "0", o);
        }

        private static void Display(int indent, string format, params object[] param)
        {
            Console.Write(new string(' ', indent * 2));
            Console.WriteLine(format, param);
        }
    }
}
