using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace TcpTestClient
{
    class Program
    {
        private static object _errorLock;

        private static DateTime endTime;

        static Program()
        {
            _errorLock = new object();
            endTime = DateTime.Now.AddMinutes(5);
        }

        static void Main(string[] args)
        {
            /*
            try
            {
                TcpClient client = new TcpClient();
                client.Connect("127.0.0.1", 8101);

                Console.WriteLine("Sending for client...");
                ThreadPool.QueueUserWorkItem(SendRequest, client);

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in Main: " + e.ToString());
            }
            */

            DateTime currentTime = DateTime.Now;
            
            while (currentTime < endTime)
            {
                try
                {
                    ThreadPool.QueueUserWorkItem(SendRequest);
                }
                catch (Exception e)
                {
                    lock (_errorLock)
                    {
                        using (TextWriter t = new StreamWriter("error.txt", true))
                        {
                            t.WriteLine(DateTime.Now + " - Thread Queueing Exception: " + e.ToString());
                        }
                    }
                }
                Thread.Sleep(10);
                currentTime = DateTime.Now;
            }
            
            Console.ReadKey();
        }

        static void SendRequest(object context)
        {
            //TcpClient client = (TcpClient)context;
            TcpClient client = null;

            try
            {
                client = new TcpClient();
                client.Connect("127.0.0.1", 8101);

                using (NetworkStream networkStream = client.GetStream())
                {
                    byte[] output = Encoding.ASCII.GetBytes("Hi, my name is...");
                    networkStream.Write(output, 0, output.Length);
                    networkStream.Flush();


                    int bytesRead = 0;
                    byte[] bytes = new byte[1024];
                    do
                    {
                        try
                        {
                            bytesRead = networkStream.Read(bytes, 0, bytes.Length);
                        }
                        catch (IOException ex)
                        {
                            lock (_errorLock)
                            {
                                using (TextWriter t = new StreamWriter("error.txt", true))
                                {
                                    t.WriteLine(DateTime.Now + " - Client Thread Exception: " + ex.ToString());
                                }
                            }
                            bytesRead = 0;
                            Console.WriteLine("Network stream error: " + ex.ToString());
                        }
                    }
                    while (networkStream.DataAvailable);

                    networkStream.Close();

                    string response = Encoding.ASCII.GetString(bytes);
                    Console.WriteLine("Server's response: " + response.Trim());
                }
            }
            catch (Exception e)
            {
                lock (_errorLock)
                {
                    using (TextWriter t = new StreamWriter("error.txt", true))
                    {
                        t.WriteLine(DateTime.Now + " - Client Thread Exception: " + e.ToString());
                    }
                }
            }
            finally
            {
                try
                {
                    if (client != null && client.Client.Connected)
                    {
                        try
                        {
                            client.Client.Close();
                        }
                        catch (Exception e)
                        {
                            lock (_errorLock)
                            {
                                using (TextWriter t = new StreamWriter("error.txt", true))
                                {
                                    t.WriteLine(DateTime.Now + " - Client Thread Exception: " + e.ToString());
                                }
                            }
                        }
                    }
                    client.Close();
                }
                catch { }
            }
        }
    }
}
