using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration.Install;
using System.ComponentModel;
using System.Diagnostics;

namespace PerfAppInstaller
{
    [RunInstaller(true)]
    public class InstallCounter : Installer
    {
        public InstallCounter()
            : base()
        {
        }

        public override void Install(System.Collections.IDictionary stateSaver)
        {
            base.Install(stateSaver);
            if (!PerformanceCounterCategory.Exists("PerfApp"))
                PerformanceCounterCategory.Create("PerfApp", "Counters for PerfApp",
                    PerformanceCounterCategoryType.SingleInstance, "Clicks", "Times the user has clicked the button.");
        }

        public override void Commit(System.Collections.IDictionary savedState)
        {
            base.Commit(savedState);
        }

        public override void Rollback(System.Collections.IDictionary savedState)
        {
            base.Rollback(savedState);
            if (PerformanceCounterCategory.Exists("PerfApp"))
                PerformanceCounterCategory.Delete("PerfApp");
        }

        public override void Uninstall(System.Collections.IDictionary savedState)
        {
            base.Uninstall(savedState);
            if (PerformanceCounterCategory.Exists("PerfApp"))
                PerformanceCounterCategory.Delete("PerfApp");
        }
    }
}
