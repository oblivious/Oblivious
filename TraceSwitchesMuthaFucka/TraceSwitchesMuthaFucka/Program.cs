#define TRACE
#define ConfigFile
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Reflection;

namespace TraceSwitchesMuthaFucka
{
    public class TraceTest
    {
        private static BooleanSwitch boolSwitch = new BooleanSwitch("mySwitch", "Switch in the muthafuckin config file!");
        private static BooleanSwitch dataSwitch = new BooleanSwitch("Data", "DataAccess module");

        private static TraceSwitch appSwitch = new TraceSwitch("traceSwitch", "In config");

        private static TraceSource ts = new TraceSource("TraceDoodle");

        [SwitchAttribute("SourceSwitch", typeof(SourceSwitch))]
        public static void Main(string[] args)
        {
            Trace.CorrelationManager.StartLogicalOperation("Main Method");
            try
            {
                Console.WindowWidth = 164;

#if(!ConfigFile)
                SourceSwitch sourceSwitch = new SourceSwitch("SourceSwitch", "Verbose");
                ts.Switch = sourceSwitch;
                int idxConsole = ts.Listeners.Add(new ConsoleTraceListener());
                ts.Listeners[idxConsole].Name = "console";
#endif
                DisplayProperties(ts);

                TraceListener console = ts.Listeners["console"];
                Trace.Indent();
                ListTraceOptions(console);

                Console.WriteLine("\n\nTraceOptions.Callstack (added):");
                console.TraceOutputOptions |= TraceOptions.Callstack;
                ListTraceOptions(console);
                Console.WriteLine("TraceEvent(Warning):");
                if (ts.Switch.Level < SourceLevels.Warning)
                    Console.WriteLine("TraceEvent should not print as the level is too low");
                ts.TraceEvent(TraceEventType.Warning, 1);
                ts.TraceInformation("Some trace information");

                Console.WriteLine("\n\nTraceOptions.DateTime (only option):");
                console.TraceOutputOptions = TraceOptions.DateTime;
                ListTraceOptions(console);
                // Issue FNF message as a warning...
                Console.WriteLine("TraceEvent(Warning):");
                if (ts.Switch.Level < SourceLevels.Warning)
                    Console.WriteLine("TraceEvent should not print as the level is too low");
                ts.TraceEvent(TraceEventType.Warning, 2, "File Test not found");

                // Issue FNF message as a verbose event using a formatted string...
                Console.WriteLine("TraceEvent(Verbose):");
                if (ts.Switch.Level < SourceLevels.Verbose)
                    Console.WriteLine("TraceEvent should not print as the level is too low");
                ts.TraceEvent(TraceEventType.Verbose, 3, "File {0} not found,", "test");

                // Issue FNF message as information...
                Console.WriteLine("\n\nTraceInformation:");
                ts.TraceInformation("File {0} not found.", "test");

                Console.WriteLine("\n\nTraceOptions.LogicalOperationStack:");
                console.TraceOutputOptions |= TraceOptions.LogicalOperationStack;
                ListTraceOptions(console);
                // Issue FNF message as an error event.
                Console.WriteLine("TraceEvent(Error):");
                if (ts.Switch.Level < SourceLevels.Error)
                    Console.WriteLine("TraceEvent should not print as the level is too low");
                ts.TraceEvent(TraceEventType.Error, 4, "File {0} not found.", "test");

                Console.WriteLine("\n\nSomeStaticFunction:");
                SomeStaticFunction();

                // Test the filter on the ConsoleTraceListener.
                Console.WriteLine("\n\nTesting Filter:");
                console.Filter = new SourceFilter("No match");
                if (ts.Switch.Level < SourceLevels.Error)
                    Console.WriteLine("TraceData should not print as the level is too low");
                ts.TraceData(TraceEventType.Error, 5, "SourceFilter should reject this message for the console trace listener.");
                console.Filter = new SourceFilter("TraceTest");
                if (ts.Switch.Level < SourceLevels.Error)
                    Console.WriteLine("TraceData should not print as the level is too low");
                ts.TraceData(TraceEventType.Error, 6, "SourceFilter should let this message through on the console trace listener.");
                console.Filter = null;
                // Use the TraceData method.
                Console.WriteLine("\n\nTesting TraceData:");
                ts.TraceData(TraceEventType.Warning, 7, new object());
                ts.TraceData(TraceEventType.Warning, 8, new object[] { "Message 1", "Message 2" });
                // Activity Tests
                Console.WriteLine("\n\nActivity Tests:");
                ts.TraceEvent(TraceEventType.Start, 9, "Will not appear until the switch is changed.");
                ts.Switch.Level = SourceLevels.ActivityTracing | SourceLevels.Critical;
                ts.TraceEvent(TraceEventType.Suspend, 10, "Switch includes ActivityTracing, this should appear");
                ts.TraceEvent(TraceEventType.Critical, 11, "Switch includes Critical, this should appear");
                ts.Flush();
                ts.Close();
                Console.WriteLine("Press any key to exit.");
                Console.Read();
            }
            catch (Exception e)
            {
                // Catch any unexpected exception.
                Console.WriteLine("Unexpected exception: " + e.ToString());
                Console.Read();
            }
            Trace.CorrelationManager.StopLogicalOperation();
            /*
            Console.WriteLine("This output covers Boolean Switches:");

            // Switch is configured in the config file.
            Console.WriteLine("Boolean switch {0} configured as {1}", boolSwitch.DisplayName, boolSwitch.Enabled.ToString());
            Console.WriteLine("Boolean switch {0} configured as {1}", dataSwitch.DisplayName, dataSwitch.Enabled.ToString());

            if (dataSwitch.Enabled)
                Console.WriteLine("Error happened at " + "somewhere!");

            Console.WriteLine("\n\n");

            Console.WriteLine("This output covers Trace Switches");
            /* Summary: Specifies what messages to output for the Debug, Trace and TraceSwitch classes.
            public enum System.Diagnostics.TraceLevel
            {
                Off = 0, // Summary: Output no tracing and debugging messages.
                Error = 1, // Summary: Output error-handling messages.
                Warning = 2, // Summary: Output warnings and error-handling messages.
                Info = 3, // Summary: Output informational messages, warnings, and error-handling messages.
                Verbose = 4, // Summary: Output all debugging and tracing messages.
            }*/
            /*
            Console.WriteLine("Trace switch {0} configured as {1}", appSwitch.DisplayName, appSwitch.Level.ToString());
            if (appSwitch.TraceError)
            {
                Console.WriteLine("Dammit Jim, he's dead!");
            }

            if (appSwitch.TraceVerbose)
            {
                Console.WriteLine("There's Klingons off the starboard bow!");
            }

            // Just a test, this does nothing yet because we're not directing the content anywhere...
            Debug.WriteLine("1st Debugging WriteLine");
            Trace.WriteLine("1st Tracing WriteLine");

            Console.WriteLine("Press any key and we'll hook up the debugging and tracing:");
            Console.ReadKey();

            // Hook shit up.
            //Debug.Listeners.Add(new ConsoleTraceListener());
            Trace.Listeners.Add(new ConsoleTraceListener());

            Console.WriteLine("Console added to Debug and Trace listeners");

            // Now it should be working...
            Debug.WriteLine("2nd Debugging WriteLine");
            Trace.WriteLine("2nd Tracing WriteLine");

            Console.WriteLine("Finished with trace and debug...");

            Debug.Assert(1 == 0, "Clearly this is false");
            Console.WriteLine("Debug indent level: {0}", Debug.IndentLevel);
            Console.WriteLine("Debug indent size: {0}", Debug.IndentSize);
            Debug.IndentLevel = 1;
            Debug.WriteLine("I like tacos");

            // Testing the Xml Writer TL to see what it looks like...
            Debug.Listeners.Clear();
            Debug.Listeners.Add(new XmlWriterTraceListener(Console.Out));

            // This writes an absolute crapload of stuff including datetime, PC name, application name, everything...
            Debug.WriteLine("Writing something in an XML format to standard output");
             * */
        }

        public static void DisplayProperties(TraceSource ts)
        {
            Console.WriteLine("TraceSource name = " + ts.Name);
            Console.WriteLine("TraceSource switch level = " + ts.Switch.Level);
            Console.WriteLine("TraceSource switch = " + ts.Switch.DisplayName);
            SwitchAttribute[] switches = SwitchAttribute.GetAll(Assembly.GetExecutingAssembly());
            foreach (var s in switches)
            {
                Console.WriteLine("Switch name = " + s.SwitchName);
                Console.WriteLine("Switch type = " + s.SwitchType);
            }
#if(ConfigFile)
            Console.WriteLine("There IS a config file!");
            // Get the custom attributes for the TraceSource
            Console.WriteLine("Number of custom trace attributes = " + ts.Attributes.Count);
            foreach (DictionaryEntry de in ts.Attributes)
                Console.WriteLine("Custom trace source attribute = {0} {1}", de.Key, de.Value);
            //Get the custom attributes for the trace source switch.
            foreach (DictionaryEntry de in ts.Switch.Attributes)
                Console.WriteLine("Custom switch attribute = {0} {1}", de.Key, de.Value);
#endif
            Console.WriteLine("Number of listeners = " + ts.Listeners.Count);
            foreach (TraceListener tl in ts.Listeners)
            {
                Console.Write("TraceListener: {0}\t", tl.Name);
                Console.WriteLine("AssemblyQualifiedName = {0}", tl.GetType().AssemblyQualifiedName);
            }
        }

        private static void ListTraceOptions(TraceListener tl)
        {
            Console.Write("TraceOptions: ");
            if ((int)(tl.TraceOutputOptions) == 0)
                Console.Write("none");
            else
            {
                bool start = true;
                for (int i = 0; i <= 6; i++)
                {
                    if (((int)(tl.TraceOutputOptions) & (int)Math.Pow(2, i)) > 0)
                    {
                        Console.Write(!start ? ", " : "");
                        Console.Write((TraceOptions)((int)Math.Pow(2, i)));
                        start = false;
                    }
                }
            }
            Console.WriteLine();
        }

        private static void SomeStaticFunction()
        {
            Trace.CorrelationManager.StartLogicalOperation("SomeStaticFunction");
            Console.WriteLine("We are in SomeStaticFunction");
            ts.TraceEvent(TraceEventType.Warning, 4, "A TraceEvent occurring in SomeStaticFunction");
            Trace.CorrelationManager.StopLogicalOperation();
        }
    }
}
