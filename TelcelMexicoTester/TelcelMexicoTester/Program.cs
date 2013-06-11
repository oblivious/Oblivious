﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TelcelMexicoTester
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Writer writer = new Writer();
			Console.SetOut(writer);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
}
