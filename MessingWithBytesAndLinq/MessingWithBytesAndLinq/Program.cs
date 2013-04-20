using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessingWithBytesAndLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] bytes = ConvertToPLI(12345);
            Console.Write("Old: ");
            foreach (byte b in bytes)
                Console.Write(b.ToString("X2"));
            Console.WriteLine();


            byte[] bytes2 = ConvertToPLI2(12345);
            Console.Write("New: ");
            foreach (byte b in bytes2)
                Console.Write(b.ToString("X2"));
            Console.WriteLine();
        }

        public static byte[] ConvertToPLI2(short value)
        {
            return BitConverter.GetBytes(value).Reverse<byte>().ToArray<byte>();
        }

        public static byte[] ConvertToPLI(short value)
        {
            string hexidecimals = BitConverter.ToString(BitConverter.GetBytes(value).Reverse<byte>().ToArray<byte>()).Replace("-", "");

            Console.WriteLine(".... " + hexidecimals);

            return Enumerable.Range(0, hexidecimals.Length).Where<int>(delegate(int x)
            {
                return (0 == (x % 2));
            }).Select<int, byte>(delegate(int x)
            {
                return Convert.ToByte(hexidecimals.Substring(x, 2), 0x10);
            }).ToArray<byte>();
        }
    }
}
