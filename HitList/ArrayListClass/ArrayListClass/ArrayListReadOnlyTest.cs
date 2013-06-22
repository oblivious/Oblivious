using System;
using System.Collections;

namespace ArrayListClass
{
    class ArrayListReadOnlyTest
    {
        public static void Run()
        {
            Utils.BannerChar('*');
            Utils.WriteCentred("Now announcing the all powerful and mighty ArrayList ReadOnly Property Test!");
            Utils.BannerChar('*');

            // Creates and initializes a new ArrayList.
            ArrayList myAL = new ArrayList();
            myAL.Add("red");
            myAL.Add("orange");
            myAL.Add("yellow");

            // Creates a read-only copy of the ArrayList.
            ArrayList myReadOnlyAL = ArrayList.ReadOnly(myAL);

            // Displays whether the ArrayList is read-only or writable.
            Console.WriteLine("myAL is {0}.", myAL.IsReadOnly ? "read-only" : "writable");
            Console.WriteLine("myReadOnlyAL is {0}.", myReadOnlyAL.IsReadOnly ? "read-only" : "writable");

            // Displays the contents of both collections.
            Console.WriteLine("\nInitially,");
            PrintArrayListsContents(myAL, myReadOnlyAL);

            // Adding an element to a read-only ArrayList throws an exception.
            Console.WriteLine("\nTrying to add a new element to the read-only ArrayList:");
            try
            {
                // strings are immutable, therefore, this cannot be done.
                myReadOnlyAL[0] = "barf";
                myReadOnlyAL.Add("green");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }

            // Adding an element to the original ArrayList affects the read-only ArrayList.
            myAL.Add("blue");

            // Displays the contents of both collections again.
            Console.WriteLine("\nAfter adding a new element to the original ArrayList,");
            PrintArrayListsContents(myAL, myReadOnlyAL);

            Console.WriteLine("\n\n\nOk, so now lets see what happens when we try to use objects:");

            try
            {
                ArrayList unedited = new ArrayList();
                unedited.Add(new FancyObject("red"));
                unedited.Add(new FancyObject("orange"));
                unedited.Add(new FancyObject("yellow"));

                ArrayList readOnly = ArrayList.ReadOnly(unedited);

                PrintFancyObjectArrayListsContents(unedited, readOnly);

                Console.WriteLine("Going to mess with the readonly list now");
                try
                {
                    FancyObject weeWee = (FancyObject)readOnly[0];
                    weeWee.Fancy = "blue";
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Weewee blew up: {0}", ex.ToString());
                }

                Console.WriteLine("The change has been made! (or not)...");
                PrintFancyObjectArrayListsContents(unedited, readOnly);
            }
            catch (Exception e)
            {
                Console.WriteLine("There was a boo boo: {0}", e.ToString());
            }
            Console.WriteLine("\nYeah, that's what I thought, you can mess with readonly to your heart's content. You can't delete the array items but you can modify their values. " + 
                "\nYou cannot however modify a string where it is an element in a readonly list. I suspect because the string is immutable and therefore modifying it will force" + 
                "\na change in it's reference which causes an exception in the readonly list as the references are not writable.");
            Console.WriteLine("\n\n");

            Utils.BannerChar('*');
            Utils.WriteCentred("Thus ends the all powerful ArrayList ReadOnly method test...");
            Utils.BannerChar('*');
        }


        private static void PrintArrayListsContents(ArrayList original, ArrayList readOnlyList)
        {
            Console.WriteLine("The original ArrayList myAL contains:");
            foreach (string myStr in original)
            {
                Console.WriteLine("   {0}", myStr);
            }
            Console.WriteLine("The read-only ArrayList myReadOnlyAL contains:");
            foreach (string myStr in readOnlyList)
            {
                Console.WriteLine("   {0}", myStr);
            }
        }

        private static void PrintFancyObjectArrayListsContents(ArrayList original, ArrayList readOnlyList)
        {
            Console.WriteLine("The original fancy ArrayList contains:");
            foreach (FancyObject fancy in original)
            {
                Console.WriteLine("   Fancy! \"{0}\"", fancy.Fancy);
            }
            Console.WriteLine("The read-only fancy ArrayList contains:");
            foreach (FancyObject fancy in readOnlyList)
            {
                Console.WriteLine("   Fancy! \"{0}\"", fancy.Fancy);
            }
        }
    }



    internal class FancyObject
    {
        private string fancy;

        public FancyObject(string colour)
        {
            fancy = colour;
        }

        public string Fancy
        {
            get { return fancy; }
            set { fancy = value; }
        }
    }
}
