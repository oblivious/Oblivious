using System;
using System.Configuration;

namespace ConfigurationManagerExample
{
    public sealed class CustomSection : ConfigurationSection
    {
        [ConfigurationProperty("name", DefaultValue = "Contoso", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string) this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("url", DefaultValue = "http://www.contoso.com", IsRequired = true)]
        [RegexStringValidator(@"\w+:\/\/[\w.]+\S*")]
        public string Url
        {
            get { return (string) this["url"]; }
            set { this["url"] = value; }
        }

        [ConfigurationProperty("port", DefaultValue = 8080, IsRequired = false)]
        [IntegerValidator(MinValue = 0, MaxValue = UInt16.MaxValue, ExcludeRange = false)]
        public int Port { get { return (int)this["port"]; } set { this["port"] = value; } }
    }
}
