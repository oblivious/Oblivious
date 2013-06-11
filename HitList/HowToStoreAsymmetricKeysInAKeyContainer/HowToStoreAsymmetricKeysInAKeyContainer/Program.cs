using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace HowToStoreAsymmetricKeysInAKeyContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string keyName = "MyKeyContainer";

                GenKey_SaveInContainer(keyName);

                GetKeyFromContainer(keyName);

                DeleteKeyFromContainer(keyName);

                GenKey_SaveInContainer(keyName);

                DeleteKeyFromContainer(keyName);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void GenKey_SaveInContainer(string ContainerName)
        {
            // Create the CspParameters object and set the key container name used to store the RSA key pair.
            CspParameters csp = new CspParameters();
            csp.KeyContainerName = ContainerName;

            // Create a new instance of RSACryptoServiceProvider that accesses the key container MyKeyContainerName.
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(csp);

            // Display the key information to the console.
            Console.WriteLine("Key added to container: \n {0}", rsa.ToXmlString(true));
        }

        public static void GetKeyFromContainer(string ContainerName)
        {
            // Create the CspParameters object and set the key container name used to store the RSA key pair.
            CspParameters csp = new CspParameters();
            csp.KeyContainerName = ContainerName;

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(csp);

            Console.WriteLine("Key retrieved from the container : \n {0}", rsa.ToXmlString(true));
        }

        public static void DeleteKeyFromContainer(string ContainerName)
        {
            CspParameters csp = new CspParameters();
            csp.KeyContainerName = ContainerName;

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(csp);

            rsa.PersistKeyInCsp = false;

            rsa.Clear();

            Console.WriteLine("Key deleted");
        }
    }
}
