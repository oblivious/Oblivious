using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;

namespace ArrayListBasic
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Basic Array List Testing:");
            ArrayList al = new ArrayList();
            al.Add("Hello");
            al.Add("World");
            al.Add(5);
            al.Add(new FileStream("deleteme", FileMode.Create));

            Console.WriteLine("The array has " + al.Count + " items");

            foreach (object o in al)
            {
                Console.WriteLine(o.ToString());
            }

            Console.WriteLine("\nRemove, Insert and Sort Testing:");
            al = new ArrayList();
            al.Add("Hello");
            al.Add("World");
            al.Add("this");
            al.Add("is");
            al.Add("a");
            al.Add("test");

            Console.WriteLine("\nBefore:");
            foreach (object s in al)
                Console.WriteLine(s.ToString());


            al.Remove("test");
            al.Insert(4, "not");

            al.Sort();

            Console.WriteLine("\nAfter:");
            foreach (object s in al)
                Console.WriteLine(s.ToString());

            al.Sort(new reverseSorter());
            Console.WriteLine("\nReversed:");
            foreach (object s in al)
                Console.WriteLine(s.ToString());

            al.Reverse();
            Console.WriteLine("\nReversed again, different method:");
            foreach (object s in al)
                Console.WriteLine(s.ToString());

            Console.WriteLine("\nBinary Search Example:");
            al = new ArrayList();
            al.AddRange(new string[] { "Hello", "World", "this", "is", "a", "test" });
            foreach (object s in al)
                Console.Write(s + " ");
            Console.WriteLine("\n\"this\" is at index: " + al.BinarySearch("this"));
        }
    }

    public class reverseSorter : IComparer
    {
        int IComparer.Compare(object x, object y)
        {
            return (new CaseInsensitiveComparer()).Compare(y, x);
        }
    }
}
