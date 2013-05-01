using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericCollectionsExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, Noodles> noodles = new Dictionary<int, Noodles>();

            noodles.Add(0, new Noodles("Hair", 50));
            noodles.Add(1, new Noodles("Pot Noodle", 10));
            noodles.Add(2, new Noodles("I don't know, sue me", 30));

            foreach(Noodles n in noodles.Values)
            {
                Console.WriteLine(n.ToString());
            }
        }
    }

    public class MySuperList<T,U> : Dictionary<T,U>
    {
        public override string ToString()
        {
            return "Brilliant!";
        }
    }

    public class Noodles : IComparable, IComparable<Noodles>, IEquatable<Noodles>
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public Noodles(string name, double price)
        {
            this.Name = name;
            this.Price = price;
        }


        #region IComparable Members

        public int CompareTo(object obj)
        {
            if (!(obj is Noodles))
                throw new ArgumentException("Object not noodles!");
            return CompareTo((Noodles)obj);
        }

        #endregion

        #region IComparable<Noodles> Members

        public int CompareTo(Noodles other)
        {
            if (this.Name == other.Name)
                return this.Price.CompareTo(other.Price);
            else
                return this.Name.CompareTo(other.Name);
        }

        #endregion

        #region IEquatable<Noodles> Members

        public bool Equals(Noodles other)
        {
            return (this.Name == other.Name) && (this.Price == other.Price);
        }

        #endregion

        public override string ToString()
        {
            return String.Format("Name: {0}, Price: {1}", this.Name, this.Price);
        }
    }
}
