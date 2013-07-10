using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BufferedStreamTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "Some input";
            byte[] bytes = Encoding.ASCII.GetBytes(input);

            MemoryStream memory = new MemoryStream();
            BufferedStream buffered = new BufferedStream(memory);
            Console.WriteLine("Before Write:");
            Console.WriteLine("Memory Position:   {0}", memory.Position);
            Console.WriteLine("Buffered Position: {0}", buffered.Position);

            memory.Write(bytes, 0, bytes.Length);

            Console.WriteLine("\nAfter Write, Before Seek():");
            Console.WriteLine("Memory Position:   {0}", memory.Position);
            Console.WriteLine("Buffered Position: {0}", buffered.Position);

            memory.Seek(0, SeekOrigin.Begin);
            Console.WriteLine("\nAfter Seek():");
            Console.WriteLine("Memory Position:   {0}", memory.Position);
            Console.WriteLine("Buffered Position: {0}", buffered.Position);

            byte[] buffer = new byte[256];

            int read = buffered.Read(buffer, 0, buffer.Length);

            Console.WriteLine("After Read():");
            Console.WriteLine("Memory Position:   {0}", memory.Position);
            Console.WriteLine("Buffered Position: {0}", buffered.Position);

            byte[] output = new byte[read];
            Array.Copy(buffer, output, read);

            Console.WriteLine("Read {0} bytes", read);
            Console.WriteLine("Output: ***{0}***", Encoding.ASCII.GetString(output));
        }
    }
}
