using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using System.Net;
using System.Net.NetworkInformation;
using System.Web;

namespace ComputerNameTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine(WindowsIdentity.GetCurrent().Name.ToString());
                Console.WriteLine(IPGlobalProperties.GetIPGlobalProperties().DomainName);
                Console.WriteLine(Dns.GetHostName());
                Console.WriteLine(System.Environment.MachineName);
                Console.WriteLine(HttpContext.Current.Server.MachineName); 
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}