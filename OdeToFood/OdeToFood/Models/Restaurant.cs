using System.Collections.Generic;

namespace OdeToFood.Models
{
	public class Restaurant
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string City { get; set; }
		public string Country { get; set; }
		public virtual ICollection<RestaurantReview> Reviews { get; set; }
	}
}