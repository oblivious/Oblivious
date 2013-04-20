using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortString
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "Microsoft .Net Framework Application Development Foundation";
            Console.WriteLine(s);
            string[] sa = s.Split(' ');
            Array.Sort(sa);
            s = String.Join(" ", sa);
            Console.WriteLine(s);

            try
            {
                throw new DerivedException();
            }
            catch (Exception e)
            {
                Console.WriteLine("{0}, {1}", e.Source, e.Message);
            }
        }
    }

    class DerivedException : Exception, IComparable
    {
        public override string Message
        {
            get
            {
                return "An error occurred in the application.";
            }
        }
        //Messing
        #region IComparable Members

        public int CompareTo(DerivedException other)
        {
            return this.Message.CompareTo(other.Message);
        }

        #endregion
    }

    interface IMessage
    {
        bool Send();
        string Message { get; set; }
        string Address { get; set; }
    }

    class EmailMessage : IMessage
    {
        #region IMessage Members

        public bool Send()
        {
            throw new NotImplementedException();
        }

        public string Message
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Address
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
