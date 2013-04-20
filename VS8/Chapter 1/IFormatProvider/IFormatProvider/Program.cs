using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Basic Example: ");
            DateTime dateValue = new DateTime(2009, 6, 1, 4, 37, 0);
            CultureInfo[] cultures = { new CultureInfo("en-US"),
                                       new CultureInfo("en-GB"),
                                       new CultureInfo("fr-FR"),
                                       new CultureInfo("it-IT"),
                                       new CultureInfo("de-DE") };
            foreach (CultureInfo culture in cultures)
                Console.WriteLine("{0}: {1}", culture.Name, dateValue.ToString(culture));

            Console.WriteLine("\n\nSecond Example:");
            long acctNumber;
            double balance;
            DayOfWeek wday;
            string output;

            acctNumber = 104254567890;
            balance = 16.34;
            wday = DayOfWeek.Monday;

            output = String.Format(new AcctNumberFormat(), "On {2}, the balance of account {0:H} was {1:C2}.", acctNumber, balance, wday);
            Console.WriteLine(output);

            wday = DayOfWeek.Tuesday;
            output = String.Format(new AcctNumberFormat(), "On {2}, the balance of account {0:I} was {1:C2}.", acctNumber, balance, wday);
            Console.WriteLine(output);

            wday = DayOfWeek.Wednesday;
            output = String.Format(new AcctNumberFormat(), "On {2}, the balance of account {0:I} was {1:C2}.", acctNumber, balance, wday);
            Console.WriteLine(output);

            long test = 64;
            bool? test2 = null;
            TypeCode code = Type.GetTypeCode(test.GetType());
            Console.WriteLine(code);
            Console.WriteLine(Type.GetTypeCode(test2.GetType()));
        }
    }

    public class AcctNumberFormat : IFormatProvider, ICustomFormatter
    {
        private const int ACCT_LENGTH = 12;

        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
                return this;
            else
                return null;
        }

        public string Format(string fmt, object arg, IFormatProvider formatProvider)
        {
            // Provide default formatting if arg is not an Int64
            if (arg.GetType() != typeof(Int64))
            {
                try
                {
                    return HandleOtherFormats(fmt, arg);
                }
                catch (FormatException e)
                {
                    throw new FormatException(String.Format("The format of '{0}' is invalid.", fmt), e);
                }
            }

            // Provide default formatting for unsupported format strings.
            string ufmt = fmt.ToUpper(CultureInfo.InvariantCulture);
            if (!(ufmt == "H" || ufmt == "I"))
            {
                try
                {
                    return HandleOtherFormats(fmt, arg);
                }
                catch (FormatException e)
                {
                    throw new FormatException(String.Format("The format of '{0}' is invalid.", fmt), e);
                }
            }

            // Convert argument to a string.
            string result = arg.ToString();

            // If account number is less that 12 characters, pad with leading zeroes.
            if (result.Length < ACCT_LENGTH)
                result = result.PadLeft(ACCT_LENGTH, '0');
            // If account number is more than 12 characters, truncate to 12 characters.
            if (result.Length > ACCT_LENGTH)
                result = result.Substring(0, ACCT_LENGTH);
            if (ufmt == "I") // Integer only format.
                return result;
            else // Add Hyphens to H format specifier
                return result.Substring(0, 5) + "-" + result.Substring(5, 3) + "-" + result.Substring(8);
        }

        private string HandleOtherFormats(string format, object arg)
        {
            if (arg is IFormattable)
                return ((IFormattable)arg).ToString(format, CultureInfo.CurrentCulture);
            else if (arg != null)
                return arg.ToString();
            else
                return String.Empty;
        }
    }
}
