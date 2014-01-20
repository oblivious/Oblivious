using System;
using System.Text;
using CodeScales.Http.Entity;
using CodeScales.Http.Methods;
using CodeScales.Http.Protocol;

namespace CodeScales.Http.Network
{
	internal abstract class Connection
	{
		private HttpConnectionFactory _factory;
		private readonly Uri _proxy;

		protected Connection(HttpConnectionFactory factory, Uri uri, Uri proxy)
		{
			_factory = factory;
			Uri = uri;
			_proxy = proxy;
		}

		protected int RequestTimeout;
		protected DateTime RequestInitiated;

		protected int TimeRemaining
		{
			get
			{
				var millisecondsRemaining = (RequestInitiated.AddMilliseconds(RequestTimeout) - DateTime.UtcNow).TotalMilliseconds;
				return millisecondsRemaining > 0 ? (int)millisecondsRemaining : 0;
			}
		}

		internal Uri Uri { get; private set; }
		internal Uri EndPointUri { get { return _proxy ?? Uri; } }

		public bool IsBusy { get; set; }

		internal abstract bool IsConnected();
		internal abstract void Connect();

		public abstract void Close();
		public abstract void SendRequestHeader(HttpRequest request);
		public abstract void SendRequestHeaderAndEntity(HttpRequest request, HttpEntity httpEntity, bool expectContinue);
		public abstract HttpResponse ReceiveResponseHeaders();
		public abstract void ReceiveResponseEntity(HttpResponse response);

		public void CheckKeepAlive(HttpResponse response)
		{
			var headers = response.Headers;

			if (!((headers[HTTP.CONN_DIRECTIVE] != null && headers[HTTP.CONN_DIRECTIVE].ToLower() == HTTP.CONN_KEEP_ALIVE) ||
				(headers[HTTP.PROXY_CONN_DIRECTIVE] != null && headers[HTTP.PROXY_CONN_DIRECTIVE].ToLower() == HTTP.CONN_KEEP_ALIVE)))
			{
				Close();
			}
		}

		protected StringBuilder GetRequestHeader(HttpRequest request)
		{
			var sb = new StringBuilder();
			sb.AppendLine(request.GetRequestLine(_proxy != null));
			sb.AppendLine("Host: " + Uri.Host);
			sb.Append(request.Headers);
			return sb;
		}
	}
}
