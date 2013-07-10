using System;
using System.Collections;
using System.Management;
using System.Collections.Generic;
using System.Text;

namespace baileySoft.Wmi.Registry
{
    class RegistryLocal:RegistryObject
    {
        #region "fields"
        ConnectionOptions options;
        ManagementScope connectionScope;
        #endregion

        #region "constructors"
        public RegistryLocal()
        {
            options = RegistryConnection.RegistryConnectionOptions();
            Connect();
            GetRegistryProperties();
        }
        #endregion

        #region "polymorphic methods"
        public override void CreateKey(baseKey RootKey, string key)
        {
            if (IsConnected)
                try
                {
                    RegistryMethod.CreateKey(connectionScope, RootKey, key);
                }
                catch (ManagementException err)
                {
                    Console.WriteLine("An error occurred: " + err.Message);
                }
        }
        public override void DeleteKey(baseKey RootKey, string key)
        {
            if (IsConnected)
                try
                {
                    RegistryMethod.DeleteKey(connectionScope, RootKey, key);
                }
                catch (ManagementException err)
                {
                    Console.WriteLine("An error occurred: " + err.Message);
                }
        }
        public override ArrayList EnumerateKeys(baseKey RootKey, string key)
        {
            ArrayList subKeys = new ArrayList();
            if (IsConnected)
                try
                {
                    subKeys.AddRange(RegistryMethod.EnumerateKeys(connectionScope, RootKey, key));
                }
                catch (ManagementException err)
                {
                    Console.WriteLine("An error occurred: " + err.Message);
                }
            return subKeys;
        }
        public override ArrayList EnumerateValues(baseKey RootKey, string key)
        {
            ArrayList alValues = new ArrayList();
            if (IsConnected)
                try
                {
                    alValues.AddRange(RegistryMethod.EnumerateValues(connectionScope, RootKey, key));
                    alValues.Sort();
                }
                catch (ManagementException err)
                {
                    Console.WriteLine("An error occurred: " + err.Message);
                }
            return alValues;
        }
        public override string GetValue(baseKey RootKey, string key, string valueName, valueType ValueType)
        {
            string Value = string.Empty;
            if (IsConnected)
                try
                {
                    Value = RegistryMethod.GetValue(connectionScope, RootKey, key, valueName, ValueType);
                }
                catch (ManagementException err)
                {
                    Console.WriteLine("An error occurred: " + err.Message);
                }
            return Value;
        }
        public override void SetValue(baseKey RootKey, string key, string valueName, string value, valueType ValueType)
        {
            if (IsConnected)
                try
                {
                    RegistryMethod.SetValue(connectionScope, RootKey, key, valueName, value, ValueType);
                }
                catch (ManagementException err)
                {
                    Console.WriteLine("An error occurred: " + err.Message);
                }
        }
        public override void CreateValue(baseKey RootKey, string key, string valueName, string value)
        {
            if (IsConnected)
                try
                {
                    RegistryMethod.CreateValue(connectionScope, RootKey, key, valueName, value);
                }
                catch (ManagementException err)
                {
                    Console.WriteLine("An error occurred: " + err.Message);
                }
        }
        public override void DeleteValue(baseKey RootKey, string key, string valueName)
        {
            if (IsConnected)
                try
                {
                    RegistryMethod.DeleteValue(connectionScope, RootKey, key, valueName);
                }
                catch (ManagementException err)
                {
                    Console.WriteLine("An error occurred: " + err.Message);
                }
        }
        public override void GetRegistryProperties()
        {
            if (IsConnected)
            {
                RegistryConnection.GetRegistryProperties(Environment.MachineName,
                                                                    options, this);
            }
        }
        public override bool Connect()
        {
            connectionScope = RegistryConnection.ConnectionScope(Environment.MachineName,
                                                                    options, this);
            return this.IsConnected;
        }
        #endregion
    
    }
}
