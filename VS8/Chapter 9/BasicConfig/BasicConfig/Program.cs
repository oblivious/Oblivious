using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace BasicConfig
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                Console.WriteLine("Working...");
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                AppSettingsSection ass = config.AppSettings;
                ass.Settings.Add("oklahoma", "hawaii");
                config.Save(ConfigurationSaveMode.Modified);
                Console.WriteLine("We're done here.");
                Console.WriteLine("Press any key to read...");
                Console.ReadKey();

                for (int i = 0; i < ConfigurationManager.AppSettings.Count; i++)
                {
                    Console.WriteLine("{0}: {1}", ConfigurationManager.AppSettings.AllKeys[i], ConfigurationManager.AppSettings[i]);
                }
                Console.WriteLine("Finished.");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.ToString());
            }
        }
    }
}
