using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;

namespace _70536_Exam_Questions.Questions
{
    class Q071
    {
        public static void Run()
        {
            Console.WriteLine("Q71 Start:");
            try
            {
                using (var isoFile = IsolatedStorageFile.GetMachineStoreForAssembly())
                using (var isoStream = new IsolatedStorageFileStream("Settings.dat", FileMode.Create, isoFile))
                using (var writer = new StreamWriter(isoStream))
                {
                    writer.Write("Contents of Settings.dat");
                }


                using (var isoFile = IsolatedStorageFile.GetMachineStoreForAssembly())
                using (var isoStream = new IsolatedStorageFileStream("Settings.dat", FileMode.Open, isoFile))
                using (var reader = new StreamReader(isoStream))
                {
                    var result = reader.ReadToEnd();
                    Console.WriteLine(result);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("\nQ71 End...");
            Console.ReadKey();
        }
    }
}
