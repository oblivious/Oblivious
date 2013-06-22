using System;
using System.Collections;

namespace ArrayListClass
{
    class Utils
    {
        public static void BannerChar(char character)
        {
            for (int i = 0; i < Console.WindowWidth; i++)
                Console.Write(character.ToString());
            Console.WriteLine();
        }

        public static void WriteCentred(string input)
        {
            Console.WriteLine();
            int indent = (Console.WindowWidth - input.Length) / 2;
            for (int i = 0; i < indent; i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine(input);
            Console.WriteLine();
            Console.WriteLine();
        }

        public static void PrintArrayContents(string arrayName, ArrayList list)
        {
            Console.WriteLine("\nThe contents of {0} are as follows: ", arrayName);
            foreach (string s in list)
            {
                Console.WriteLine("   \"{0}\"", s); 
            }
            Console.WriteLine();
        }
    }
}
