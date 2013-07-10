using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace HastableCollisionTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var table = new Hashtable();

            Console.WriteLine("Basic addition of objects:");

            var one = new Hashable(1, "one");
            var two = new Hashable(2, "two");
            var three = new Hashable(3, "three");

            table.Add(one.Id, one);
            table.Add(two.Id, two);
            table.Add(three.Id, three);

            Console.WriteLine("Hashtable[{0}]: {1}", one.Id, table[one.Id]);
            Console.WriteLine("Hashtable[{0}]: {1}", two.Id, table[two.Id]);
            Console.WriteLine("Hashtable[{0}]: {1}", three.Id, table[three.Id]);

            Console.WriteLine("\nCreating a collision:");

            var four = new Hashable(1, "four");

            table.Add(four.Id, four);

            Console.WriteLine("Hashtable[{0}]: {1}", one.Id, table[one.Id]);
            Console.WriteLine("Hashtable[{0}]: {1}", two.Id, table[two.Id]);
            Console.WriteLine("Hashtable[{0}]: {1}", three.Id, table[three.Id]);
        }
    }

    public class Hashable
    {
        public int Id { get; private set; }
        public string Value { get; private set; }

        public Hashable(int id, string value)
        {
            Id = id;
            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
