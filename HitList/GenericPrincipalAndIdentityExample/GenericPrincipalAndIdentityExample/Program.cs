using System;
using System.Security;
using System.Security.Principal;
using System.Threading;
using System.Collections;

namespace GenericPrincipalAndIdentityExample
{
	class Program
	{
		static void Main(string[] args)
		{
			/*
			IEnumerator enumerator = SecurityManager.PolicyHierarchy();
			IEnumerable enumerable = (IEnumerable)enumerator;
			Console.WriteLine("Didn't crash and burn like I expected.");
			*/

			// Create a generic identity.
			GenericIdentity myIdentity = new GenericIdentity("MyIdentity");

			// Create a generic principal.
			string[] myRoles = { "Manager", "Teller" };
			GenericPrincipal myPrincipal = new GenericPrincipal(myIdentity, myRoles);

			// Attach
			Thread.CurrentPrincipal = myPrincipal;

			// Print values to the console.
			string name = Thread.CurrentPrincipal.Identity.Name;
			bool auth = myPrincipal.Identity.IsAuthenticated;
			bool isInRole = myPrincipal.IsInRole("Manager");

			Console.WriteLine("The name is: " + name);
			Console.WriteLine("IsAuthenticated: " + auth);
			Console.WriteLine("Is this a Manager?: " + isInRole);
		}
	}
}
