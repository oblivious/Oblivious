using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CreateStruct
{
    class Program
    {
        static void Main(string[] args)
        {
            Person p = new Person("Mike", "Jones", 25, Gender.Male);
            Console.WriteLine(p);
        }
    }

    struct Person
    {
        public string firstName;
        public string lastName;
        public int age;
        public Gender gender;

        public Person(string _firstName, string _lastName, int _age, Gender _gender)
        {
            firstName = _firstName;
            lastName = _lastName;
            age = _age;
            gender = _gender;
        }

        public override string ToString()
        {
            return firstName + " " + lastName + " (" + gender.ToString() + "), age " + age;
        }
    }

    public enum Gender : int
    {
        Male,
        Female
    }
}
