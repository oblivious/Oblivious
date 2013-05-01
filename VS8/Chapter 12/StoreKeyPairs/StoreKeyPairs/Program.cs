using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace StoreKeyPairs
{
    class Program
    {
        static void Main(string[] args)
        {
            CspParameters persistentCsp = new CspParameters();
            persistentCsp.KeyContainerName = "AsymmetricExample";
            RSACryptoServiceProvider myRSA = new RSACryptoServiceProvider(512, persistentCsp);
            myRSA.PersistKeyInCsp = true;
            RSAParameters privateKey = myRSA.ExportParameters(true);
            int i = 0;
            foreach (byte thisByte in privateKey.D)
            {
                Console.Write(thisByte.ToString("X2") + " ");
                i++;
            }
            Console.WriteLine("\nKey contains {0} bytes.", i);

            string messageString = "Hello, World!";
            byte[] messageBytes = Encoding.Unicode.GetBytes(messageString);
            byte[] encryptedMessage = myRSA.Encrypt(messageBytes, false);
            byte[] decryptedBytes = myRSA.Decrypt(encryptedMessage, false);
            Console.WriteLine(Encoding.Unicode.GetString(decryptedBytes));
        }
    }
}
