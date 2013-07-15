using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration.Install;
using System.ComponentModel;

namespace fabianse70536
{
    //[RunInstaller(true)]
    class MyInstaller : Installer 
    {
        public MyInstaller() : base()
        {
            this.Committing += new InstallEventHandler(Commiting_EH);
            this.Committed += new InstallEventHandler(Commited_EH);
        }

        private void Commiting_EH(object sender, InstallEventArgs e)
        {

        }

        private void Commited_EH(object sender, InstallEventArgs e)
        {

        }

        public override void Install(System.Collections.IDictionary stateSaver)
        {
            base.Install(stateSaver);
            
        }

        public override void Commit(System.Collections.IDictionary savedState)
        {
            base.Commit(savedState);
        }

        public override void Rollback(System.Collections.IDictionary savedState)
        {
            base.Rollback(savedState);
        }


    }
}
