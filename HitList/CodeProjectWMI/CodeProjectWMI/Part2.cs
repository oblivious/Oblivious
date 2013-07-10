using System;
using System.Management;

namespace CodeProjectWMI
{
    class Part2
    {
        public static void Run()
        {
            string processName = "notepad.exe";
            Console.WriteLine("Creating Process: " + processName);
            Console.WriteLine(StartProcess(Environment.MachineName, processName));
        }

        private static string StartProcess( string machineName, string processPath )
        {
            ManagementClass processTask = new ManagementClass(@"\\" + machineName + @"\root\cimv2", "Win32_Process", null);

            ManagementBaseObject methodParams = processTask.GetMethodParameters("Create");
            methodParams["CommandLine"] = processPath;

            ManagementBaseObject exitCode = processTask.InvokeMethod("Create", methodParams, null);

            return TranslateProcessStartExitCode(exitCode["ReturnValue"].ToString());
        }

        private static string TranslateProcessStartExitCode( string exitCode )
        {
            string code = string.Empty;
            int eCode = Convert.ToInt32(exitCode);
            switch ( eCode )
            {
                case 0:
                    return "Successful (Completion)";
                case 1:
                    return "Access (Denied)";
                case 2:
                    return "Insufficient (Privilege)";
                case 8:
                    return "Unknown (Failure)";
                case 9:
                    return "Path (Not Found)";
                case 21:
                    return "Invalid (Parameter)";
                default:
                    return "Unknown Code: " + eCode;
            }
        }
    }


}
