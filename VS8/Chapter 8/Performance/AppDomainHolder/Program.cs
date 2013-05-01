using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppDomainHolder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AppDomainHolder: Creating Domain.");
            AppDomain appDomain = AppDomain.CreateDomain("Temporary");

            Console.WriteLine("AppDomainHolder: Executing Assembly.");
            appDomain.ExecuteAssembly("Performance.exe");

            Console.WriteLine("AppDomainHolder: Finished, press key to unload AppDomain.");
            Console.ReadKey();
            AppDomain.Unload(appDomain);

            Console.WriteLine("AppDomainHolder: AppDomain unloaded.");
            GC.Collect();
            Console.ReadKey();
        }
    }
}
