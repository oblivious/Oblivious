using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace EncryptionSample
{
    class Program
    {
        static void Main(string[] args)
        {
            string inFileName = @"C:\Users\Donal\Documents\win.ini";
            string outFileName = @"C:\Users\Donal\Documents\win.ini.enc";

            SymmetricAlgorithm myAlg = new RijndaelManaged();

            myAlg.GenerateKey();

            Console.WriteLine(Encoding.ASCII.GetString(myAlg.Key));
            
            using (FileStream inFile = new FileStream(inFileName, FileMode.Open, FileAccess.Read))
            using (FileStream outFile = new FileStream(outFileName, FileMode.Create, FileAccess.Write))
            {
                byte[] fileData = new byte[inFile.Length];
                inFile.Read(fileData, 0, (int)inFile.Length);

                ICryptoTransform encryptor = myAlg.CreateEncryptor();

                using (CryptoStream encryptStream = new CryptoStream(outFile, encryptor, CryptoStreamMode.Write))
                {
                    encryptStream.Write(fileData, 0, fileData.Length);
                }
            }

            using (FileStream readFile = new FileStream(outFileName, FileMode.Open, FileAccess.Read))
            {
                byte[] fileData = new byte[readFile.Length];
                readFile.Read(fileData, 0, (int)readFile.Length);

                ICryptoTransform decryptor = myAlg.CreateDecryptor();

                using (CryptoStream decryptStream = new CryptoStream(outFileName, decryptor, CryptoStreamMode.Read))
                {
                    decryptStream.Read(
                }
            }
        }
    }
}
