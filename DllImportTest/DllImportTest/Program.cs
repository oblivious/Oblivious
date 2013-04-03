using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace DllImportTest
{
    class Program
    {
        [DllImport("user32.dll", EntryPoint = "MessageBox")]
        private static extern int ShowBox(IntPtr hWnd, string text, string caption, uint type);

        static void Main(string[] args)
        {
            ShowBox(new IntPtr(0), "Hello, world!", "My box", 0);
        }
    }
}
