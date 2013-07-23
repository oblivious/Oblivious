using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Generics
{
    class Program
    {
        static void Main(string[] args)
        {
            ApplyConsoleWindowSettings();

            List<string> codeFiles = GetNamesOfCodeFilesInProjectDirectory();

            var choice = DisplayMenuChoices(codeFiles);

            var codeChoice = codeFiles[choice];

            InstantiateAndRunWeaponOfChoice(codeChoice);
        }

        private static void ApplyConsoleWindowSettings()
        {
            Console.WindowWidth = 160;
            Console.WindowHeight = 40;
        }

        private static List<string> GetNamesOfCodeFilesInProjectDirectory()
        {
            string path = Directory.GetCurrentDirectory();

            string[] files = Directory.GetFiles(path + @"\..\..", "*.cs");

            var fileNames = new List<string>();

            foreach (var s in files)
            {
                FileInfo fileInfo = new FileInfo(s);
                if (!fileInfo.Name.Equals("Program.cs"))
                    fileNames.Add(fileInfo.Name.Substring(0, fileInfo.Name.Length - 3));
            }

            fileNames.Sort();

            return fileNames;
        }

        private static int DisplayMenuChoices(List<string> codeFiles)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n   Please choose a number between 1 and {0}, or enter 'q' to quit:\n", codeFiles.Count);

                var i = 1;
                foreach (string s in codeFiles)
                {
                    Console.WriteLine("{0, 4}. {1}", i, s);
                }
                Console.Write("\n   > ");
                var input = Console.ReadLine();
                int choice;
                if (Int32.TryParse(input, out choice) && choice >= 1 && choice <= codeFiles.Count)
                {
                    Console.Clear();
                    return choice - 1;
                }
                if (input.ToLower().Equals("q"))
                    Environment.Exit(0);
            }
        }

        private static void InstantiateAndRunWeaponOfChoice(string codeChoice)
        {
            Console.WriteLine("Calling the Run() method of {0}\n", codeChoice);
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();

                Type type = assembly.GetType("Generics." + codeChoice);

                MethodInfo method = type.GetMethod("Run", BindingFlags.Static | BindingFlags.Public);

                method.Invoke(null, null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine("\nRun() has completed.");
        }
    }
}
