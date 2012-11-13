using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace HtmlEncodeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Some text < > + @ / \\ £ % ^");
            Console.WriteLine(HttpUtility.HtmlEncode("Some text < > + @ / \\ £ % ^"));
        }
    }
}
