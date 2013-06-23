using System;
using System.Collections;
using System.Threading;
using System.Globalization;

namespace ArrayListClass
{
    class ArrayListSynchronizedPropertyTest
    {
        public static void Run()
        {
            try
            {
                Utils.BannerChar('*');
                Utils.WriteCentred("Now announcing the weenie IsSynchronized Property Test!");
                Utils.BannerChar('*');

                Console.WriteLine("The following code is garbage that we already know...");
                ArrayList myCollection = new ArrayList();

                lock (myCollection.SyncRoot)
                {
                    foreach (object item in myCollection)
                    {
                        // Insert code here... duuuuhhhhhh
                    }
                }
                Console.WriteLine("The garbage has ended, so sayeth the wielder of the keyboard.");

                ArrayList myAL = new ArrayList();
                myAL.Add("The ");
                myAL.Add("quick ");
                myAL.Add("brown ");
                myAL.Add("fox ");
                myAL.Add("jumped ");
                myAL.Add("over ");
                myAL.Add("the ");
                myAL.Add("lazy ");
                myAL.Add("dog... ");

                ArrayList mySyncdAL = ArrayList.Synchronized(myAL);

                Console.WriteLine("myAL is {0}", myAL.IsSynchronized ? "synchronized" : "not synchronized");
                Console.WriteLine("mySyncdAL is {0}", mySyncdAL.IsSynchronized ? "synchronized" : "not synchronized");

                Console.WriteLine("\n\nHow boring...");
                Console.WriteLine("\nNow let's run a stress test on our synced ArrayList using a bunch of worker threads!\n");
                Console.Write("Want to continue? (y/n): ");
                int input = Console.Read();
                if (input == (byte)'y')
                {
                    Thread.Sleep(2000);
                    Console.WriteLine("Here goes!");
                    Thread.Sleep(2000);
                    Console.WriteLine("\nNow creating 100 worker threads to print out the contents of the array list 100 times, simultaneously:\n");
                    Thread.Sleep(2000);
                    for (int i = 0; i < 100; i++)
                    {
                        ThreadPool.QueueUserWorkItem(TortureTheArrayList, myAL);
                    }
                    Thread.Sleep(7000);
                    Console.WriteLine("\n\nFinished torturing the poor fox :)");
                }
                else
                    Console.WriteLine("Spoilsport");

                Console.WriteLine("\nNote, don't make changes to an enumerable while it's being enumerated. Exceptions shall be thrown!\n");

                Utils.BannerChar('*');
                Utils.WriteCentred("Thus ends the weenie IsSynchronized Property Test!");
                Utils.BannerChar('*');
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went terribly wrong, oh well: {0}", e.ToString());
            }
        }

        private static void TortureTheArrayList(object input)
        {
            try
            {
                ArrayList list = (ArrayList)input;
                for (int i = 0; i < 100; i++)
                {
                    foreach (string s in list)
                        Console.Write(s);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("A thread blew up... {0}" + e.ToString());
            }
        }
    }
}
