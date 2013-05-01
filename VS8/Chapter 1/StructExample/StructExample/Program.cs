using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StructExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Cycle degrees = new Cycle(0, 359);
            Cycle quarters = new Cycle(1, 4);

            for (int i = 0; i <= 8; i++)
            {
                degrees += 90;
                quarters += 1;
                Console.WriteLine("degrees = {0}, quarters = {1}", degrees, quarters);
            }

            Titles t = Titles.Dr;
            Console.WriteLine("{0}.", t);
        }
    }

    enum Titles { Mr, Ms, Mrs, Dr };

    struct Cycle
    {
        int val, min, max;

        public Cycle(int min, int max)
        {
            this.val = min;
            this.min = min;
            this.max = max;
        }

        public int Value
        {
            get { return this.val; }
            set
            {
                if (value > this.max)
                    this.Value = value - this.max + this.min - 1;
                else if (value < this.min)
                    this.Value = this.min - value + this.max - 1;
                else
                    this.val = value;
            }
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }

        public int ToInteger()
        {
            return Value;
        }

        public static Cycle operator +(Cycle arg1, int arg2)
        {
            arg1.Value += arg2;
            return arg1;
        }

        public static Cycle operator -(Cycle arg1, int arg2)
        {
            arg1.Value -= arg2;
            return arg1;
        }
    }
}
