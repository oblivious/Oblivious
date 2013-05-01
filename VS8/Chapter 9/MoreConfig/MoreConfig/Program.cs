using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml;

namespace MoreConfig
{
    
    public class MySettings
    {
        public string lastUser;
        public int lastNumber;

        public MySettings()
        {
        }
    }

    public class CustomConfigHandler : IConfigurationSectionHandler
    {
        public CustomConfigHandler()
        {
        }

        public object Create(object parent, object configContext, XmlNode section)
        {
            MySettings settings = new MySettings();
            settings.lastUser = section.SelectSingleNode("lastUser").InnerText;
            settings.lastNumber = Int32.Parse(section.SelectSingleNode("lastNumber").InnerText);
            return settings;
        }
    }     

    public class MyHandler : ConfigurationSection
    {
        public MyHandler()
        {
        }

        [ConfigurationProperty("lastUser", DefaultValue = "User", IsRequired = true)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\", MinLength = 1, MaxLength = 60)]
        public string LastUser
        {
            get { return (string)this["lastUser"]; }
            set { this["lastUser"] = value; }
        }

        [ConfigurationProperty("lastNumber")]
        public int LastNumber
        {
            get { return (int)this["lastNumber"]; }
            set { this["lastNumber"] = value; }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MySettings settings1 = (MySettings)ConfigurationManager.GetSection("customSettings1");
            Console.WriteLine(settings1.lastUser);
            Console.WriteLine(settings1.lastNumber);
            
            MyHandler settings2 = (MyHandler)ConfigurationManager.GetSection("customSettings2");
            Console.WriteLine(settings2.LastUser);
            Console.WriteLine(settings2.LastNumber);

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Remove("Title");
            config.AppSettings.Settings.Add("Title", "Noodles");
            config.AppSettings.Settings.Remove("Number");
            config.AppSettings.Settings.Add("Number", "12");
            config.Save(ConfigurationSaveMode.Modified);
        }
    }
}
