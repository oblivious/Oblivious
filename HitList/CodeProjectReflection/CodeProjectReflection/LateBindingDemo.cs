using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace CodeProjectReflection
{
    class LateBindingDemo
    {
        internal static void Run()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var classType = assembly.GetType("CodeProjectReflection.Car");

            var obj = Activator.CreateInstance(classType);

            var methodInfo = classType.GetMethod("IsMoving");

            var isCarMoving = (bool) methodInfo.Invoke(obj, null);

            Console.WriteLine("Car moving status is: {0}", isCarMoving ? "Moving" : "Not Moving");

            var parameters = new object[3];
            parameters[0] = 32456;
            parameters[1] = 32810;
            parameters[2] = 10.6;
            methodInfo = classType.GetMethod("CalculateKpl");
            var kpl = (double) methodInfo.Invoke(obj, parameters);
            Console.WriteLine("Kilometers per litre is: {0}", kpl);
            Console.ReadKey();

            Console.WriteLine("Now to try to create an object using a constructor...");
            Thread.Sleep(1000);

            var constructorInfo = classType.GetConstructor(new Type[0]);

            Console.WriteLine("Constructor Info: {0}", constructorInfo == null ? "null" : "not null");

            var newCar = (Car)constructorInfo.Invoke(null);

            Console.WriteLine("Constructor was invoked with no parameters...");

            Console.WriteLine("Shiny new car: ");
            Console.WriteLine("  Speed: {0}", newCar.Speed);
            Console.WriteLine("  Colour: {0}", newCar.Colour);
            Console.WriteLine("  IsMoving: {0}", newCar.IsMoving());

            Console.ReadKey();
        }
    }
}
