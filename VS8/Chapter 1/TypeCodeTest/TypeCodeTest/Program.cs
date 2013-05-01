using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypeCodeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            bool a = true;
            int b = 0;
            long c = 0;
            DateTime d = DateTime.Now;
            double e = 0;
            WriteObjectInfo(a);
            WriteObjectInfo(b);
            WriteObjectInfo(c);
            WriteObjectInfo(d);
            WriteObjectInfo(e);
        }

        static void WriteObjectInfo(object testObject)
        {
            TypeCode typeCode = Type.GetTypeCode(testObject.GetType());

            switch (typeCode)
            {
                case TypeCode.Boolean:
                    Console.WriteLine("Boolean: {0}", testObject);
                    break;

                case TypeCode.Double:
                    Console.WriteLine("Double: {0}", testObject);
                    break;

                default:
                    Console.WriteLine("{0}: {1}", typeCode.ToString(), testObject);
                    break;
            }
        }
    }
}
