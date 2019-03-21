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
    public class VoucherTransferLogsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/VoucherTransferLogs
        public ActionResult Index()
        {
            return View(db.VoucherTransferLogs.ToList());
        }

        // GET: Admin/VoucherTransferLogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VoucherTransferLog voucherTransferLog = db.VoucherTransferLogs.Find(id);
            if (voucherTransferLog == null)
            {
                return HttpNotFound();
            }
            return View(voucherTransferLog);
        }

        // GET: Admin/VoucherTransferLogs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/VoucherTransferLogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,User_Id,Content,CreatedOn")] VoucherTransferLog voucherTransferLog)
        {
            if (ModelState.IsValid)
            {
                db.VoucherTransferLogs.Add(voucherTransferLog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(voucherTransferLog);
        }

        // GET: Admin/VoucherTransferLogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VoucherTransferLog voucherTransferLog = db.VoucherTransferLogs.Find(id);
            if (voucherTransferLog == null)
            {
                return HttpNotFound();
            }
            return View(voucherTransferLog);
        }

        // POST: Admin/VoucherTransferLogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,User_Id,Content,CreatedOn")] VoucherTransferLog voucherTransferLog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(voucherTransferLog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(voucherTransferLog);
        }

        // GET: Admin/VoucherTransferLogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VoucherTransferLog voucherTransferLog = db.VoucherTransferLogs.Find(id);
            if (voucherTransferLog == null)
            {
                return HttpNotFound();
            }
            return View(voucherTransferLog);
        }

        // POST: Admin/VoucherTransferLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VoucherTransferLog voucherTransferLog = db.VoucherTransferLogs.Find(id);
            db.VoucherTransferLogs.Remove(voucherTransferLog);
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
