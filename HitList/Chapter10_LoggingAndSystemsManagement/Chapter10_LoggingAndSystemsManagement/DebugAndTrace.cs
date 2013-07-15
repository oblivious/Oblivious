using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Chapter10_LoggingAndSystemsManagement
{
    public class DebugAndTrace
    {
        public static void Run()
        {
            Console.WriteLine("Example using Debug: \n");
            Debug.Listeners.Add(new ConsoleTraceListener());
            Debug.AutoFlush = true;
            Debug.Indent();
            Debug.WriteLine("Starting Application");
            Console.WriteLine("Hello World!");
            Debug.WriteLine("Ending Application");

            Console.WriteLine("\nExample including Trace:\n");
            Debug.WriteLine("Debug: Starting application");
            Trace.WriteLine("Trace: Starting application");
            Console.WriteLine("Hello World!");
            Trace.WriteLine("Trace: Ending application");
            Debug.WriteLine("Debug: Ending Application");

            Console.WriteLine("\nExample using Debug.WriteLineIf():");
            int random = new Random().Next(2);
            Debug.WriteLineIf(random > 0, "This time it was true!");
            Debug.WriteLineIf(random == 0, "This time it was false...");
            Debug.Unindent();
        }
    }
}
