using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericContraints
{
    class Program
    {
        static void Main(string[] args)
        {
            Generics<Bubbles, string> gen = new Generics<Bubbles, string>();
            Bubbles a = new Bubbles();
            a.Value = "very bubbly";
            string b = "bubbles";
            Console.WriteLine(gen.BubbleString(a, b));
            //Console.WriteLine(gen.BubbleString(b, a)); //Won't compile because generics are typesafe, aww.
        }
    }

    class Generics<T, U>
        where T : Bubbles, IComparable<T>, new()
    {
        public Generics()
        {
        }

        public string BubbleString(T t, U u)
        {
            if (t is Bubbles && u is string)
            {
                return (t as Bubbles).Value + " " + (u as string);
            }
            else
                return "nope";
        }
    }

    class Bubbles : IComparable<Bubbles>
    {
        private string value;

        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public Bubbles()
        {
            this.value = "bubbles";
        }

        #region IComparable<Bubbles> Members
        public int CompareTo(Bubbles other)
        {
            return this.Value.CompareTo(other.Value);
        }
        #endregion
    }
}
