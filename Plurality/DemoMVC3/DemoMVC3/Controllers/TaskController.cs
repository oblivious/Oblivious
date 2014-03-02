using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoMVC3.Models;

namespace DemoMVC3.Controllers
{
    public class TaskController : Controller
    {
        //
        // GET: /Task/

        public ActionResult Index()
        {
            ViewBag.Message = "";
            return View();
        }

        [HttpPost]
        public ActionResult Index(string taskinput)
        {
            ViewBag.Message = taskinput;
            return View();
        }


        // GET /Task/List
        public ActionResult List()
        {
            return View(TaskManager.GetTasks());
        }
    }
}
