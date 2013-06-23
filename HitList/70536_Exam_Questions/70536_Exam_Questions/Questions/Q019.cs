using System;
using System.Reflection;

namespace _70536_Exam_Questions.Questions
{
    class Q019
    {
        public static void Run()
        {
            Console.WriteLine("\nQ019 Start:\n");

            var test = Assembly.GetCallingAssembly();
            Console.WriteLine("Calling Assembly Name: " + test.FullName);

            Console.ReadKey();

            // This variable holds the amount of indenting that should be used when displaying each line on information.
            var indent = 0;

            // Display information about the EXE assembly.
            var assemblyA = Assembly.GetExecutingAssembly();
            Display(indent, "Assembly identity={0}", assemblyA.FullName);
            Display(indent + 1, "Codebase={0}", assemblyA.CodeBase);

            // Display the set of assemblies our assemblies reference.
            Display(indent, "Referenced assemblies:");
            foreach (var an in assemblyA.GetReferencedAssemblies())
            {
                Display(indent + 1, "Name={0}, Version={1}, Culture={2}, PublicKey token={3}",
                    an.Name, an.Version, an.CultureInfo.Name, BitConverter.ToString(an.GetPublicKeyToken()));
            }
            Console.ReadKey();
            Display(indent, "");

            // Display information about each assembly loading into this AppDomain.
            foreach (var assemblyB in AppDomain.CurrentDomain.GetAssemblies())
            {
                Display(indent, "Assembly: {0}", assemblyB);

                // Display information about each module of this assembly.
                foreach (var module in assemblyB.GetModules())
                {
                    Display(indent + 1, "Module: {0}", module.Name);
                }
                Console.ReadKey();

                // Display information about each type exported from this assembly.
                indent += 1;
                foreach (var type in assemblyB.GetExportedTypes())
                {
                    Display(0, "");
                    Display(indent, "type: {0}", type);

                    // For each type, show its members & their custom attributes.
                    indent += 1;
                    foreach (var memberInfo in type.GetMembers())
                    {
                        Display(indent, "Member: {0}", memberInfo.Name);
                        DisplayAttributes(indent, memberInfo);

                        // If the member is a method, display information about its parameters.
                        if (memberInfo.MemberType == MemberTypes.Method)
                        {
                            foreach (var parameterInfo in ((MethodInfo) memberInfo).GetParameters())
                            {
                                Display(indent + 1, "Parameter: Type={0}, name={1}", parameterInfo.ParameterType, parameterInfo.Name);
                            }
                        }

                        // If the member is a property, display information about the property's accessor methods.
                        if (memberInfo.MemberType == MemberTypes.Property)
                        {
                            foreach (var accessorMethod in ((PropertyInfo) memberInfo).GetAccessors())
                            {
                                Display(indent + 1, "Accessor method: {0}", accessorMethod);
                            }
                        }
                    }
                    Console.ReadKey();
                    indent -= 1;
                }
                Console.ReadKey();
                indent -= 1;
            }
            Console.ReadKey();
            Console.WriteLine("\nQ019 End...\n");
        }

        // Displays the custom attributes applied to the specified member.
        public static void DisplayAttributes(int indent, MemberInfo mi)
        {
            // Get the set of custom attributes; if none exist, just return.
            var attrs = mi.GetCustomAttributes(false);

            // Display the custom attributes applied to this member.
            Display(indent + 1, "Attributes:");
            foreach (var o in attrs)
            {
                Display(indent + 2, "{0}", o.ToString());
            }
        }

        public static void Display(int indent, string format, params object[] param)
        {
            Console.Write(new string(' ', indent * 2));
            Console.WriteLine(format, param);
        }
    }
}
