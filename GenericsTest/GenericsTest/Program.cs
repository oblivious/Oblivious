using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericsTest
{
    class Program
    {
        const int quantity = 10000000;

        static void Main(string[] args)
        {
            int value;
            long start, end;
            for (int j = 0; j < 10; j++)
            {
                value = 0;
                Stack objectStack = new Stack(quantity);
                start = DateTime.Now.Ticks;
                for (int i = 0; i < quantity; i++)
                {
                    objectStack.Push(i);
                }
                for (int i = 0; i < quantity; i++)
                {
                    value += (int)objectStack.Pop();
                }
                end = DateTime.Now.Ticks;
                Console.WriteLine("Object based method returned value {0} in {1} ticks", value, end - start);

                value = 0;
                Stack<int> genericStack = new Stack<int>(quantity);
                start = DateTime.Now.Ticks;
                for (int i = 0; i < quantity; i++)
                {
                    genericStack.Push(i);
                }
                for (int i = 0; i < quantity; i++)
                {
                    value += genericStack.Pop();
                }
                end = DateTime.Now.Ticks;
                Console.WriteLine("Generics based method returned value {0} in {1} ticks", value, end - start);
            }
        }
    }

    public class Stack
    {
        readonly int m_Size;
        int m_StackPointer = 0;
        object[] m_Items;
        public Stack()
            : this(100)
        {}
        public Stack(int size)
        {
            m_Size = size;
            m_Items = new object[m_Size];
        }

        public void Push(object item)
        {
            if (m_StackPointer >= m_Size)
                throw new StackOverflowException();
            m_Items[m_StackPointer] = item;
            m_StackPointer++;
        }

        public object Pop()
        {
            m_StackPointer--;
            if (m_StackPointer >= 0)
                return m_Items[m_StackPointer];
            else
            {
                m_StackPointer = 0;
                throw new InvalidOperationException("Cannot pop an empty stack");
            }
        }
    }

    public class Stack<T>
    {
        readonly int m_Size;
        int m_StackPointer = 0;
        T[] m_Items;

        public Stack()
            : this(100)
        {
        }

        public Stack(int size)
        {
            m_Size = size;
            m_Items = new T[m_Size];
        }

        public void Push(T item)
        {
            if (m_StackPointer >= m_Size)
                throw new StackOverflowException();
            m_Items[m_StackPointer] = item;
            m_StackPointer++;
        }
        public T Pop()
        {
            m_StackPointer--;
            if (m_StackPointer >= 0)
            {
                return m_Items[m_StackPointer];
            }
            else
            {
                m_StackPointer = 0;
                throw new InvalidOperationException("Cannot pop an empty stack");
            }
        }
    }
}
