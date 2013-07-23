using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace WindowsImpersonationContextClass
{
    // Example from: http://msdn.microsoft.com/en-us/library/system.security.principal.windowsimpersonationcontext.aspx
    class Program
    {
        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, int dwLoginType, int dwLogonProvider, out SafeTokenHandle phToken);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public extern static bool CloseHandle(IntPtr handle);

        [PermissionSetAttribute(SecurityAction.Demand, Name = "FullTrust")]
        public static void Main(string[] args)
        {
            SafeTokenHandle safeTokenHandle;
            try
        }
    }
}
