using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;
using MVC_Homework1.Models;
using MVC_Homework1.Models.ViewModels;

namespace MVC_Homework1.Controllers
{
    public class 客戶信息Controller : BaseController //Controller
    {
        private readonly I客戶信息Repository infoRepository;

        public 客戶信息Controller()
        {
            infoRepository = RepositoryHelper.Get客戶信息Repository();
        }

        // GET: 客戶信息
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Data(QueryOption query)
        {
            var sources = infoRepository.Search(query.Keyword);

            return Json(new QueryOptionResult<客戶信息>(query, sources));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                infoRepository.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
