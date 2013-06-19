using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace ConsoleApplication8
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri serviceUrl = new Uri("http://192.168.217.17:8888/pretups/ChannelReceiver?REQUEST_GATEWAY_CODE=EzeTOPGW&REQUEST_GATEWAY_TYPE=EXTGW&LOGIN=EzeTOPTH&PASSWORD=Eze1Top&SOURCE_TYPE=EXTGW&SERVICE_PORT=190");

            HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(serviceUrl);

            try
            {
                webRequest.Method = "POST";
                webRequest.Timeout = 18000;
                webRequest.ContentType = "text/xml";
                webRequest.KeepAlive = false;
                webRequest.Pipelined = false;

                WebHeaderCollection headers = webRequest.Headers;
                for (int i = 0; i < headers.Count; i++)
                {
                    Console.WriteLine(headers.GetKey(i) + " " + headers[i]);
                }
                Console.WriteLine("Read/Write timeout: ");
                Console.WriteLine("1: " + DateTime.Now.ToString());
                using (StreamWriter streamOut = new StreamWriter(webRequest.GetRequestStream()))
                {
                    streamOut.Write("<xml>test request</xml>");
                    streamOut.Close();
                }
                Console.WriteLine("2: " + DateTime.Now.ToString());
            }
            finally
            {
                Console.WriteLine("3: " + DateTime.Now.ToString());
                webRequest.Abort();
                Console.WriteLine("4: " + DateTime.Now.ToString());
            }
            Console.ReadKey();
        }
    }
}
