using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace StreamMessing
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                StreamWriter sw = new StreamWriter("text.txt");
                sw.WriteLine("Hello, World!");
                sw.Flush();
                sw.Close();
                sw.Dispose();

                StreamReader sr = new StreamReader("text.txt");
                Console.WriteLine(sr.ReadToEnd());
                sr.Close();
                sr.Dispose();

                StreamReader failReader = new StreamReader(@"C:\boot.ini");
                Console.WriteLine(failReader.ReadToEnd());
                failReader.Close();
                failReader.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

            try
            {
                File.Delete("text.txt");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception deleting file: " + e.Message);
            }

            StreamReader sr;
            try
            {
                sr = new StreamReader("text.txt");
                Console.WriteLine(sr.ReadToEnd());
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("The file could not be found");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("You do not have sufficient permissions.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error reading file: " + e.Message);
            }
            finally
            {
                sr.Close();
            }
        }
    }
}
