using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace LabEncryptingAndDecryptingFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            string inFileName = args[0];
            string outFileName = args[1];
            string password = args[2];

            //Encrypt
            /*
            byte[] saltValueBytes = Encoding.ASCII.GetBytes("This is my salt");
            Rfc2898DeriveBytes passwordKey = new Rfc2898DeriveBytes(password, saltValueBytes);

            RijndaelManaged alg = new RijndaelManaged();
            alg.Key = passwordKey.GetBytes(alg.KeySize / 8);
            alg.IV = passwordKey.GetBytes(alg.BlockSize / 8);

            FileStream inFile = new FileStream(inFileName, FileMode.Open, FileAccess.Read);
            byte[] fileData = new byte[inFile.Length];
            inFile.Read(fileData, 0, (int)inFile.Length);

            ICryptoTransform encryptor = alg.CreateEncryptor();
            FileStream outFile = new FileStream(outFileName, FileMode.OpenOrCreate, FileAccess.Write);
            CryptoStream encryptStream = new CryptoStream(outFile, encryptor, CryptoStreamMode.Write);

            encryptStream.Write(fileData, 0, fileData.Length);

            encryptStream.Close();
            inFile.Close();
            outFile.Close();
             * */

            //Decrypt
            byte[] saltValueBytes = Encoding.ASCII.GetBytes("This is my salt");
            Rfc2898DeriveBytes passwordKey = new Rfc2898DeriveBytes(password, saltValueBytes);

            RijndaelManaged alg = new RijndaelManaged();
            alg.Key = passwordKey.GetBytes(alg.KeySize / 8);
            alg.IV = passwordKey.GetBytes(alg.BlockSize / 8);

            ICryptoTransform decryptor = alg.CreateDecryptor();
            FileStream inFile = new FileStream(inFileName, FileMode.Open, FileAccess.Read);
            CryptoStream decryptStream = new CryptoStream(inFile, decryptor, CryptoStreamMode.Read);
            byte[] fileData = new byte[inFileName.Length];
            decryptStream.Read(fileData, 0, (int)inFileName.Length);

            FileStream outFile = new FileStream(outFileName, FileMode.OpenOrCreate, FileAccess.Write);
            outFile.Write(fileData, 0, fileData.Length);

            decryptStream.Close();
            inFile.Close();
            outFile.Close();
        }
    }
}
