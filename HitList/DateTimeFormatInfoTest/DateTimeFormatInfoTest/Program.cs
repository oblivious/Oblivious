using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading;

namespace DateTimeFormatInfoTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-MX", false);

            DateTimeFormatInfo dtfi = new CultureInfo("es-MX", false).DateTimeFormat;
            DateTime dt = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
            string dateString = dt.ToString(dtfi.LongDatePattern);

            Console.WriteLine(dateString);

            AppDomain.CurrentDomain.ActivationContext
        }
    }
}
