using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BinaryWriterReader
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream fs = new FileStream("data.bin", FileMode.Create);
            BinaryWriter w = new BinaryWriter(fs);
            for (int i = 0; i < 11; i++)
                w.Write((long)i);
            w.Close();
            fs.Close();

            fs = new FileStream("data.bin", FileMode.Open, FileAccess.Read);
            BinaryReader r = new BinaryReader(fs);
            for (int i = 0; i < 11; i++)
                Console.WriteLine(r.ReadInt64());
            r.Close();
            fs.Close();
        }
    }
}
