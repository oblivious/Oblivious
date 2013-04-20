using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace UsingMarshal
{
    public struct Point
    {
        public int x, y;
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Demonstrate the use of public static fields of the Marshal class.
            Console.WriteLine("SystemDefaultCharSize={0}, SystemMaxDBCSCharSize={1}",
                Marshal.SystemDefaultCharSize, Marshal.SystemMaxDBCSCharSize);

            // Demonstrate the use of the SizeOf method of the Marshal class.
            Console.WriteLine("Number of bytes needed by a Point struct: {0}", Marshal.SizeOf(typeof(Point)));
            Point p = new Point();
            Console.WriteLine("Number of bytes needed by a Point object: {0}", Marshal.SizeOf(p));

            // Demonstrate how to call GlobalAlloc and GlobalFree using the Marshal class.
            // This allocates memory and returns a point to that memory in the unmanaged memory of the application.
            IntPtr hGlobal = Marshal.AllocHGlobal(100);
            Marshal.FreeHGlobal(hGlobal);

            // Demonstrates how to use the Marshal class to get the Win32 error code when a Win32 method fails.
            Boolean f = CloseHandle(new IntPtr(-1));
            if (!f)
                Console.WriteLine("CloseHandle call failed with an error code of: {0}", Marshal.GetLastWin32Error());
        }

        [DllImport("Kernel32", ExactSpelling = true, SetLastError = true)]
        static extern Boolean CloseHandle(IntPtr h);
    }
}
