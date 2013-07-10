using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;

namespace BufferedStreamExample
{
    class Program
    {
        const int dataArraySize = 200;
        const int streamBufferSize = 1000;
        const int numberOfLoops = 100;

        static void Main(string[] args)
        {
            string remoteName = Environment.MachineName;

            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            clientSocket.Connect(new IPEndPoint(Dns.GetHostEntry(remoteName).AddressList[0], 1800));

            Console.WriteLine("Client is connected.\n");

            using (Stream netStream = new NetworkStream(clientSocket, true), 
                bufStream = new BufferedStream(netStream, streamBufferSize))
            {
                Console.WriteLine("NetworkStream {0} seeking.\n", bufStream.CanSeek ? "supports" : "does not support");

                if (bufStream.CanWrite)
                {
                    SendData(netStream, bufStream);
                }

                if (bufStream.CanRead)
                {
                    ReceiveData(netStream, bufStream);
                }

                Console.WriteLine("\nShutting down the connection.");

                bufStream.Close();
            }
        }

        static void SendData(Stream netStream, Stream bufStream)
        {
            DateTime startTime;
            double networkTime, bufferedTime;

            byte[] dataToSend = new byte[dataArraySize];
            new Random().NextBytes(dataToSend);

            Console.WriteLine("Sending data using NetworkStream.");
            startTime = DateTime.Now;
            int j = 0;
            for (int i = 0; i < numberOfLoops; i++)
            {
                netStream.Write(dataToSend, 0, dataToSend.Length);
                j++;
            }
            Console.WriteLine("Completed {0} iterations of writing to the network stream.", j);
            networkTime = (DateTime.Now - startTime).TotalSeconds;
            Console.WriteLine("{0} bytes sent in {1} seconds.\n", numberOfLoops * dataToSend.Length, networkTime.ToString("F1"));

            Console.WriteLine("Sending data using BufferedStream.");
            startTime = DateTime.Now;

            j = 0;
            for (int i = 0; i < numberOfLoops; i++)
            {
                bufStream.Write(dataToSend, 0, dataToSend.Length);
                j++;
            }
            Console.WriteLine("Completed {0} iterations of writing to the buffered stream.", j);
            bufStream.Flush();
            bufferedTime = (DateTime.Now - startTime).TotalSeconds;
            Console.WriteLine("{0} bytes sent in {1} seconds.\n", numberOfLoops * dataToSend.Length, bufferedTime.ToString("F1"));

            Console.WriteLine("Sending data using the buffered network stream was {0} {1} that using the network stream alone.\n",
                (networkTime / bufferedTime).ToString("P0"), bufferedTime < networkTime ? "faster" : "slower");
        }

        static void ReceiveData(Stream netStream, Stream bufStream)
        {
            DateTime startTime;
            double networkTime, bufferedTime = 0;
            int bytesReceived = 0;
            byte[] receivedData = new byte[dataArraySize];

            Console.WriteLine("Receiving data using NetworkStream.");
            startTime = DateTime.Now;
            while (bytesReceived < numberOfLoops * receivedData.Length)
            {
                bytesReceived += netStream.Read(receivedData, 0, receivedData.Length);
            }

            networkTime = (DateTime.Now - startTime).TotalSeconds;
            Console.WriteLine("{0} bytes received in {1} seconds.\n", bytesReceived, networkTime.ToString("F1"));

            Console.WriteLine("Receiving data using BufferedStream.");
            bytesReceived = 0;
            startTime = DateTime.Now;

            int numBytesToRead = receivedData.Length * numberOfLoops;
            Console.WriteLine("\n\nStarting bufferedStream read, numBytesToRead={0}\n\n", numBytesToRead);

            int iterations = 0;
            while (numBytesToRead > 0)
            {
                iterations++;
                int n = bufStream.Read(receivedData, 0, receivedData.Length);

                if (n == 0)
                {
                    Console.WriteLine("Ending buffered stream read on iteration {0}, numBytesToRead: {1}", iterations, numBytesToRead);
                    break;
                }

                bytesReceived += n;
                numBytesToRead -= n;
            }

            bufferedTime = (DateTime.Now - startTime).TotalSeconds;
            Console.WriteLine("{0} bytes received in {1} seconds.\n", bytesReceived, bufferedTime.ToString("F1"));

            Console.WriteLine("Received data using the buffered network stream was {0} {1} than using the network stream alone.",
                (networkTime / bufferedTime).ToString("P0"), bufferedTime < networkTime ? "faster" : "slower");

        }
    }
}
