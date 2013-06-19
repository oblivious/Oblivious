using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace TruncateTransactionIdTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int transactionID = 12345678;
                string systemTrace = (((transactionID % 1000000) * 2) % 1000000).ToString("D6");
                Console.WriteLine(systemTrace);
                Console.WriteLine(transactionID.ToString("D6"));
                string temp;
                systemTrace = (temp = transactionID.ToString(CultureInfo.InvariantCulture)).Substring(temp.Length - 6);
                Console.WriteLine(systemTrace);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.ReadKey();
        }
    }
}
