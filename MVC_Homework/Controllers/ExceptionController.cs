using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Homework.Controllers
{
    [HandleError(View = "CustomerError")]
    public class ExceptionController : Controller
    {
        // GET: Exception
        public ActionResult Index()
        {
            return View();
        }
    }
}