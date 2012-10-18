using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Numerics;

namespace Formatting_Test
{
    class Program
    {
        static void Main(string[] args)
        {
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
                    break;
                    baseNumber = 8;
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
            for (int ctr = bytes.GetUpperBound(0); ctr >= bytes.GetLowerBound(0); ctr--)
            {
                string byteString = Convert.ToString(bytes[ctr], baseNumber);
                if (baseNumber == 2)
                {
                    byteString = new String('0', 8 - byteString.Length) + byteString;
                }
                else if (baseNumber == 8)
            }
        }
    }
}
