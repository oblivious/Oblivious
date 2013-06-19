using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace ConsoleApplication7
{
    class Program
    {
        public static bool TrustAllCertificateCallback(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        static void Main(string[] args)
        {
            string response = string.Empty;

            Uri serviceUrl = new Uri("http://192.168.30.150:3002/ezetop/" + "top_up?msisdn=7999648&amount=25&ezetopid=910000");

            ServicePointManager.ServerCertificateValidationCallback = TrustAllCertificateCallback;

            WebRequest webRequest = WebRequest.Create(serviceUrl);
            webRequest.Timeout = 18000;

            try
            {
                using (StreamReader streamIn = new StreamReader(((HttpWebResponse)webRequest.GetResponse()).GetResponseStream()))
                {
                    response = streamIn.ReadToEnd();
                    streamIn.Close();
                }
            }
            finally
            {
                webRequest.Abort();
            }

            try
            {
                string[] responseValues = response.Split('\n');
                for (int i = 0; i < responseValues.Length; i++)
                {
                    Console.WriteLine("responseValues[" + i + "]: " + responseValues[i]);
                }
                Console.WriteLine("Status: " + responseValues[0].Substring(0, responseValues[0].Length - 5).Trim());
                Console.WriteLine("StatusCode: " + responseValues[4].Substring(4).Trim());
                Console.WriteLine("TransactionID: " + responseValues[3].Substring(14, responseValues[3].Length - 18).Trim());
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception occurred while parsing the response: " + e.ToString());
            }

            serviceUrl = new Uri("http://192.168.30.150:3002/ezetop/" + "validate?msisdn=7999648");

            webRequest = WebRequest.Create(serviceUrl);
            webRequest.Timeout = 18000;

            try
            {
                using (StreamReader streamIn = new StreamReader(((HttpWebResponse)webRequest.GetResponse()).GetResponseStream()))
                {
                    response = streamIn.ReadToEnd();
                    streamIn.Close();
                }
            }
            finally
            {
                webRequest.Abort();
            }

            try
            {
                string responseValue = response.Trim();
                Console.WriteLine("Response: " + response);
                Console.WriteLine("Status: " + responseValue.Substring(12, responseValue.Length - 26));
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception occurred while parsing the response: " + e.ToString());
            }
            Console.ReadKey();
        }
    }
}
