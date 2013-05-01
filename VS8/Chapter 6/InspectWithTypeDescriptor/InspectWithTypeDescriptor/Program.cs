using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;

namespace InspectWithTypeDescriptor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Drawing stuff:\n");
            Console.WriteLine("\tIcon Attributes:");
            AttributeCollection attributes = TypeDescriptor.GetAttributes(typeof(Icon));
            foreach (Attribute attribute in attributes) Console.WriteLine("\t- " + attribute.GetType().Name);

            Console.WriteLine("\n\tIcon Properties:");
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(new Icon(@"C:\Program Files (x86)\Steam\Public\steam_tray.ico"));
            foreach (PropertyDescriptor property in properties) Console.WriteLine("\t- " + property.Name);

            Console.WriteLine("\n\nAttributes on System.Object are:");
            AttributeCollection attributes1 = TypeDescriptor.GetAttributes(typeof(object));
            foreach (Attribute attribute in attributes1) Console.WriteLine("- " + attribute.GetType().Name);

            Console.WriteLine("\nProperties of a string object are:");
            PropertyDescriptorCollection properties1 = TypeDescriptor.GetProperties("A String");
            foreach (PropertyDescriptor property in properties1) Console.WriteLine("- " + property.Name);

            Console.WriteLine("\nEvents on the AppDomain we are running in are:");
            EventDescriptorCollection events = TypeDescriptor.GetEvents(AppDomain.CurrentDomain);
            foreach (EventDescriptor @event in events) Console.WriteLine("- " + @event.Name);
        }
    }
}
