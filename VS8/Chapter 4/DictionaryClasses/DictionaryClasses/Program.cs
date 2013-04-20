using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace DictionaryClasses
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Collections.ArrayList al;
            System.Collections.BitArray ba;
            System.Collections.Hashtable ht;
            System.Collections.Queue q;
            System.Collections.SortedList sl;
            System.Collections.Stack s;

            System.Collections.Specialized.BitVector32 bv;
            System.Collections.Specialized.HybridDictionary hd;
            System.Collections.Specialized.ListDictionary ld; // Uses a singly linked list which is why it's only good for a few items.
            System.Collections.Specialized.NameValueCollection nvc;
            System.Collections.Specialized.OrderedDictionary od;
            System.Collections.Specialized.StringCollection sc;
            System.Collections.Specialized.StringDictionary sd;

            al = new ArrayList();
            ba = new BitArray(32);
            ht = new Hashtable();
            q = new Queue();
            sl = new SortedList();
            s = new Stack();
            
            //BitVector32... so much fun to be had.
            //RunBitVector32Example();
            
            bool item;
            int trues;
            DateTime start, end;
            start = DateTime.Now;
            item = false;
            trues = 0;
            ba = new BitArray(20);
            for (int i = 0; i < 20; i += 2)
            {
                ba.Set(i, true);
            }
            Console.Write("BitArray {");
            for (int i = 0; i < 20; i++ )
                Console.Write(ba.Get(i) ? "1" : "0");
            Console.WriteLine("}");

            for (int j = 0; j < 1000000; j++)
            {
                for (int i = 0; i < 20; i++)
                {
                    item = ba.Get(i);
                    if (item)
                        trues++;
                }
            }
            end = DateTime.Now;
            Console.WriteLine("{0} trues values were counted...", trues);
            Console.WriteLine("BitArray took: {0}ms", (end - start).TotalMilliseconds.ToString("0.00"));

            bv = new BitVector32();
            start = DateTime.Now;
            item = false;
            trues = 0;
            int l = 1;
            for (int i = 0; i < 10; i++)
            {
                bv[l] = true;
                l *= 4;
            }
            Console.WriteLine(Math.Pow(2, 2));
            Console.WriteLine(bv.ToString());
            for (int j = 0; j < 1000000; j++)
            {
                int k = 1;
                for (int i = 0; i < 20; i++)
                {
                    item = bv[k];
                    k *= 2;
                    if (item)
                        trues++;
                }
            }
            end = DateTime.Now;
            Console.WriteLine("{0} trues values were counted...", trues);
            Console.WriteLine("BitVector32 took: {0}ms", (end - start).TotalMilliseconds.ToString("0.00"));
        }

        private static void RunBitVector32Example()
        {
            // Creates and initializes a BitVector32 with all bit flags set to FALSE.
            BitVector32 myBV = new BitVector32(0);

            // Creates masks to isolate each of the first five bit flags. 
            int myBit1 = BitVector32.CreateMask();
            int myBit2 = BitVector32.CreateMask(myBit1);
            int myBit3 = BitVector32.CreateMask(myBit2);
            int myBit4 = BitVector32.CreateMask(myBit3);
            int myBit5 = BitVector32.CreateMask(myBit4);

            Console.WriteLine("Mask Values: myBit1: {0}, myBit2: {1}, myBit3: {2}, myBit4: {3}, myBit5: {4}", myBit1, myBit2, myBit3, myBit4, myBit5);

            // Sets the alternating bits to TRUE.
            Console.WriteLine("Setting alternating bits to TRUE:");
            Console.WriteLine("   Initial:         {0}", myBV.ToString());
            myBV[myBit1] = true;
            Console.WriteLine("   myBit1 = TRUE:   {0}", myBV.ToString());
            myBV[myBit3] = true;
            Console.WriteLine("   myBit3 = TRUE:   {0}", myBV.ToString());
            myBV[myBit5] = true;
            Console.WriteLine("   myBit5 = TRUE:   {0}", myBV.ToString());

            // Creates and initializes a BitVector32.
            myBV = new BitVector32(0);

            // Creates four sections in the BitVector32 with maximum values 6, 3, 1, and 15. 
            // mySect3, which uses exactly one bit, can also be used as a bit flag.
            BitVector32.Section mySect1 = BitVector32.CreateSection(6);
            BitVector32.Section mySect2 = BitVector32.CreateSection(3, mySect1);
            BitVector32.Section mySect3 = BitVector32.CreateSection(1, mySect2);
            BitVector32.Section mySect4 = BitVector32.CreateSection(15, mySect3);

            // Displays the values of the sections.
            Console.WriteLine("Initial values:");
            Console.WriteLine("\tmySect1: {0}", myBV[mySect1]);
            Console.WriteLine("\tmySect2: {0}", myBV[mySect2]);
            Console.WriteLine("\tmySect3: {0}", myBV[mySect3]);
            Console.WriteLine("\tmySect4: {0}", myBV[mySect4]);

            // Sets each section to a new value and displays the value of the BitVector32 at each step.
            Console.WriteLine("Changing the values of each section:");
            Console.WriteLine("\tInitial:    \t{0}", myBV.ToString());
            myBV[mySect1] = 5;
            Console.WriteLine("\tmySect1 = 5:\t{0}", myBV.ToString());
            myBV[mySect2] = 3;
            Console.WriteLine("\tmySect2 = 3:\t{0}", myBV.ToString());
            myBV[mySect3] = 1;
            Console.WriteLine("\tmySect3 = 1:\t{0}", myBV.ToString());
            myBV[mySect4] = 9;
            Console.WriteLine("\tmySect4 = 9:\t{0}", myBV.ToString());

            // Displays the values of the sections.
            Console.WriteLine("New values:");
            Console.WriteLine("\tmySect1: {0}", myBV[mySect1]);
            Console.WriteLine("\tmySect2: {0}", myBV[mySect2]);
            Console.WriteLine("\tmySect3: {0}", myBV[mySect3]);
            Console.WriteLine("\tmySect4: {0}", myBV[mySect4]);
        }
    }
}
