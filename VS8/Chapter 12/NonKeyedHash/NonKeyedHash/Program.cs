using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace NonKeyedHash
{
    class Program
    {
        static void Main(string[] args)
        {
            //Unkeyed hashing

            MD5 myHash = new MD5CryptoServiceProvider();
            FileStream file = new FileStream(args[0], FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(file);

            myHash.ComputeHash(reader.ReadBytes((int)file.Length));
            Console.WriteLine(Convert.ToBase64String(myHash.Hash));


            //Keyed hashing

            byte[] saltValueBytes = Encoding.ASCII.GetBytes("This is my salt");
            Console.WriteLine(args[1]);
            Rfc2898DeriveBytes passwordKey = new Rfc2898DeriveBytes(args[1].Trim(), saltValueBytes);

            byte[] secretKey = passwordKey.GetBytes(16);

            HMACSHA1 hash = new HMACSHA1(secretKey);

            FileStream file1 = new FileStream(args[2], FileMode.Open, FileAccess.Read);
            BinaryReader reader1 = new BinaryReader(file1);

            hash.ComputeHash(reader1.ReadBytes((int)file1.Length));
            Console.WriteLine(Convert.ToBase64String(hash.Hash));
        }
    }
}
