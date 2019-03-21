using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyRewards.Models;
using MyRewards.Services;

namespace MyRewards.Areas.Merchants.Controllers
{
    public class VouchersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Merchants/Vouchers
        //public ActionResult Index()
        //{
        //    return View(db.Vouchers.ToList());
        //}

        // GET: Merchants/Vouchers
        public ActionResult Index(DateTime settledOn)
        {
            Merchant merchant = null;
            using (MerchantService _merchantService = new MerchantService(User.Identity))
            {
                merchant = _merchantService.Merchant();
            }
            var vouchers = db.Vouchers.Where(v => v.Merchant_Id == merchant.Id && v.SettledOn==settledOn);
            return View(vouchers.ToList());
        }

        // GET: Merchants/Vouchers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voucher voucher = db.Vouchers.Find(id);
            if (voucher == null)
            {
                return HttpNotFound();
            }
            return View(voucher);
        }

        // GET: Merchants/Vouchers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Merchants/Vouchers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Guid,Sender_Id,Receiver_Id,ActionType_Id,SpendFlag,CreatedOn,UpdatedOn,Expiry,SettleFlag,SettledOn,Order_Id,VoucherOrder_Id,Merchant_Id,VoucherSpend_Id")] Voucher voucher)
        {
            if (ModelState.IsValid)
            {
                db.Vouchers.Add(voucher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(voucher);
        }

        // GET: Merchants/Vouchers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voucher voucher = db.Vouchers.Find(id);
            if (voucher == null)
            {
                return HttpNotFound();
            }
            return View(voucher);
        }

        // POST: Merchants/Vouchers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Guid,Sender_Id,Receiver_Id,ActionType_Id,SpendFlag,CreatedOn,UpdatedOn,Expiry,SettleFlag,SettledOn,Order_Id,VoucherOrder_Id,Merchant_Id,VoucherSpend_Id")] Voucher voucher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(voucher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(voucher);
        }

        // GET: Merchants/Vouchers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voucher voucher = db.Vouchers.Find(id);
            if (voucher == null)
            {
                return HttpNotFound();
            }
            return View(voucher);
        }

        // POST: Merchants/Vouchers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Voucher voucher = db.Vouchers.Find(id);
            db.Vouchers.Remove(voucher);
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
