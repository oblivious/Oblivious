using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Win32MessageBox
{
    class Win32MessageBox
    {
        [DllImport("User32.dll")]
        private static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

        public static void Show(string message, string caption)
        {
            MessageBox(new IntPtr(0), message, caption, 0);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Win32MessageBox.Show("Hello, world!", "My box");
        }
    }
}
