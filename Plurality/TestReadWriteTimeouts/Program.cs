using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.IO;

namespace TestReadWriteTimeouts
{
	class Program
	{
		static void Main(string[] args)
		{
			TcpClient client = new TcpClient();

			Console.WriteLine(client.ReceiveTimeout);
			Console.WriteLine(client.SendTimeout);
		}
	}
}
