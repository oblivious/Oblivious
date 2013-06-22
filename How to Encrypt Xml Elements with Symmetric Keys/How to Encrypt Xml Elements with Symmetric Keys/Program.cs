using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Xml;
using System.Security.Cryptography.Xml;

namespace How_to_Encrypt_Xml_Elements_with_Symmetric_Keys
{
    class Program
    {
        static void Main(string[] args)
        {
            RijndaelManaged key = null;

            try
            {
                // Create a new Rijndael key.
                key = new RijndaelManaged();

                // Load an XML document.
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.PreserveWhitespace = true;
                xmlDoc.Load("test.xml");

                XmlElement elementToEncrypt = (XmlElement)xmlDoc.GetElementsByTagName("")[0];

                EncryptedXml eXml = new EncryptedXml();

                // Abandoned because not relevant...
        }
    }
}
