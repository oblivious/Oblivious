using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace CspParametersClass
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Store Key Example
            //----------------------------------------------------------------------------------------------------------
            // Creates the CspParameters object and sets the key container name used to store the RSA key pair
            CspParameters cp = new CspParameters();
            cp.KeyContainerName = "DonalKeyContainerName";

            // Instantiates the Rsa instance accessing the key container DonalKeyContainerName
            RSACryptoServiceProvider rsa1 = new RSACryptoServiceProvider(cp);
            // add the below line to delete the key entry in DonalKeyContainerName
            rsa1.PersistKeyInCsp = false;

            // writes out the current key pair used in the rsa instance
            Console.WriteLine("Key is: \n" + rsa1.ToXmlString(true) + "\n\n\n");
            //----------------------------------------------------------------------------------------------------------


            // Smart Card Sign Example
            //----------------------------------------------------------------------------------------------------------
            // To identify the Smart Card Cryptographic Providers on your computer, use the Microsoft Registry Editor (Regedit.exe).
            // The available Smart Card Cryptographic Providers are listed in HKEY_LOCAL_MACHINE\Software\Microsoft\Cryptography\Defaults\Provider.

            // Create a new CspParameters object that identifies a Smart Card Cryptographic Provider.
            // The 1st parameter comes from HKEY_LOCAL_MACHINE\Software\Microsoft\Cryptography\Defaults\Provider Types.
            // The 2nd parameter comes from HKEY_LOCAL_MACHINE\Software\Microsoft\Cryptography\Defaults\Provider.
            CspParameters csp = new CspParameters(24, "Microsoft Enhanced RSA and AES Cryptographic Provider");
            csp.Flags = CspProviderFlags.UseDefaultKeyContainer;

            // Initialize an RSACryptoServiceProvider object using the CspParameters object.
            RSACryptoServiceProvider rsa2 = new RSACryptoServiceProvider(csp);

            // Create some data to sign.
            byte[] data = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7 };

            Console.WriteLine("Data                  : " + BitConverter.ToString(data));

            // Sign the data using the Smart Card Cryptographics Provider.
            byte[] sig = rsa2.SignData(data, "SHA1");

            Console.WriteLine("Signature             : " + BitConverter.ToString(sig));

            // Verify the data using the Smart Card Cryptographic Provider.
            bool verified = rsa2.VerifyData(data, "SHA1", sig);

            Console.WriteLine("Verified              : " + verified);
            //----------------------------------------------------------------------------------------------------------
        }
    }
}
