using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCollections
{
    using System.Collections;

    // A delegate type for hooking up change notifications.
    public delegate void ChangedEventHandler(object sender, ListChangedEventArgs e);

    // A class that works just like ArrayList, but sends event
    // notifications whenever the list changes.
    public class ListWithChangedEvent : ArrayList
    {
        // An event that clients can use to be notified whenever the
        // elements of the list change.
        public event ChangedEventHandler Changed;

        // Invoke the Changed event; called whenever list changes
        protected virtual void OnChanged(ListChangedEventArgs e)
        {
            Console.WriteLine("*** OnChanged Called ***");
            if (Changed != null)
                Changed(this, e);
        }

        // Override some of the methods that can change the list;
        // invoke evnet after each
        public override int Add(object value)
        {
            int i = base.Add(value);
            OnChanged(new ListChangedEventArgs() { AddedItem = "This added: " + (string)value});
            return i;
        }

        public override void Clear()
        {
            base.Clear();
            OnChanged(new ListChangedEventArgs() { AddedItem = "List cleared."});
        }

        public override object this[int index]
        {
            set
            {
                base[index] = value;
                OnChanged(new ListChangedEventArgs());
            }
        }
    }

    public class ListChangedEventArgs : EventArgs
    {
        public string AddedItem { get; set; }
    }
}
namespace TestEvents
{
    using MyCollections;

    class EventListener
    {
        private ListWithChangedEvent list;

        public EventListener(ListWithChangedEvent list)
        {
            this.list = list;
            // Add "ListChanged" to the Changed event on "List".
            this.list.Changed += new ChangedEventHandler(ListChanged);
        }

        // This will be called whenever the list changes.
        private void ListChanged(object sender, EventArgs e)
        {
            Console.WriteLine("This is called when the event fires.");
            if (e is ListChangedEventArgs)
                Console.WriteLine("Triggered by: " + (e as ListChangedEventArgs).AddedItem);
        }

        public void Detach()
        {
            // Detach the event and delete the list.
            this.list.Changed -= new ChangedEventHandler(ListChanged);
            //this.list = null;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create a new list.
            ListWithChangedEvent list = new ListWithChangedEvent();

            // Create a class that listens to the list's change event.
            EventListener listener = new EventListener(list);

            list.Add("item 1");
            string value = (string)list[0];
            Console.WriteLine("Called get: " + value);
            list.Clear();
            listener.Detach();
            Console.WriteLine("No longer listening for the event.");
            list.Add("item 2");
        }
    }
}
