using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace CryptoTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            RijndaelManaged rijndael = new RijndaelManaged();
            rijndael.Mode = CipherMode.CBC;
            rijndael.Padding = PaddingMode.None;
            rijndael.KeySize = 
        }
    }
}
