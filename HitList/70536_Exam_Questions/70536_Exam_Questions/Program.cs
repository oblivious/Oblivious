using System;
using System.Reflection;

namespace _70536_Exam_Questions
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter question number: ");
            var input = Console.ReadLine();

            if (input != null && input.Length < 3)
                input = input.PadLeft(3, '0');

            Type type;
            try
            {
                type = Assembly.GetExecutingAssembly().GetType("_70536_Exam_Questions.Questions.Q" + input);
                if (type == null)
                    throw new Exception();
            }
            catch (Exception)
            {
                Console.WriteLine("Q" + input + " is not a valid selection.");
                return;
            }

            MethodInfo methodInfo;
            try
            {
                methodInfo = type.GetMethod("Run");
            }
            catch (Exception)
            {
                Console.WriteLine("Q" + input + " does not implement the \"Run()\" method.");
                return;
            }

            try
            {
                methodInfo.Invoke(null, null);
            }
            catch (Exception)
            {
                Console.WriteLine("Exception thrown while attempting to execute the \"Run()\" method on class Q" + input + ".");
            }
        }
    }
}
