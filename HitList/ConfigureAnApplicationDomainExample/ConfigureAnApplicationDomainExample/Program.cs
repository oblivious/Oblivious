using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using System.Security.Permissions;

namespace ConfigureAnApplicationDomainExample
{
	class Program
	{
		[SecurityPermission(SecurityAction.LinkDemand, ControlDomainPolicy=true)]
		static void Main(string[] args)
		{
			try
			{
				ActivationContext ac = AppDomain.CurrentDomain.ActivationContext;
				if (ac == null)
					Console.WriteLine("Activation Context was null");
				else
					Console.WriteLine(ac.Identity.FullName);

				ActivationContext ad = ActivationContext.CreatePartialActivationContext(
					new ApplicationIdentity(AppDomain.CurrentDomain.ApplicationIdentity.FullName));

				AppDomainSetup domainInfo = new AppDomainSetup(ad);
				domainInfo.ApplicationBase = "f:\\work\\development\\latest";

				AppDomain domain = AppDomain.CreateDomain("MyDomain", null, domainInfo);

				Console.WriteLine("Host domain: " + AppDomain.CurrentDomain.FriendlyName);
				Console.WriteLine("Child domain: " + domain.FriendlyName);
				Console.WriteLine("Application base is: " + domain.SetupInformation.ApplicationBase);

				AppDomain.Unload(domain);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
		}

		[SecurityPermission(SecurityAction.LinkDemand, ControlDomainPolicy = true)]
		public void Run()
		{
			Main(new string[] { });
			Console.ReadLine();
		}
	}
}
