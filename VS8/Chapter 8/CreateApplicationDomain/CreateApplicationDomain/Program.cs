using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CreateApplicationDomain
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain ad = AppDomain.CurrentDomain;
            Console.WriteLine(ad.FriendlyName);

            AppDomain adn = AppDomain.CreateDomain("NewDomain");
            Console.WriteLine(adn.FriendlyName);

            adn.ExecuteAssembly("Blah.exe");
            adn.ExecuteAssemblyByName("Blah");

            AppDomain.Unload(adn);

            Console.WriteLine("Base Directory: " + ad.BaseDirectory);
            Console.WriteLine("Dynamic Directory: " + ad.DynamicDirectory);
            Console.WriteLine("Relative Search Path: " + ad.RelativeSearchPath);
            Console.WriteLine("Shadow Copy Files: " + ad.ShadowCopyFiles);
        }
    }
}
