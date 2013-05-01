using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Specialized;

namespace AssortedListTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            SortedList sl = new SortedList();
            sl.Add("Stack", "Represents a LIFO collection of objects.");
            sl.Add("Queue", "Represents a FIFO collection of objects.");
            sl.Add("SortedList", "Represents a collection of key/value pairs.");

            foreach (DictionaryEntry de in sl)
            {
                Console.WriteLine("{0,12}: {1}", de.Key, de.Value);
            }

            Console.WriteLine("\n" + sl["Queue"]);
            Console.WriteLine(sl.GetByIndex(1));

            Console.WriteLine("\nName Value Collection:");
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("Stack", "Represents a LIFO collection of objects.");
            nvc.Add("Stack", "A pile of pancakes.");
            nvc.Add("Queue", "Represents a FIFO collection of objects.");
            nvc.Add("Queue", "In England, a line.");
            nvc.Add("SortedList", "Represents a collection of key/value pairs.");

            foreach (string s in nvc.GetValues(0))
                Console.WriteLine(s);

            foreach (string s in nvc.GetValues("Queue"))
                Console.WriteLine(s);
        }
    }
}
