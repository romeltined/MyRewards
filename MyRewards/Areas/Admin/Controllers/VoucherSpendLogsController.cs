using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyRewards.Models;

namespace MyRewards.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Administrators")]
    public class VoucherSpendLogsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/VoucherSpendLogs
        public ActionResult Index()
        {
            return View(db.VoucherSpendLogs.ToList());
        }

        // GET: Admin/VoucherSpendLogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VoucherSpendLog VoucherSpendLog = db.VoucherSpendLogs.Find(id);
            if (VoucherSpendLog == null)
            {
                return HttpNotFound();
            }
            return View(VoucherSpendLog);
        }

        // GET: Admin/VoucherSpendLogs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/VoucherSpendLogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Content,CreatedOn")] VoucherSpendLog VoucherSpendLog)
        {
            if (ModelState.IsValid)
            {
                db.VoucherSpendLogs.Add(VoucherSpendLog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(VoucherSpendLog);
        }

        // GET: Admin/VoucherSpendLogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VoucherSpendLog VoucherSpendLog = db.VoucherSpendLogs.Find(id);
            if (VoucherSpendLog == null)
            {
                return HttpNotFound();
            }
            return View(VoucherSpendLog);
        }

        // POST: Admin/VoucherSpendLogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Content,CreatedOn")] VoucherSpendLog VoucherSpendLog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(VoucherSpendLog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(VoucherSpendLog);
        }

        // GET: Admin/VoucherSpendLogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VoucherSpendLog VoucherSpendLog = db.VoucherSpendLogs.Find(id);
            if (VoucherSpendLog == null)
            {
                return HttpNotFound();
            }
            return View(VoucherSpendLog);
        }

        // POST: Admin/VoucherSpendLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VoucherSpendLog VoucherSpendLog = db.VoucherSpendLogs.Find(id);
            db.VoucherSpendLogs.Remove(VoucherSpendLog);
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
