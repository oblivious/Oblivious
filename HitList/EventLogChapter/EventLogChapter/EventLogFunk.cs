using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace EventLogChapter
{
    class EventLogFunk
    {
        public static void Run()
        {
            if (!EventLog.SourceExists("Bender"))
                EventLog.CreateEventSource("Bender", "Death to Humans!");

            EventLog eventLog = new EventLog("Death to Humans!");
            eventLog.Source = "Bender";

            eventLog.WriteEntry("Bite my shiny metal ass!", EventLogEntryType.Warning);
        }
    }

    class EventLogReading
    {
        public static void Run()
        {
            EventLog myLog = new EventLog("Application");

            foreach (EventLogEntry entry in myLog.Entries)
            {
                if (entry.EntryType.Equals(EventLogEntryType.Error))
                {
                    Console.WriteLine("{0} - {1} - \"{2}\"", entry.TimeWritten, entry.MachineName, entry.Message);
                }
            }
        }
    }
}
