using System;

namespace ArrayListClass
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WindowWidth = 160;
            Console.WindowHeight = 36;
            Console.BufferHeight = 2400;

            ArrayListReadOnlyTest.Run();
            ArrayListSynchronizedPropertyTest.Run();
            ArrayListItemPropertyTest.Run();
        }
    }
}
