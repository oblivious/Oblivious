using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDictionaryExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a dictionary that contains no more than three entries.
            IDictionary d = new SimpleDictionary(3);

            // Add three people and their ages to the dictionary.
            d.Add("Jeff", 40);
            d.Add("Kristin", 34);
            d.Add("Aidan", 1);

            Console.WriteLine("Number of elements in dictionary = {0}", d.Count);

            Console.WriteLine("Does dictionary contain 'Jeff'? {0}", d.Contains("Jeff"));

            Console.WriteLine("Jeff's age is {0}", d["Jeff"]);

            // Display every entry's key and value.
            foreach (DictionaryEntry de in d)
            {
                Console.WriteLine("{0} is {1} years old.", de.Key, de.Value);
            }
            // Remove an entry that exists.
            d.Remove("Jeff");

            // Remove an entry that does not exist, but do not throw an exception.
            d.Remove("Max");

            // Show the names (keys) of the people in the dictionary.
            foreach (string s in d.Keys)
                Console.WriteLine(s);

            // Show the ages (values) of the people in the dictionary.
            foreach (int age in d.Values)
                Console.WriteLine(age);

        }
    }
}
