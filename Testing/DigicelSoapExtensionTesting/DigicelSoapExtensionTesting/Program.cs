using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace DigicelSoapExtensionTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            DigicelFijiRequestMessage request = new DigicelFijiRequestMessage();

            Console.WriteLine(request.ToXML());
            Console.ReadKey();
        }
    }

    public class DigicelFijiRequestMessage
    {
        public string username { get; set; }
        public string password { get; set; }
        public string number { get; set; }
        public int amount { get; set; }
        public string currency { get; set; }
        public string transactionID { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(string.Empty);
            sb.Append(" | Top Up Request Message: ");
            sb.Append(" | Username: ").Append(username);
            sb.Append(" | Password: ").Append(password);
            sb.Append(" | Phone Number: ").Append(number);
            sb.Append(" | Amount: ").Append(amount.ToString());

            return sb.ToString();
        }

        public string ToXML()
        {
            // TODO -- INPUTPARAMSET
            XNamespace soapenv = "http://schemas.xmlsoap.org/soap/envelope/";
            XNamespace xsd = "http://www.w3.org/2001/XMLSchema";
            XNamespace wsse = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd";
            XNamespace wsu = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd";
            XNamespace axis = "http://axis.soap.service.s2100.product.redknee.com";

            XDocument request = new XDocument(
                   new XElement(soapenv + "Envelope",
                       new XAttribute(XNamespace.Xmlns + "axis", "http://axis.soap.service.s2100.product.redknee.com"),
                       new XAttribute(XNamespace.Xmlns + "soapenv", "http://schemas.xmlsoap.org/soap/envelope/"),
                       new XAttribute(XNamespace.Xmlns + "xsd", "http://axis.soap.service.s2100.product.redknee.com/xsd"),
                       new XElement(soapenv + "Header",
                           new XElement(wsse + "Security",
                               new XAttribute(XNamespace.Xmlns + "wsse", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"),
                               new XElement(wsu + "Timestamp",
                                    new XAttribute(XNamespace.Xmlns + "wsu", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd"),
                                        new XElement(wsu + "Created", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ")),
                                        new XElement(wsu + "Expires", DateTime.Now.AddDays(1).ToString("yyyy-MM-ddTHH:mm:ssZ"))),
                               new XElement(wsse + "UsernameToken",
                                   new XElement(wsse + "Username", "ezetop"),
                                   new XElement(wsse + "Password", "ezetop", new XAttribute("Type", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordText"))
                                   ))
                           ),
                       new XElement(soapenv + "Body",
                           new XElement(axis + "requestCredit5In",
                               new XElement(axis + "user", username),
                               new XElement(axis + "password", password),
                               new XElement(axis + "msisdn", number),
                               new XElement(axis + "amount", amount),
                               new XElement(axis + "currencyType", currency),
                               new XElement(axis + "udpExp", true),
                               new XElement(axis + "extendExpiryBy", "TEST"),
                               new XElement(axis + "inputParamSet",
                                   new XElement(xsd + "parameterID", "TEST"),
                                   new XElement(xsd + "value",
                                        new XElement(xsd + "___intvalue", "TEST"),
                                        new XElement(xsd + "___longValue", "TEST"),
                                        new XElement(xsd + "___floatValue", "TEST"),
                                        new XElement(xsd + "___doubleValue", "TEST"),
                                        new XElement(xsd + "___stringValue", "TEST"),
                                        new XElement(xsd + "___booleanValue", "TEST"),
                                        new XElement(xsd + "___byteArrayValue", "TEST"),
                                        new XElement(xsd + "__discriminator", "TEST"),
                                        new XElement(xsd + "__uninitialized", "TEST"))),
                               new XElement(axis + "erReference", transactionID)
                               )
                           )
                       )
                   );
            return request.ToString();
        }
    }
}
