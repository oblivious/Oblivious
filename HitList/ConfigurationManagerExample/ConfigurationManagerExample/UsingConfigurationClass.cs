using System;
using System.Configuration;

namespace ConfigurationManagerExample
{
    class UsingConfigurationClass
    {
        // Show how to create an instance of the Configuration class that represents this application configuration file.
        internal static void CreateConfigurationFile()
        {
            try
            {
                // Create a custom configuration section.
                var customSection = new CustomSection();

                // Get the current configuration file.
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                // Create the custom section entry in <configSections> group and the related target section in <configuration>.
                if (config.Sections["CustomSection"] == null)
                    config.Sections.Add("CustomSection", customSection);

                // Create and add an entry to appSettings section.

                const string connStringName = "LocalSqlServerExpress";
                const string connString = @"data source=.\SQLEXPRESS:Integrated Security=SSPI;AttachDBFilename=|DataDirectory|aspnetdb.mdf;User Instance=true";
                const string providerName = "System.Data.SqlClient";

                var connStrSettings = new ConnectionStringSettings
                {
                    Name = connStringName,
                    ConnectionString = connString,
                    ProviderName = providerName
                };

                config.ConnectionStrings.ConnectionStrings.Add(connStrSettings);

                // Add an entry to appSettings section.
                var appStgCnt = ConfigurationManager.AppSettings.Count;
                var newKey = "NewKey" + appStgCnt;

                var newValue = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();

                config.AppSettings.Settings.Add(newKey, newValue);

                // Save the configuration file.
                customSection.SectionInformation.ForceSave = true;
                config.Save(ConfigurationSaveMode.Full);

                Console.WriteLine("Created configuration file: {0}", config.FilePath);
            }
            catch (ConfigurationErrorsException e)
            {
                Console.WriteLine("CreateConfigruationFile: {0}", e);
            }
        }

        internal static void GetCustomSection()
        {
            try
            {
                // Get the current configuration fiel.
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var customSection = (CustomSection) config.GetSection("CustomSection");

                Console.WriteLine("Section name: {0}", customSection.Name);
                Console.WriteLine("Url: {0}", customSection.Url);
                Console.WriteLine("Port: {0}", customSection.Port);
            }
            catch (ConfigurationErrorsException cee)
            {
                Console.WriteLine("using GetSection(string): {0}", cee);
            }
        }

        internal static void SaveConfigurationFile()
        {
            try
            {
                // Get the current configuration file.
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                // Save the full configuration file and force save even if th efile was not modified.
                config.SaveAs("MyConfigFull.config", ConfigurationSaveMode.Full, true);

                Console.WriteLine("Saved config file as 'MyConfigFull.config' using the mode: {0}", ConfigurationSaveMode.Full);

                config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                // Save on the part of the configuration file that was modified.
                config.SaveAs("MyConfigModified.config", ConfigurationSaveMode.Modified, true);
                Console.WriteLine("Saved config gile as 'MyConfigModified.config' using the mode: {0}", ConfigurationSaveMode.Modified);

                config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                // Save the full configuration file.
                config.SaveAs("MyConfigMinimal.config");
                Console.WriteLine("Saved config file as MyConfigMinimal.config using the mode: {0}", ConfigurationSaveMode.Minimal);
            }
            catch (ConfigurationErrorsException cee)
            {
                Console.WriteLine("SaveConfigurationFile: {0}", cee);
            }
        }

        internal static void GetSections(string section)
        {
            try
            {
                // Get the current configuration file.
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                switch (section)
                {
                    case "appSettings":
                        try
                        {
                            var appSettings = config.AppSettings;
                            Console.WriteLine("Section name: {0}", appSettings.SectionInformation.SectionName);

                            // Get the AppSettings section elements.
                            Console.WriteLine();
                            Console.WriteLine("Using AppSettings property.");
                            Console.WriteLine("Application settings:");

                            // Get the KeyValueConfigurationCollection from the configuration.
                            var settings = config.AppSettings.Settings;

                            // Display each KeyValueConfigurationElement.
                            foreach (KeyValueConfigurationElement keyValueElement in settings)
                            {
                                Console.WriteLine("Key: {0}", keyValueElement.Key);
                                Console.WriteLine("Value: {0}", keyValueElement.Value);
                                Console.WriteLine();
                            }
                        }
                        catch (ConfigurationErrorsException cee)
                        {
                            Console.WriteLine("Using AppSettings property: {0}", cee);
                        }
                        break;
                    case "connectionStrings":
                        try
                        {
                            var connectionStringSection = config.ConnectionStrings;
                            Console.WriteLine("Section name: {0}", connectionStringSection.SectionInformation.SectionName);

                            if (connectionStringSection.ConnectionStrings.Count != 0)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Using ConnectionStrings property.");
                                Console.WriteLine("Connection strings:");

                                // Get the collection elements.
                                foreach (ConnectionStringSettings connection in connectionStringSection.ConnectionStrings)
                                {
                                    Console.WriteLine("Name: {0}", connection.Name);
                                    Console.WriteLine("Connection string: {0}", connection.ConnectionString);
                                    Console.WriteLine("Provider {0}", connection.ProviderName);
                                }
                            }
                        }
                        catch (ConfigurationErrorsException cee)
                        {
                            Console.WriteLine("Using ConnectionStrings property: {0}", cee);
                        }
                        break;
                    default:
                        Console.WriteLine("GetSections: Unknown section {0}", section);
                        break;
                }
            }
            catch (ConfigurationErrorsException cee)
            {
                Console.WriteLine("GetSections: {0}", cee);
            }
        }

        // Show how to use the Configuration object properties to obtain configuration file information.
        public static void GetConfigurationInformation()
        {
            try
            {
                // Get the current configuration file.
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                Console.WriteLine("Reading configuration information:");

                Console.WriteLine("Machine level: {0}", config.EvaluationContext.IsMachineLevel);
                Console.WriteLine("File path: {0}", config.FilePath);
                Console.WriteLine("Has file: {0}", config.HasFile);

                var groups = config.SectionGroups;
                Console.WriteLine("Groups: {0}", groups.Count);
                foreach (ConfigurationSectionGroup group in groups)
                {
                    Console.WriteLine("Group Name: {0}", group.Name);
                    Console.WriteLine("Group Type: {0}", group.Type);
                }

                var sections = config.Sections;
                Console.WriteLine("Sections: {0}", sections.Count);

                foreach (ConfigurationSection section in sections)
                {
                    Console.WriteLine("Section Name: {0}", section.SectionInformation.SectionName);
                    Console.WriteLine("Section Type: {0}", section.SectionInformation.Type);
                }
            }
            catch (ConfigurationErrorsException cee)
            {
                Console.WriteLine("GetConfigurationInformation: {0}", cee);
            }
        }
    }
}
