using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace InterfacesExercise
{
    class BiFormatter : ICustomFormatter, IFormatProvider
    {
        public object GetFormat(Type formatType)
        {
            if (formatType == null)
                throw new ArgumentNullException("format type was null");
            if (formatType is ICustomFormatter)
                return this;
            else
                return null;
        }

        public string Format(string format, object obj, IFormatProvider provider)
        {
            if (format == null)
                throw new ArgumentNullException("format cannot be null");
            if (obj == null)
                throw new ArgumentNullException("object cannot be null");
            Car car = obj as Car;
            if (car == null)
                return HandleOtherFormats(format, obj);
            switch (format)
                case: 
                    return 
        }

        public string HandleOtherFormats(string format, object obj)
        {
            if (obj is IFormattable)
                return ((IFormattable)obj).ToString(format, CultureInfo.CurrentCulture);
            if (obj != null)
                return obj.ToString();
            return string.Empty;
        }
    }
}
