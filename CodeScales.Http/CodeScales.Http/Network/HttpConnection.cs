/* Copyright (c) 2010 CodeScales.com
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*/

/* Modified 15th January 2014 by Donal O Byrne
 * (Connection abstract class references added to replace HttpConnection)
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using CodeScales.Http.Methods;
using CodeScales.Http.Entity;
using CodeScales.Http.Protocol;

namespace CodeScales.Http.Network
{
    internal class HttpConnection : Connection
    {
        //private bool m_isBusy = false;
        private Socket m_socket = null;

        internal HttpConnection(HttpConnectionFactory factory, Uri uri, Uri proxy)
			:base(factory, uri, proxy)
        {
        }

        internal override bool IsConnected()
        {
            return (m_socket != null
                    && m_socket.Connected);
        }
        
        internal override void Connect()
        {
            m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint remoteEP = new IPEndPoint(Dns.Resolve(this.EndPointUri.Host).AddressList[0], EndPointUri.Port);
            m_socket.Connect(remoteEP);

            m_socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 60 * 1000);
            m_socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 60 * 1000);
        }

        public override void Close()
        {
            if (m_socket != null)
            {
                m_socket.Close();
            }
        }
                
        public override void SendRequestHeader(HttpRequest request)
		{
			RequestInitiated = request.Initiated;
			RequestTimeout = request.Timeout;

            if (!this.IsConnected())
            {
                throw new HttpNetworkException("Socket is closed or not ready");
            }
            this.m_socket.Send(Encoding.ASCII.GetBytes(GetRequestHeader(request).ToString()));
            
            int counter = 0;
            // wait another timeout period for the response to arrive.
            while (!(this.m_socket.Available > 0) && counter < (request.Timeout / 100))
            {
                counter++;
                Thread.Sleep(100);
            }
        }
                
        public override void SendRequestHeaderAndEntity(HttpRequest request, HttpEntity httpEntity, bool expectContinue)
		{
			RequestInitiated = request.Initiated;
			RequestTimeout = request.Timeout;

            if (!this.IsConnected())
            {
                throw new HttpNetworkException("Socket is closed or not ready");
            }
            byte[] header = Encoding.ASCII.GetBytes(GetRequestHeader(request).ToString());
            byte[] body = httpEntity.Content;
            
            // first send the headers
            Send(header, 0, header.Length, TimeRemaining);
            
            // then look for 100-continue response for no more than 2 seconds
            if (expectContinue)
            {
                // 2 seconds timeout for 100-continue
                WaitForDataToArriveAtSocket(2 * 1000);
                if (this.m_socket.Available > 0)
                {
                    // now read the 100-continue response
                    HttpResponse response = ReceiveResponseHeaders();
                    if (response.ResponseCode != 100)
                    {
                        throw new HttpNetworkException("reponse returned before entity was sent, but it is not 100-continue");
                    }
                }
            }

            byte[] message = body;   

            int sendBufferSize = this.m_socket.SendBufferSize;
            int size = (message.Length > sendBufferSize) ? sendBufferSize : message.Length;
            for (int i = 0; i < message.Length; i = i + size)
            {
                int remaining = (size < (message.Length - i)) ? size : (message.Length - i);
                Send(message, i, remaining, TimeRemaining);
            }
            
        }

        private void WaitForDataToArriveAtSocket(int timeout)
        {
            int counter = 0;
            // wait another timeout period for the response to arrive.
            while (!(this.m_socket.Available > 0) && counter < (timeout / 100))
            {
                counter++;
                Thread.Sleep(100);
            }
        }

        private void Send(byte[] buffer, int offset, int size, int timeout)
        {
            int startTickCount = Environment.TickCount;
            int sent = 0;  // how many bytes is already sent
            do
            {
                if (Environment.TickCount > startTickCount + timeout)
                    throw new Exception("Timeout.");
                try
                {
                    sent += this.m_socket.Send(buffer, offset + sent, size - sent, SocketFlags.None);
                }
                catch (SocketException ex)
                {
                    if (ex.SocketErrorCode == SocketError.WouldBlock ||
                        ex.SocketErrorCode == SocketError.IOPending ||
                        ex.SocketErrorCode == SocketError.NoBufferSpaceAvailable)
                    {
                        // socket buffer is probably full, wait and try again
                        Thread.Sleep(30);
                    }
                    else
                        throw ex;  // any serious error occurr
                }
            } while (sent < size);
        }

        public override HttpResponse ReceiveResponseHeaders()
        {
            if (!this.IsConnected())
            {
                throw new HttpNetworkException("Socket is closed or not ready");
            }

            WaitForDataToArriveAtSocket(RequestTimeout);

            HttpResponse httpResponse = new HttpResponse();
            httpResponse.RequestUri = this.Uri;
            string Header = "";
            WebHeaderCollection Headers = new WebHeaderCollection();

            byte[] bytes = new byte[10];
            while (this.m_socket.Receive(bytes, 0, 1, SocketFlags.None) > 0)
            {
                Header += Encoding.ASCII.GetString(bytes, 0, 1);
                if (bytes[0] == '\n' && Header.EndsWith("\r\n\r\n"))
                    break;
            }
            MatchCollection matches = new Regex("[^\r\n]+").Matches(Header.TrimEnd('\r', '\n'));
            for (int n = 1; n < matches.Count; n++)
            {
                string[] strItem = matches[n].Value.Split(new char[] { ':' }, 2);
                if (strItem.Length > 1)
                {
                    if (!strItem[0].Trim().ToLower().Equals("set-cookie"))
                    {
                        Headers.Add(strItem[0].Trim(), strItem[1].Trim());
                    }
                    else
                    {
                        Headers.Add(strItem[0].Trim(), strItem[1].Trim());
                        // HTTPProtocol.AddHttpCookie(strItem[1].Trim(), cookieCollection);
                    }
                }
            }

            httpResponse.Headers = Headers;

            // set the response code
            if (matches.Count > 0)
            {
                try
                {
                    string firstLine = matches[0].Value;
                    int index1 = firstLine.IndexOf(" ");
                    int index2 = firstLine.IndexOf(" ", index1 + 1);
                    httpResponse.ResponseCode = Int32.Parse(firstLine.Substring(index1 + 1, index2 - index1 - 1));
                }
                catch (Exception ex)
                {
                    throw new HttpNetworkException("Response Code is missing from the response");
                }
            }          

            return httpResponse;

        }

        public override void ReceiveResponseEntity(HttpResponse response)
        {
            if (!this.IsConnected())
            {
                throw new HttpNetworkException("Socket is closed or not ready");
            }

            string chunkedHeader = EntityUtils.GetTransferEncoding(response.Headers);
            if (chunkedHeader != null
                && chunkedHeader.ToLower().Equals(HTTP.CHUNK_CODING))
            {
                List<byte> byteBuffer = new List<byte>();
                BasicHttpEntity httpEntity = new BasicHttpEntity();
                httpEntity.ContentLength = 0;
                httpEntity.ContentType = EntityUtils.GetContentType(response.Headers);
                response.Entity = httpEntity;

                int chunkSize = EntityUtils.ConvertHexToInt(ReceiveLine());
                while (chunkSize > 0)
                {
                    // for each chunk...
                    byteBuffer.AddRange(ReceiveBytes(chunkSize));
                    httpEntity.ContentLength += chunkSize;
                    string test = ReceiveLine();
                    chunkSize = EntityUtils.ConvertHexToInt(ReceiveLine());
                }

                httpEntity.Content = byteBuffer.ToArray();
            }
            else
            {
                // TODO: support "Transfer-Encoding: chunked"
                int length = EntityUtils.GetContentLength(response.Headers);
                if (length > 0)
                {
                    BasicHttpEntity httpEntity = new BasicHttpEntity();
                    httpEntity.ContentLength = length;
                    httpEntity.Content = ReceiveBytes(length).ToArray();
                    httpEntity.ContentType = EntityUtils.GetContentType(response.Headers);
                    response.Entity = httpEntity;
                }
            }
                         
            return;
        }

        private string ReceiveLine()
        {
            WaitForDataToArriveAtSocket(RequestTimeout);

            string line = string.Empty;
            byte[] bytes = new byte[10];
            while (this.m_socket.Receive(bytes, 0, 1, SocketFlags.None) > 0)
            {
                line += Encoding.ASCII.GetString(bytes, 0, 1);
                if (bytes[0] == '\n' && line.EndsWith("\r\n"))
                    break;
            }
            return line.Replace("\r\n","");
        }

        private List<byte> ReceiveBytes(long size)
        {
            WaitForDataToArriveAtSocket(RequestTimeout);

            List<byte> byteBuffer = new List<byte>();
            int minSize = 10240;
            if (size < 10240)
            {
                minSize = (int)size;
            }
            byte[] RecvBuffer = new byte[minSize];
            long nBytes, nTotalBytes = 0;
            int RecvSize = minSize;
            
            // loop to receive response buffer
            while ((nBytes = this.m_socket.Receive(RecvBuffer, 0, RecvSize, SocketFlags.None)) > 0)
            {
                // increment total received bytes
                nTotalBytes += nBytes;

                // add received buffer to response string
                if (nBytes < minSize)
                {
                    byte[] smallByteArray = new byte[nBytes];
                    for (int i = 0; i < nBytes; i++)
                    {
                        smallByteArray[i] = RecvBuffer[i];
                    }
                    byteBuffer.AddRange(smallByteArray);
                }
                else
                {
                    byteBuffer.AddRange(RecvBuffer);
                }

                if (nTotalBytes >= size && size > 0)
                    break;
                else
                {
                    long nBytesLeft = size - nTotalBytes;
                    if (nBytesLeft < RecvSize)
                    {
                        RecvSize = (int)nBytesLeft;
                    }
                }
            }
            return byteBuffer;
        }
    }
}
