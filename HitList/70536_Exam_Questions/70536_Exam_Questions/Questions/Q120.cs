using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace _70536_Exam_Questions.Questions
{
    internal class Q120
    {
        public static void Run()
        {
            Console.WriteLine("\nQ120 Start:\n");

            List<Listener> listeners = new List<Listener>();

            Publisher publisher = new Publisher();

            for (int i = 0; i < 5; i++)
            {
                if (i % 3 == 0)
                    listeners.Add(new BadListener(publisher));
                else
                    listeners.Add(new GoodListener(publisher));
            }

            Console.WriteLine("Direct event");
            Console.WriteLine("------------");
            publisher.RaiseDirect();

            Console.WriteLine("Iterative event");
            Console.WriteLine("---------------");
            publisher.RaiseIterative();

            Console.WriteLine("\nQ120 End...\n");
        }
    }

    internal class Publisher
    {
        public event EventHandler MyEvent;

        public void RaiseDirect()
        {
            var handler = MyEvent;
            if (handler == null)
            {
                Console.WriteLine("NoListeners");
                return;
            }

            try
            {
                handler(this, null);
            }
            catch (Exception)
            {
                Console.WriteLine("A listener threw an exception in its hander");
            }
        }

        public void RaiseIterative()
        {
            var handler = MyEvent;
            if (handler == null)
            {
                Console.WriteLine("No listeners");
                return;
            }

            foreach (EventHandler h in handler.GetInvocationList())
            {
                try
                {
                    h(this, null);
                }
                catch (Exception)
                {
                    Console.WriteLine("A listener threw an exception in its handler.");
                }
            }
        }
}

    class Listener
    {
        private static int _number = 0;

        public string Name { get; private set; }

        public Listener()
        {
            _number++;
            Name = this.GetType().Name + _number.ToString(CultureInfo.InvariantCulture);
        }
    }

    class GoodListener : Listener
    {
        public GoodListener(Publisher publisher)
        {
            publisher.MyEvent += new EventHandler(publisher_MyEvent);
        }

        void publisher_MyEvent(object sender, EventArgs e)
        {
            Console.WriteLine(string.Format("{0} handled event", Name));
        }
    }

    class BadListener : Listener
    {
        public BadListener(Publisher publisher)
        {
            publisher.MyEvent += new EventHandler(publisher_MyEvent);
        }

        void publisher_MyEvent(object sender, EventArgs e)
        {
            Console.WriteLine(string.Format("{0} threw in handler", Name));
            throw new Exception("failure");
        }
    }
}
