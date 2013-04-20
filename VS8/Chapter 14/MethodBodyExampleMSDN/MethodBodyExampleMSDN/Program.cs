using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace MethodBodyExampleMSDN
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get method body information
            MethodInfo mi = typeof(Program).GetMethod("MethodBodyExample");
            MethodBody mb = mi.GetMethodBody();
            Console.WriteLine("\r\nMethod: {0}", mi);

            // Display the general information included in the MethodBody object.
            Console.WriteLine("    Local variables are initialized: {0}", mb.InitLocals);
            Console.WriteLine("    Maximum number of items on the operand stack: {0}", mb.MaxStackSize);

            // Display information about the local variables in the method body.
            Console.WriteLine();
            foreach (LocalVariableInfo lvi in mb.LocalVariables)
                Console.WriteLine("Local variable: {0}", lvi);

            // Display exception handling clauses.
            Console.WriteLine();
            foreach (ExceptionHandlingClause ehc in mb.ExceptionHandlingClauses)
            {
                Console.WriteLine(ehc.Flags.ToString());

                // The FilterOffset property is meaningful only for Filter clauses.
                // The CatchType property is not meaningful for Filter or Finally clauses.
                switch (ehc.Flags)
                {
                    case ExceptionHandlingClauseOptions.Filter:
                        Console.WriteLine("        Filter Offset: {0}", ehc.FilterOffset);
                        break;
                    case ExceptionHandlingClauseOptions.Finally:
                        break;
                    default:
                        Console.WriteLine("    Type of exception: {0}", ehc.CatchType);
                        break;
                }

                Console.WriteLine("       Handler Length: {0}", ehc.HandlerLength);
                Console.WriteLine("       Handler Offset: {0}", ehc.HandlerOffset);
                Console.WriteLine("     Try Block Length: {0}", ehc.TryLength);
                Console.WriteLine("     Try Block Offset: {0}", ehc.TryOffset);
            }
        }

        // The Main method contains code to analyze this method, using the
        // properties and methods of the MethodBody class.
        public void MethodBodyExample(object arg)
        {
            // Define some local variable. In addition to these variables,
            // the local variable list includes the variables scoped to
            // the catch clauses.
            int var1 = 42;
            string var2 = "Forty-two";

            try
            {
                // Depending on the input value, throw an ArgumentException or an
                // ArgumentNullException to test the Catch clauses.
                if (arg == null)
                {
                    throw new ArgumentNullException("The argument cannot be null.");
                }
                if (arg.GetType() == typeof(string))
                {
                    throw new ArgumentException("The argument cannot be a string.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ordinary exception-handling clause caught: {0}", ex.GetType());
            }
            finally
            {
                var1 = 3033;
                var2 = "Another string.";
            }
        }
    }
}
