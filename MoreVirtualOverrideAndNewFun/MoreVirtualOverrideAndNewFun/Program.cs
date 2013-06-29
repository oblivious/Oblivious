using System;

namespace MoreVirtualOverrideAndNewFun
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var topLevelClass = new MyTopLevelClass();

            Console.WriteLine("As Base Class: " + ((MyBaseClass)topLevelClass).ToString());
            Console.WriteLine("As Derived Class:" + ((MyDerivedClass)topLevelClass).ToString());
            Console.WriteLine("As Top Level Class: " + topLevelClass);

            Console.WriteLine("Base Class Valid Length: " + MyBaseClass.ValidLength);
            Console.WriteLine("Derived Class Valid Length:" + MyDerivedClass.ValidLength);
            Console.WriteLine("Top Level Class Valid Length: " + MyTopLevelClass.ValidLength);

            Console.WriteLine("As Base Class: " + ((MyBaseClass)topLevelClass).IsValid);
            Console.WriteLine("As Derived Class:" + ((MyDerivedClass)topLevelClass).IsValid);
            Console.WriteLine("As Top Level Class: " + topLevelClass.IsValid);


            topLevelClass.Noodle();

            TC obj = new TC();
            obj.Display();

            var fivesie = new Fivesie();
            fivesie.TheMethod();
            ((Onesie)fivesie).TheMethod();
        }
    }

    public /*abstract*/ class MyBaseClass
    {
        private const int Length = 1;

        public static int ValidLength { get { return 1; } }

        public virtual bool IsValid { get { return Length == ValidLength; } }

        public override string ToString()
        {
            return "MyBaseClass.ToString()";
        }

        public virtual void Noodle()
        {
            Console.WriteLine("NoodleBase");
        }
    }

    public /*abstract*/ class MyDerivedClass : MyBaseClass
    {
        private const int Length = 2;

        public static new int ValidLength { get { return MyBaseClass.ValidLength + 1; } }

        public override bool IsValid { get { return Length == ValidLength; } }

        public new string ToString()
        {
            return "MyDerivedClass.ToString()";
        }

        public new void Noodle()
        {
            Console.WriteLine("NoodleDerived");
        }
    }

    public class MyTopLevelClass : MyDerivedClass
    {
        private const int Length = 3;

        public static new int ValidLength { get { return MyDerivedClass.ValidLength + 1; } }

        public override bool IsValid { get { return Length == ValidLength; } }
        /*
        public override string ToString()
        {
            return "MyTopLevelClass.ToString()";
        }
         * */
    }

    class BC
    {
        public void Display()
        {
            System.Console.WriteLine("BC::Display");
        }
    }

    class DC : BC
    {
        new public void Display()
        {
            System.Console.WriteLine("DC::Display");
        }
    }

    class TC : DC
    {

    }

    internal class Onesie
    {
        public virtual void TheMethod()
        {
            Console.WriteLine("Onesie");
        }
    }

    internal class Twosie : Onesie
    {
        public override void TheMethod()
        {
            Console.WriteLine("Twosie");
        }
    }

    internal class Threesie : Twosie
    {
        public override void TheMethod()
        {
            Console.WriteLine("Threesie");
        }
    }

    internal class Foursie : Threesie
    {
        public new void TheMethod()
        {
            Console.WriteLine("Foursie");
        }
    }

    internal class Fivesie : Foursie
    {
    }
}
