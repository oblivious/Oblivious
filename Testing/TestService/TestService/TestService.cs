using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace TestService
{
    enum SimpleServiceCustomCommands
    {
        StopWorker = 128,
        RestartWorker,
        CheckWorker
    }

    public partial class TestService : ServiceBase
    {
        public TestService()
        {
            InitializeComponent();
            if (!EventLog.SourceExists("MySource"))
            {
                EventLog.CreateEventSource("MySource", "MyNewLog");
            }
            eventLog1.Source = "MySource";
            eventLog1.Log = "MyNewLog";
        }

        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("In OnStart");
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("In OnStop");
        }

        protected override void OnContinue()
        {
            eventLog1.WriteEntry("In OnContinue");
        }

        protected override void OnCustomCommand(int command)
        {
            eventLog1.WriteEntry("Received custom command: " + (SimpleServiceCustomCommands)command);
        }
    }
}
