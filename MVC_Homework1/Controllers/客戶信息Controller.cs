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
        private 客戶資料Entities db = new 客戶資料Entities();
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
            var infos = infoRepository.Search(query.Keyword)
                .OrderBy(query.GetSortString())
                .GetCurrentPage(query)
                .ToArray();


            return Json(new QueryOptionResult<客戶信息[]>(query, infos));
        }

        // GET: 客戶信息/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶信息 客戶信息 = db.客戶信息.Find(id);
            if (客戶信息 == null)
            {
                return HttpNotFound();
            }
            return View(客戶信息);
        }

        // GET: 客戶信息/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: 客戶信息/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "客戶名稱,銀行數量,聯絡人數量")] 客戶信息 客戶信息)
        {
            if (ModelState.IsValid)
            {
                db.客戶信息.Add(客戶信息);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(客戶信息);
        }

        // GET: 客戶信息/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶信息 客戶信息 = db.客戶信息.Find(id);
            if (客戶信息 == null)
            {
                return HttpNotFound();
            }
            return View(客戶信息);
        }

        // POST: 客戶信息/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "客戶名稱,銀行數量,聯絡人數量")] 客戶信息 客戶信息)
        {
            if (ModelState.IsValid)
            {
                db.Entry(客戶信息).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(客戶信息);
        }

        // GET: 客戶信息/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶信息 客戶信息 = db.客戶信息.Find(id);
            if (客戶信息 == null)
            {
                return HttpNotFound();
            }
            return View(客戶信息);
        }

        // POST: 客戶信息/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            客戶信息 客戶信息 = db.客戶信息.Find(id);
            db.客戶信息.Remove(客戶信息);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
