using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumericFormatStrings
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 1234;
            Console.WriteLine(i.ToString("D10"));

            int myTimeout = 44000;
            Console.WriteLine("Integer: " + myTimeout);

            byte[] asBytes = BitConverter.GetBytes(myTimeout);
            foreach (byte b in asBytes)
            {
                Console.Write(b.ToString("x2") + " ");
            }
            Console.WriteLine();

            string value = Encoding.UTF8.GetString(asBytes);
            Console.WriteLine("Value: " + value);

            byte[] backToBytes = Encoding.UTF8.GetBytes(value);
            foreach (byte b in backToBytes)
            {
                Console.Write(b.ToString("x2") + " ");
            }
            Console.WriteLine();

            Console.WriteLine("Integer: " + BitConverter.ToInt32(backToBytes, 0));
        }
    }
}
