using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace ServerExample
{
    class Program
    {
        static void Main(string[] args)
        {
            const int WSAETIMEDOUT = 10060;

            Socket serverSocket;

            int bytesReceived, totalReceived = 0;
            byte[] receivedData = new byte[40000];

            byte[] dataToSend = new byte[40000];
            new Random().NextBytes(dataToSend);

            IPAddress ipAddress = Dns.GetHostEntry(Environment.MachineName).AddressList[0];

            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 1800);

            using (Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                listenSocket.Bind(ipEndPoint);
                listenSocket.Listen(1);

                serverSocket = listenSocket.Accept();
                Console.WriteLine("Client has connected to Server.\n");
            }

            try
            {
                Console.Write("Sending data... ");
                int bytesSent = serverSocket.Send(dataToSend, 0, dataToSend.Length, SocketFlags.None);
                Console.WriteLine("{0} bytes sent.\n", bytesSent.ToString());

                serverSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 2000);

                Console.Write("Receiving data... ");
                try
                {
                    do
                    {
                        bytesReceived = serverSocket.Receive(receivedData, 0, receivedData.Length, SocketFlags.None);
                        totalReceived += bytesReceived;
                    }
                    while (bytesReceived != 0);
                }
                catch (SocketException e)
                {
                    if (e.ErrorCode == WSAETIMEDOUT)
                    {
                    }
                    else
                    {
                        Console.WriteLine("{0}: {1}\n", e.GetType().Name, e.Message);
                    }
                }
                finally
                {
                    Console.WriteLine("{0} bytes received.\n", totalReceived.ToString());
                }
            }
            finally
            {
                serverSocket.Shutdown(SocketShutdown.Both);
                Console.WriteLine("Connection shut down.");
                serverSocket.Close();
            }
        }
    }
}
