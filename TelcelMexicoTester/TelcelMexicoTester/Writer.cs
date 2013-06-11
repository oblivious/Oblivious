using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TelcelMexicoTester
{
	class Writer : System.IO.TextWriter
	{
		public override Encoding Encoding
		{
			get { throw new NotImplementedException(); }
		}

		public override void Write(string value)
		{
			Form1.GetInstance().WriteLine(value);
			this.Flush();
			base.Write(value);
		}

		public override void WriteLine(string value)
		{
			this.Write(value + "\n");
		}
	}
}
