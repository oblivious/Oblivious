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

            Console.WriteLine(x + y);
            short z = x;
            Console.WriteLine(z);
            double c = x;
            Console.WriteLine(c);
        }
    }

    class MyClass
    {
        private int value { get; set; }

        public int Value
        {
            get { return this.value;}
            set { this.value = value; }
        }

        public MyClass(int _value)
        {
            this.value = _value;
        }

        public static MyClass operator +(MyClass a, MyClass b)
        {
            return new MyClass(a.Value + b.Value);
        }

        public static explicit operator double(MyClass a)
        {
            return a.Value;
        }

        public static explicit operator MyClass(double d)
        {
            return new MyClass((int)d);
        }

        public static implicit operator short(MyClass a)
        {
            return ((short)a.Value);
        }

        public override string ToString()
        {
            return "Value: " + this.Value;
        }
    }
}
