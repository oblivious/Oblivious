using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;
using System.Security.Principal;

namespace ActivationContextSecurityInfoEtc
{
    public class Program : MarshalByRefObject
    {
        [SecurityPermission(SecurityAction.LinkDemand, ControlDomainPolicy=true)]
        public static void Main(string[] args)
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            Console.WriteLine(Thread.CurrentPrincipal.Identity.Name);
            if (!principal.IsInRole(WindowsBuiltInRole.Administrator))
            {
                Console.WriteLine("You're not an administrator, dummy!");
                Console.Read();
                return;
            }

            //AppDomain domain = AppDomain.CreateDomain("My Happy Domain");
            ActivationContext ac = AppDomain.CurrentDomain.ActivationContext;
            // Throws here because ac is null.
            ApplicationIdentity ai = ac.Identity;
            Console.WriteLine("Full name = " + ai.FullName);
            Console.WriteLine("Code base = " + ai.CodeBase);
        }

        [SecurityPermission(SecurityAction.LinkDemand, ControlDomainPolicy = true)]
        public void Run()
        {
            Main(new string[] { });
            Console.ReadLine();
        }

        //[DllImport("kernel32.dll")]
        //private static extern IntPtr CreateActCtx(ref ACTCTX actctx);
    }
}
