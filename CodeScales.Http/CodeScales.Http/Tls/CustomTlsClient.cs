using System;
using System.Net.Sockets;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Tls;

namespace CodeScales.Http.Tls
{
	/// <summary>
	/// Connects to a server via TLS/SSL with client certificate authentication
	/// </summary>
	internal class CustomTlsClient : DefaultTlsClient
	{
		private bool _hasConnected;

		private TcpClient _tcpClient;
		private TlsProtocolHandler _tlsProtocolHandler;

		private readonly AsymmetricKeyParameter _asymmetricKeyParameter;
		private readonly Certificate _clientCertificates;
		private readonly Action<Certificate> _serverCertificateValidator;

		public bool Connected { get { return _hasConnected && SocketIsConnectedAndReadable(); } }

		public SecurityParameters SecurityParameters { get; set; }
		public int Available { get { return _tcpClient != null ? _tcpClient.Available : 0; } }

		internal CustomTlsClient(
			Certificate clientCertificates,
			AsymmetricKeyParameter asymmetricKeyParameter,
			Action<Certificate> serverCertificateValidator)
		{
			_clientCertificates = clientCertificates;
			_asymmetricKeyParameter = asymmetricKeyParameter;
			_serverCertificateValidator = serverCertificateValidator;
		}

		/// <summary>
		/// Connect to the server and wrap the socket in a TLS handler.
		/// </summary>
		public void Connect(string ipAddress, int port, int sendTimeout, int receiveTimeout)
		{
			_tcpClient = new TcpClient { SendTimeout = sendTimeout, ReceiveTimeout = receiveTimeout };
			_tcpClient.Connect(ipAddress, port);

			_tlsProtocolHandler = new TlsProtocolHandler(_tcpClient.GetStream());
			_tlsProtocolHandler.Connect(this);

			_hasConnected = true;
		}

		public void Close()
		{
			try
			{
				_tlsProtocolHandler.Stream.Dispose();
			}
			catch { }
		}

		public void Send(byte[] bytes)
		{
			_tlsProtocolHandler.Stream.Write(bytes, 0, bytes.Length);
			_tlsProtocolHandler.Stream.Flush();
		}

		public byte ReadByte()
		{
			return (byte)_tlsProtocolHandler.Stream.ReadByte();
		}

		public int Receive(byte[] bytes, int offset, int count)
		{
			return _tlsProtocolHandler.Stream.Read(bytes, offset, count);
		}

		/// <summary>
		/// Method that returns the TlsAuthentication provider for this client.
		/// </summary>
		public override TlsAuthentication GetAuthentication()
		{
			return new ClientServerAuthenticator(
				new DefaultTlsSignerCredentials(context, _clientCertificates, _asymmetricKeyParameter),
				_serverCertificateValidator);
		}

		/// <summary>
		/// Override GetCipherSuites to return the same list of ciphers as the previous implementation.
		/// </summary>
		/// <returns></returns>
		public override int[] GetCipherSuites()
		{
			return new[]
				{
					CipherSuite.TLS_RSA_WITH_AES_128_CBC_SHA,
					CipherSuite.TLS_RSA_WITH_AES_256_CBC_SHA,
					CipherSuite.TLS_RSA_WITH_3DES_EDE_CBC_SHA,
					CipherSuite.TLS_ECDHE_RSA_WITH_AES_128_CBC_SHA,
					CipherSuite.TLS_ECDHE_RSA_WITH_AES_256_CBC_SHA,
					CipherSuite.TLS_ECDHE_ECDSA_WITH_AES_128_CBC_SHA,
					CipherSuite.TLS_ECDHE_ECDSA_WITH_AES_256_CBC_SHA,
					CipherSuite.TLS_DHE_DSS_WITH_AES_128_CBC_SHA,
					CipherSuite.TLS_DHE_DSS_WITH_AES_256_CBC_SHA,
					CipherSuite.TLS_DHE_DSS_WITH_3DES_EDE_CBC_SHA,
					CipherSuite.TLS_RSA_WITH_RC4_128_SHA,
					CipherSuite.TLS_RSA_WITH_RC4_128_MD5
				};

		}

		/// <summary>
		/// Override GetKeyExchange to only allow the server to select the ciphers we provided above
		/// </summary>
		/// <returns></returns>
		public override TlsKeyExchange GetKeyExchange()
		{
			switch (selectedCipherSuite)
			{
				case CipherSuite.TLS_RSA_WITH_3DES_EDE_CBC_SHA://
				case CipherSuite.TLS_RSA_WITH_AES_128_CBC_SHA://
				case CipherSuite.TLS_RSA_WITH_AES_256_CBC_SHA://
				case CipherSuite.TLS_RSA_WITH_RC4_128_MD5:
				case CipherSuite.TLS_RSA_WITH_RC4_128_SHA:
					return CreateRsaKeyExchange();

				case CipherSuite.TLS_DHE_DSS_WITH_3DES_EDE_CBC_SHA://
				case CipherSuite.TLS_DHE_DSS_WITH_AES_128_CBC_SHA://
				case CipherSuite.TLS_DHE_DSS_WITH_AES_256_CBC_SHA://
					return CreateDheKeyExchange(KeyExchangeAlgorithm.DHE_DSS);

				case CipherSuite.TLS_ECDHE_ECDSA_WITH_AES_128_CBC_SHA://
				case CipherSuite.TLS_ECDHE_ECDSA_WITH_AES_256_CBC_SHA://
					return CreateECDheKeyExchange(KeyExchangeAlgorithm.ECDHE_ECDSA);

				case CipherSuite.TLS_ECDHE_RSA_WITH_AES_128_CBC_SHA://
				case CipherSuite.TLS_ECDHE_RSA_WITH_AES_256_CBC_SHA://
					return CreateECDheKeyExchange(KeyExchangeAlgorithm.ECDHE_RSA);

				default:
					/*
					* Note: internal error here; the TlsProtocolHandler verifies that the
					* server-selected cipher suite was in the list of client-offered cipher
					* suites, so if we now can't produce an implementation, we shouldn't have
					* offered it!
					*/
					throw new TlsFatalAlert(AlertDescription.internal_error);
			}
		}

		/// <summary>
		/// Override of cipher selection following server's choice
		/// </summary>
		/// <returns></returns>
		public override TlsCipher GetCipher()
		{
			switch (selectedCipherSuite)
			{
				case CipherSuite.TLS_RSA_WITH_3DES_EDE_CBC_SHA:
				case CipherSuite.TLS_DHE_DSS_WITH_3DES_EDE_CBC_SHA:
					return cipherFactory.CreateCipher(context, EncryptionAlgorithm.cls_3DES_EDE_CBC, DigestAlgorithm.SHA);

				case CipherSuite.TLS_RSA_WITH_AES_128_CBC_SHA:
				case CipherSuite.TLS_DHE_DSS_WITH_AES_128_CBC_SHA:
				case CipherSuite.TLS_ECDHE_ECDSA_WITH_AES_128_CBC_SHA:
				case CipherSuite.TLS_ECDHE_RSA_WITH_AES_128_CBC_SHA:
					return cipherFactory.CreateCipher(context, EncryptionAlgorithm.AES_128_CBC, DigestAlgorithm.SHA);

				case CipherSuite.TLS_RSA_WITH_AES_256_CBC_SHA:
				case CipherSuite.TLS_DHE_DSS_WITH_AES_256_CBC_SHA:
				case CipherSuite.TLS_ECDHE_ECDSA_WITH_AES_256_CBC_SHA:
				case CipherSuite.TLS_ECDHE_RSA_WITH_AES_256_CBC_SHA:
					return cipherFactory.CreateCipher(context, EncryptionAlgorithm.AES_256_CBC, DigestAlgorithm.SHA);

				case CipherSuite.TLS_RSA_WITH_RC4_128_MD5:
					return cipherFactory.CreateCipher(context, EncryptionAlgorithm.RC4_128, DigestAlgorithm.MD5);

				case CipherSuite.TLS_RSA_WITH_RC4_128_SHA:
					return cipherFactory.CreateCipher(context, EncryptionAlgorithm.RC4_128, DigestAlgorithm.SHA);

				default:
					/*
					* Note: internal error here; the TlsProtocolHandler verifies that the
					* server-selected cipher suite was in the list of client-offered cipher
					* suites, so if we now can't produce an implementation, we shouldn't have
					* offered it!
					*/
					throw new TlsFatalAlert(AlertDescription.internal_error);
			}
		}

		private bool SocketIsConnectedAndReadable()
		{
			try { return !(_tcpClient.Client.Poll(1000, SelectMode.SelectRead) && _tcpClient.Client.Available == 0); }
			catch (SocketException) { return false; }
		}
	}
}
