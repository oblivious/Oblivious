using System;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace GzipTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string ourInput = "some stupid string";
            Console.WriteLine("Unencoded Data: " + ourInput);
            string encodedString = Compress(ourInput);
            Console.WriteLine("Encoded Data: " + encodedString);
            Console.WriteLine("Decoded Data: " + Decompress(encodedString));
            Console.ReadLine();
        }
        public static string Compress(string inputString)
        {
            Stream s = new MemoryStream(UTF8Encoding.Default.GetBytes(inputString));
            using (GZipStream Compress = new GZipStream(s, CompressionMode.Compress))
            {
                s.
                return (Compress.);
            }
        }
        public static string Decompress(string outputString)
        {
            Stream s = new MemoryStream(UTF8Encoding.Default.GetBytes(outputString));
            using (GZipStream Decompress = new GZipStream(s, CompressionMode.Decompress))
            {
                return(Decompress.ToString());
            }
        }

    }
}
