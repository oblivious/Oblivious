using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Text;

namespace ConfigurationManagerExample
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.WindowWidth = 150;
            Console.WindowHeight = 40;
            Console.Write("Roaming configuration file: ");
            GetRoamingConfigurationFile();
            Console.WriteLine();
            Console.Write("Configuration file: ");
            var path = GetConfigurationFile();
            Console.WriteLine();
            Console.WriteLine("Connection Strings:");
            //ConnectionStrings();
            using (var reader = File.OpenText(path))
            {
                Console.WriteLine(reader.ReadToEnd());
            }
            Console.WriteLine("Press any key to proceed to the menu...");
            Console.ReadKey();

            while (true)
            {
                UserMenu();
                Console.Write("> ");
                var selection = Console.ReadKey().KeyChar.ToString(CultureInfo.InvariantCulture);

                if (selection.ToLower().Equals("q"))
                    break;

                Console.Clear();

                switch (selection)
                {
                    case "1":
                        // Show how to create an instance of the Configuration class.
                        UsingConfigurationClass.CreateConfigurationFile();
                        break;
                    case "2":
                        // Show how to use GetSection(string) method.
                        UsingConfigurationClass.GetCustomSection();
                        break;
                    case "3":
                        // Show how to use ConnectionStrings.
                        UsingConfigurationClass.SaveConfigurationFile();
                        break;
                    case "4":
                        // Show how to use the AppSettings property.
                        UsingConfigurationClass.GetSections("appSettings");
                        break;
                    case "5":
                        // Show how to use the ConnectionStrings property.
                        UsingConfigurationClass.GetSections("connectionStrings");
                        break;
                    case "6":
                        // Show how to obtain configuraiton file information.
                        UsingConfigurationClass.GetConfigurationInformation();
                        break;
                    default:
                        Console.WriteLine("Invalid selection.");
                        break;
                }
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        private static string GetConfigurationFile()
        {
            string filePath = null;
            try
            {
                // Get the current application configuration file.
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                Console.WriteLine(config.FilePath);

                filePath = config.FilePath;
            }
            catch (Exception e)
            {
                Console.WriteLine("[Exception error: {0}]", e);
            }
            return filePath;
        }

        private static void GetRoamingConfigurationFile()
        {
            try
            {
                // Get the roaming configuration that applies to the current user.
                var roamingConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoaming);

                Console.WriteLine(roamingConfig.FilePath);
            }
            catch (Exception e)
            {
                Console.WriteLine("[Exception error: {0}]", e);
            }
        }

        // Create a connection string element and add it to the connection strings section.
        private static void ConnectionStrings()
        {
            // Get the application configuration file.
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // Get the current connection strings count.
            var connStrCnt = ConfigurationManager.ConnectionStrings.Count;

            // Create the connection string name.
            var csName = "ConnStr" + connStrCnt;

            // Create a connection string element and save it to the configuration file.

            // Create a connection string element.
            var csSettings = new ConnectionStringSettings(csName,
                "LocalSqlServer: data source=127.0.0.1;Integrated Security=SSPI;Initial Catalog=aspnetdb", "System.Data.SqlClient");

            // Get the connection strings section.
            var csSection = config.ConnectionStrings;

            // Add the new element.
            csSection.ConnectionStrings.Add(csSettings);

            // Save the configuration file.
            config.Save(ConfigurationSaveMode.Modified);
        }

        private static void UserMenu()
        {
            Console.Clear();

            var applicationName = Environment.GetCommandLineArgs()[0] + ".exe";

            var sb = new StringBuilder();

            sb.AppendLine("Application: " + applicationName);
            sb.AppendLine("Make your selection:\n");
            sb.AppendLine("?    -- Display help.");
            sb.AppendLine("q    -- Exit the application.\n");

            sb.AppendLine("1    -- Instantiate the Configuration class.");
            sb.AppendLine("2    -- Use GetSection(string) to read a custom section.");
            sb.AppendLine("3    -- Use SaveAs methods to save the configuration file.");
            sb.AppendLine("4    -- Use AppSettings property to read the appSettings section.");
            sb.AppendLine("5    -- Use ConnectionStrings property to read the connectionStrings section.");
            sb.AppendLine("6    -- Use Configuration class properties to obtain configuration information.");

            Console.Write(sb.ToString());
        }
    }
}
