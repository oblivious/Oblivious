using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using System.Threading;
using System.Security.Permissions;

namespace IdentityTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nWindows Identity");
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            Console.WriteLine("Authentication Type: " + identity.AuthenticationType);
            Console.WriteLine("Name: " + identity.Name);
            Console.WriteLine("Is System: " + identity.IsSystem);
            Console.WriteLine("Is Authenticated: " + identity.IsAuthenticated);
            Console.WriteLine("Owner Value: " + identity.Owner.Value);
            Console.WriteLine("User Value: " + identity.User.Value);
            Console.WriteLine("Token: " + identity.Token.ToString());
            Console.WriteLine("Groups:");
            IdentityReferenceCollection groups = identity.Groups;
            foreach (IdentityReference ir in groups)
            {
                Console.WriteLine(" - Value: " + ir.Value);
            }

            
            Console.WriteLine("\nWindows Principal");
            AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
            WindowsPrincipal principal = (WindowsPrincipal)Thread.CurrentPrincipal;
            Console.WriteLine("Identity Name: " + principal.Identity.Name);

            Console.WriteLine("\nUser Roles: ");
            Console.WriteLine("Administrator: " + principal.IsInRole(WindowsBuiltInRole.Administrator));
            Console.WriteLine("Power User: " + principal.IsInRole(WindowsBuiltInRole.PowerUser));
            Console.WriteLine("User: " + principal.IsInRole(WindowsBuiltInRole.User));

            try
            {
                PrincipalPermission pp = new PrincipalPermission(PermissionState.None);
                Console.WriteLine("ToString: " + pp.ToString());
                pp.Demand();
                Console.WriteLine("Success!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            try
            {
                SuperDuperMethod();
            }
            catch (Exception e)
            {
                Console.WriteLine("Super Duper method threw a far out exception: " + e.ToString());
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role="Administrators")]
        public static void SuperDuperMethod()
        {
            Console.WriteLine("Oh, yeah, in the Super Duper method, man it's so cool");
        }
    }
}
