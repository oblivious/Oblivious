using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;


namespace _70536_Exam_Questions.Questions
{
    class Q092
    {
        public static void Run()
        {
            Console.WriteLine("\nQ092 Start:\n");

            var t = typeof (string);

            var subString = t.GetMethod("Substring", new Type[] {typeof (int), typeof (int)});

            var result = subString.Invoke("Hello, World!", new object[] {7, 5});

            Console.WriteLine("{0} returned \"{1}\".", subString, result);

            Console.WriteLine("\nQ092 End...\n");
        }
    }
}
