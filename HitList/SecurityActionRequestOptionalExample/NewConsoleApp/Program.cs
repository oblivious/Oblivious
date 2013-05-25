using System;
using System.Security;
using System.Security.Permissions;
using System.IO;

[assembly: FileIOPermission(SecurityAction.RequestOptional, Unrestricted = true)]
[assembly: UIPermission(SecurityAction.RequestMinimum, Unrestricted = true)]

public class MyClass
{
	public MyClass()
	{
	}

	public static void Main(string[] args)
	{
		//Put any code that requires optional permissions in the try block.  
		try
		{
			File.Create("test.txt");
			Console.WriteLine("The file has been created.");
		}
		//Catch the security exception and inform the user that the  
		//application was not granted FileIOPermission. 
		catch (SecurityException)
		{
			Console.WriteLine("This application does not have permission to write to the disk.");
		}
	}
}