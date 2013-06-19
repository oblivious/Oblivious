using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace HashtableTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Hashtable table = new Hashtable();
                table.Add("noodles", "noodles");
                Console.WriteLine(table["noodles"]);
                string output = (string)table["rice"];
                if (output == null)
                    Console.WriteLine("Rice was null...");
                Console.WriteLine(table["rice"]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.ReadKey();
        }
    }
}
