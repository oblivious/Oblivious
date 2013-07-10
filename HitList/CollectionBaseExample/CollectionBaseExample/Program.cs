using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollectionBaseExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create an init a new CollectionBase.
            Int16Collection myI16 = new Int16Collection();

            // Add elements to the collection.
            myI16.Add((short) 1);
            myI16.Add((short) 2);
            myI16.Add((short) 3);
            myI16.Add((short) 5);
            myI16.Add((short) 7);

            // Display the contents of the collection using foreach. This is the preferred method.
            Console.WriteLine( "Contents of the collection (using foreach):" );
            PrintValues1( myI16 );
          
            // Display the contents of the collection using the enumerator.
            Console.WriteLine( "Contents of the collection (using enumerator):" );
            PrintValues2( myI16 );
          
            // Display the contents of the collection using the Count property and the Item property.
            Console.WriteLine( "Initial contents of the collection (using Count and Item):" );
            PrintIndexAndValues( myI16 );
          
            // Search the collection with Contains and IndexOf.
            Console.WriteLine( "Contains 3: {0}", myI16.Contains( 3 ) );
            Console.WriteLine( "2 is at index {0}.", myI16.IndexOf( 2 ) );
            Console.WriteLine();
          
            // Insert an element into the collection at index 3.
            myI16.Insert( 3, (Int16) 13 );
            Console.WriteLine( "Contents of the collection after inserting at index 3:" );
            PrintIndexAndValues( myI16 );
          
            // Get and set an element using the index.
            myI16[4] = 123;
            Console.WriteLine( "Contents of the collection after setting the element at index 4 to 123:" );
            PrintIndexAndValues( myI16 );
          
            // Remove an element from the collection.
            myI16.Remove( (Int16) 2 );
          
            // Display the contents of the collection using the Count property and the Item property.
            Console.WriteLine( "Contents of the collection after removing the element 2:" );
            PrintIndexAndValues( myI16 );
        }

        private static void PrintIndexAndValues( Int16Collection myCol )
        {
            for (int i = 0; i < myCol.Count; i++)
            {
                Console.WriteLine("\t[{0}]:\t{1}", i, myCol[i]);
            }
            Console.WriteLine();
        }

        private static void PrintValues1(Int16Collection myI16)
        {
            Console.Write("\n\t");
            int i = 1;
 	        foreach (var i16 in myI16)
            {
                Console.Write(i16);
                if (i != myI16.Count)
                    Console.Write(", ");
                i++;
            }
            Console.WriteLine("\n");
        }
        
        private static void PrintValues2(Int16Collection myCol)
        {
            Console.Write("\n\t");

            IEnumerator myEnumerator = myCol.GetEnumerator();
            int i = 0;
            while ( myEnumerator.MoveNext() )
            {
                Console.Write("{0}", myEnumerator.Current);
                if (++i != myCol.Count)
                    Console.Write(", ");
            }
            Console.WriteLine("\n"); 
        }
    }

    public class Int16Collection : CollectionBase
    {
        public short this[int index]
        {
            get { return (short)List[index]; }
            set { List[index] = value; }
        }

        public int Add( short value )
        {
            return List.Add(value);
        }

        public int IndexOf( short value )
        {
            return List.IndexOf(value);
        }

        public void Insert( int index, short value )
        {
            List.Insert( index, value );
        }

        public void Remove ( short value)
        {
            List.Remove( value );
        }

        public bool Contains ( short value )
        {
            return List.Contains( value );
        }

        protected override void OnInsert( int index, object value )
        {
            // Whatever
        }

        protected override void OnRemove( int index, object value )
        {
            // Whatever
        }

        protected override void OnSet( int index, object oldValue, object newValue )
        {
            // Whatever
        }

        protected override void OnValidate( object value )
        {
            if ( value.GetType() != typeof(System.Int16) )
                throw new ArgumentException("value must be of type Int16", "value");
        }
    }
}
