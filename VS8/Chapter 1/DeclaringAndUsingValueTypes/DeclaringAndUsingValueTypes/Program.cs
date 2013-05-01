using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeclaringAndUsingValueTypes
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Person p = new Person("Tony", "Allen", 32, Gender.Male);
                Console.WriteLine(p);

                StringBuilder sb = new StringBuilder("something", 4, 5, 10);
                Console.WriteLine(sb.ToString());
                sb.Append(" thingly");
                Console.WriteLine(sb.ToString());

                sb = new StringBuilder(5, 13);
                sb.Append("another");
                Console.WriteLine(sb.ToString());
                sb.Append(" thing");
                Console.WriteLine(sb.ToString());

                int[] ar = { 3, 1, 2 };
                Array.Sort(ar, Comparer<int>.Default);
                Console.WriteLine("{0}, {1}, {2}", ar[0], ar[1], ar[2]);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

    struct Person
    {
        public string firstName;
        public string lastName;
        public int age;
        public Gender gender;

        public Person(string firstName, string lastName, int age, Gender gender)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.age = age;
            this.gender = gender;
        }

        public override string ToString()
        {
            return this.firstName + " " + this.lastName + " (" + this.gender + "), age " + this.age.ToString();
        }
    }

    enum Gender
    {
        Male,
        Female
    }
}
