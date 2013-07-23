using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace XmlSerializerPrivateTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Meeting myMeeting = new Meeting("Goals");

            myMeeting.roomNumber = 110;
            string[] attendees = new string[2] { "John", "Mary" };

            myMeeting.invitees = attendees;

            XmlSerializer xs = new XmlSerializer(typeof(Meeting));

            StreamWriter writer = new StreamWriter("meeting.xml");

            xs.Serialize(writer, myMeeting);

            writer.Close();
        }
    }

    public class Meeting
    {
        private string title;
        public int roomNumber;
        public string[] invitees;

        public Meeting()
        {
        }

        public Meeting(string t)
        {
            title = t;
        }
    }
}
