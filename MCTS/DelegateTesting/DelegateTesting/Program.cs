using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelegateTesting
{
    class Program
    {
        public delegate void Del(string message);

        static void Main(string[] args)
        {
            Del handler = DelegateMethod;

            handler("Hello World");

            MethodWithCallback(1, 2, handler);

            InstancedClass instance = new InstancedClass();

            Del handler1 = instance.Method1;
            Del handler2 = instance.Method2;

            handler1("noodles");
            handler2("cheese");

            Del handler3 = handler1 + handler2;

            handler3("bacon");

            Console.WriteLine("Number of methods on invocation list: " + handler3.GetInvocationList().GetLength(0));

            Console.ReadKey();
        }

        public static void DelegateMethod(string message)
        {
            Console.WriteLine(message);
        }

        public static void MethodWithCallback(int param1, int param2, Del callback)
        {
            callback("The number is: " + (param1 + param2).ToString());
        }
    }

    class InstancedClass
    {
        public void Method1(string message)
        {
            Console.WriteLine("Method1 says: " + message);
        }

        public void Method2(string message)
        {
            Console.WriteLine("Method2 says: " + message);
        }
    }
}
