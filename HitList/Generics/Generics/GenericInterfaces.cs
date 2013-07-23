using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generics
{
    class GenericInterfaces
    {
        public static void Run()
        {

        }
    }

    // Type parameter T in angle brackets.
    public class GenericList<T> : IEnumerable<T>
    {
        protected Node head;
        protected Node current = null;

        // Nested class is also generic on T
        protected class Node
        {
            public Node next;
            private T data; //T as private member datatype

            public Node(T t) // T used in non-generic constructor
            {
                next = null;
                data = t;
            }

            public Node Next
            {
                get { return next; }
                set { next = value; }
            }

            public T Data //T as return type of property
            {
                get { return data; }
                set { data = value; }
            }
        }

        public GenericList() // Constructor
        {
            head = null;
        }

        public void AddHead(T t) // T as method parameter type
        {
            Node n = new Node(t);
            n.Next = head;
            head = n;
        }

        // Implementation of the iterator
        public IEnumerator<T> GetEnumerator()
        {
            Node current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        // IEnumerable<T> inherits from IEnumerable, therefore this class must implement both the generic and non-generic
        // versions of GetEnumerator. In more cases, the non-geneic method can simply call the generic method.
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }


}
