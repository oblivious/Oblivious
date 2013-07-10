using System;

namespace CodeProjectWMI
{
    class Program
    {
        static void Main( string[] args )
        {
            Console.BufferHeight = 5000;
            Console.WindowWidth = 162;
            Console.WindowHeight = 56;

            ManagementScopeTest.Run();
            //TackingOnManagementEventWatcher.Run();
            //Part2.Run();
            /*

            ManagementScope scope = new ManagementScope(@"\\.\root\cimv2");

            ManagementClass task = new ManagementClass(scope, new ManagementPath("DEFAULT:StdRegProv"), new ObjectGetOptions());

            ManagementBaseObject methodParams = task.GetMethodParameters("EnumKey");

            methodParams["hdefKey"] = baseKey.HKEY_LOCAL_MACHINE;
            methodParams["sSubKeyName"] = @"SOFTWARE\Microsoft\Windows\CurrentVersion";

            ManagementBaseObject exitCode = task.InvokeMethod("EnumKey", methodParams, null);

            string[] subKeys = (string[])exitCode["sNames"];

            foreach ( var s in subKeys )
            {
                Console.WriteLine(s);
            }
             * */
        }
    }

    public enum baseKey : uint
    {
        HKEY_CLASSES_ROOT = 0x80000000,
        HKEY_CURRENT_USER = 0x80000001,
        HKEY_LOCAL_MACHINE = 0x80000002,
        HKEY_USERS = 0x80000003,
        HKEY_CURRENT_CONFIG = 0x80000005
    }
    public enum valueType
    {
        BINARY,
        DWORD,
        EXPANDED_STRING,
        MULTI_STRING,
        STRING
    }
}
