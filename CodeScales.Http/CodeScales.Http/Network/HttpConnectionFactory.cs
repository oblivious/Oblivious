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
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Tls;

namespace CodeScales.Http.Network
{
    internal class HttpConnectionFactory
    {
        public Connection GetConnnection(Uri uri, Uri proxy)
        {
            return GetConnectionPrivate(uri, proxy);
        }

		internal Connection GetConnection(Uri uri, Uri proxy, AsymmetricKeyParameter asymmetricKeyParameter, 
											Certificate clientCertificates, Action<Certificate> serverCertificateValidator)
		{
			return GetConnectionHttpsPrivate(uri, proxy, asymmetricKeyParameter,
												clientCertificates, serverCertificateValidator);
		}
        
        // make sure the connection is still open and from the same host
        public Connection GetConnnection(Uri uri, Uri proxy, Connection liveConnection)
        {
            if (liveConnection != null
                && liveConnection.IsConnected()
                && liveConnection.Uri.Host.ToLower() == uri.Host.ToLower())
            {
                return liveConnection;
            }
            else
            {
                liveConnection.Close();
                return GetConnectionPrivate(uri, proxy);
            }
        }

        private Connection GetConnectionPrivate(Uri uri, Uri proxy)
        {
            Connection conn = new HttpConnection(this, uri, proxy);
			return InitiateConnection(conn);
        }

	    private Connection GetConnectionHttpsPrivate(Uri uri, Uri proxy, AsymmetricKeyParameter asymmetricKeyParameter,
	                                                 Certificate clientCertificates,
	                                                 Action<Certificate> serverCertificateValidator)
	    {
			Connection conn = new HttpsConnection(this, uri, proxy, clientCertificates, asymmetricKeyParameter, serverCertificateValidator);
		    return InitiateConnection(conn);
	    }

	    private Connection InitiateConnection(Connection connection)
	    {
		    connection.IsBusy = true;
		    connection.Connect();
		    return connection;
	    }
    }
}
