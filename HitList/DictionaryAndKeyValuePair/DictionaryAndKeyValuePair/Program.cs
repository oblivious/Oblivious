using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DictionaryAndKeyValuePair
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();

            dictionary.Add(1, "one");
            dictionary.Add(2, "two");
            dictionary.Add(3, "three");

            Console.WriteLine("\n   Key, Value");
            foreach (var kvp in dictionary)
            {
                Console.WriteLine("{0,6}{1,7}", kvp.Key, kvp.Value);
            }
            Console.WriteLine();
        }
    }
}
