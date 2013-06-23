using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;

namespace EventLogExamples
{
    class EventLogClassTest
    {
        internal static void Run()
        {
            // Create the source if it does not already exist.
            if (!EventLog.SourceExists("EventLogClassTestSource"))
            {
                // An event log source should not be created and immediately used.
                // There is a latency time to enable the source, it should be created
                // prior to executing the application that uses the source.
                // Execute this sample a second time to use the new source...
                EventLog.CreateEventSource("EventLogClassTestSource", "EventLogClassTestLog");
                Console.WriteLine("CreatedEventSource");
                Console.WriteLine("Exiting, execute the application a second time to use the source.");
                // The source is created. Exit the application to allow it to be registered.
                return;
            }

            // Create an EventLog instance and assign its source.
            EventLog myLog = new EventLog("EventLogClassTestLog", "Amphion", "EventLogClassTestSource");

            // Write an entry to the log.
            myLog.WriteEntry("Writing to event log on " + myLog.MachineName);

            CloseEventLog(myLog);

            EventLog[] eventLogs = EventLog.GetEventLogs();
            foreach (EventLog el in eventLogs)
            {
                Console.WriteLine("Log Display Name: {0}, Machine Name: {1}, Source Name: {2}", el.LogDisplayName, el.MachineName, el.Source);
            }

            Console.WriteLine("EventLog.LogNameFromSourceName(\"EventLogClassTestSource\") = {0}",EventLog.LogNameFromSourceName("EventLogClassTestSource", "."));
        }

        private static void CloseEventLog(EventLog log)
        {
            try
            {
                log.Close();
            }
            catch (Win32Exception we)
            {
                Console.WriteLine("Error closing event log: " + we.ToString());
            }
        }
    }
}
