using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericsVCasting
{
    class Program
    {
        static void Main(string[] args)
        {
            int sum = 0;
            DateTime start, end;
            start = DateTime.Now;
            List<int> numbers = new List<int>();
            for (int i = 0; i < 1000000; i++)
            {
                numbers.Add(i);
            }
            for (int i = 0; i < 1000000; i++)
            {
                sum += numbers[i];
            }
            Console.WriteLine("Sum is: " + sum);
            end = DateTime.Now;
            Console.WriteLine("Generics: " + (end - start).TotalMilliseconds + "ms");
            sum = 0;
            start = DateTime.Now;
            object[] objects = new object[1000000];
            for (int i = 0; i < 1000000; i++)
            {
                objects[i] = i;
            }
            for (int i = 0; i < 1000000; i++)
            {
                sum += (int)objects[i];
            }
            end = DateTime.Now;
            Console.WriteLine("Sum is: " + sum);
            Console.WriteLine("Objects: " + (end - start).TotalMilliseconds + "ms");

            Obj oa = new Obj("Hello, ", "World!");
            Console.WriteLine((string)oa.t + (string)oa.u);

            Gen<string, string> ga = new Gen<string, string>("Hello, ", "World!");
            Console.WriteLine(ga.t + ga.u);

            Obj ob = new Obj(10.125, 2005);
            Console.WriteLine((double)ob.t + (int)ob.u);

            Gen<double, int> gb = new Gen<double, int>(10.125, 2005);
            Console.WriteLine(gb.t + gb.u);

            sum = 0;
            start = DateTime.Now;
            Obj oc = new Obj(1, 2);
            for (int i = 0; i < 10000000; i++)
            {
                sum += (int)oc.t;
                sum += (int)oc.u;
            }
            Console.WriteLine("Sum is: " + sum);
            end = DateTime.Now;
            Console.WriteLine("Objects: " + (end - start).TotalMilliseconds + "ms");
            sum = 0;
            start = DateTime.Now;
            Gen<int, int> gc = new Gen<int, int>(1, 2);
            for (int i = 0; i < 10000000; i++)
            {
                sum += gc.t;
                sum += gc.u;
            }
            Console.WriteLine("Sum is: " + sum);
            end = DateTime.Now;
            Console.WriteLine("Generics: " + (end - start).TotalMilliseconds + "ms");
        }
    }

    class Obj
    {
        public Object t;
        public Object u;

        public Obj(Object t, Object u)
        {
            this.t = t;
            this.u = u;
        }
    }

    class Gen<T, U>
    {
        public T t;
        public U u;

        public Gen(T t, U u)
        {
            this.t = t;
            this.u = u;
        }
    }
}
