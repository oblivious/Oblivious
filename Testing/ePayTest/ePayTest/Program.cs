using System;
using System.ServiceModel;
using System.Text;
using System.Xml;

namespace ePayTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string User = "ezet0pprod";
                string Password = "1pa$$word!";
                int timeout = 30000;

                BasicHttpBinding binding = new BasicHttpBinding()
                {
                    SendTimeout = TimeSpan.FromMilliseconds(timeout),
                    OpenTimeout = TimeSpan.FromMilliseconds(timeout),
                    CloseTimeout = TimeSpan.FromMilliseconds(timeout),
                    ReceiveTimeout = TimeSpan.FromMilliseconds(timeout),
                    AllowCookies = false,
                    BypassProxyOnLocal = false,
                    HostNameComparisonMode = HostNameComparisonMode.StrongWildcard,
                    MaxBufferSize = 65536,
                    MaxBufferPoolSize = 524288,
                    MaxReceivedMessageSize = 65536,
                    MessageEncoding = WSMessageEncoding.Text,
                    TextEncoding = Encoding.UTF8,
                    TransferMode = TransferMode.Buffered,
                    UseDefaultWebProxy = true,
                    ReaderQuotas = new XmlDictionaryReaderQuotas()
                    {
                        MaxDepth = 32,
                        MaxStringContentLength = 8192,
                        MaxArrayLength = 16384,
                        MaxBytesPerRead = 4096,
                        MaxNameTableCharCount = 16384
                    },
                    Security = new BasicHttpSecurity()
                    {
                        Mode = BasicHttpSecurityMode.Transport,
                        Transport = new HttpTransportSecurity()
                        {
                            ClientCredentialType = HttpClientCredentialType.None,
                            ProxyCredentialType = HttpProxyCredentialType.None
                        },
                        Message = new BasicHttpMessageSecurity()
                        {
                            ClientCredentialType = BasicHttpMessageCredentialType.UserName
                        }
                    }
                };

                EndpointAddress address = new EndpointAddress("https://tms2.epayworldwide.com/epayTMS.svc");

                TMSService.AuthenticationServiceClient serviceAuthentication = new TMSService.AuthenticationServiceClient(binding, address);
                Console.WriteLine("Logging in...");
                TMSService.Token token = serviceAuthentication.Login(User, Password, "TMS");
                Console.WriteLine("Acquired token: username - " + token.Username + ", app name - " + token.AppName);
            }
            catch (Exception e)
            {
                Console.WriteLine("Caught exception: " + e.ToString());
            }
        }
    }
}
