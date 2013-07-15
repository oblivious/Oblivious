using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuidTest
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 20; i++)
            {
                Guid guid = Guid.NewGuid();
                Console.WriteLine(guid);
            }
        }
    }
}
