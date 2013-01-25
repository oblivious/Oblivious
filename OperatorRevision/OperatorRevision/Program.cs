using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OperatorRevision
{
    class Program
    {
        static void Main(string[] args)
        {
            MyClass x = new MyClass(1);
            MyClass y = new MyClass(2);

            MyClass o = x + y;

            Console.WriteLine(o.ToString());
            //short z = x;
            //Console.WriteLine(z);
            double c = (double)x;
            Console.WriteLine(c);
            byte b = (byte)x;
            Console.WriteLine(b);
        }
    }

    class MyClass
    {
        private int item { get; set; }

        public int Item
        {
            get { return this.item;}
            set { this.item = value; }
        }

        public MyClass(int _value)
        {
            this.item = _value;
        }

        public static MyClass operator +(MyClass a, MyClass b)
        {
            return new MyClass(a.Item + b.Item);
        }

        public static explicit operator double(MyClass a)
        {
            return a.Item;
        }

        public static explicit operator MyClass(double d)
        {
            return new MyClass((int)d);
        }
        
        public override string ToString()
        {
            return "Value: " + this.Item;
        }

        public static implicit operator short(MyClass a)
        {
            return ((short)a.Item);
        }
    }
}
