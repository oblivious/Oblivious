using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace EnumWindows
{
    public delegate bool CallBack(int hwnd, int lParam);

    class Program
    {
        // Create a prototype for the function
        [DllImport("User32.dll")]
        public static extern int EnumWindows(CallBack x, int y);

        // Declare the method to handle the callback
        public static bool Report(int hwnd, int lParam)
        {
            Console.WriteLine("Window handle is " + hwnd);
            return true;
        }

        public static void Main(string[] args)
        {
            // Create an instance of the delegate
            CallBack myCallBack = new CallBack(Program.Report);

            // Call the function
            EnumWindows(myCallBack, 0);
        }
    }
}
