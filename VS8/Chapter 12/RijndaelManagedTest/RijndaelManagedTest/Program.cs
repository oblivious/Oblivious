using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace RijndaelManagedTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string password = "P@S5w0r]>";

            RijndaelManaged myAlg = new RijndaelManaged();

            byte[] salt = Encoding.ASCII.GetBytes("This is my salt.");
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, salt);

            myAlg.Key = key.GetBytes(myAlg.KeySize / 8);
            myAlg.IV = key.GetBytes(myAlg.BlockSize / 8);
        }
    }
}
