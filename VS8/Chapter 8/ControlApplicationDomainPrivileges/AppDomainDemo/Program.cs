using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Policy;
using System.Security;

namespace AppDomainDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            object[] hostEvidence = {new Zone(SecurityZone.MyComputer)};
            Evidence e = new Evidence(hostEvidence, null);

            // Create an AppDomain.
            AppDomain d = AppDomain.CreateDomain("New Domain", e);

            // Run the assembly
            // TODO: Edit the path to the executable file
            d.ExecuteAssemblyByName("ShowWinIni");
        }
    }
}
