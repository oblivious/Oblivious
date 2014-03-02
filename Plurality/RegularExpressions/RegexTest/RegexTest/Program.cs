/*
 * Created by SharpDevelop.
 * User: Donal
 * Date: 13/08/2013
 * Time: 22:36
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Text.RegularExpressions;

namespace RegexTest
{
	class Program
	{
		private const string MatchSuccess = "{0} @{1}:{2}";
		
		public static void Main(string[] args)
		{
			/*
			Console.WriteLine("Hello World!");
			
			// TODO: Implement Functionality Here
			
			Regex regex = new Regex("cat");
			Match match = regex.Match("catcat");
			
			Console.WriteLine("Index: {0}, Length: {1}", match.Index, match.Length);
			
			foreach (var capture in match.Captures)
			{
				Console.WriteLine("ToString(): " + capture.ToString());
			}
			
			foreach (var group in match.Groups)
			{
				Console.WriteLine("ToString(): " + group.ToString());
			}
			*/
			
			//var subject = args[1];//"zzzcatzzzcat";
			var regex = new Regex(args[0]);//"cat");
			var match = regex.Match(args[1]);
			
			while (match.Success)
			{
				Console.WriteLine(MatchSuccess, match.Success, match.Index, match.Length);
				match = match.NextMatch();
			}
			Console.WriteLine(match.Success);
		}
	}
}