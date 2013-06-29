using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Net.Sockets;
using System.Net;
using System.Security.Authentication;
using System.Threading;
using System.Collections;

namespace System
{
    public static class StringExtensions
    {
        public static string Indented(this String input, int indent)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < indent * 3; i++)
            {
                sb.Append(" ");
            }
            sb.Append(input);
            return sb.ToString();
        }
    }
}

namespace _70536_Exam_Questions.Questions
{
    class Q037
    {
        private static Thread _serverThread;

        public static void Run()
        {
            Console.WriteLine("\nQ037 Start:\n");

            Console.WriteLine("The base class for SslStream is an abstract class named AuthenticatedStream which itself inherits from the Stream base class.\n");

            Console.WriteLine("The AuthenticatedStream specific properties are:\n");
            Console.WriteLine("IsAuthenticated".Indented(1));
            Console.WriteLine("IsEncrypted".Indented(1));
            Console.WriteLine("IsMutuallyAuthenticated".Indented(1) + ", which means both client and server have been authenticated.");
            Console.WriteLine("IsServer".Indented(1) + ", which says if the \"Local side of the connection was authenticated as the server\"...");
            Console.WriteLine("IsSigned".Indented(1) + ", which says if the data sent using the stream is signed.");
            Console.WriteLine("LeaveInnerStreamOpen".Indented(1) + ", don't close the underlying stream when closing this AuthenticatedStream?");

            Console.WriteLine("\nThere are no methods that are additional to those inherited from Stream.\n");

            Console.WriteLine("\nOn to the actual SslStream!\n");
            Console.WriteLine("Constructors:\n");
            Console.WriteLine("SslStream(Stream)".Indented(1));
            Console.WriteLine("SslStream(Stream, Boolean)".Indented(1));
            Console.WriteLine("SslStream(Stream, Boolean, RemoteCertificateValidationCallback)".Indented(1));
            Console.WriteLine("SslStream(Stream, Boolean, RemoteCertificateValidationCallback, LocalCertificateSelectionCallback)".Indented(1));
            Console.WriteLine("SslStream(Stream, Boolean, RemoteCertificateValidationCallback, LocalCertificateSelectionCallback, EncryptionPolicy)".Indented(1));

            Console.WriteLine("\nThe Boolean controls the stream closure behaviour previously discussed.".Indented(1));
            Console.WriteLine("An example of a RemoteCertificateValidationCallback delegate is included in this source, but it doesn't work.".Indented(1));
            Console.WriteLine("The LocalCertificateSelectionCallback is uesful if you have multiple certs and needs to choose a cert dynamically.".Indented(1));
            Console.WriteLine("The certificates are passed into the callback method, signature: X509Certificate LocalCertificateSelectionCallback(".Indented(1));
            Console.WriteLine("object sender, string targetHost, X509CertificateCollection localCertificates, X509Certificate remoteCertificate, string[] acceptableIssuers)".Indented(1));


            /*
            // Create a TCP/IP client socket.
            // Machine name is the host running the server application.
            TcpClient client = new TcpClient(Dns.GetHostName(), 443);
            Console.WriteLine("Client connected.");
            // Create an SSL stream that will close the client's stream.
            SslStream sslStream = new SslStream(
                client.GetStream(),
                false,
                ValidateServerCertificate,
                null);

            try
            {
                sslStream.AuthenticateAsClient(Dns.GetHostName());
            }
            catch (AuthenticationException ae)
            {
                Console.WriteLine("Exception: {0}", ae.Message);
                if (ae.InnerException != null)
                    Console.WriteLine("Inner exception: {0}", ae.InnerException.Message);

                Console.WriteLine("Authentication failed - closing the connection.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Sorry honey, that just ain't gonna work: " + e.ToString());
            }
            finally
            {
                client.Close();
            }
            */
            try
            {
                Console.WriteLine("\n\nCreating the server thread.");
                _serverThread = new Thread(new ThreadStart(SslTcpServer.RunServer));
                _serverThread.IsBackground = true;
                _serverThread.Start();
                Console.WriteLine("Server thread started.");

                Thread.Sleep(200);
                Console.WriteLine("Running the client.");
                SslTcpClient.RunClient();

                Console.WriteLine("Client finished running.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went kablooie: " + e.ToString());
            }

            Console.ReadKey();
            Console.WriteLine("Stopping the server.");
            SslTcpServer.StopServer();

            Console.WriteLine("\nQ037 End...\n");
        }

        //Example of a RemoteCertificateValidationDelegate
        public static bool ValidateServerCertificate(
            object sender,
            X509Certificate certificate,
            X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
        {
            return true;
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            Console.WriteLine("Certificate error: {0}", sslPolicyErrors);

            // Do not allow this client to communicate with unauthenticated servers.
            return false;
        }
    }

    public sealed class SslTcpServer
    {
        static bool listening = true;
        static X509Certificate serverCertificate = null;
        // The certificate parameter specifies the name of the file containing the machine certificate.
        public static void RunServer()
        {
            serverCertificate = X509Certificate.CreateFromCertFile("TempCert.cer");
            // Create a TCP/IP (IPv4) socket and listen for incoming connections.
            TcpListener listener = new TcpListener(IPAddress.Any, 30000);
            listener.Start();
            while (listening)
            {
                Console.WriteLine("Server: Waiting for a client to connect...");
                // Application blocks while waiting for incoming connections.
                TcpClient client = listener.AcceptTcpClient();
                ProcessClient(client);
            }
            Console.WriteLine("Server: SslTcpServer has exited");
            listener.Stop();
        }

        public static void StopServer()
        {
            listening = false;
        }

        static void ProcessClient(TcpClient client)
        {
            // A client has connected. Create the SslStream using the client's network stream.
            SslStream sslStream = new SslStream(client.GetStream(), false);
            // Authenticate the server but don't require the client to authenticate.
            try
            {
                sslStream.AuthenticateAsServer(serverCertificate, false, SslProtocols.Tls, true);
                // Display the properties and settings for the authenticated stream.
                DisplaySecurityLevel(sslStream);
                DisplaySecurityServices(sslStream);
                DisplayCertificateInformation(sslStream);
                DisplayStreamProperties(sslStream);

                // Set timeouts for the read and write to 5 seconds.
                sslStream.ReadTimeout = 5000;
                sslStream.WriteTimeout = 5000;

                // Read a message from the client.
                Console.WriteLine("Server: Waiting for client message...");
                string messageData = ReadMessage(sslStream);
                Console.WriteLine("Server: Received: {0}", messageData);

                // Write a message to the client.
                byte[] message = Encoding.UTF8.GetBytes("Hello from the server.<EOF>");
                Console.WriteLine("Server: Sending hello message.");
                sslStream.Write(message);
                sslStream.Flush();
                Thread.Sleep(200);
            }
            catch (AuthenticationException ae)
            {
                Console.WriteLine("Server: Exception: {0}", ae.Message);
                if (ae.InnerException != null)
                {
                    Console.WriteLine("Server: Inner Exception: {0}", ae.InnerException.Message);
                }
                Console.WriteLine("Server: Authentication failed - closing the connection.");
            }
            finally
            {
                // The client stream will be closed with the sslStream because we specified this behvaiour when creating the sslStream.
                sslStream.Close();
                client.Close();
            }
        }

        private static string ReadMessage(SslStream sslStream)
        {
            // Read the message sent by the client.
            // The client signals the end of the message using the "<EOF>" marker.
            byte[] buffer = new byte[2048];
            StringBuilder messageData = new StringBuilder();
            int bytes = -1;
            do
            {
                // Read the client's test message.
                bytes = sslStream.Read(buffer, 0, buffer.Length);

                // Use decoder class to convert from bytes to UTF8
                // in case a character spans two buffers.
                Decoder decoder = Encoding.UTF8.GetDecoder();
                char[] chars = new char[decoder.GetCharCount(buffer, 0, bytes)];
                decoder.GetChars(buffer, 0, bytes, chars, 0);
                messageData.Append(chars);
                // Check for EOF or an empty message.
                if (messageData.ToString().IndexOf("<EOF>") != -1)
                {
                    break;
                }

            } while (bytes != 0);

            return messageData.ToString();
        }

        static void DisplaySecurityLevel(SslStream stream)
        {
            Console.WriteLine("Server: Cipher: {0} strength {1}", stream.CipherAlgorithm, stream.CipherStrength);
            Console.WriteLine("Server: Hash: {0} strength {1}", stream.HashAlgorithm, stream.HashStrength);
            Console.WriteLine("Server: Key exchange: {0} strength {1}", stream.KeyExchangeAlgorithm, stream.KeyExchangeStrength);
            Console.WriteLine("Server: Protocol: {0}", stream.SslProtocol);
        }

        static void DisplaySecurityServices(SslStream stream)
        {
            Console.WriteLine("Server: Is authenticated: {0} as server? {1}", stream.IsAuthenticated, stream.IsServer);
            Console.WriteLine("Server: IsSigned: {0}", stream.IsSigned);
            Console.WriteLine("Server: IsEncrypted: {0}", stream.IsEncrypted);
        }

        static void DisplayStreamProperties(SslStream stream)
        {
            Console.WriteLine("Server: Can read: {0}, write {1}", stream.CanRead, stream.CanWrite);
            Console.WriteLine("Server: Can timeout: {0}", stream.CanTimeout);
        }

        static void DisplayCertificateInformation(SslStream stream)
        {
            Console.WriteLine("Server: Certificate revocation list checked: {0}", stream.CheckCertRevocationStatus);

            X509Certificate localCertificate = stream.LocalCertificate;
            if (stream.LocalCertificate != null)
            {
                Console.WriteLine("Server: Local cert was issued to {0} and is valid from {1} until {2}.",
                    localCertificate.Subject,
                    localCertificate.GetEffectiveDateString(),
                    localCertificate.GetExpirationDateString());
            }
            else
            {
                Console.WriteLine("Server: Local certificate is null.");
            }

            // Display the properties of the client's certificate.
            X509Certificate remoteCertificate = stream.RemoteCertificate;
            if (stream.RemoteCertificate != null)
            {
                Console.WriteLine("Server: Remote cert was issued to {0} and is valid from {1} until {2}.",
                    remoteCertificate.Subject,
                    remoteCertificate.GetEffectiveDateString(),
                    remoteCertificate.GetExpirationDateString());
            }
            else
            {
                Console.WriteLine("Server: Remote certificate is null.");
            }
        }
    }

    public class SslTcpClient
    {
        private static Hashtable certificateErrors = new Hashtable();
        
        // The following method is invoked by the RmoteCertificateValidationDelegate.
        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;
            Console.WriteLine("Client: Certificate error: {0}", sslPolicyErrors);

            if (chain.ChainStatus.Length == 1 &&
                (sslPolicyErrors == SslPolicyErrors.RemoteCertificateChainErrors || certificate.Subject == certificate.Issuer) &&
                chain.ChainStatus[0].Status == X509ChainStatusFlags.UntrustedRoot)
            {
                Console.WriteLine("Client: Ignoring the errors...");
                return true;
            }

            // Do not allow this client to communicate with unauthenticated servers.
            return false;
        }

        public static void RunClient()
        {
            // Create a TCP/IP client socket. machineName is the host running the server application.
            TcpClient client = new TcpClient("Amphion", 30000);
            Console.WriteLine("Client: Client connected.");
            
            // Create an SSL stream that will close the client's stream.
            SslStream sslStream = new SslStream(
                client.GetStream(),
                false,
                new RemoteCertificateValidationCallback(ValidateServerCertificate),
                null);

            try
            {
                sslStream.AuthenticateAsClient("FakeServerName");
            }
            catch (AuthenticationException ae)
            {
                Console.WriteLine("Client: Exception: {0}", ae.Message);
                if (ae.InnerException != null)
                {
                    Console.WriteLine("Client: Inner exception: {0}", ae.InnerException.Message);
                }
                Console.WriteLine("Client: Authentication failed - closing the connection.");
                client.Close();
                return;
            }

            // Encode a test message into a byte array. Signal the end of the message using the "<EOF>".
            byte[] message = Encoding.UTF8.GetBytes("Hello from the client.<EOF>");

            // Send hello message to the server.
            sslStream.Write(message);
            sslStream.Flush();

            Thread.Sleep(200);

            // Read message from the server.
            string serverMessage = ReadMessage(sslStream);
            Console.WriteLine("Client: Server says: {0}", serverMessage);
            // Close the client connection.
            client.Close();
            Console.WriteLine("Client: Client closed.");
        }

        static string ReadMessage(SslStream sslStream)
        {
            // Read the message sent by the server. The end of the message is signalled using the "<EOF>" marker.
            byte[] buffer = new byte[2048];
            StringBuilder messageData = new StringBuilder();
            int bytes = -1;
            do
            {
                Console.WriteLine("Client: Reading from the stream...");
                bytes = sslStream.Read(buffer, 0, buffer.Length);
                Console.WriteLine("Client: Read {0} bytes from the stream.", bytes);
                // Use Decoder class to convert from bytes to UTF8 in case a character spawns two buffers.
                Decoder decoder = Encoding.UTF8.GetDecoder();
                char[] chars = new char[decoder.GetCharCount(buffer, 0, bytes)];

                decoder.GetChars(buffer, 0, bytes, chars, 0);

                messageData.Append(chars);
                // Check for EOF.
                if (messageData.ToString().IndexOf("<EOF>") != -1)
                    break;
            } while (bytes != 0);

            return messageData.ToString();
        }
    }
}