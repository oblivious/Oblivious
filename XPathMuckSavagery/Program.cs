using System;
using System.IO;
using System.Xml.Linq;
using System.Xml.XPath;

namespace XPathMuckSavagery
{
    class Program
    {
        static void Main(string[] args)
        {
            string sampleXml = null;
            using (var reader = File.OpenText("sample.xml"))
            {
                sampleXml = reader.ReadToEnd();
            }

            var xDoc = XDocument.Parse(sampleXml);

            var element = xDoc.XPathSelectElement("inventory/snack/chips/amount");
            Console.WriteLine("(\"inventory/snack/chips/amount\"), value: {0}", element.Value);

            var attribute = xDoc.XPathSelectElement("inventory/snack/chips").Attribute("supplier");
            Console.WriteLine("(\"inventory/snack/chips\").Attribute(\"supplier\"), value: {0}", attribute.Value);
        }
    }
}
