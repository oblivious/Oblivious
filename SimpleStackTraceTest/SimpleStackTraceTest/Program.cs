using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleStackTraceTest
{
    /// <summary>
    /// The point of this is to show the stack trace behaviour when exceptions are handled in different ways.
    /// It's very easy to lose data if you're not careful.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowWidth = 150;
            Console.WindowHeight = 40;
            ExceptionThrower thrower = new ExceptionThrower();

            thrower.MyMethod2();

            Console.WriteLine("Our first test preserves the stack trace by storing the exception in a variable:\n");
            Console.WriteLine(thrower.MyException.ToString() + "\n\n\n");

            try
            {
                thrower.MyMethod3();
            }
            catch (Exception e)
            {
                Console.WriteLine("The second test also preserves the stack trace by rethrowing the " +
                    "exception as an inner exception:\n");
                Console.WriteLine(e.ToString() + "\n\n\n");
            }

            try
            {
                thrower.MyMethod4();
            }
            catch (Exception e)
            {
                Console.WriteLine("Our final test is utter wank, as it rethrows the exception, resetting the stack trace:\n");
                Console.WriteLine(e.ToString());
            }
        }
    }

    class ExceptionThrower
    {
        public Exception MyException { get; set; }

        private void MyMethod1()
        {
            throw new Exception("My exception");
        }

        public void MyMethod2()
        {
            try
            {
                this.MyMethod1();
            }
            catch (Exception e)
            {
                this.MyException = e;
            }
        }

        public void MyMethod3()
        {
            try
            {
                this.MyMethod1();
            }
            catch (Exception e)
            {
                throw new Exception("We've got a live one here!", e);
            }
        }

        public void MyMethod4()
        {
            try
            {
                this.MyMethod1();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
