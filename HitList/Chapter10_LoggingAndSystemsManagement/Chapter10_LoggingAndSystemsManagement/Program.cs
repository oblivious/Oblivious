using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading;

namespace Chapter10_LoggingAndSystemsManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowWidth = 164;
            Console.WindowHeight = 54;
            List<Type> validTypes = new List<Type>();
            Assembly assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (type.Name != "Program")
                {
                    var method = type.GetMethod("Run", BindingFlags.Static | BindingFlags.Public);
                    if (method != null)
                    {
                        validTypes.Add(type);
                    }
                }
            }
            while (true)
            {
                Console.WriteLine("\n\n\tSelect Class to Run:\n");
                var index = 1;
                foreach (var type in validTypes)
                {
                    Console.WriteLine("\t\t{0}. {1}", index, type.Name);
                    index++;
                }
                Console.WriteLine("\t\tq. Quit");
                Console.Write("\n\n\t> ");
                string input = Console.ReadLine();
                Console.WriteLine();

                if (input.ToLowerInvariant().Equals("q"))
                    break;

                int choice;
                if (!Int32.TryParse(input, out choice) && !(1 <= choice && choice <= validTypes.Count))
                    return;

                Console.Write("\n\tRunning class {0}", validTypes[choice - 1].Name);
                for (int i = 0; i < 3; i++)
                {
                    Console.Write(".");
                    Thread.Sleep(500);
                }
                Console.Clear();

                MethodInfo methodInfo = validTypes[choice - 1].GetMethod("Run", BindingFlags.Static | BindingFlags.Public);
                methodInfo.Invoke(null, null);

                Console.Write("\nPress any key to continue...");
                Console.ReadKey();

                Console.Clear();
            }
        }
    }
}
