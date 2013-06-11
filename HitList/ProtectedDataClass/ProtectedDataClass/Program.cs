using System;
using System.Text;
using System.Security.Cryptography;

namespace ProtectedDataClass
{
    public class Program
    {
        // Create a byte array for additional entropy when using the Protect method.
        private static byte[] additionalEntropyBytes = { 9, 8, 7, 6, 5 };

        public static void Main(string[] args)
        {
            // Create a simple byte array containing data to be encrypted.
            byte[] secret = Encoding.UTF8.GetBytes("Bite my shiny metal ass!<======8");
            Console.WriteLine("This is the original value of the secret:");
            PrintValues(secret, "secret");

            DoProtectedData(secret);

            DoProtectedMemory(secret);

            Console.WriteLine("That's all folks!");
        }

        private static void DoProtectedData(byte[] secret)
        {
            Console.WriteLine("ProtectedData\n-------------------------------------------------------------------------");

            // Encrypt the data.
            byte[] encryptedSecret = Protect(secret);
            Console.WriteLine("The encrypted byte array is: ");
            PrintValues(encryptedSecret, "encryptedSecret");
            Console.WriteLine("Encrypted Data Length: " + encryptedSecret.Length);

            // Decrypt the data and store in a byte array.
            byte[] originalData = Unprotect(encryptedSecret);
            Console.WriteLine("{0}The original data is:", Environment.NewLine);
            PrintValues(originalData, "originalData");
        }

        private static void DoProtectedMemory(byte[] secret)
        {
            Console.WriteLine("ProtectedMemory\n-------------------------------------------------------------------------");
            Console.WriteLine("Note that \"ProtectedMemory\" requires array lengths to be multiples of 16.");
            PrintValues(secret, "secret");
            ProtectedMemory.Protect(secret, MemoryProtectionScope.SameProcess);
            PrintValues(secret, "secret");
            ProtectedMemory.Unprotect(secret, MemoryProtectionScope.SameProcess);
            PrintValues(secret, "secret");
        }

        private static byte[] Protect(byte[] data)
        {
            try
            {
                // Encrypt the data using DataProtectionScope.CurrentUser. The result can be decrypted
                // only by the same current user.
                return ProtectedData.Protect(data, additionalEntropyBytes, DataProtectionScope.CurrentUser);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("Data was not encrypted. An error occurred.");
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        private static byte[] Unprotect(byte[] data)
        {
            try
            {
                // Decrypt the data using DataProtectionScope.CurrentUser.
                return ProtectedData.Unprotect(data, additionalEntropyBytes, DataProtectionScope.CurrentUser);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("Data was not decrypted. An error occurred.");
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        private static void PrintValues(byte[] array, string name)
        {
            Console.WriteLine("Array \"{0}\" value: \"{1}\"\n", name, Encoding.UTF8.GetString(array));
        }
    }
}
