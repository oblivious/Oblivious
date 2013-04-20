using System;
using System.Collections.Generic;
using System.Text;

namespace CreateStruct
{
    class Program
    {
        static void Main(string[] args)
        {
            Manager m = new Manager("Tony", "Allen", 32, Person.Genders.Male, "0857174820", "Here of course");
            Console.WriteLine(m.ToString());
        }

        class Person
        {
            public string firstName;
            public string lastName;
            public int age;
            public Genders gender;

            public Person(string _firstName, string _lastName, int _age, Genders _gender)
            {
                firstName = _firstName;
                lastName = _lastName;
                age = _age;
                gender = _gender;
            }

            public override string ToString()
            {
                return firstName + " " + lastName + " (" + gender + "), age " + age;
            }

            public enum Genders : int { Male, Female };
        }

        class Manager : Person
        {
            public string phoneNumber;
            public string officeLocation;

            public Manager(string firstName, string lastName, int age, Genders gender, string phoneNumber, string officeLocation)
                : base(firstName, lastName, age, gender)
            {
                this.phoneNumber = phoneNumber;
                this.officeLocation = officeLocation;
            }

            public override string ToString()
            {
                return base.ToString() + ", " + this.phoneNumber + ", " + this.officeLocation;
            }
        }
    }
}
