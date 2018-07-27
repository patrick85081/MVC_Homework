using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_Homework1.Controllers.ActionResults;
using MVC_Homework1.Models;
using MVC_Homework1.Models.ViewModels;
using X.PagedList;

namespace MVC_Homework1.Controllers
{
    [Authorize(Roles = "Admin,Customer")]
    public class 客戶資料Controller : BaseController //Controller
    {
        private readonly I客戶資料Repository customerRepository;

        //public 客戶資料Controller()
        //{
        //    customerRepository = RepositoryHelper.Get客戶資料Repository();
        //}

        public 客戶資料Controller(I客戶資料Repository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        // GET: 客戶資料
        public ActionResult Index(客戶資料QueryOption query)
        {
            ViewBag.CategoryList = customerRepository.Get客戶分類()
                .ToArray()
                .Select(c => new SelectListItem()
                {
                    Selected = c == query.Category,
                    Text = string.IsNullOrEmpty(c) ? "全部" : c,
                    Value = c,
                });

            var 客戶資料 = customerRepository.Search(query.Keyword, query.Category)
                .OrderBy(query.GetSortString())
                .ToPagedList(query.Page, query.GetPageSize());

            ViewBag.QueryOption = query;

            return View(客戶資料);
        }

        public ActionResult ExcelExport()
        {
            return this.ExcelFile(customerRepository.All().ToArray());
        }

        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = customerRepository.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        [ChildActionOnly]
        public ActionResult ConcatDetail(int id)
        {
            var concats = customerRepository.Find(id)
                .客戶聯絡人;

            return PartialView(concats);
        }

        // GET: 客戶資料/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: 客戶資料/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,客戶分類")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                customerRepository.Add(客戶資料);
                customerRepository.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: 客戶資料/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = customerRepository.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection form)
        {
            var customer = customerRepository.Find(id);
            if (customer == null)
                return HttpNotFound();

            if (TryUpdateModel(customer))
            {
                customerRepository.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: 客戶資料/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = customerRepository.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶資料 客戶資料 = customerRepository.Find(id);
            customerRepository.Delete(客戶資料);
            customerRepository.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                customerRepository.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

