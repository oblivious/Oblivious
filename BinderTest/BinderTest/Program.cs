using System;
using System.Reflection;
using System.Globalization;

namespace BinderTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get the type of MyClass1.
            Type myType = typeof(MyClass1);
            // Get the instance of MyClass1.
            MyClass1 myInstance = new MyClass1();
            Console.WriteLine("\nDisplaying the results of using the MyBinder binder.\n");
            // Get the method information for MyMethod.
            MethodInfo myMethod = myType.GetMethod("MyMethod", BindingFlags.Public | BindingFlags.Instance,
                new MyBinder(), new Type[] { typeof(short), typeof(short) }, null);
            Console.WriteLine(myMethod);
            // Invoke MyMethod.
            myMethod.Invoke(myInstance, BindingFlags.InvokeMethod, new MyBinder(), new object[] { (int)32, (int)66 }, CultureInfo.CurrentCulture);
        }
    }

    public class MyClass1
    {
        public short myFieldB;
        public int myFieldA;
        public void MyMethod(long i, char k)

        {
            Console.WriteLine("\nThis is MyMethod(long i, char k)");
            Console.WriteLine("Passed parameters were {0} and {1}", i, k);
        }
        public void MyMethod(long i, long j)
        {
            Console.WriteLine("\nThis is MyMethod(long i, long j)");
            Console.WriteLine("Passed parameters were {0} and {1}", i, j);
        }
    }
}
