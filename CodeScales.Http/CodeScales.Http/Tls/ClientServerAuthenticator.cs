using System;
using Org.BouncyCastle.Crypto.Tls;

namespace CodeScales.Http.Tls
{
	/// <summary>
	/// Provides TLS/SSL authentication of server certificate and serves up client certificate credentials
	/// </summary>
	public class ClientServerAuthenticator : TlsAuthentication
	{
		private readonly TlsSignerCredentials _tlsSignerCredentials;
		private readonly Action<Certificate> _serverCertificateValidator;

		public ClientServerAuthenticator(TlsSignerCredentials tlsSignerCredentials, Action<Certificate> serverCertificateValidator)
		{
			_tlsSignerCredentials = tlsSignerCredentials;
			_serverCertificateValidator = serverCertificateValidator;
		}

		#region TlsAuthentication interface implementation
		/// <summary>
		/// Callback for validation of the certificate the server provides.
		/// The ability to inject a method was added in case we ever actually want to validate a server certificate.
		/// </summary>
		/// <param name="serverCertificate">The cert provided by the server</param>
		public void NotifyServerCertificate(Certificate serverCertificate)
		{
			if (_serverCertificateValidator != null)
				_serverCertificateValidator(serverCertificate);
		}

		/// <summary>
		/// Callback for client authentication
		/// </summary>
		/// <param name="certificateRequest">The certificate request from the server you're connecting to</param>
		/// <returns>The set of credentials to prove to the server that you are who you say you are</returns>
		public TlsCredentials GetClientCredentials(CertificateRequest certificateRequest)
		{
			return _tlsSignerCredentials;
		}
		#endregion
	}
}