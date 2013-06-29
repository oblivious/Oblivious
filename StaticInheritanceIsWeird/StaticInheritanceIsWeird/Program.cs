using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StaticInheritanceIsWeird
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(Fruit.Create<Apple>().Define());
            Console.WriteLine(Fruit.Create<Orange>().Define());
        }
    }

    public abstract class Fruit
    {
        public abstract string Define();

        public static T Create<T>() where T : Fruit, new()
        {
            return new T();
        }
    }

    public class Apple : Fruit
    {
        public override string Define()
        {
            return "Apple";
        }
    }

    public class Orange : Fruit
    {
        public override string Define()
        {
            return "Orange";
        }
    }
}
