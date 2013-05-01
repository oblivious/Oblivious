using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace SerializingADataSet
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlSerializer ser = new XmlSerializer(typeof(DataSet));

            DataSet ds = new DataSet("myDataSet");
            DataTable t = new DataTable("table1");
            DataColumn c = new DataColumn("thing");
            t.Columns.Add(c);
            ds.Tables.Add(t);
            DataRow r;
            for (int i = 0; i < 10; i++)
            {
                r = t.NewRow();
                r[0] = "Thing " + i;
                t.Rows.Add(r);
            }
            TextWriter writer = new StreamWriter("data.xml");
            ser.Serialize(writer, ds);
            writer.Close();
        }
    }
}
