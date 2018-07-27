using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Homework.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Overview = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Overview = "Your contact page.";

            return View();
        }
    }
}