using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime myDateTime = new DateTime(2012, 5, 14, 15, 59, 19);
            Console.WriteLine("\"" + myDateTime.ToString("MM/dd/yyyy hh:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-US")) + "\"");


            Console.WriteLine("\"" + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-US"))+ "\"");
            Console.ReadKey();
        }
    }
}
