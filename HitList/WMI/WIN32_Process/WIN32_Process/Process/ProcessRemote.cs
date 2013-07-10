using System;
using System.Collections;
using System.Collections.Generic;
using System.Management;
using System.Text;

namespace baileysoft.Wmi.Process
{
    class ProcessRemote
    {
        #region "fields"
        private string userName;
        private string password;
        private string domain;
        private string machineName;
        private ManagementScope connectionScope;
        private ConnectionOptions options;

        #endregion

        #region "constructors"
        public ProcessRemote(string userName,
                             string password,
                             string domain,
                             string machineName)
        {
            this.userName = userName;
            this.password = password;
            this.domain = domain;
            this.machineName = machineName;
            options = ProcessConnection.ProcessConnectionOptions();
            if (domain != null || userName != null)
            {
                options.Username = domain + "\\" + userName;
                options.Password = password;
            }
            connectionScope = ProcessConnection.ConnectionScope(machineName, options);
        }
        #endregion

        #region "polymorphic methods"
        public ArrayList RunningProcesses()
        {
            ArrayList alProcesses = new ArrayList();
            alProcesses = ProcessMethod.RunningProcesses(connectionScope);
            return alProcesses;
        }
        public ArrayList ProcessProperties(string processName)
        {
            ArrayList alProperties = new ArrayList();
            alProperties = ProcessMethod.ProcessProperties(connectionScope,
                                                           processName);
            return alProperties;
        }
        public string CreateProcess(string processPath)
        {
            return ProcessMethod.StartProcess(machineName, processPath);
        }
        public void TerminateProcess(string processName)
        {
            ProcessMethod.KillProcess(connectionScope, processName);
        }
        public void SetPriority(string processName, ProcessPriority.priority priority)
        {
            ProcessMethod.ChangePriority(connectionScope, processName, priority);
        }
        public string GetProcessOwner(string processName)
        {
            return ProcessMethod.ProcessOwner(connectionScope, processName);
        }
        public string GetProcessOwnerSID(string processName)
        {
            return ProcessMethod.ProcessOwnerSID(connectionScope, processName);
        }
        #endregion
    }
}
