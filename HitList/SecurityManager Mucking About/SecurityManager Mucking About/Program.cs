using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Security.Policy;
using Extensions;
using System.Security.Permissions;

namespace SecurityManager_Mucking_About
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ArrayList zone;
                ArrayList origin;

                SecurityManager.GetZoneAndOrigin(out zone, out origin);

                Console.WriteLine("Listing zone information:");
                foreach (var v in zone)
                {
                    Zone z = (Zone)v;
                    WriteToFitConsole("ToString(): " + z.ToString(), 4);
                    WriteToFitConsole("\tSecurityZone: " + z.SecurityZone, 4);
                }

                Console.WriteLine("\n\nListing origin information:");
                foreach (var v in origin)
                {
                    UrlIdentityPermission u = (UrlIdentityPermission)v;
                    WriteToFitConsole("ToString(): " + u.ToString(), 4);
                    WriteToFitConsole("\tSecurityZone: " + u.Url, 4);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("It done broke: " + e.ToString());
            }
        }

        public static void WriteToFitConsole(string input, int indent)
        {
            if (input == null)
                return;

            int consoleWidth = Console.WindowWidth;

            if (indent + input.Length <= consoleWidth)
            {
                Console.WriteLine(GetIndent(indent) + input);
            }
            else
            {
                while (input.Length > 0)
                {
                    int written = consoleWidth - indent;
                    Console.WriteLine(GetIndent(indent) + (input.Length >= written ? input.Substring(0, written) : input));
                    input = input.Length > written ? input.Substring(written, input.Length - written) : string.Empty;
                }
            }
        }

        private static string GetIndent(int indent)
        {
            StringBuilder output = new StringBuilder();
            for (int i = 0; i < indent; i++)
                output.Append(" ");
            return output.ToString();
        }
    }
}
