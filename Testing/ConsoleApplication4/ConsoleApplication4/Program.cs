using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace ConsoleApplication4
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                serRecarga service = new serRecarga();

                string response = service.GetSaldoBl();

                Console.WriteLine("response");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.ReadKey();
        }
    }
}
