﻿using System;

namespace Chapter15
{
    class ArrayBoundsChecking
    {
        static void Main(string[] args)
        {
            for (int i = 0; i <= args.Length; i++)
            {
                Console.WriteLine(args[i]);
            }
        }
    }
}
