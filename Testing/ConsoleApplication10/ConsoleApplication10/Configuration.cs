using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ConsoleApplication10
{
    #region Language Configuration
    public class LanguageElement : ConfigurationElement
    {
        public LanguageElement()
        { }

        public LanguageElement(string name, string value)
        {
            Name = name;
            Value = value;
        }

        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("value", IsRequired = true)]
        public string Value
        {
            get { return (string)this["value"]; }
            set { this["value"] = value; }
        }
    }

    public class LanguageCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new LanguageElement();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((LanguageElement)element).Name;
        }
    }

    public class LanguageConfigSection : ConfigurationSection
    {
        public LanguageConfigSection()
        { }

        [ConfigurationProperty("", IsRequired = true, IsDefaultCollection = true)]
        public LanguageCollection Languages
        {
            get { return ((LanguageCollection)(this[""])); }
            set { this[""] = value; }
        }
    }

    internal class LanguageConfig
    {
        internal static Dictionary<string, string> Languages;
        static LanguageConfig()
        {
            Languages = new Dictionary<string, string>();
            LanguageConfigSection section = (LanguageConfigSection)ConfigurationManager.GetSection("languages");
            foreach (LanguageElement i in section.Languages)
            {
                Languages.Add(i.Name, i.Value);
            }
        }
    }
    #endregion Language Configuration


    #region Process Configuration
    public class ProcessConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("", IsRequired = true, IsDefaultCollection = true)]
        public ProcessCollection Processes
        {
            get { return ((ProcessCollection)(this[""])); }
            set { this[""] = value; }
        }
    }

    public class ProcessCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ProcessElement();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ProcessElement)element).Name;
        }
    }

    public class ProcessElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string)base["name"]; }
            set { base["name"] = value; }
        }
    }

    internal class ProcessesConfig
    {
        internal static List<string> Processes;
        static ProcessesConfig()
        {
            Processes = new List<string>();
            ProcessConfigSection section = (ProcessConfigSection)ConfigurationManager.GetSection("Processes");
            foreach (ProcessElement i in section.Processes)
            {
                Processes.Add(i.Name);
            }
        }
    }
    #endregion Process Configuration
}