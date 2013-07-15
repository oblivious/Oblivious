using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Chapter10_LoggingAndSystemsManagement
{
    public class TraceSwitchExample
    {
        private static TraceSwitch appSwitch = new TraceSwitch("mySwitch", "Switch in config file");
        private static BooleanSwitch boolSwitch = new BooleanSwitch("booleanSwitch", "Switch in config file");

        public static void Run()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("The following is the configuration data for this trace switch: \n");
            sb.AppendLine("    <configuration>");
            sb.AppendLine("      <system.diagnostics>");
            sb.AppendLine("    	 <switches>");
            sb.AppendLine("    	   <add name=\"mySwitch\" value=\"1\"/>");
            sb.AppendLine("    	   <add name=\"booleanSwitch\" value=\"1\"/>");
            sb.AppendLine("    	 </switches>");
            sb.AppendLine("      </system.diagnostics>");
            sb.AppendLine("    </configuration>");
            Console.WriteLine(sb.ToString());

            Console.WriteLine("\n\nThe TraceSwitch is defined as a static variable of this class as:");
            WriteColour("   private static ", ConsoleColor.Blue, ConsoleColor.White);
            WriteColour("TraceSwitch", ConsoleColor.DarkCyan, ConsoleColor.White);
            WriteColour(" appSwitch = ", ConsoleColor.Black, ConsoleColor.White);
            WriteColour("new ", ConsoleColor.Blue, ConsoleColor.White);
            WriteColour("TraceSwitch", ConsoleColor.DarkCyan, ConsoleColor.White);
            WriteColour("(", ConsoleColor.Black, ConsoleColor.White);
            WriteColour("\"mySwitch\"", ConsoleColor.DarkRed, ConsoleColor.White);
            WriteColour(", ", ConsoleColor.Black, ConsoleColor.White);
            WriteColour("\"Switch in config file\"", ConsoleColor.DarkRed, ConsoleColor.White);
            WriteColour(");   ", ConsoleColor.Black, ConsoleColor.White);

            Console.WriteLine("\n\nThe output of the example is:\n");

            Console.WriteLine("Trace switch {0} configured as {1}", appSwitch.DisplayName, appSwitch.Level.ToString());

            Console.WriteLine("The level of appSwitch is {0}", appSwitch.Level);
            Console.WriteLine("As a result the levels are:");
            Console.WriteLine("appSwitch.TraceInfo is " + appSwitch.TraceInfo);
            Console.WriteLine("appSwitch.TraceVerbose is " + appSwitch.TraceVerbose);
            Console.WriteLine("appSwitch.TraceWarning is " + appSwitch.TraceWarning);
            Console.WriteLine("appSwitch.TraceError is " + appSwitch.TraceError);

            Console.WriteLine("\n\nSetting appSwitch.Level to TraceLevel.Off.");
            appSwitch.Level = TraceLevel.Off;
            Console.WriteLine("As a result the levels are:");
            Console.WriteLine("appSwitch.TraceInfo is " + appSwitch.TraceInfo);
            Console.WriteLine("appSwitch.TraceVerbose is " + appSwitch.TraceVerbose);
            Console.WriteLine("appSwitch.TraceWarning is " + appSwitch.TraceWarning);
            Console.WriteLine("appSwitch.TraceError is " + appSwitch.TraceError);

            Console.WriteLine("\nThen there is the BooleanSwitch class...");
            Console.WriteLine("\n\nThe BooleanSwitch is defined as a static variable of this class as:");
            WriteColour("   private static ", ConsoleColor.Blue, ConsoleColor.White);
            WriteColour("BooleanSwitch", ConsoleColor.DarkCyan, ConsoleColor.White);
            WriteColour(" boolSwitch = ", ConsoleColor.Black, ConsoleColor.White);
            WriteColour("new ", ConsoleColor.Blue, ConsoleColor.White);
            WriteColour("BooleanSwitch", ConsoleColor.DarkCyan, ConsoleColor.White);
            WriteColour("(", ConsoleColor.Black, ConsoleColor.White);
            WriteColour("\"booleanSwitch\"", ConsoleColor.DarkRed, ConsoleColor.White);
            WriteColour(", ", ConsoleColor.Black, ConsoleColor.White);
            WriteColour("\"Switch in config file\"", ConsoleColor.DarkRed, ConsoleColor.White);
            WriteColour(");   ", ConsoleColor.Black, ConsoleColor.White);

            Console.WriteLine("\n\nboolSwitch.Enabled = " + boolSwitch.Enabled);
        }

        private static void WriteColour(string input, ConsoleColor foreground, ConsoleColor background)
        {
            Console.ForegroundColor = foreground;
            Console.BackgroundColor = background;
            Console.Write(input);
            Console.ResetColor();
        }
    }
}
