using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using System.Threading;

namespace DirectlyAccessingAPrincipalObject
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);

                Console.WriteLine("Getting current principal");
                IPrincipal myPrincipal = Thread.CurrentPrincipal;

                Console.WriteLine("Name: " + myPrincipal.Identity.Name);
                Console.WriteLine("AuthenicationsType: " + myPrincipal.Identity.AuthenticationType);
                Console.WriteLine("IsAuthenticated: " + myPrincipal.Identity.IsAuthenticated);

                Console.WriteLine("\nNow setting principal policy on the app domain to windows.");
                AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
                Console.WriteLine("CurrentPrincipal Type: " + Thread.CurrentPrincipal.GetType());
                Console.WriteLine("CurrentPrincipal Name: " + Thread.CurrentPrincipal.Identity.Name);

                WindowsPrincipal newPrincipal = (WindowsPrincipal)Thread.CurrentPrincipal;

                Console.WriteLine("Name: " + newPrincipal.Identity.Name);
                Console.WriteLine("AuthenicationsType: " + newPrincipal.Identity.AuthenticationType);
                Console.WriteLine("IsAuthenticated: " + newPrincipal.Identity.IsAuthenticated + "\n\n");

                WindowsIdentity myIdent = WindowsIdentity.GetCurrent();
                Console.WriteLine("Name: " + myIdent.Name);
                Console.WriteLine("AuthenicationsType: " + myIdent.AuthenticationType);
                Console.WriteLine("IsAuthenticated: " + myIdent.IsAuthenticated);

                WindowsPrincipal principal = new WindowsPrincipal(myIdent);

                if (principal.IsInRole(@"BUILTIN\Administrators"))
                {
                    Console.WriteLine("You are an administrator");
                }
                else
                {
                    Console.WriteLine("You are not an administrator");
                }

                if (principal.IsInRole(WindowsBuiltInRole.Administrator))
                {
                    Console.WriteLine("You are an administrator");
                }
                else
                {
                    Console.WriteLine("You are not an administrator");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
