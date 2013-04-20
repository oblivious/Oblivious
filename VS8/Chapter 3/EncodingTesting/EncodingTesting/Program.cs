using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace EncodingTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding e = Encoding.GetEncoding("Korean");

            byte[] encoded;
            encoded = e.GetBytes("Hello, World!");

            for (int i = 0; i < encoded.Length; i++)
                Console.WriteLine("Byte {0}: {1}", i , encoded[i]);

            EncodingInfo[] eis = Encoding.GetEncodings();
            foreach (EncodingInfo ei in eis)
                Console.WriteLine((ei.CodePage.ToString() + ":").PadRight(8) + "{0}, {1}", ei.Name.PadRight(25), ei.DisplayName);

            StreamWriter swUtf7 = new StreamWriter("utf7.txt", false, Encoding.UTF7);
            swUtf7.WriteLine("Hello, World!");
            swUtf7.Close();

            StreamWriter swUtf8 = new StreamWriter("utf8.txt", false, Encoding.UTF8);
            swUtf8.WriteLine("Hello, World!");                     
            swUtf8.Close();

            StreamWriter swUtf16 = new StreamWriter("utf16.txt", false, Encoding.Unicode);
            swUtf16.WriteLine("Hello, World!");
            swUtf16.Close();

            StreamWriter swUtf32 = new StreamWriter("utf32.txt", false, Encoding.UTF32);
            swUtf32.WriteLine("Hello, World!");
            swUtf32.Close();

            StreamReader sr = new StreamReader("utf7.txt", Encoding.UTF7);
            Console.WriteLine(sr.ReadToEnd());
            sr.Close();

        }
    }
}
