using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using CodeScales.Http.Entity;
using CodeScales.Http.Methods;
using CodeScales.Http.Protocol;
using CodeScales.Http.Tls;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Tls;

namespace CodeScales.Http.Network
{
	internal class HttpsConnection : Connection
	{
		private CustomTlsClient _tlsClient;

		internal HttpsConnection(HttpConnectionFactory factory, Uri uri, Uri proxy, Certificate clientCertificates,
									AsymmetricKeyParameter asymmetricKeyParameter, Action<Certificate> serverCertificateValidator)
			: base(factory, uri, proxy)
		{
			_tlsClient = new CustomTlsClient(clientCertificates, asymmetricKeyParameter, serverCertificateValidator);
		}

		internal override bool IsConnected()
		{
			return _tlsClient.Connected;
		}

		internal override void Connect()
		{
			_tlsClient.Connect(Uri.Host, Uri.Port, 10000, 10000);
		}

		public override void Close()
		{
			_tlsClient.Close();
		}

		public override void SendRequestHeader(HttpRequest request)
		{
			RequestInitiated = request.Initiated;
			RequestTimeout = request.Timeout;

			if (!IsConnected())
			{
				throw new HttpNetworkException("Tls client is closed or not ready (SendRequestHeader)");
			}
			_tlsClient.Send(Encoding.ASCII.GetBytes(GetRequestHeader(request).ToString()));

			int counter = 0;
			// wait another timeout period for the response to arrive.
			while (!(_tlsClient.Available > 0) && counter < (request.Timeout / 100))
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
				throw new HttpNetworkException("Tls client is closed or not ready (SendRequestHeaderAndEntity)");
			}
			byte[] header = Encoding.ASCII.GetBytes(GetRequestHeader(request).ToString());

			_tlsClient.Send(header);

			if (expectContinue)
			{
				WaitForDataToArrive(2 * 1000);
				if (_tlsClient.Available > 0)
				{
					// now read the 100-continue response
					HttpResponse response = ReceiveResponseHeaders();
					if (response.ResponseCode != 100)
					{
						throw new HttpNetworkException("response returned before entity was sent, but it is not 100-continue");
					}
				}
			}

			byte[] body = httpEntity.Content;
			_tlsClient.Send(body);
		}

		public override HttpResponse ReceiveResponseHeaders()
		{
			if (!IsConnected())
			{
				throw new HttpNetworkException("Tls client is closed or not ready (ReceiveResponseHeaders)");
			}

			WaitForDataToArrive(RequestTimeout);

			var httpResponse = new HttpResponse();
			httpResponse.RequestUri = Uri;
			var header = "";
			var headers = new WebHeaderCollection();

			var bytes = new byte[1];
			do
			{
				bytes[0] = _tlsClient.ReadByte();
				header += Encoding.ASCII.GetString(bytes, 0, 1);
				if (bytes[0] == '\n' && header.EndsWith("\r\n\r\n"))
					break;
			} while (bytes[0] > 0);

			MatchCollection matches = new Regex("[^\r\n]+").Matches(header.TrimEnd('\r', '\n'));
			for (int n = 1; n < matches.Count; n++)
			{
				string[] strItem = matches[n].Value.Split(new char[] {':'}, 2);
				if (strItem.Length > 1)
				{
					if (!strItem[0].Trim().ToLower().Equals("set-cookie"))
					{
						headers.Add(strItem[0].Trim(), strItem[1].Trim());
					}
					else
					{
						headers.Add(strItem[0].Trim(), strItem[1].Trim());
						// HTTPProtocol.AddHttpCookie(strItem[1].Trim(), cookieCollection);
					}
				}
			}

			httpResponse.Headers = headers;

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
				throw new HttpNetworkException("Tls client is closed or not ready (ReceiveResponseEntity)");
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

		private void WaitForDataToArrive(int timeout)
		{
			int counter = 0;
			// wait another timeout period for the response to arrive.
			while (!(_tlsClient.Available > 0) && counter < (timeout / 100))
			{
				counter++;
				Thread.Sleep(100);
			}
		}

		private string ReceiveLine()
		{
			WaitForDataToArrive(RequestTimeout);

			string line = string.Empty;
			byte[] bytes = new byte[10];
			while (_tlsClient.Receive(bytes, 0, 1) > 0)
			{
				line += Encoding.ASCII.GetString(bytes, 0, 1);
				if (bytes[0] == '\n' && line.EndsWith("\r\n"))
					break;
			}
			return line.Replace("\r\n", "");
		}

		private List<byte> ReceiveBytes(long size)
		{
			WaitForDataToArrive(RequestTimeout);

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
			while ((nBytes = _tlsClient.Receive(RecvBuffer, 0, RecvSize)) > 0)
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
