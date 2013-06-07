using System.Security.Permissions;
using System;
using System.Security.Principal;
using System.Threading;

namespace PrincipalPermissionExample
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
				/*
				string id1 = "Bob";
				string role1 = "Manager";
				PrincipalPermission principalPermission1 = new PrincipalPermission(id1, role1);

				PrincipalPermission principalPermission2 = new PrincipalPermission("Louise", "Supervisor");

				(principalPermission1.Union(principalPermission2)).Demand();
				*/

				WindowsPrincipal principal = (WindowsPrincipal)Thread.CurrentPrincipal;
				Console.WriteLine("Identity name: " + principal.Identity.Name);
				Console.WriteLine("Authentication type: " + principal.Identity.AuthenticationType);
				Console.WriteLine("IsInRole(\"administrators\"): " + principal.IsInRole("Administrators").ToString());

				Console.WriteLine("Checking that the current user is \"EZETOP\\dobyrne\" and that they are in the " + 
					"\"administrators\" group \\ role:");

				PrincipalPermission permission = new PrincipalPermission("EZETOP\\dobyrne", "administrators");
				permission.Demand();

				Console.WriteLine("Demand succeeded.");

				Console.WriteLine("\nChecking that the current user is in the \"administrators\" role. I know we did" + 
					" it already but it's on the page:");
				PrincipalPermission principalPerm = new PrincipalPermission(null, "Administrators");
				principalPerm.Demand();
				Console.WriteLine("Demand succeeded.");

				Console.WriteLine("\nPlease be sure to note that if either the \"name\" or \"role\" passed to a " +
					"PrincipalPermission constructor are null it means that any value is accepted.");

				Console.WriteLine("Checking if unrestricted: ");
				PrincipalPermission finalPerm = new PrincipalPermission(PermissionState.Unrestricted);
				finalPerm.Demand();
				Console.WriteLine("Demand succeeded, the current principal is unrestricted.");


			}
			catch (Exception e)
			{
				Console.WriteLine("Exception: " + e.ToString());
			}
		}
	}
}
