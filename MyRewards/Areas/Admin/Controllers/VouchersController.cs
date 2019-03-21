using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyRewards.Models;
using Microsoft.AspNet.Identity;
using PagedList;

namespace MyRewards.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Administrators")]
    public class VouchersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Vouchers
        //[Authorize]
        //public ActionResult Index()
        //{
        //    return View(db.Vouchers.ToList());
        //}

        // GET: Vouchers/Details/5
        [Authorize]
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

        // GET: Vouchers/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var voucherType = db.VoucherTypes.OrderBy(v => v.Name).ToList();
            ViewBag.SelectedVoucher = new SelectList(voucherType, "Id", "Description");
            return View();
        }

        // POST: Vouchers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(string selectedVoucher)
        {
            if (ModelState.IsValid)
            {
                VoucherType voucherType = new VoucherType();
                voucherType = db.VoucherTypes.Find(Int32.Parse(selectedVoucher));
                Guid guid = Guid.NewGuid();
                var voucher = new Voucher
                {
                    VoucherType = voucherType,
                    Guid = guid.ToString(),
                    Sender_Id = int.Parse(User.Identity.GetUserId()),
                    Receiver_Id = int.Parse(User.Identity.GetUserId()),
                    ActionType_Id = 0,
                    SpendFlag = false,
                    CreatedOn = DateTime.UtcNow,
                    UpdatedOn = DateTime.UtcNow
                };
                db.Vouchers.Add(voucher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Create");
        }

        // GET: Vouchers/Edit/5
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

        // POST: Vouchers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Guid,Sender_Id,Receiver_Id,ActionType_Id,SpendFlag,CreatedOn,UpdatedOn")] Voucher voucher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(voucher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(voucher);
        }

        // GET: Vouchers/Delete/5
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

        // POST: Vouchers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Voucher voucher = db.Vouchers.Find(id);
            db.Vouchers.Remove(voucher);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // GET: Vouchers
        //[Authorize]
        public ActionResult Index(int? voucherSpendId, DateTime? dateFrom, DateTime? dateTo, int? page)
        {

            List<Voucher> result = new List<Voucher>();
            DateTime vdateFrom;
            DateTime vdateTo;

            if(dateFrom!=null && dateTo!=null)
            {
                vdateFrom = dateFrom.Value;
                vdateTo = dateTo.Value.AddDays(1);
                result = db.Vouchers.Where(v => v.UpdatedOn >= vdateFrom && v.UpdatedOn < vdateTo).ToList();
                ViewBag.DateFrom = dateFrom;
                ViewBag.DateTo = dateTo;
            }

            if (voucherSpendId != null)
            {
                result = db.Vouchers.Where(v => v.VoucherSpend_Id == voucherSpendId).ToList();
                ViewBag.VoucherSpendId = voucherSpendId;
            }


            IPagedList<Voucher> voucherlist = null;
            int pageSize = 10;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            voucherlist = result.ToPagedList(pageIndex, pageSize);
            return View(voucherlist);          

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
