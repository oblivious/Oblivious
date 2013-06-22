using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iterators_CSharpProgramming
{
    class Program
    {
        static void Main(string[] args)
        {
            // - An iterator is a section of code that returns an ordered sequence of values of the same type.
            // - An iterator can be used as the body of a method, an operator, or a get accessor.
            // - The iterator code uses the yield return statement to return each element in turn. yield break ends the iteration.
            // - Multiple iterators can be implemented on a class. Each iterator must have a unique name just like any class member, and
            // -  can be invoked by client code in a foreach statement as follows: foreach (int x in SampleClass.Iterator2){}
            // - The return type of an iterator must be IEnumerable, IEnumerator, IEnumerator<T>, or IEnumerator<T>.
            // - Iterators are the basis for the deferred execution behaviour in LINQ queries.

            // The yield keyword is used to specify the value, or values, that are returned. When the yield return statement is reached, 
            //  the current location is stored. Execution is restarted from this location the next time that the iterator is called.

            // Iterators are especially useful with collection classes, providing an easy way to iterate complex data structures such as binary trees.


            SimpleExample.Run();
        }
    }
}
