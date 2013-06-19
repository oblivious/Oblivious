using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Web.Mvc;
using TechTest;

namespace TechTestWebSite.Controllers
{
    public class HomeController : Controller
    {
        private TechTestDataContext db = new TechTestDataContext();

        public ActionResult Index()
        {
            ViewBag.Message = "Distributors";

            return View(db.Distributors.ToList());
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
