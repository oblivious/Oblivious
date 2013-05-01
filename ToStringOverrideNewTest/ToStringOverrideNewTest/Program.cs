using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToStringOverrideNewTest
{
    // What I learned:
    // The keyword "override" will replace the method entirely and always (afaik),
    // even if the reference used is of the type of the base class underneath.
    // The keyword "new" does not hide or replace the base method if it is called
    // using a reference from a base class.
    // The keyword "virtual" can be combined with "new" but it doesn't seem to have
    // any effect...

    // http://stackoverflow.com/questions/159978/c-sharp-keyword-usage-virtualoverride-vs-new
    // The "new" keyword doesn't have anything to do with the base class, it signifies
    // that the method is out on its own...
    // If you are doing real polymorphism you should ALWAYS use "override".
    // The second response also builds on it so check that out...

    class Program
    {
        static void Main(string[] args)
        {
            BaseClass bc = new BaseClass();
            Console.WriteLine(bc.ToString());

            Inheritor ih = new Inheritor();
            Console.WriteLine(ih.ToString());

            TopLevel tl = new TopLevel();
            Console.WriteLine(tl.ToString());

            BaseClass tk = new TopLevel();
            Console.WriteLine(tk.ToString());
        }
    }

    class BaseClass
    {
        public override string ToString()
        {
            return "I am the base class";
        }
    }

    class Inheritor : BaseClass
    {
        public virtual new string ToString()
        {
            return "I am the inheritor";
        }
    }


    class TopLevel : Inheritor
    {
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
