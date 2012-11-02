using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonToManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Manager m = new Manager("Mike", "Jones", 25, Gender.Male, "016966000", "Dublin");
            Console.WriteLine(m);
        }
    }

    class Person
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

    class Manager : Person
    {
        public string phoneNumber;
        public string officeLocation;

        public Manager(string _firstName, string _lastName, int _age, Gender _gender, string _phoneNumber, string _officeLocation)
            : base(_firstName, _lastName, _age, _gender)
        {
            phoneNumber = _phoneNumber;
            officeLocation = _officeLocation;
        }

        public override string ToString()
        {
            return base.ToString() + ", " + phoneNumber + ", " + officeLocation;
        }
    }


    public enum Gender : int
    {
        Male,
        Female
    }
}
