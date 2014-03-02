using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Linq;

namespace NHibernateDemo.Code
{
    public class Customers
    {
        public IEnumerable<Customer> GetCustomersList()
        {
            IList<Customer> customers;

            using (var session = Global.SessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                customers = session.Query<Customer>().OrderBy(x => x.LastName).ToList();

                tx.Commit();
            }
            return customers;
        }

        public void AddCustomer(Customer customer)
        {
            if (String.IsNullOrWhiteSpace(customer.FirstName) || String.IsNullOrWhiteSpace(customer.LastName))
                return;

            using (var session = Global.SessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                session.Save(customer);
                tx.Commit();
            }
        }

        public void DeleteCustomer(int Id, string FirstName, string LastName)
        {
            using (var session = Global.SessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                session.Delete(new Customer() { Id = Id, FirstName = FirstName, LastName = LastName});
                tx.Commit();
            }
        }
    }
}