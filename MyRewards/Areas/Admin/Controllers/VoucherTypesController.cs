using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyRewards.Models;
using MyRewards.Entities;

namespace MyRewards.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Administrators")]
    public class VoucherTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private NopCommerceContext ndb = new NopCommerceContext();

        // GET: VoucherTypes
        public ActionResult Index()
        {
            return View(db.VoucherTypes.ToList());
        }

        // GET: VoucherTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VoucherType voucherType = db.VoucherTypes.Find(id);
            if (voucherType == null)
            {
                return HttpNotFound();
            }
            return View(voucherType);
        }

        // GET: VoucherTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VoucherTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Product_Id,Name,Description,Amount")] VoucherType voucherType)
        {
            if (ModelState.IsValid)
            {
                db.VoucherTypes.Add(voucherType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(voucherType);
        }

        // GET: VoucherTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VoucherType voucherType = db.VoucherTypes.Find(id);
            if (voucherType == null)
            {
                return HttpNotFound();
            }
            return View(voucherType);
        }

        // POST: VoucherTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Product_Id,Name,Description,Amount")] VoucherType voucherType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(voucherType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(voucherType);
        }

        // GET: VoucherTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VoucherType voucherType = db.VoucherTypes.Find(id);
            if (voucherType == null)
            {
                return HttpNotFound();
            }
            return View(voucherType);
        }

        // POST: VoucherTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VoucherType voucherType = db.VoucherTypes.Find(id);
            db.VoucherTypes.Remove(voucherType);
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
