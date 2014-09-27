﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OdeToFood.Models
{
	public class OdeToFoodDb : DbContext
	{
		public DbSet<Restaurant> Restaurants { get; set; }
		public DbSet<RestaurantReview> Reviews { get; set; }
		
		public OdeToFoodDb() : base(@"Data Source=localhost;Initial Catalog=TestDb;Integrated Security=SSPI")
		{
		}
	}
}