using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Data;

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
		Thread.Sleep(2000);
		using (var context = new NorthwindDataContext())
		{
			return (from c in context.Customers
					where c.CustomerID == custID
					select c).SingleOrDefault();

		}
	}
}