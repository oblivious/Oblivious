using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.IsolatedStorage;

namespace StreamLabPage88
{
    class Program
    {
        static void Main(string[] args)
        {
            MemoryStream ms = new MemoryStream();

            StreamWriter sw = new StreamWriter(ms);

            Console.WriteLine("Enter 'quit' on a blank line to exit.");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "quit")
                    break;
                sw.WriteLine(input);
            }
            sw.Flush();

            IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForAssembly();

            IsolatedStorageFileStream fs = new IsolatedStorageFileStream("output.txt", FileMode.Create, isoStore);
            ms.WriteTo(fs);

            sw.Close();
            ms.Close();
            fs.Close();

            IsolatedStorageFileStream tr = new IsolatedStorageFileStream("output.txt", FileMode.Open, isoStore);
            StreamReader sr = new StreamReader(tr);
            Console.Write(sr.ReadToEnd());
            sr.Close();
            tr.Close();
        }
    }
}
