using System;
using System.Collections.Generic;

namespace Flags
{
    class Program
    {
        public enum SomeRootVegetables
        {
            HorseRadish,
            Radish,
            Turnip,
        }

        [Flags]
        public enum Seasons
        {
            None = 0,
            Summer = 1,
            Autumn = 2,
            Winter = 4,
            Spring = 8,
            All = Summer | Autumn | Winter | Spring
        }

        public class Example
        {
            public static void Main()
            {
                Dictionary<SomeRootVegetables, Seasons> AvailableIn = new Dictionary<SomeRootVegetables, Seasons>();

                AvailableIn[SomeRootVegetables.HorseRadish] = Seasons.All;
                AvailableIn[SomeRootVegetables.Radish] = Seasons.Spring;
                AvailableIn[SomeRootVegetables.Turnip] = Seasons.Spring | Seasons.Autumn;

                Seasons[] theSeasons = new Seasons[] { Seasons.Summer, Seasons.Autumn, Seasons.Winter, Seasons.Spring };

                foreach (Seasons season in theSeasons)
                {
                    Console.Write(String.Format(
                        "The following root vegetables are harvested in {0}:\n",
                        season.ToString("G")));
                    foreach (KeyValuePair<SomeRootVegetables, Seasons> item in AvailableIn)
                    {
                        if (((Seasons)item.Value & season) > 0)
                            Console.Write(String.Format("  {0:G}\n",
                                (SomeRootVegetables)item.Key));
                    }
                }
                Console.ReadLine();
            }
        }
    }
}
