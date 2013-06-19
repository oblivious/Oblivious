using System;
using System.IO;

namespace FileIOTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            using (FileStream fs = new FileStream("testFile.txt", FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
            {
                string input;

                using (TextReader reader = new StreamReader("inputFile.txt"))
                {
                    input = reader.ReadToEnd();
                    Console.WriteLine("Content of file: ");
                    Console.WriteLine(string.Format(input, "SomeOperator"));
                    reader.Close();
                }
                using (StreamWriter writer = new StreamWriter(fs, System.Text.Encoding.UTF8, 512))
                {
                    writer.Write(string.Format(input, "SomeOperator"));
                    writer.Flush();
                    writer.Close();
                }
            }
        }
    }
}
