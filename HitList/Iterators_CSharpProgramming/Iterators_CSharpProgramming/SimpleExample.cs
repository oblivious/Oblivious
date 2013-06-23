using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iterators_CSharpProgramming
{
    class SimpleExample
    {
        public static void Run()
        {
            // Create an instance of the collection class
            DaysOfTheWeek week = new DaysOfTheWeek();

            // Iterate with foreach
            foreach (string day in week)
            {
                System.Console.Write(day);
            }
            Console.WriteLine();
        }
    }

    class DaysOfTheWeek : System.Collections.IEnumerable
    {
        string[] days = { "Sun", "Mon", "Tue", "Wed", "Thr", "Fri", "Sat" };

        public System.Collections.IEnumerator GetEnumerator()
        {
            for (int i = 0; i < days.Length; i++)
            {
                if (i == days.Length - 1)
                    yield return days[i] + ".";
                else
                    yield return days[i] + ", ";
            }
        }
    }
}
