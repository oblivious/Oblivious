using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Chapter10_LoggingAndSystemsManagement
{
    public class EventLogTest
    {
        public static void Run()
        {
            if (!EventLog.SourceExists("Bender"))
            {
                Console.WriteLine("Creating the event log source named \"Bender\"");
                EventLog.CreateEventSource("Bender", "Death to Humans!");
            }
            else
            {
                Console.WriteLine("The event log source named \"Bender\" already exists.");
            }

            Console.WriteLine("\nThe log for the source named \"Bender\" is {0}", EventLog.LogNameFromSourceName("Bender", "."));

            EventLog eventLog = new EventLog("Death to Humans!");
            eventLog.Source = "Bender";

            Console.Write("\nEnter some text to write to the event log: ");
            string input = Console.ReadLine();
            eventLog.WriteEntry(input, EventLogEntryType.Warning);

            Console.WriteLine("\n\nThe \"Death to Humans!\" event log contains the following entries: ");

            foreach (EventLogEntry entry in eventLog.Entries)
            {
                Console.WriteLine("{0} - {1} - \"{2}\"", entry.TimeWritten, entry.MachineName, entry.Message);
            }
        }
    }
}
