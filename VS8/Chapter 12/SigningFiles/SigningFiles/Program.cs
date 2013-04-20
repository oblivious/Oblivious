using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace SigningFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            RSACryptoServiceProvider signer = new RSACryptoServiceProvider();

            FileStream file = new FileStream(args[0], FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(file);
            byte[] data = reader.ReadBytes((int)file.Length);

            byte[] signature = signer.SignData(data, new SHA256CryptoServiceProvider());

            string publicKey = signer.ToXmlString(false);

            Console.WriteLine("Signature: " + Convert.ToBase64String(signature));
            reader.Close();
            file.Close();

            RSACryptoServiceProvider verifier = new RSACryptoServiceProvider();

            verifier.FromXmlString(publicKey);

            FileStream file2 = new FileStream(args[0], FileMode.Open, FileAccess.Read);
            BinaryReader reader2 = new BinaryReader(file2);
            byte[] data2 = reader2.ReadBytes((int)file2.Length);

            if (verifier.VerifyData(data2, new SHA256CryptoServiceProvider(), signature))
                Console.WriteLine("Signature verified");
            else 
                Console.WriteLine("Signature NOT verified");
            reader2.Close();
            file2.Close();
        }
    }
}
