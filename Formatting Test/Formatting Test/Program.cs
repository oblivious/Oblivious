using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Numerics;
using System.Threading;

namespace Formatting_Test
{
    class Program
    {
        static void Main()
        { 
            //
            Console.WindowWidth = 100;
            Console.WindowHeight = 40;

            Console.WriteLine(CultureInfo.CurrentCulture);
            Console.WriteLine(CultureInfo.CurrentUICulture);

            IFormatProvider bin = new BinaryFormatter();

            byte byteValue = 124;
            Console.WriteLine(String.Format(bin, "{0} (binary: {0:B}) (octal: {0:O}) (hex: {0:H})", byteValue));

            int intValue = 23045;
            Console.WriteLine(String.Format(bin, "{0} (binary: {0:B}) (octal: {0:O}) (hex: {0:H})", intValue));

            ulong ulngValue = 31906574882;
            Console.WriteLine(String.Format(new BinaryFormatter(), "{0}\n   (binary: {0:B})\n   (octal: {0:O})\n   (hex: {0:H})", ulngValue));

            BigInteger bigIntValue = BigInteger.Multiply(Int64.MaxValue, 2);
            Console.WriteLine(String.Format(new BinaryFormatter(), "{0}\n   (binary: {0:B})\n   (octal: {0:O})\n   (hex: {0:H})", bigIntValue));

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            long acctNumber;
            double balance;
            DayOfWeek wday;
            string output;

            acctNumber = 104254567890;
            balance = 16.34;
            wday = DayOfWeek.Monday;

            output = String.Format(new AcctNumberFormat(),
                                   "On {2}, the balance of account {0:H} was {1:C2}.",
                                   acctNumber, balance, wday);
            Console.WriteLine(output);

            wday = DayOfWeek.Tuesday;
            output = String.Format(new AcctNumberFormat(),
                                   "On {2}, the balance of account {0:I} was {1:C2}.",
                                   acctNumber, balance, wday);
            Console.WriteLine(output);
        }
    }
}
