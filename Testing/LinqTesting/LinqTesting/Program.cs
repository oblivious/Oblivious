using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Thingy> thingies = new List<Thingy>();
            thingies.Add(new Thingy(DateTime.Now + new TimeSpan(0, 10, 0)));
            thingies.Add(new Thingy(DateTime.Now + new TimeSpan(0, 15, 0)));
            thingies.Add(new Thingy(DateTime.Now));
            thingies.Add(new Thingy(DateTime.Now + new TimeSpan(0, 5, 0)));

            foreach (Thingy t in thingies)
            {
                Console.WriteLine(t.SomeDateTime);
            }

            var test = (from t in thingies
                        select t.SomeDateTime).Min();

            Console.WriteLine("Minimum: " + test);
        }
    }

    class Thingy
    {
        public DateTime SomeDateTime { get; set; }

        public Thingy(DateTime someDateTime)
        {
            SomeDateTime = someDateTime;
        }
    }
}
