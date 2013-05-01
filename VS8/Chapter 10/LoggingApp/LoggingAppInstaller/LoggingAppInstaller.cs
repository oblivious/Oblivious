using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Configuration.Install;
using System.ComponentModel;
using System.Collections;

namespace LoggingAppInstaller
{
    [RunInstaller(true)]
    public class InstallLog : Installer
    {
        public InstallLog()
            : base()
        {
        }

        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);
        }

        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);
            if (!EventLog.Exists("LoggingApp Log"))
                EventLog.CreateEventSource("LoggingApp", "LoggingApp Log");
        }

        public override void Rollback(IDictionary savedState)
        {
            base.Rollback(savedState);
            RemoveLog();
        }

        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);
            RemoveLog();
        }

        private void RemoveLog()
        {
            if (EventLog.Exists("LoggingApp Log"))
            {
                EventLog.DeleteEventSource("LoggingApp");
                EventLog.Delete("LoggingApp Log");
            }
        }
    }
}
