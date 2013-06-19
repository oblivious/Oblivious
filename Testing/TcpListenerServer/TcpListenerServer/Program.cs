using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;

namespace TcpListenerServer
{
    class Program
    {
        private static bool listening = true;

        static void Main(string[] args)
        {
            Console.WriteLine("Creating server...");
            Server server = new Server(IPAddress.Any, 8101);
            var serverThread = new Thread(server.StartListening);
            serverThread.IsBackground = true;
            Console.WriteLine("Starting server thread...");
            serverThread.Start();
            Console.WriteLine("Press x to restart the server, press any other key to exit");
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.KeyChar.Equals('x'))
                {
                    Console.WriteLine();
                    server.StopListening();
                }
                else
                    break;
            }
        }
    }
}
