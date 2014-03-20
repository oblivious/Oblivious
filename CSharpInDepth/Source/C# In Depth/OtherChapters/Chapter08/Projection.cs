﻿using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Chapter08
{
    [Description("Listing 8.6")]
    class Projection
    {
        static void Main()
        {
            List<Person> family = new List<Person>                 
            {
                new Person { Name="Holly", Age=34 },
                new Person { Name="Jon", Age=33 },
                new Person { Name="Tom", Age=6 },
                new Person { Name="Robin", Age=4 },
                new Person { Name="William", Age=4 }
            };

            var converted = family.ConvertAll(delegate(Person person)
                { return new { person.Name, IsAdult = (person.Age >= 18) }; }
            );

            foreach (var person in converted)
            {
                Console.WriteLine("{0} is an adult? {1}",
                                  person.Name, person.IsAdult);
            }
        }
    }
}
