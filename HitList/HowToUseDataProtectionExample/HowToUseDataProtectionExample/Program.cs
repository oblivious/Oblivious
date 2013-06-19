using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Diagnostics;

namespace HowToUseDataProtectionExample
{
    public class Program
    {
        static void Main(string[] args)
        {
            Run();
        }

        private static void Run()
        {
            try
            {
                // Memory Encryption - ProtectedMemory

                // Create the original data to be encrypted (The data length should be a multiple of 16).
                byte[] toEncrypt = UnicodeEncoding.ASCII.GetBytes("ThisIsSomeData16");

                Console.WriteLine("Original data: " + UnicodeEncoding.ASCII.GetString(toEncrypt));
                Console.WriteLine("Encrypting...");

                // Encrypt the data in memory.
                EncryptInMemoryData(toEncrypt, MemoryProtectionScope.SameLogon);

                Console.WriteLine("Encrypted data: " + UnicodeEncoding.ASCII.GetString(toEncrypt));
                Console.WriteLine("Decrypting...");

                // Decrypt the data in memory.
                DecryptInMemoryData(toEncrypt, MemoryProtectionScope.SameLogon);

                Console.WriteLine("DecryptedData: " + UnicodeEncoding.ASCII.GetString(toEncrypt));

                // Data Encryption - ProtectedData

                // Create the original data to be encrypted
                toEncrypt = UnicodeEncoding.ASCII.GetBytes("This is some data of any length.");

                // Create a file.
                FileStream fStream = new FileStream("Data.dat", FileMode.OpenOrCreate);

                // Create some random entropy.
                byte[] entropy = CreateRandomEntropy();

                Console.WriteLine();
                Console.WriteLine("Original data: " + UnicodeEncoding.ASCII.GetString(toEncrypt));
                Console.WriteLine("Encrypting and writing to disk...");

                // Encrypt a copy of the data to the stream.
                int bytesWritten = EncryptDataToStream(toEncrypt, entropy, DataProtectionScope.CurrentUser, fStream);

                fStream.Close();

                Process process = new Process();
                process.StartInfo = new ProcessStartInfo("notepad.exe", "Data.dat");
                process.Start();
                process.WaitForExit();

                Console.WriteLine("Reading data from disk and decrypting...");

                // Open the file.
                fStream = new FileStream("Data.dat", FileMode.Open);

                // Read from the stream and decrypt the data.
                byte[] decryptData = DecryptDataFromStream(entropy, DataProtectionScope.CurrentUser, fStream, bytesWritten);

                fStream.Close();

                Console.WriteLine("Decrypted data: " + UnicodeEncoding.ASCII.GetString(decryptData));
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.ToString());
            }
        }

        private static void EncryptInMemoryData(byte[] buffer, MemoryProtectionScope scope)
        {
            if (buffer.Length <= 0)
                throw new ArgumentException("buffer");
            if (buffer == null)
                throw new ArgumentNullException("buffer");

            ProtectedMemory.Protect(buffer, scope);
        }

        private static void DecryptInMemoryData(byte[] buffer, MemoryProtectionScope scope)
        {
            if (buffer.Length <= 0)
                throw new ArgumentException("buffer");
            if (buffer == null)
                throw new ArgumentNullException("buffer");

            ProtectedMemory.Unprotect(buffer, scope);
        }

        private static byte[] CreateRandomEntropy()
        {
            // Create a byte array to hold the random value.
            byte[] entropy = new byte[16];

            // Create a new instance of the RNGCryptoServiceProvider.
            // Fill the array with a random value.
            new RNGCryptoServiceProvider().GetBytes(entropy);

            // Return the array.
            return entropy;
        }

        private static int EncryptDataToStream(byte[] buffer, byte[] entropy, DataProtectionScope scope, Stream stream)
        {
            if (buffer.Length <= 0)
                throw new ArgumentException("buffer");
            if (buffer == null)
                throw new ArgumentNullException("buffer");
            if (entropy.Length <= 0)
                throw new ArgumentException("entropy");
            if (entropy == null)
                throw new ArgumentNullException("entropy");
            if (stream == null)
                throw new ArgumentNullException("stream");

            int length = 0;

            // Encrypt the data in memory. The result is stored in the same array as the original data.
            byte[] encryptedData = ProtectedData.Protect(buffer, entropy, scope);

            if (stream.CanWrite && encryptedData != null)
            {
                stream.Write(encryptedData, 0, encryptedData.Length);

                length = encryptedData.Length;
            }

            return length;
        }

        private static byte[] DecryptDataFromStream(byte[] entropy, DataProtectionScope scope, Stream stream, int length)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            if (length <= 0)
                throw new ArgumentException("length");
            if (entropy == null)
                throw new ArgumentNullException("entropy");
            if (entropy.Length <= 0)
                throw new ArgumentException("entropy");

            byte[] inBuffer = new byte[length];
            byte[] outBuffer;

            // Read the encrypted data from a stream.
            if (stream.CanRead)
            {
                stream.Read(inBuffer, 0, length);

                outBuffer = ProtectedData.Unprotect(inBuffer, entropy, scope);
            }
            else
            {
                throw new IOException("Could not read the stream");
            }

            // Return the length that was written to the stream.
            return outBuffer;
        }
    }
}
