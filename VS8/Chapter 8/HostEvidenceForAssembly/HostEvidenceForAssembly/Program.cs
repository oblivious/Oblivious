using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Policy;
using System.Security;

namespace HostEvidenceForAssembly
{
    class Program
    {
        static void Main(string[] args)
        {
            object[] hostEvidence = { new Zone(SecurityZone.MyComputer) };
            Evidence internetEvidence = new Evidence(hostEvidence, null);
            AppDomain myDomain = AppDomain.CreateDomain("MyDomain");
            myDomain.ExecuteAssembly("SecondAssembly.exe", internetEvidence);

            AppDomainSetup setup = new AppDomainSetup();
            Console.WriteLine(setup.ToString());
            Console.WriteLine(typeof(object));

            objectArrayTest(new object[] { "Hello", "World"});

            AppDomainSetup ads = new AppDomainSetup();
            ads.ApplicationBase = "file://" + Environment.CurrentDirectory;
            ads.DisallowBindingRedirects = false;
            ads.DisallowCodeDownload = true;
            ads.ConfigurationFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;

            AppDomain b = AppDomain.CreateDomain("New Domain", null, ads);

            ads = AppDomain.CurrentDomain.SetupInformation;
            Console.WriteLine("ApplicationBase: " + ads.ApplicationBase);
            Console.WriteLine("ApplicationName: " + ads.ApplicationName);
            Console.WriteLine("DisallowCodeDownload: " + ads.DisallowCodeDownload);
            Console.WriteLine("DisallowBindingRedirects: " + ads.DisallowBindingRedirects);
        }

        static void objectArrayTest(object input)
        {
            Console.WriteLine(input.ToString());
            object[] results = (object[])input;
            Console.WriteLine(results.ToString());
            foreach (object o in results)
            {
                Console.WriteLine((string)o);
            }
        }
    }
}
