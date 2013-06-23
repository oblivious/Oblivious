using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace EventLogExamples
{
    class CreateEventSourceSample1
    {
        static void Run()
        {
            string messageFile = "message.txt";

            string myLogName;
            string sourceName = "SampleApplicationSource";

            // Create the event source if it does not exist.
            if (!EventLog.SourceExists(sourceName))
            {
                // Create a new event source for the custom event log named "myNewLog".
                myLogName = "myNewLog";
                EventSourceCreationData mySourceData = new EventSourceCreationData(sourceName, myLogName);

                // Set the message resource file that the event source references.
                // All event resource identifiers correspond to test in this file.
                if (!System.IO.File.Exists(messageFile))
                {
                    Console.WriteLine("Input message resource file does not exist - {0}", messageFile);
                    messageFile = "";
                }
                else
                {
                    // Set the specified file as the resource file for message text,
                    //   category text, and message parameter strings.
                    mySourceData.MessageResourceFile = messageFile;
                    mySourceData.CategoryResourceFile = messageFile;
                    mySourceData.CategoryCount = CategoryCount;
                }
            }
        }
    }
}
