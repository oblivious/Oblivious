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


            byte byteValue = 124;
            Console.WriteLine(String.Format(new BinaryFormatter(),
                                            "{0} (binary: {0:B}) (octal: {0:O}) (hex: {0:H})", byteValue));

            int intValue = 23045;
            Console.WriteLine(String.Format(new BinaryFormatter(),
                                            "{0} (binary: {0:B}) (octal: {0:O}) (hex: {0:H})", intValue));

            ulong ulngValue = 31906574882;
            Console.WriteLine(String.Format(new BinaryFormatter(),
                                            "{0}\n   (binary: {0:B})\n   (octal: {0:O})\n   (hex: {0:H})",
                                            ulngValue));

            BigInteger bigIntValue = BigInteger.Multiply(Int64.MaxValue, 2);
            Console.WriteLine(String.Format(new BinaryFormatter(),
                                            "{0}\n   (binary: {0:B})\n   (octal: {0:O})\n   (hex: {0:H})",
                                            bigIntValue));


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

    class BinaryFormatter : IFormatProvider, ICustomFormatter
    {
        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
                return this;
            else
                return null;
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            int baseNumber;
            string thisFmt = string.Empty;

            if (!String.IsNullOrEmpty(format))
                thisFmt = format.Length > 1 ? format.Substring(0, 1) : format;

            byte[] bytes;
            if (arg is sbyte)
            {
                string byteString = ((sbyte)arg).ToString("X2");
                bytes = new byte[1] { Byte.Parse(byteString, NumberStyles.HexNumber) };
            }
            else if (arg is byte)
            {
                bytes = new byte[1] { (byte)arg };
            }
            else if (arg is short)
            {
                bytes = BitConverter.GetBytes((short)arg);
            }
            else if (arg is int)
            {
                bytes = BitConverter.GetBytes((int)arg);
            }
            else if (arg is long)
            {
                bytes = BitConverter.GetBytes((long)arg);
            }
            else if (arg is ushort)
            {
                bytes = BitConverter.GetBytes((ushort)arg);
            }
            else if (arg is uint)
            {
                bytes = BitConverter.GetBytes((uint)arg);
            }
            else if (arg is ulong)
            {
                bytes = BitConverter.GetBytes((ulong)arg);
            }
            else if (arg is BigInteger)
            {
                bytes = ((BigInteger)arg).ToByteArray();
            }
            else
            {
                try
                {
                    return HandleOtherFormats(format, arg);
                }
                catch (FormatException e)
                {
                    throw new FormatException(String.Format("The format of '{0}' is invalid.", format), e);
                }
            }

            switch (thisFmt.ToUpper())
            {
                // Binary formatting.
                case "B":
                    baseNumber = 2;
                    break;
                case "O":
                    baseNumber = 8;
                    break;
                case "H":
                    baseNumber = 16;
                    break;
                default:
                    try
                    {
                        return HandleOtherFormats(format, arg);
                    }
                    catch (FormatException e)
                    {
                        throw new FormatException(String.Format("The format of '{0}' is invalid.", format), e);
                    }
            }

            string numericString = string.Empty;

            //Calculate the minimum required number of characters to display a value to a certain base.
            double numChars = Math.Log(256d, baseNumber);
            int numCharsAsInt = (int)(numChars + 0.5d);

            for (int ctr = bytes.GetUpperBound(0); ctr >= bytes.GetLowerBound(0); ctr--)
            {
                string byteString = Convert.ToString(bytes[ctr], baseNumber);
                byteString = new String('0', numCharsAsInt - byteString.Length) + byteString;
                numericString += byteString + " ";
            }
            return numericString.Trim();
        }

        private string HandleOtherFormats(string format, object arg)
        {
            if (arg is IFormattable)
            {
                return ((IFormattable)arg).ToString(format, CultureInfo.CurrentCulture);
            }
            else if (arg != null)
            {
                return arg.ToString();
            }
            else
            {
                return String.Empty;
            }
        }
    }

    public class AcctNumberFormat : IFormatProvider, ICustomFormatter
    {
        private const int ACCT_LENGTH = 12;

        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
            {
                return this;
            }
            else
            {
                return null;
            }
        }

        public string Format(string fmt, object arg, IFormatProvider formatProvider)
        {
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

            string result = arg.ToString();

            if (result.Length < ACCT_LENGTH)
            {
                result = result.PadLeft(ACCT_LENGTH, '0');
            }
            if (result.Length > ACCT_LENGTH)
            {
                result = result.Substring(0, ACCT_LENGTH);
            }
            if (ufmt == "I")
                return result;
            else
                return result.Substring(0, 5) + "-" + result.Substring(5, 3) + "-" + result.Substring(8);
        }

        private string HandleOtherFormats(string format, object arg)
        {
            if (arg is IFormattable)
                return ((IFormattable)arg).ToString(format, CultureInfo.CurrentCulture);
            else if (arg != null)
                return arg.ToString();
            else
                return string.Empty;
        }
    }
}
