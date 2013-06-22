using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ArrayListClass
{
    class ArrayListItemPropertyTest
    {
        public static void Run()
        {
            Utils.BannerChar('*');
            Utils.WriteCentred("Now announcing the pointless Item Property Test!");
            Utils.BannerChar('*');

            Console.WriteLine("Creating an ArrayList...");
            ArrayList myList = new ArrayList();
            myList.Add("red");
            myList.Add("green");
            myList.Add("blue");

            Utils.PrintArrayContents("myList", myList);

            Console.WriteLine("Now selecting the contents of the array using an index:");
            Console.WriteLine("   myList[0] = {0}", myList[0]);
            Console.WriteLine("   myList[1] = {0}", myList[1]);
            Console.WriteLine("   myList[2] = {0}", myList[2]);

            Console.WriteLine("You can also use the index to change the values:");
            Console.WriteLine("Setting myList[0] = \"purple\"");
            myList[0] = "purple";

            Utils.PrintArrayContents("myList", myList);

            Console.WriteLine("\nAll done with my tomfoolery.\n");

            ScrambleList.Run();

            Utils.BannerChar('*');
            Utils.WriteCentred("Thus ends the pointless Item Property Test!");
            Utils.BannerChar('*');
        }
    }

    class ScrambleList : ArrayList
    {
        public static void Run()
        {
            // Create an empty ArrayList, and add some elements.
            ScrambleList integerList = new ScrambleList();

            for (int i = 0; i < 10; i++)
                integerList.Add(i);

            Console.WriteLine("Ordered:\n");
            foreach (int value in integerList)
            {
                Console.Write("{0}, ", value);
            }
            Console.WriteLine("<end>\n\nScrambled:\n");

            // Scramble the order of the items in the list.
            integerList.Scramble();

            foreach (int value in integerList)
            {
                Console.Write("{0}, ", value);
            }
            Console.WriteLine("<end>\n");
        }

        private void Scramble()
        {
            int limit = this.Count;
            int temp;
            int swapIndex;
            Random rnd = new Random();
            for (int i = 0; i < limit; i++)
            {
                // The Item property of ArrayList is the default indexer. This, this[i] is used instead of Item[i].
                temp = (int)this[i];
                swapIndex = rnd.Next(0, limit - 1);
                this[i] = this[swapIndex];
                this[swapIndex] = temp;
            }
        }
    }
}
