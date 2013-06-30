using System;
using System.Reflection;

namespace CodeProjectReflection
{
    class AssemblyDemo
    {
        internal static void Run()
        {
            while (true)
            {
                var exit = false;
                Console.WriteLine(
                    "Select 1, 2 or 3 to load using Load(<name>), LoadFrom(<path>) or simply GetExecutingAssembly() respectively.");
                Console.Write("> ");
                var input = Console.ReadKey().KeyChar;
                Console.WriteLine();
                Assembly assembly = null;
                switch (input)
                {
                    case '1':
                        assembly = Assembly.Load("mscorlib");
                        break;
                    case '2':
                        assembly = Assembly.LoadFrom(@"C:\Windows\Microsoft.NET\Framework\v4.0.30319\caspol.exe");
                        break;
                    case '3':
                        assembly = Assembly.GetExecutingAssembly();
                        break;
                    default:
                        exit = true;
                        break;
                }
                if (exit)
                    break;

                var types = assembly.GetTypes();

                Console.WriteLine("***** Types *****");

                foreach (var type in types)
                {
                    Console.WriteLine(type.Name);
                }

                var attributes = Attribute.GetCustomAttributes(assembly);
                Console.WriteLine("***** Assembly Attributes *****");
                foreach (var attribute in attributes)
                {
                    Console.WriteLine(attribute.TypeId);
                }

                Console.ReadKey();
            }
        }
    }
}
