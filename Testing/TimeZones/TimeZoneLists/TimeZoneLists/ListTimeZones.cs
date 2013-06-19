using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace TimeZoneLists
{
    class ListTimeZones
    {
        static void Main(string[] args)
        {
            try
            {
                var myList = GetTimeZones();
                var mexicans = new List<TimeZoneInfo>();
                foreach (var zone in myList)
                {
                    Console.WriteLine(zone.DisplayName + ", " + zone.StandardName);
                    if (zone.DisplayName.Contains("Mexico City"))
                        mexicans.Add(zone);
                }
                var helper = new ezeHelper.ezeHelper();
                Console.WriteLine(helper.GetTimeForZone("E. Africa Standard Time", true).ToString(CultureInfo.InvariantCulture));
                foreach (var v in mexicans)
                {
                    Console.WriteLine("Found Mexico City: " + v.DisplayName + ", " + v.StandardName);
                    Console.WriteLine("Time zone Id: " + v.Id);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.ReadLine();
        }

        /// <summary>
        /// Method to get a List of Windows TimeZones, copied from ezeHelper
        /// </summary>
        /// <returns>List of Windows TimeZones</returns>
        public static List<TimeZoneInfo> GetTimeZones()
        {
            var x = TimeZoneInfo.Local;
            var mexicanTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Central Standard Time (Mexico)");
            Console.WriteLine("Is Daylight Savings: " + mexicanTime.IsDaylightSavingTime());
            Console.WriteLine("Kind: " + mexicanTime.Kind);
            mexicanTime = mexicanTime.ToLocalTime();
            Console.WriteLine("Is Daylight Savings: " + mexicanTime.IsDaylightSavingTime());
            Console.WriteLine("Kind: " + mexicanTime.Kind);
            Console.WriteLine("Is Daylight Savings: " + DateTime.Now.IsDaylightSavingTime());
            Console.WriteLine("Kind: " + DateTime.Now.Kind);
            Console.ReadKey();

            Console.WriteLine("Fetching system time zones...");
            DateTime startTime = DateTime.Now;
            var timeZones = new List<TimeZoneInfo>(TimeZoneInfo.GetSystemTimeZones());
            Console.WriteLine("Time taken to fetch system time zones: {0}ms", (int)(DateTime.Now - startTime).TotalMilliseconds);

            Console.WriteLine("Selecting Mexico City timezone...");
            var mexies = from t in timeZones
                         where t.DisplayName.Contains("Mexico City")
                         select t;
            Console.WriteLine("Total time taken to fetch system time zones and select the mexican timezone: {0}ms",
                (int)(DateTime.Now - startTime).TotalMilliseconds);

            timeZones.Sort((left, right) =>
            {
                int comparison = left.BaseUtcOffset.CompareTo(right.BaseUtcOffset);
                return comparison == 0 ? string.CompareOrdinal(left.DisplayName, right.DisplayName) : comparison;
            });
            return timeZones;
        }
    }
}
