using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;

namespace UsingTheMarshalClass
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Point
    {
        public int x;
        public int y;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct Rect
    {
        [FieldOffset(0)] public int left;
        [FieldOffset(4)] public int top;
        [FieldOffset(8)] public int right;
        [FieldOffset(12)] public int bottom;
    }

    class Win32API
    {
        [DllImport("User32.dll")]
        public static extern bool PtInRect(ref Rect r, Point p);
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Ok, so this is really pseudocode.
            /*
            Boolean f = Win32Call();
            if (!f)
                Console.WriteLine("Error: {0}", Marshal.GetLastWin32Error);
             * */

            Console.WriteLine("Struct: " + Marshal.SizeOf(typeof(Point)));
            Point p = new Point();
            Console.WriteLine("Object: " + Marshal.SizeOf(p));
        }
    }
}
