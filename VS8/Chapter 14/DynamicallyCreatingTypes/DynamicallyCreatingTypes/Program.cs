using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace DynamicallyCreatingTypes
{
    class Program
    {
        static void Main(string[] args)
        {
            AssemblyBuilder ab = AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName("dynAssembly"), AssemblyBuilderAccess.RunAndSave);

            ModuleBuilder mb = ab.DefineDynamicModule("dynMod");

            TypeBuilder tb = mb.DefineType("dynType", TypeAttributes.Class | TypeAttributes.Public);

            ConstructorBuilder cb = tb.DefineDefaultConstructor(MethodAttributes.Public);
                
            MethodBuilder method = tb.DefineMethod("Greet", MethodAttributes.Public | MethodAttributes.Static);

            ILGenerator dynCode = method.GetILGenerator();

            dynCode.EmitWriteLine("Hello, world!");

            dynCode.Emit(OpCodes.Ret);

            Type myDynType = tb.CreateType();

            myDynType.GetMethod("Greet").Invoke(null, null);
        }
    }
}
