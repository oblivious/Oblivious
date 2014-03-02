using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NHibernateDemo.Code
{
    public class Customer: IComparable<Customer>
    {
        public virtual int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }

        int IComparable<Customer>.CompareTo(Customer other)
        {
            return (LastName + FirstName).CompareTo(other.LastName + other.FirstName);
        }
    }
}