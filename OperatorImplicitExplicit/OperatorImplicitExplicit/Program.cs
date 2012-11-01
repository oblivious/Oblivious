using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OperatorImplicitExplicit
{
    class Program
    {
        static void Main(string[] args)
        {
            MyClass x = new MyClass(2);
            MyClass y = new MyClass(-3);

            //Testing the operator keyword
            MyClass z = x + y;

            Console.WriteLine(z.Value);

            z = 2 + x;

            Console.WriteLine(z.Value);

            z = x - y;

            Console.WriteLine(z.Value);

            //Testing the implicit keyword
            double d = x;

            Console.WriteLine(d.ToString("0.00"));

            //Testing the explicit keyword
            short s = (short)x;

            Console.WriteLine(s);
        }
    }

    class MyClass
    {
        private int value;

        public int Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public MyClass(int val)
        {
            this.value = val;
        }
        
        public static MyClass operator +(MyClass a, MyClass b)
        {
            return new MyClass(a.Value + b.Value);
        }
        /* //Testing showed that even though this method has a different signature to the above method
         * // it won't work because the operation doesn't take into account the following assignment.
         * // in effect the "int" is irrelevant, you can't have more than one "operator +(MyClass, MyClass)"
        public static int operator +(MyClass a, MyClass b)
        {
            return a.Value + b.Value;
        }
        */

        // There are two similar instances of this operation because the order of the parameters
        // is important.
        public static MyClass operator +(MyClass a, int b)
        {
            return new MyClass(a.Value + b);
        }
        public static MyClass operator +(int a, MyClass b)
        {
            return new MyClass(a + b.Value);
        }

        public static MyClass operator -(MyClass a, MyClass b)
        {
            return new MyClass(a.Value - b.Value);
        }

        public static implicit operator double(MyClass a)
        {
            return (double)a.Value;
        }

        public static explicit operator short(MyClass a)
        {
            return (short)a.Value;
        }
    }
}
