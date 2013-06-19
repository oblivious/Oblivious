using System;
using System.Collections.Specialized;
using System.Web;
using System.Globalization;

namespace ConsoleApplication1
{
    class Program
    {

        static void Main(string[] args)
        {
            string querystring = @"VASRefId=00023011371&VASDateTime=20120321123642&Status=471&MSISDN=918891419047&PTDateTime=20120321123642&TalkTime=0.0&CustomerBal=null";

            NameValueCollection responseCollection = null;

            try
            {
                responseCollection = HttpUtility.ParseQueryString(querystring);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("ParseQueryString threw a wobbly");
            }

            string vasRefId = responseCollection.Get("VASRefId");
            string vasDateTime = responseCollection.Get("VASDateTime");
            string status = responseCollection.Get("Status");
            string msisdn = responseCollection.Get("MSISDN");
            string pTDateTime = responseCollection.Get("PTDateTime");
            //Don't care about Talktime or Customer Balance.

            Console.WriteLine("VASRefId = " + vasRefId);
            Console.WriteLine("VASDateTime = " + vasDateTime);
            Console.WriteLine("Status = " + status);
            Console.WriteLine("MSISDN = " + msisdn);
            Console.WriteLine("PTDateTime = " + pTDateTime);

            /*
            Regex responseRegex = new Regex(@"VASRefId=(\w{1,12})&VASDateTime=(\d{14}|null)&Status=(\d{3}|null)&MSISDN=(\d{12}|null)&PTDateTime=(\d{14}|null)&TalkTime=(\d+\.\d+)&CustomerBal=(\d+.?\d*|null)$", RegexOptions.IgnoreCase | RegexOptions.Singleline);

            string querystring = @"VASRefId=00023010908&VASDateTime=20120321122001&Status=200&MSISDN=919041949445&PTDateTime=20120321122001&TalkTime=88.66&CustomerBal=451.94";

            Match m = responseRegex.Match(querystring);

            Console.WriteLine(m.Success);
             * */

            double throwaway = 7.00;
            Console.WriteLine(throwaway);
            string mystring = Convert.ToString(throwaway);
            Console.WriteLine(mystring);

            int balance;
            string balanceString = "1";
            bool balanceParsed = int.TryParse(balanceString, out balance);
            Console.WriteLine(balance.ToString("#,#", CultureInfo.InvariantCulture));
        }
    }
}
