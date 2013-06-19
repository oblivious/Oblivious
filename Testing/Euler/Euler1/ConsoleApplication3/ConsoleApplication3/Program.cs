using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDTS.DirectTopUpServices.BusinessEntities;

namespace ConsoleApplication3
{
    class Program
    {
        private int result;
        private readonly DateTime _startTime = new DateTime(2010, 08, 04);
        static void Main(string[] args)
        {
            Program program = new Program();

            long transactionID = (long)(DateTime.Now - program._startTime).TotalMilliseconds;
            Console.WriteLine("Milliseconds: " + transactionID);
            byte[] bytes = BitConverter.GetBytes(transactionID);
            Console.Write("Bytes: ");
            foreach (byte bite in bytes)
            {
                Console.Write(bite.ToString() + " ");
            }
            Console.WriteLine();
            System.Collections.Generic.IEnumerable<byte> bytesNumerable = bytes.Reverse<byte>();

            bytes = bytesNumerable.ToArray<byte>();

            Console.Write("Reversed: ");
            foreach (byte bite in bytes)
            {
                Console.Write(bite.ToString() + " ");
            }
            Console.WriteLine();
            string fakeTransactionID = bytes.Aggregate<byte, string>(string.Empty, (s, b) => s + b.ToString("x2"));
            Console.WriteLine("Transaction ID: " + fakeTransactionID);
            fakeTransactionID = "eze" + fakeTransactionID.Substring(fakeTransactionID.Length - 9);
            Console.WriteLine("Truncated: " + fakeTransactionID);
        }
    }
}
