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
    public class VoucherOrderLogsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/VoucherOrderLogs
        public ActionResult Index()
        {
            return View(db.VoucherOrderLogs.ToList());
        }

        // GET: Admin/VoucherOrderLogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VoucherOrderLog voucherOrderLog = db.VoucherOrderLogs.Find(id);
            if (voucherOrderLog == null)
            {
                return HttpNotFound();
            }
            return View(voucherOrderLog);
        }

        // GET: Admin/VoucherOrderLogs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/VoucherOrderLogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Order_Id,Customer_Id,Content,Status_Id,PaidDateUtc,CreatedOn,UpdatedOn")] VoucherOrderLog voucherOrderLog)
        {
            if (ModelState.IsValid)
            {
                db.VoucherOrderLogs.Add(voucherOrderLog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(voucherOrderLog);
        }

        // GET: Admin/VoucherOrderLogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VoucherOrderLog voucherOrderLog = db.VoucherOrderLogs.Find(id);
            if (voucherOrderLog == null)
            {
                return HttpNotFound();
            }
            return View(voucherOrderLog);
        }

        // POST: Admin/VoucherOrderLogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Order_Id,Customer_Id,Content,Status_Id,PaidDateUtc,CreatedOn,UpdatedOn")] VoucherOrderLog voucherOrderLog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(voucherOrderLog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(voucherOrderLog);
        }

        // GET: Admin/VoucherOrderLogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VoucherOrderLog voucherOrderLog = db.VoucherOrderLogs.Find(id);
            if (voucherOrderLog == null)
            {
                return HttpNotFound();
            }
            return View(voucherOrderLog);
        }

        // POST: Admin/VoucherOrderLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VoucherOrderLog voucherOrderLog = db.VoucherOrderLogs.Find(id);
            db.VoucherOrderLogs.Remove(voucherOrderLog);
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
