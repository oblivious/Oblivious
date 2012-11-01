using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace InterfacesExercise2
{
    class BinaryFormatter : ICustomFormatter, IFormatProvider
    {
        public object GetFormat(Type type)
        {
            if (type == typeof(ICustomFormatter))
                return this;
            else
                return null;
        }

        public string Format(string format, object obj, IFormatProvider provider)
        {
            int baseValue;
            string fmt;

            if (!String.IsNullOrEmpty(format))
                fmt = format.Substring(0,1).ToUpperInvariant();
            else
                fmt = format;

            byte[] bytes;
            if (obj is sbyte)
            {
                string byteString = ((sbyte)obj).ToString("X2");
                bytes = new byte[1] { Byte.Parse(byteString, System.Globalization.NumberStyles.HexNumber) };
            }
        }

        private string HandleOtherFormats(string format, object obj)
        {
            if (obj is IFormattable)
                return ((IFormattable)obj).ToString(format, CultureInfo.CurrentCulture);
            else if (obj != null)
                return obj.ToString();
            else
                return string.Empty;
        }
    }
}
