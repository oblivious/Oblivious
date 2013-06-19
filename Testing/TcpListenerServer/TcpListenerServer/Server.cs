using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;

namespace TcpListenerServer
{
    internal class Server
    {
        private TcpListener _listener;
        private bool _listening;
        private object _syncRoot;
        private IPAddress _address;
        private ushort _port;
        private DateTime _lastError;
        private object _outputLock;
        private object _errorLock;

        private const int ERROR_INTERVAL_SECONDS = 60;

        public Server(IPAddress address, ushort port)
        {
            this._address = address;
            this._port = port;
            this._syncRoot = new object();
            this._outputLock = new object();
            this._errorLock = new object();
            this._lastError = DateTime.Now.AddSeconds(-ERROR_INTERVAL_SECONDS);
        }

        public void StopListening()
        {
            this._listening = false;
        }

        public void StartListening()
        {
            while (true)
            {
                try
                {
                    lock (this._syncRoot)
                    {
                        this._listener = new TcpListener(this._address, this._port);
                        this._listener.Start();
                        Console.WriteLine("Server has started listening.");
                        this._listening = true;
                    }
                    do
                    {
                        TcpClient state = this._listener.AcceptTcpClient();
                        ThreadPool.QueueUserWorkItem(new WaitCallback(this.ProcessClient), state);
                    }
                    while (this._listening);
                    this._listener.Stop();
                    Console.WriteLine("Server has stopped listening.");
                }
                catch (Exception e)
                {
                    lock (_errorLock)
                    {
                        using (TextWriter t = new StreamWriter("error.txt", true))
                        {
                            t.WriteLine(DateTime.Now + " - Exception: " + e.ToString());
                        }
                    }
                    try
                    {
                        lock (this._syncRoot)
                        {
                            if (this._listener.Server.Connected)
                                this._listener.Server.Disconnect(true);
                            this._listener.Stop();
                        }
                    }
                    catch (Exception ex)
                    {
                        lock (_errorLock)
                        {
                            using (TextWriter t = new StreamWriter("error.txt", true))
                            {
                                t.WriteLine(DateTime.Now + " - Listener Exception: " + ex.ToString());
                            }
                        }
                    }
                    /*
                    if (this._lastError > (DateTime.Now.AddSeconds(-ERROR_INTERVAL_SECONDS)))
                    {
                        Thread.Sleep(ERROR_INTERVAL_SECONDS * 1000);
                    }
                    this._lastError = DateTime.Now;
                     * */
                }
            }
        }

        protected virtual void ProcessClient(object context)
        {
            TcpClient client = (TcpClient)context;
            try
            {
                using (NetworkStream stream = client.GetStream())
                {
                    stream.ReadTimeout = 60000;
                    stream.WriteTimeout = 60000;

                    int bytesRead = 0;
                    byte[] bytes = null;
                    List<byte> message = new List<byte>();
                    do
                    {
                        try
                        {
                            bytes = new byte[1024];
                            bytesRead = stream.Read(bytes, 0, bytes.Length);
                            if (bytesRead > 0)
                            {
                                message.AddRange(bytes.Take(bytesRead));
                            }
                        }
                        catch (IOException ex)
                        {
                            bytesRead = 0;
                            lock (_errorLock)
                            {
                                using (TextWriter t = new StreamWriter("error.txt", true))
                                {
                                    t.WriteLine(DateTime.Now + " - Network Stream Exception: " + ex.ToString());
                                }
                            }

                        }
                    }
                    while (stream.DataAvailable);
                    lock (_outputLock)
                    {
                        using (TextWriter t = new StreamWriter("output.txt", true))
                        {
                            byte[] output = message.ToArray();
                            t.WriteLine(DateTime.Now + " - Output Received: " + Encoding.ASCII.GetString(output, 0, output.Length));
                        }
                    }
                    byte[] response = Encoding.ASCII.GetBytes("Message received, thank you.");
                    stream.Write(response, 0, response.Length);
                    stream.Close();
                }
            }
            catch (Exception e)
            {
                lock (_errorLock)
                {
                    using (TextWriter t = new StreamWriter("error.txt", true))
                    {
                        t.WriteLine(DateTime.Now + " - TcpClient Exception: " + e.ToString());
                    }
                }
            }
            finally
            {
                if (client != null)
                {
                    client.Close();
                }
            }
        }
    }
}
