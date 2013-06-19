using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MexicoTimeZoneCodeTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowWidth = 167;

            var mexicoTimeZoneCode = new MexicoTimeZoneCode();
            var errors = 0;

            for (int i = 0; i < 365; i++)
            {
                var controlMexicoTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now.AddDays(i), "Central Standard Time (Mexico)");
                var newMethodMexicanTime = mexicoTimeZoneCode.GetMyMexicoTime(TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now.AddDays(i), "Central Standard Time (Mexico)"));
                var oldMethodMexicanTime = mexicoTimeZoneCode.GetRealMexicoTime(DateTime.Now.AddDays(i));

                if (!newMethodMexicanTime.ToString(CultureInfo.InvariantCulture)
                    .Equals(oldMethodMexicanTime.ToString(CultureInfo.InvariantCulture)))
                {
                    errors++;


                    Console.WriteLine("Control time is: {6} new time {0} did not match old time {1} on day {2} of month {3} newDST: {4} oldDST:{5}", 
                        newMethodMexicanTime.ToString(CultureInfo.InvariantCulture).PadRight(19), 
                        oldMethodMexicanTime.ToString(CultureInfo.InvariantCulture).PadRight(19),
                        newMethodMexicanTime.DayOfWeek.ToString().PadRight(9),
                        newMethodMexicanTime.Month.ToString("00"),
                        (newMethodMexicanTime.IsDaylightSavingTime() + ",").PadRight(6),
                        oldMethodMexicanTime.IsDaylightSavingTime().ToString().PadRight(6),
                        controlMexicoTime.ToString(CultureInfo.InvariantCulture));
                }
                else
                {
                    /*
                    Console.WriteLine("{0} matched {1}",
                        newMethodMexicanTime.ToString(CultureInfo.InvariantCulture),
                        oldMethodMexicanTime.ToString(CultureInfo.InvariantCulture));*/
                }
            }
            Console.WriteLine("Number of errors: {0}", errors);
            Console.WriteLine("Finished");
            Console.ReadKey();
        }

        internal static DateTime NewMexicanTime(DateTime testTime)
        {
            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(testTime, "Central Standard Time (Mexico)");
        }
    }

    internal class MexicoTimeZoneCode
    {
        public DateTime GetRealMexicoTime(DateTime time)
        {
            DateTime timeInMexico = GetTimeForZone("Central Standard Time (Mexico)", time);
            
            if (LastWeekOfMarch(timeInMexico) && timeInMexico.DayOfWeek > DayOfWeek.Sunday || FirstWeekOfApril(timeInMexico) && timeInMexico.DayOfWeek < DayOfWeek.Sunday)
            {
                return timeInMexico.AddHours(-1);
            }
            else return timeInMexico;
        }

        public DateTime GetMyMexicoTime(DateTime time)
        {
            if ((time.Month == 3 && (7 - (int)time.DayOfWeek) + time.Day > 31) || (time.Month == 4 && time.Day - (int)time.DayOfWeek < 1))
            {
                return time.AddHours(1);
            }
            return time;

            /*
            if (LastWeekOfMarch(time) && ((int)time.DayOfWeek) < ((int)DayOfWeek.Sunday))
                return time.AddHours(-1);
            else if (FirstWeekOfApril(time) && ((int)time.DayOfWeek) < ((int)DayOfWeek.Sunday))
                return time.AddHours(1);
            return time;
             */
        }

        private bool LastWeekOfMarch(DateTime timeToCheck)
        {
            if (timeToCheck.Month == 3 && timeToCheck.Day + 7 > 31)
                return true;
            else
                return false;
        }
        private bool FirstWeekOfApril(DateTime timeToCheck)
        {
            if (timeToCheck.Month == 4 && timeToCheck.Day - 7 < 1)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Deprecated DO NOT USE - Gets current time for specified zone. Note the time returned is NOT offset by DST
        /// </summary>
        /// <param name="_zone">string TimeZone Standard Name</param>
        /// <returns>DateTime - not offset by DST</returns>
        public DateTime GetTimeForZone(string _zone, DateTime time)
        {
            List<TimeZoneInfo> mylist = GetTimeZones();
            TimeZoneInfo tzFound = mylist.Find(i => i.Id == _zone);
            DateTime timeForZone = time.AddHours((double)tzFound.BaseUtcOffset.Hours);
            return timeForZone;
        }

        /// <summary>
        /// Method to get a List of Windows TimeZones
        /// </summary>
        /// <returns>List of Windows TimeZones</returns>
        public List<TimeZoneInfo> GetTimeZones()
        {
            List<TimeZoneInfo> timeZones = new List<TimeZoneInfo>(TimeZoneInfo.GetSystemTimeZones());
            timeZones.Sort(
                delegate(TimeZoneInfo left, TimeZoneInfo right)
                {
                    int comparison = left.BaseUtcOffset.CompareTo(right.BaseUtcOffset);
                    return comparison == 0 ? string.CompareOrdinal(left.DisplayName, right.DisplayName) : comparison;
                }
            );
            return timeZones;
        }
    }
}
