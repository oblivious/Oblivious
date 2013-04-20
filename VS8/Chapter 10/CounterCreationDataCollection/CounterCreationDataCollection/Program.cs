using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace CounterCreationDataCollections
{
    class Program
    {
        static void Main(string[] args)
        {
            CounterCreationDataCollection counters = new CounterCreationDataCollection();
            counters.Add(new CounterCreationData("Sales", "Number of total sales", PerformanceCounterType.NumberOfItems64));
            counters.Add(new CounterCreationData("Active Users", "Number of active users", PerformanceCounterType.NumberOfItems64));
            counters.Add(new CounterCreationData("Sales value", "Total value of all sales", PerformanceCounterType.NumberOfItems64));
            PerformanceCounterCategory.Create("MyApp Counters", "Counters describing the performance of MyApp",
                PerformanceCounterCategoryType.SingleInstance, counters);

            PerformanceCounter pc = new PerformanceCounter("MyApp Counters", "Sales", false);

            pc.RawValue = 7;
            pc.Decrement();
            pc.Increment();
            pc.IncrementBy(3);
        }
    }
}
