using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Chapter10_LoggingAndSystemsManagement
{
    class StackTraceExample
    {
        public static void Run()
        {
            StackTraceExample sample = new StackTraceExample();
            try
            {
                sample.MyPublicMethod();
            }
            catch (Exception)
            {
                // Create a StackTrace that captures filename, line number, and column information for the current thread.
                StackTrace st = new StackTrace(true);
                for (int i = 0; i < st.FrameCount; i++)
                {
                    // Note that high up the call stack, there is only one stack frame.
                    StackFrame sf = st.GetFrame(i);
                    Console.WriteLine("\nHigh up the call stack, method: {0}", sf.GetMethod());

                    Console.WriteLine("High up the call stack, Line Number: {0}", sf.GetFileLineNumber());
                }
            }
        }

        public void MyPublicMethod()
        {
            MyProtectedMethod();
        }

        protected void MyProtectedMethod()
        {
            MyInternalClass mic = new MyInternalClass();
            mic.ThrowsException();
        }

        class MyInternalClass
        {
            public void ThrowsException()
            {
                try
                {
                    throw new Exception("A problem was encountered.");
                }
                catch (Exception e)
                {
                    // Create a StackTrace that captures filename, line number and column information.
                    StackTrace st = new StackTrace(true);
                    string stackIndent = "";
                    for (int i = 0; i < st.FrameCount; i++)
                    {
                        StackFrame sf = st.GetFrame(i);
                        Console.WriteLine();
                        Console.WriteLine(stackIndent + " Method: {0}", sf.GetMethod());
                        Console.WriteLine(stackIndent + " File: {0}", sf.GetFileName());
                        Console.WriteLine(stackIndent + " Line Number: {0}", sf.GetFileLineNumber());
                        stackIndent += "  ";
                    }
                    throw e;
                }
            }
        }
    }
}
