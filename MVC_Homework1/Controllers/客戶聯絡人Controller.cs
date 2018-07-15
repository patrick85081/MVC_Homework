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
    public class 客戶聯絡人Controller : BaseController //Controller
    {
        private readonly I客戶聯絡人Repository concatRepository;
        private readonly I客戶資料Repository customerRepository;

        public 客戶聯絡人Controller()
        {
            var unitOfWork = RepositoryHelper.GetUnitOfWork();
            concatRepository = RepositoryHelper.Get客戶聯絡人Repository(unitOfWork);
            customerRepository = RepositoryHelper.Get客戶資料Repository(unitOfWork);
        }

        // GET: 客戶聯絡人
        public ActionResult Index(客戶聯絡人QueryOption query)
        {
            var 客戶聯絡人 = concatRepository.Search(query.Keyword, query.Job)
                .OrderBy(query.GetSortString())
                .ToPagedList(query.Page, query.GetPageSize());

            ViewBag.QueryOption = query;

            return View(客戶聯絡人);
        }

        public ActionResult ExcelExport()
        {
            return this.ExcelFile(concatRepository.All().ToArray());
        }

        // GET: 客戶聯絡人/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = concatRepository.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(customerRepository.All(), "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶聯絡人/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                concatRepository.Add(客戶聯絡人);
                concatRepository.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(customerRepository.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = concatRepository.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(customerRepository.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                var db = concatRepository.UnitOfWork.Context;
                db.Entry(客戶聯絡人).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(customerRepository.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        [HttpPost]
        public ActionResult BatchUpdate(IList<客戶聯絡人BatchViewModel> concats, 客戶聯絡人QueryOption query)
        {
            ViewBag.QueryOption = query;
            if (ModelState.IsValid)
            {
                foreach (var viewModel in concats)
                {
                    var concat = concatRepository.Find(viewModel.Id);
                    concat.職稱 = viewModel.職稱;
                    concat.手機 = viewModel.手機;
                    concat.電話 = viewModel.電話;
                }
                concatRepository.UnitOfWork.Commit();
                return RedirectToAction("Index", query);
            }

            return Index(query);
        }

        // GET: 客戶聯絡人/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = concatRepository.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶聯絡人 客戶聯絡人 = concatRepository.Find(id);
            concatRepository.Delete(客戶聯絡人);
            concatRepository.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                customerRepository.UnitOfWork.Context.Dispose();
                concatRepository.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
