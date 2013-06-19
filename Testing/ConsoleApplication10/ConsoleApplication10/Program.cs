using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Resources;
using System.Reflection;

namespace ConsoleApplication10
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var j = LanguageConfig.Languages.Keys;
                List<string> k = j.ToList();
                foreach (var i in j)
                {
                    Console.WriteLine(i + ", " + LanguageConfig.Languages[i]);
                }
                foreach (var l in k)
                {
                    Console.WriteLine(l);
                }
                Console.WriteLine(ConfigurationManager.AppSettings["defaultTheme"]);
                Console.ReadLine();
                ConfigurationManager.RefreshSection("appSettings");

                ResourceManager rm = new ResourceManager("ConsoleApplication10.Resources", Assembly.GetExecutingAssembly());
                System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("es");
                Console.WriteLine(rm.GetString("name", ci));
                Console.WriteLine(rm.GetString("name", new System.Globalization.CultureInfo("en")));
                Console.WriteLine(rm.GetString("name"));
                Console.ReadLine();
            }
        }
    }
}
