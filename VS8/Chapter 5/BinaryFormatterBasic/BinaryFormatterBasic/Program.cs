using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;

namespace BinaryFormatterBasic
{
    class Program
    {
        static void Main(string[] args)
        {
            string data = "This must be stored in a file.";

            FileStream fs = new FileStream("SerializedString.Data", FileMode.Create);

            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(fs, data);

            fs.Close();

            fs = new FileStream("SerializedDate.Data", FileMode.Create);

            bf.Serialize(fs, DateTime.Now);

            fs.Close();
            
            fs = new FileStream("SerializedDate.Data", FileMode.Open);

            DateTime output = (DateTime)bf.Deserialize(fs);

            Console.WriteLine(output);

            fs.Close();
        }
    }
}
