using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(GetSum(3, 3) + GetSum(5, 5) - GetSum(15, 15));
        }
        private static int GetSum(int input, int amount)
        {
            return input < 1000 ? input + GetSum(input + amount, amount) : 0;
        }
    }
}
