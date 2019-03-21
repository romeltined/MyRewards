using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyRewards.Models;
using MyRewards.Extensions;

namespace MyRewards.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class StaffsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Staffs
        public ActionResult Index()
        {
            return View(db.Staffs.ToList());
        }

        // GET: Admin/Staffs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // GET: Admin/Staffs/Create
        public ActionResult Create(int merchantId)
        {
            Merchant merchant = db.Merchants.Find(merchantId);
            ViewBag.MerchantId = merchant.Id;
            ViewBag.MerchantName = merchant.Name;
            ViewBag.MerchantDescription = merchant.Description;
            return View();
        }

        // POST: Admin/Staffs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Guid,Name,PhoneNumber,Block")] Staff staff, int merchantId)
        {
            if (ModelState.IsValid)
            {
                Merchant merchant = db.Merchants.Find(merchantId);
                ViewBag.MerchantId = merchant.Id;
                ViewBag.MerchantName = merchant.Name;
                ViewBag.MerchantDescription = merchant.Description;
                Guid guid = Guid.NewGuid();
                staff.Guid = guid.ToString();
                merchant.Staffs.Add(staff);
                db.Entry(merchant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("../Merchants/Details/", new { id = merchant.Id });
            }

            return View();
        }

        // GET: Admin/Staffs/Edit/5
        public ActionResult Edit(int? id, int merchantId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            ViewBag.MerchantId = merchantId;
            return View(staff);
        }

        // POST: Admin/Staffs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Guid,Name,PhoneNumber,Block")] Staff staff, int merchantId)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("../Merchants/Details", new { id = merchantId });
            }
            return View(staff);
        }

        // GET: Admin/Staffs/Delete/5
        public ActionResult Delete(int? id, int merchantId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            ViewBag.MerchantId = merchantId;
            return View(staff);
        }

        // POST: Admin/Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int merchantId)
        {
            Staff staff = db.Staffs.Find(id);
            db.Staffs.Remove(staff);
            db.SaveChanges();
            return RedirectToAction("../Merchants/Details", new { id = merchantId });
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
