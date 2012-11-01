using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Numerics;

namespace Formatting_Test
{
    class BinaryFormatter : IFormatProvider, ICustomFormatter
    {
        private static int baseID = 0;
        private int myID;

        public BinaryFormatter()
        {
            baseID++;
            myID = baseID;
        }

        public object GetFormat(Type formatType)
        {
            Console.WriteLine("myID is " + myID + ",GetFormat called with type: " + formatType.AssemblyQualifiedName);
            if (formatType == typeof(ICustomFormatter))
                return this;
            else
                return null;
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            Console.WriteLine("myID is " + myID + ", Format called with formatProvider: " + formatProvider.GetType().AssemblyQualifiedName);
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
            double numChars = Math.Log(256, baseNumber);
            int numCharsAsInt = (int)(numChars + 0.5d);

            string[] array = new string[4];
            Console.WriteLine("Lower bound: " + array.GetLowerBound(0) + ", Upper bound: " + array.GetUpperBound(0));

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
}
