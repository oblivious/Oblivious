using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace LoadAndRunAddonsDynamicallyLab
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (string f in Directory.GetFiles(Environment.CurrentDirectory, "Addon-*.dll"))
            {
                Assembly asm = Assembly.LoadFile(f);
                object[] descs = asm.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                AssemblyDescriptionAttribute desc = (AssemblyDescriptionAttribute)descs[0];
                Console.WriteLine(desc.Description);

                foreach (Type t in asm.GetTypes())
                {
                    Console.WriteLine("Type Name: " + t.Name);
                    if (t.Name.EndsWith("Greeter"))
                    {
                        MethodInfo greet = t.GetMethod("Greet");
                        greet.Invoke(null, null);
                        break;
                    }
                }
            }
        }
    }
}
