using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.IsolatedStorage;
using System.Reflection;

namespace IsolatedStorageExample
{
    class Program
    {
        static void Main(string[] args)
        {
            IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForAssembly();

            IsolatedStorageFileStream isoFile = new IsolatedStorageFileStream("myfile.txt", FileMode.Create, isoStore);
            Console.WriteLine(isoFile.GetType().GetField("m_FullPath", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(isoFile).ToString());

            StreamWriter sw = new StreamWriter(isoFile);

            sw.WriteLine("This text is written to an isolated storage file.");

            sw.Close();
            isoFile.Close();


            isoFile = new IsolatedStorageFileStream("myfile.txt", FileMode.Open, isoStore);


            StreamReader sr = new StreamReader(isoFile);

            Console.WriteLine(sr.ReadToEnd());

            sr.Close();
            isoFile.Close();
        }
    }
}
