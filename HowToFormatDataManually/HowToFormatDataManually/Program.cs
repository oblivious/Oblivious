using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading;

namespace HowToFormatDataManually
{
    class Program
    {
        static void Main(string[] args)
        {
            FormatData(new CultureInfo("es-ES"));
            FormatData(new CultureInfo("ru-RU"));
            FormatData(new CultureInfo("en-US"));

            Console.ReadKey();
        }

        private static void FormatData(CultureInfo ci)
        {
            // Display the selected culture.
            Console.WriteLine(ci.ToString() + ":");

            // Identify the number and copy it to a string
            double d = 1234567.89;
            string formattedNumber = d.ToString();

            // Identify the locatino of the culture-sensitive decimal point in the number
            int decimalIndex = formattedNumber.IndexOf(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator) + 1;

            // Extract only the decimal portion of the number
            formattedNumber = formattedNumber.Substring(decimalIndex, formattedNumber.Length - decimalIndex);

            // Add the culture-specific decimal point before the number
            formattedNumber = ci.NumberFormat.NumberDecimalSeparator + formattedNumber;
            
            // Extract only the whole portion of the number
            string wholeDigits = Math.Floor(d).ToString();

            // Add each whole digit to formatted Number, with grouping separators
            for (int a = 0; a < wholeDigits.Length; a++)
            {
                // Examine CultureInfo.NumberFormat.NumberGroupSizes to determine
                // whether the current locatino requires a separator
                foreach (int sep in ci.NumberFormat.NumberGroupSizes)
                {
                    if ((a > 0) && ((a % sep) == 0))
                    {
                        formattedNumber = ci.NumberFormat.NumberGroupSeparator + formattedNumber;
                    }

                    // Add the number to the final string
                    formattedNumber = wholeDigits.ToCharArray()[wholeDigits.Length - a - 1] + formattedNumber;
                }
            }

            // Display the manual results and the automatically formatted version
            Console.WriteLine("  Manual:    {0}", formattedNumber);
            Console.WriteLine("  Automatic: {0}", d.ToString("N", ci));
        }
    }
}
