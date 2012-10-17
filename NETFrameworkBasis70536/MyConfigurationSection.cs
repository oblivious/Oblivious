using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace fabianse70536
{
    class MyConfigurationSection : ConfigurationSection 
    {

        [ConfigurationProperty("Name", IsRequired=false, DefaultValue="unknown name")]
        public String Name 
        {
            get { return (String)base["Name"]; }
            set { base["Name"] = value; }
        }

        [ConfigurationProperty("Code", IsRequired = false, DefaultValue = 0)]
        public int Code
        {
            get { return (int)base["Code"]; }
            set { base["Code"] = value; }
        }


    }
}
