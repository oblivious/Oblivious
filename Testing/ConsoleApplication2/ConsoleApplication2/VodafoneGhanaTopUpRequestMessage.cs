using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ConsoleApplication2
{
    public class VodafoneGhanaTopUpRequestMessage
    {
        public string ID { get; set; }
        public int TransactionID { get; set; }
        public int Application { get; set; }
        public int Action { get; set; }
        public string PhoneNumber { get; set; }
        public int CPTimer { get; set; }
        public int Amount { get; set; }
        public int Currency { get; set; }

        public string ToLogFormat()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" | TransactionID: " + this.TransactionID);
            sb.Append(" | Application: " + this.Application);
            sb.Append(" | Action: " + this.Action);
            sb.Append(" | PhoneNumber: " + this.PhoneNumber);
            sb.Append(" | CPTimer: " + this.CPTimer);
            sb.Append(" | Amount: " + this.Amount);
            sb.Append(" | Currency: " + this.Currency);
            return sb.ToString();
        }

        public string ToRequestFormat()
        {
            string declaration = "<?xml version=\"1.0\"?>\n";
            string docType = "<!DOCTYPE cp_request SYSTEM \"cp_req_websvr.dtd\">\n";
            XDocument request = new XDocument(
                new XElement("cp_request",
                    new XElement("cp_id", this.ID),
                    new XElement("cp_transaction_id", this.TransactionID),
                    new XElement("op_transaction_id", ""),
                    new XElement("application", this.Application),
                    new XElement("action", this.Action),
                    new XElement("user_id", this.PhoneNumber),
                    new XElement("cp_timer", this.CPTimer),
                    new XElement("transaction_price", this.Amount),
                    new XElement("transaction_currency", this.Currency)
                    )
                );
            return declaration + docType + request.ToString();
        }
    }
}
