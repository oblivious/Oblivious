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
using CodeScales.Http.Cookies;
using CodeScales.Http.Entity;
using CodeScales.Http.Methods;
using CodeScales.Http.Network;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Tls;

namespace CodeScales.Http
{
    public class HttpClient
    {
        private readonly HttpCookieStore _cookieStore = new HttpCookieStore();
        private Uri _proxy = null;

		private readonly AsymmetricKeyParameter _asymmetricKeyParameter;
		private readonly Certificate _clientCertificates;
		private readonly Action<Certificate> _serverCertificateValidator;

	    private const int DefaultRequestTimeout = 16000;

	    public HttpClient()
	    {
		    MaxRedirects = 1;
	    }

		public HttpClient(
			Certificate clientCertificates,
			AsymmetricKeyParameter asymmetricKeyParameter,
			Action<Certificate> serverCertificateValidator)
			: this()
		{
			_clientCertificates = clientCertificates;
			_asymmetricKeyParameter = asymmetricKeyParameter;
			_serverCertificateValidator = serverCertificateValidator;
		}

	    public Uri Proxy { set { _proxy = value; } }

		public int MaxRedirects { get; set; }

        public HttpResponse Execute(HttpRequest request)
        {
            return Navigate(request, null);
        }

        public HttpResponse Execute(HttpRequest request, HttpBehavior httpBehavior)
        {
            return Navigate(request, httpBehavior);
        }

        private HttpResponse Navigate(HttpRequest request, HttpBehavior httpBehavior)
        {
	        request.Initiated = DateTime.UtcNow;
	        if (request.Timeout == 0)
				request.Timeout = DefaultRequestTimeout;

            bool ContinueRedirect = true;
            HttpResponse response = null;

            var connFactory = new HttpConnectionFactory();

			var connection = GetConnectionForUriScheme(connFactory, request.Uri);

            HttpBehavior.RedirectStep rs = null;
            string redirectUri = null;
            int responseCode = 0;
            int redirectCounter = 0;

            try
            {
                while (ContinueRedirect)
                {
                    try
                    {
                        response = SendRequestAndGetResponse(connection, request);
                        redirectUri = response.Location;
                        responseCode = response.ResponseCode;

                        // response code 100 means that we need to wait for another response
                        // and receive the response from the socket again on the same connection
                        if (responseCode == 100)
                        {
                            response = GetResponse(connection);
                            redirectUri = response.Location;
                            responseCode = response.ResponseCode;
                        }

                        if (httpBehavior != null)
                        {
                            rs = httpBehavior.GetNextStep();
                            rs.Compare(responseCode, redirectUri);
                            ContinueRedirect = !httpBehavior.IsEmpty();
                        }
                        else
                        {
                            ContinueRedirect = (redirectCounter < MaxRedirects && (responseCode == 301 || responseCode == 302));
                            redirectCounter++;
                        }

                        if (ContinueRedirect)
                        {
                            request = new HttpGet(new Uri(redirectUri));
                            // make sure the connection is still open and redirect url is from the same host
                            connection = GetConnectionForUriScheme(connFactory, request.Uri);
                        }

                    }
                    catch (Exception ex)
                    {
                        int i = 0;
                        throw ex;
                    }
                }
            }
            finally
            {
                connection.Close();
            }
            return response;      

        }

		private Connection GetConnectionForUriScheme(HttpConnectionFactory connFactory, Uri uri)
	    {
			Connection connection;
			if (uri.Scheme.Equals(Uri.UriSchemeHttps, StringComparison.InvariantCultureIgnoreCase))
				connection = connFactory.GetConnection(uri, _proxy, _asymmetricKeyParameter,
														_clientCertificates, _serverCertificateValidator);
			else
				connection = connFactory.GetConnnection(uri, this._proxy);
			return connection;
	    }

	    private HttpResponse SendRequestAndGetResponse(Connection connection, HttpRequest request)
        {
            _cookieStore.WriteCookiesToRequest(request);
            
            // if we need to send a body (not only headers)
            if (request.GetType().GetInterface("HttpEntityEnclosingRequest") != null)
            {
                var heer = (HttpEntityEnclosingRequest)request;
                connection.SendRequestHeaderAndEntity(request, heer.Entity, heer.ExpectContinue);
            }
            else
            {
                connection.SendRequestHeader(request);
            }

            return GetResponse(connection);
        }

        private HttpResponse GetResponse(Connection connection)
        {
            HttpResponse response = connection.ReceiveResponseHeaders();
            _cookieStore.ReadCookiesFromResponse(response);
            connection.ReceiveResponseEntity(response);
            
            // for response code 100 we expect another http message to arrive later
            if (response.ResponseCode != 100)
            {
                connection.CheckKeepAlive(response);
            }
            return response;
        }

        private string GetRedirectUri(Uri uri)
        {
            if (uri != null)
                return uri.AbsoluteUri;
            else
                return "";
        }
    }
}
