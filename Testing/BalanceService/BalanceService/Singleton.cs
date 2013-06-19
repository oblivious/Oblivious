using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Reflection;
using Utility.LoggingService;

namespace BalanceService
{
    public sealed class Singleton
    {
        private static volatile Singleton instance;
        private static object syncRoot = new Object();
        public IDictionary<string, Type> GatewayTable = new Dictionary<string, Type>();
        public string LatestVersion;
        

        public Singleton() 
        {          
            ReadOperators();

            LatestVersion = "V" + DateTime.Now.ToString(Settings.VERSION_PATTERN);
        }

        public static Singleton Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Singleton();
                    }
                }

                return instance;
            }
        }

        public void Initialize()
        {
            lock (syncRoot)
            {
                instance = new Singleton();
            }
        }

        private void ReadOperators()
        {
            string fixedDll;
            
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "bin";
            
            string[] files = Directory.GetFiles(path, "*.dll");

            foreach (string dll in files)
            {
                fixedDll = dll.Substring(dll.LastIndexOf('\\') + 1, dll.Length - (dll.LastIndexOf('\\') + 1));

                if (fixedDll.ToUpper().StartsWith(Settings.GATEWAY_START_WITH.ToUpper()) & (!fixedDll.ToUpper().Contains(Settings.ISL) & !fixedDll.ToUpper().Contains(Settings.IMPL) & !fixedDll.ToUpper().Contains(Settings.TESTING)))
                {
                    string gatewayName = null;
                    Type gatewayType = null;
                    bool hasBalance = HasGetBalanceMethod(dll, out gatewayName, out gatewayType);

                    if(hasBalance) 
                    {
                        GatewayTable.Add(new KeyValuePair<string,Type>(gatewayName,gatewayType));
                    }                    
                }
            }
        }

        private bool HasGetBalanceMethod(string dll, out string name, out Type type)
        {
            string balanceMethod = null;
            name = string.Empty;

            Assembly library = Assembly.LoadFrom(dll);

            type = library.GetType(library.GetName().Name);
            
            //In case GateWay is with lower w
            if (type == null)
            {
                string tempName = library.GetName().Name;
                tempName = tempName.Replace("OperatorGateway", "OperatorGateWay");

                type = library.GetType(tempName);                
            }

            foreach (MethodInfo m in type.GetMethods())
            {
                if (m.Name.ToLower().StartsWith(Settings.BALANCE_METHOD.ToLower()))
                {
                    balanceMethod = m.Name;

                }
            }

            if (balanceMethod == null)
            {
                return false;
            }

            name = type.Name;

            return true;
        }
    
    }

}