using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_Homework1.Models;
using MVC_Homework1.ViewModels;

namespace MVC_Homework1.Controllers
{
    public class 客戶信息Controller : Controller
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

            var infos = sources.OrderBy(query.GetSortString())
                .GetCurrentPage(query);

            query.SetPageCount(sources.GetPageCount(query));

            
            return Json(new QueryOptionResult<客戶信息[]>(query, infos.ToArray()));
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
