using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Generics
{
    class CollectionGeneric
    {
        public static void Run()
        {
            Collection<string> dinosaurs = new Collection<string>();

            dinosaurs.Add("Psitticosaurus");
            dinosaurs.Add("Caudipteryx");
            dinosaurs.Add("Compsohnathus");
            dinosaurs.Add("Muttaburrasaurus");

            Console.WriteLine("{0} dinosaurs:", dinosaurs.Count);
            Display(dinosaurs);

            Console.WriteLine("\nIndexOf(\"Muttaburrasaurus\"): {0}", dinosaurs.IndexOf("Muttaburrasaurus"));

            Console.WriteLine("\nContains(\"Caudipteryx\"): {0}",  dinosaurs.Contains("Caudipteryx"));

            Console.WriteLine("\nInsert(2, \"Nanotyrannus\")");
            dinosaurs.Insert(2, "Nanotyrannus");
            Display(dinosaurs);

            Console.WriteLine("\ndinosaurs[2]: {0}", dinosaurs[2]);

            Console.WriteLine("\ndinosaurs[2] = \"Microraptor\"");
            dinosaurs[2] = "Microraptor";
            Display(dinosaurs);

            Console.WriteLine("\nRemove(\"Microraptor\")");
            dinosaurs.Remove("Microraptor");
            Display(dinosaurs);

            Console.WriteLine("\nRemoveAt(0)");
            dinosaurs.RemoveAt(0);
            Display(dinosaurs);

            Console.WriteLine("\ndinosaurs.Clear()");
            dinosaurs.Clear();
            Console.WriteLine("Count: {0}", dinosaurs.Count);
        }

        private static void Display(Collection<string> cs)
        {
            Console.WriteLine();
            int i = 0;
            foreach (string item in cs)
            {
                Console.Write(item);
                if (++i < cs.Count)
                    Console.Write(", ");
            }
            Console.WriteLine();
        }
    }
}
