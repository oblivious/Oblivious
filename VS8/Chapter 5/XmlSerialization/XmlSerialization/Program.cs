using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace XmlSerialization
{
    class Program
    {
        static void Main(string[] args)
        {
            
            FileStream fs = new FileStream("SerializedData.xml", FileMode.Create);
            XmlWriter writer = XmlWriter.Create(fs);
            writer.WriteElementString("dateTime", "2013-01-10T19:57:58.9751947+00:00");
            writer.Close();
            //XmlSerializer xs = new XmlSerializer(typeof(DateTime));
            //xs.Serialize(fs, DateTime.Now);
            fs.Close();
            
            
            //XmlSerializer xs = new XmlSerializer(typeof(DateTime));
            fs = new FileStream("SerializedData.xml", FileMode.Open);
            XmlReader xr = XmlReader.Create(fs);
            xr.ReadStartElement();
            DateTime previousTime = xr.ReadContentAsDateTime();
            //DateTime previousTime = (DateTime)xs.Deserialize(fs);
            Console.WriteLine("Current Time:  " + DateTime.Now);
            Console.WriteLine("Previous Time: " + previousTime);
            xr.Close();
            fs.Close();
            /*
            FileStream fs = new FileStream("SerializedDate.xml", FileMode.Create);
            XmlSerializer xs = new XmlSerializer(typeof(DateTime));
            xs.Serialize(fs, DateTime.Now);
            fs.Close();

            fs = new FileStream("SerializedDate.xml", FileMode.Open);
            DateTime previousTime = (DateTime)xs.Deserialize(fs);
            fs.Close();
            Console.WriteLine("Day: " + previousTime.DayOfWeek + ", Time: " + previousTime.TimeOfDay.ToString());
            Console.WriteLine("Day: " + DateTime.Now.DayOfWeek + ", Time: " + DateTime.Now.TimeOfDay.ToString());
             * */
        }
    }
}
