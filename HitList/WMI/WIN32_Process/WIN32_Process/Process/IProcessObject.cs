using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace baileysoft.Wmi.Process
{
    interface IProcessObject
    {
        ArrayList RunningProcesses();
        ArrayList ProcessProperties(string processName);
        string CreateProcess(string processPath);
        void TerminateProcess(string processName);
        void SetPriority(string processName, ProcessPriority.priority priority);
        string GetProcessOwner(string processName);
        string GetProcessOwnerSID(string processName);
    }
}
