using System;
using System.Text;

namespace CodeProjectReflection
{
    // With Reflection, we can dynamically create an instance of a type, bind the type to an existing object,
    // or get the type from an existing object and invoke its methods or access its fields and properties. 
    // We can also access attribute information using Reflection.
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowWidth = 140;
            Console.WindowHeight = 40;
            Console.BufferHeight = 5000;

            /*
            Console.WriteLine("Stage 1: Retrieving Type information\n");
            var c = new Car();
            var t = c.GetType();
            Console.WriteLine("Retrieved type information via System.Object.GetType():");
            Console.WriteLine(t.FullName);
            Console.ReadKey();

            var s = Type.GetType("CodeProjectReflection.Car", false, true);
            Console.WriteLine("Retrieved type information via Type.GetType():");
            Console.WriteLine(s.FullName);
            Console.ReadKey();

            var r = typeof(Car);
            Console.WriteLine("Retrieved type information via typeof(<type name>):");
            Console.WriteLine(r.FullName);
            Console.ReadKey();

            Console.Clear();
            Console.WriteLine("Stage 2: Displaying Type information using the properties of System.Type\n");
            GetTypeProperties(typeof(Car));
            Console.ReadKey();

            Console.Clear();
            Console.WriteLine("Stage 3: Wander off and look at some MSDN example...");
            MemberInfoExample.Run();
            Console.ReadKey();

            Console.Clear();
            Console.WriteLine("Stage 4: Listing methods");
            GetMethodsDemo.Run();
            Console.ReadKey();

            Console.Clear();
            Console.WriteLine("Stage 5: Listing fields and properties");
            GetFieldsAndPropertiesDemo.Run();
            Console.ReadKey();

            Console.Clear();
            Console.WriteLine("Stage 6: Listing interfaces");
            GetInterfacesDemo.Run();
            Console.ReadKey();

            Console.Clear();
            Console.WriteLine("Stage 7: Listing methods and their parameters");
            GetParameterInfoDemo.Run();
            Console.ReadKey();

            Console.Clear();
            Console.WriteLine("Stage 8: Listing constructors");
            GetConstructorsDemo.Run();
            Console.ReadKey();
             
            Console.Clear();
            Console.WriteLine("Stage 9: Assembly Demo");
            AssemblyDemo.Run();
            Console.ReadKey();
            */

            Console.Clear();
            Console.WriteLine("Stage 10: Late Binding Demo");
            LateBindingDemo.Run();
            Console.ReadKey();
        }

        private static void GetTypeProperties(Type t)
        {
            var sb = new StringBuilder();

            // Properties retrieve the strings
            sb.AppendLine("Analysis of type " + t.Name);
            sb.AppendLine("Type Name: " + t.Name);
            sb.AppendLine("Full Name: " + t.FullName);
            sb.AppendLine("Namespace: " + t.Namespace);

            // Properties retrieve references
            var tBase = t.BaseType;

            sb.AppendLine("Base Type: " + (tBase != null ? tBase.Name : "(none)"));

            var tUnderlyingSystem = t.UnderlyingSystemType;

            sb.AppendLine("Underlying System Type: " + tUnderlyingSystem.Name);

            sb.AppendLine("Is Abstract Class: " + t.IsAbstract);
            sb.AppendLine("Is an Array: " + t.IsArray);
            sb.AppendLine("Is a Class: " + t.IsClass);
            sb.AppendLine("Is a COM object: " + t.IsCOMObject);

            sb.AppendLine("\nPublic Members:");
            var members = t.GetMembers();

            foreach (var nextMember in members)
            {
                sb.AppendLine(nextMember.DeclaringType + " " + nextMember.MemberType + " " + nextMember.Name);
            }
            Console.WriteLine(sb);
        }
    }
}
