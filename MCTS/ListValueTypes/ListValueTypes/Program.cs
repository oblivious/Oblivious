using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListValueTypes
{
    class Program
    {
        static void Main(string[] args)
        {
            sbyte a = 0;
            byte b = 0;
            short c = 0;
            int d = 0;
            long e = 0;
            string s = ""; ;
            Exception ex = new Exception();

            object[] types = { a, b, c, d, e, s, ex };

            foreach (object o in types)
            {
                string type;
                if (o.GetType().IsValueType)
                    type = "Value type";
                else
                    type = "Reference type";
                Console.WriteLine("{0}: {1}", o.GetType(), type);
            }
        }
    }
}
