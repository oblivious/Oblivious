using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Chapter03
{
    class StackAndQueue
    {
        static void Main()
        {
            Queue<int> queue = new Queue<int>();
            Stack<int> stack = new Stack<int>();

            for (int i = 0; i < 10; i++)
            {
                queue.Enqueue(i);
                stack.Push(i);
            }

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Stack:{0} Queue:{1}",
                                   stack.Pop(), queue.Dequeue());
            }
        }
    }
}
