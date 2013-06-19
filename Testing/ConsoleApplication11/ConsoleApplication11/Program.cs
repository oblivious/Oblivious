using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace ConsoleApplication11
{
    class Program
    {
        static void Main(string[] args)
        {
            string product = "<Product><CountryID>3</CountryID><CountryISO>JM</CountryISO><CountryCode>JM</CountryCode><CountryName>Jamaica</CountryName><OperatorID>19</OperatorID><OperatorCode>DC</OperatorCode><OperatorName>Digicel Jamaica</OperatorName><CustomerCareNumber>+ 1 (876) 380-7626</CustomerCareNumber><PhoneNumberMask>+1 (876)-xxx-xxxx</PhoneNumberMask><MinMaxValueRange><MinValue>1</MinValue><MaxValue>70</MaxValue></MinMaxValueRange></Product>";

            XElement element = XElement.Parse(product);

            //Console.WriteLine(element.ToString());

            XmlSerializer serializer = new XmlSerializer(typeof(Product));

            //StringReader stringReader = new StringReader(product);
            
            //Console.WriteLine(stringReader.ReadToEnd());

            XmlReader reader = XmlReader.Create(new StringReader(product));

            Product testProduct;

            Console.WriteLine(reader.Value);

            testProduct = (Product)serializer.Deserialize(reader);

            Console.WriteLine(testProduct.OperatorName);
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://EDTS.DirectTopUpServices.DataTypes/2006/10")]
    public partial class Product
    {

        private int countryIDField;

        private string countryISOField;

        private string countryCodeField;

        private string countryNameField;

        private int operatorIDField;

        private string operatorCodeField;

        private string operatorNameField;

        private string logoUrlField;

        private string commercialCodeField;

        private string customerCareNumberField;

        private string phoneNumberMaskField;

        private Denomination denominationsField;

        private MinMaxValueRange minMaxValueRangeField;

        /// <remarks/>
        public int CountryID
        {
            get
            {
                return this.countryIDField;
            }
            set
            {
                this.countryIDField = value;
            }
        }

        /// <remarks/>
        public string CountryISO
        {
            get
            {
                return this.countryISOField;
            }
            set
            {
                this.countryISOField = value;
            }
        }

        /// <remarks/>
        public string CountryCode
        {
            get
            {
                return this.countryCodeField;
            }
            set
            {
                this.countryCodeField = value;
            }
        }

        /// <remarks/>
        public string CountryName
        {
            get
            {
                return this.countryNameField;
            }
            set
            {
                this.countryNameField = value;
            }
        }

        /// <remarks/>
        public int OperatorID
        {
            get
            {
                return this.operatorIDField;
            }
            set
            {
                this.operatorIDField = value;
            }
        }

        /// <remarks/>
        public string OperatorCode
        {
            get
            {
                return this.operatorCodeField;
            }
            set
            {
                this.operatorCodeField = value;
            }
        }

        /// <remarks/>
        public string OperatorName
        {
            get
            {
                return this.operatorNameField;
            }
            set
            {
                this.operatorNameField = value;
            }
        }

        /// <remarks/>
        public string LogoUrl
        {
            get
            {
                return this.logoUrlField;
            }
            set
            {
                this.logoUrlField = value;
            }
        }

        /// <remarks/>
        public string CommercialCode
        {
            get
            {
                return this.commercialCodeField;
            }
            set
            {
                this.commercialCodeField = value;
            }
        }

        /// <remarks/>
        public string CustomerCareNumber
        {
            get
            {
                return this.customerCareNumberField;
            }
            set
            {
                this.customerCareNumberField = value;
            }
        }

        /// <remarks/>
        public string PhoneNumberMask
        {
            get
            {
                return this.phoneNumberMaskField;
            }
            set
            {
                this.phoneNumberMaskField = value;
            }
        }

        /// <remarks/>
        public Denomination Denominations
        {
            get
            {
                return this.denominationsField;
            }
            set
            {
                this.denominationsField = value;
            }
        }

        /// <remarks/>
        public MinMaxValueRange MinMaxValueRange
        {
            get
            {
                return this.minMaxValueRangeField;
            }
            set
            {
                this.minMaxValueRangeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
   // [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://EDTS.DirectTopUpServices.DataTypes/2006/10")]
    public partial class Denomination
    {

        private int numberOfDenominationsField;

        private double[] denominationsField;

        /// <remarks/>
        public int NumberOfDenominations
        {
            get
            {
                return this.numberOfDenominationsField;
            }
            set
            {
                this.numberOfDenominationsField = value;
            }
        }

        /// <remarks/>
        public double[] Denominations
        {
            get
            {
                return this.denominationsField;
            }
            set
            {
                this.denominationsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
   //[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://EDTS.DirectTopUpServices.DataTypes/2006/10")]
    public partial class MinMaxValueRange
    {

        private double minValueField;

        private double maxValueField;

        /// <remarks/>
        public double MinValue
        {
            get
            {
                return this.minValueField;
            }
            set
            {
                this.minValueField = value;
            }
        }

        /// <remarks/>
        public double MaxValue
        {
            get
            {
                return this.maxValueField;
            }
            set
            {
                this.maxValueField = value;
            }
        }
    }
}
