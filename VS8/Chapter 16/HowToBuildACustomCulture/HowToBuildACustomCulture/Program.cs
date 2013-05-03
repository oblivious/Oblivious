using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace HowToBuildACustomCulture
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a CultureAndRegionIngoBuilder object
            CultureAndRegionInfoBuilder cib = new CultureAndRegionInfoBuilder("en-PL", CultureAndRegionModifiers.None);

            // Populate the new CultureAndRegionInfoBuilder object with culture information
            cib.LoadDataFromCultureInfo(new CultureInfo("en-US"));

            // Populate the new CultureAndRegionInfoBuilder object with region information.
            cib.LoadDataFromRegionInfo(new RegionInfo("US"));

            // Define culture specific settings.
            cib.CultureEnglishName = "Pig Latin";
            cib.CultureNativeName = "Igpay Atinlay";
            cib.IsMetric = true;
            cib.ISOCurrencySymbol = "PLD";
            cib.RegionEnglishName = "Pig Latin Region";
            cib.RegionNativeName = "Igpay Atinlay Egionray";

            // Register the customer culture (requires administrative privilieges).
            //cib.
            //cib.Register();

            // Display some of the properties of the custom culture.
            CultureInfo ci = new CultureInfo("en-PL");

            Console.WriteLine("Name: . . . . . . . . . . . . . . . . . . . {0}", ci.Name);
            Console.WriteLine("EnglishName:. . . . . . . . . . . . . . . . {0}", ci.EnglishName);
            Console.WriteLine("NativeName: . . . . . . . . . . . . . . . . {0}", ci.NativeName);
            Console.WriteLine("TwoLetterISOLanguageName: . . . . . . . . . {0}", ci.TwoLetterISOLanguageName);
            Console.WriteLine("ThreeLetterISOLanguageName: . . . . . . . . {0}", ci.ThreeLetterISOLanguageName);
            Console.WriteLine("ThreeLetterWindowsLanguageName: . . . . . . {0}", ci.ThreeLetterWindowsLanguageName);
        }
    }
}
