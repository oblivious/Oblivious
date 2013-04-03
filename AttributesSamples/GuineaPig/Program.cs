using System;
using System.Collections.Generic;
using System.Text;

namespace GuineaPig
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("I'm a Guinea Pig!");
        }
    }

    public class GuineaPig1
    {
        public override string ToString()
        {
            return "I'm GuineaPig1";
        }
    }

    public class GuineaPig2
    {
        public override string ToString()
        {
            return "I'm GuineaPig2";    
        }
    }
}
