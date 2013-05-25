﻿using System;
using System.Collections;
using System.Diagnostics;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Reflection;
using System.IO;

[assembly: FileIOPermission(SecurityAction.RequestMinimum, Unrestricted=true)]

namespace SimpleSandboxing
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create the permission set to grant to other assemblies. 
			// In this case we are granting the permissions found in the LocalIntranet zone.
			PermissionSet permissionSet = GetNamedPermissionSet("LocalIntranet");
			if (permissionSet == null)
				return;
			AppDomainSetup appDomainSetup = new AppDomainSetup();
			// Identify the folder to use for the sandbox.
			Directory.CreateDirectory("C:\\Sandbox");
			appDomainSetup.ApplicationBase = "C:\\Sandbox";
			// Copy the application to be executed to the sandbox.
			File.Copy(@"..\..\..\NewConsoleApp\Bin\Debug\NewConsoleApp.exe",
				"C:\\sandbox\\NewConsoleApp.exe", true);
			File.Copy(@"..\..\..\NewConsoleApp\Bin\Debug\NewConsoleApp.pdb",
				"C:\\sandbox\\NewConsoleApp.pdb", true);

			Evidence hostEvidence = new Evidence();

			// Create the sandboxed domain.
			AppDomain sandbox = AppDomain.CreateDomain(
				"Sandboxed Domain",
				hostEvidence,
				appDomainSetup,
				permissionSet,
				GetStrongName(Assembly.GetExecutingAssembly()));
			sandbox.ExecuteAssemblyByName("NewConsoleApp");
		}

		public static StrongName GetStrongName(Assembly assembly)
		{
			if (assembly == null)
				throw new ArgumentNullException("assembly");

			AssemblyName assemblyName = assembly.GetName();
			Debug.Assert(assemblyName != null, "Could not get assembly name");

			// Get the public key blob. 
			byte[] publicKey = assemblyName.GetPublicKey();
			if (publicKey == null || publicKey.Length == 0)
				throw new InvalidOperationException("Assembly is not strongly named");

			StrongNamePublicKeyBlob keyBlob = new StrongNamePublicKeyBlob(publicKey);

			// Return the strong name. 
			return new StrongName(keyBlob, assemblyName.Name, assemblyName.Version);
		}
		private static PermissionSet GetNamedPermissionSet(string name)
		{
			IEnumerator policyEnumerator = SecurityManager.PolicyHierarchy();

			// Move through the policy levels to the machine policy level. 
			while (policyEnumerator.MoveNext())
			{
				PolicyLevel currentLevel = (PolicyLevel)policyEnumerator.Current;

				if (currentLevel.Label == "Machine")
				{
					NamedPermissionSet copy = currentLevel.GetNamedPermissionSet(name);
					return (PermissionSet)copy;
				}
			}
			return null;
		}
	}
}