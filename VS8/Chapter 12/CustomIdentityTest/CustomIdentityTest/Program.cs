using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;

namespace CustomIdentityTest
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    class CustomIdentity : IIdentity
    {
        private bool isAuthenticated;
        private string name, authenticationType;

        private string firstName, lastName, address, city, state, zip;

        public CustomIdentity()
        {
            this.name = String.Empty;
            this.isAuthenticated = false;
            this.authenticationType = "None";

            this.firstName = String.Empty;
            this.lastName = String.Empty;
            this.address = String.Empty;
            this.city = String.Empty;
            this.state = String.Empty;
            this.zip = String.Empty;
        }

        public CustomIdentity(bool isLogin, string newAuthenticationType, string newFirstName,
            string newLastName, string newAddress, string newCity, string newState, string newZip)
        {
            this.name = newFirstName + newLastName;
            this.isAuthenticated = isLogin;
            this.authenticationType = newAuthenticationType;

            this.firstName = newFirstName;
            this.lastName = newLastName;
            this.address = newAddress;
            this.city = newCity;
            this.state = newState;
            this.zip = newZip;
        }

        public bool IsAuthenticated { get { return this.isAuthenticated; } }
        public string Name { get { return this.name; } }
        public string AuthenticationType { get { return this.name; } }
        public string FirstName { get { return this.firstName; } }
        public string LastName { get { return this.lastName; } }
        public string Address { get { return this.address; } }
        public string City { get { return this.city; } }
        public string State { get { return this.state; } }
        public string Zip { get { return this.zip; } }
    }
}
