using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OdeToFood.Models;

namespace OdeToFood.Controllers
{
    public class ReviewsController : Controller
	{
		OdeToFoodDb _db = new OdeToFoodDb();

		public ActionResult Index([Bind(Prefix="id")]int restaurantId)
		{
			var restaurant = _db.Restaurants.Find(restaurantId);
			if (restaurant == null)
			{
				return HttpNotFound();
			}
			return View(restaurant);
		}

		protected override void Dispose(bool disposing)
		{
			_db.Dispose();
			base.Dispose(disposing);
		}
	}
}
