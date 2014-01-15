using System;
using System.Collections.Generic;
using System.Linq;
using Data;

namespace Biz
{
	/// <summary>
	/// Summary description for BAL
	/// </summary>
	public class BAL
	{
		public List<string> GetCountries()
		{
			using (var context = new NorthwindDataContext())
			{
				return (from c in context.Customers 
						select c.Country).Distinct().ToList();
			}
		}

		public List<Customer> GetCustomersByCountry(string country)
		{
			using (var context = new NorthwindDataContext())
			{
				return (from c in context.Customers
				        where c.Country == country
				        select c).ToList();
			}
		}

		public Customer GetCustomer(string custID)
		{
			using (var context = new NorthwindDataContext())
			{
				return (from c in context.Customers
				        where c.CustomerID == custID
				        select c).SingleOrDefault();

			}
		}
	}
}