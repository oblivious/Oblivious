using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChaseTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ChaseMerchantProfileList list = new ChaseMerchantProfileList();

            var result = list.GetSupportedCurrenciesForPaymentType();

            foreach (var v in result)
            {
                Console.WriteLine(v + " " + (int)v);
            }
        }
    }

    public class ChaseMerchantProfileList : Dictionary<CurrencyIsoCode, string>
    {
        private readonly Dictionary<string, string> _merchantIds;
        private readonly Dictionary<string, string> _currencyCodes;

        protected const string EurCurrencyIso = "EUR";
        protected const string GbpCurrencyIso = "GBP";
        protected const string UsdCurrencyIso = "USD";

        private const string TerminalId = "001";
        private const string RoutingDefinition = "000001";

        public ChaseMerchantProfileList()
        {
            _merchantIds = new Dictionary<string, string>
                               {
                                   { EurCurrencyIso, "203490" },
                                   { GbpCurrencyIso, "203488" },
                                   { UsdCurrencyIso, "203441" }
                               };

            _currencyCodes = new Dictionary<string, string>
                                 {
                                     { EurCurrencyIso, "978" },
                                     { GbpCurrencyIso, "826" },
                                     { UsdCurrencyIso, "840" }
                                 };
        }

        public IEnumerable<CurrencyIsoCode> GetSupportedCurrenciesForPaymentType()
        {
            return _currencyCodes.Keys.Select(x => Enum.Parse(typeof(CurrencyIsoCode), x)).Cast<CurrencyIsoCode>().ToList();
        }
    }

    public enum CurrencyIsoCode
    {
        EUR = 978,
        GBP = 826,
        USD = 840
    }
}
