using MyRewards.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MyRewards.Services;

namespace MyRewards.Areas.Merchants.Controllers
{
    [Authorize(Roles = "Managers")]
    public class StaffsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Merchants/Staffs/Create
        public ActionResult Create()
        {
            using (MerchantService _merchantService = new MerchantService(User.Identity))
            {
                Merchant merchant = _merchantService.Merchant();
                ViewBag.MerchantName = merchant.Name;
                ViewBag.MerchantDescription = merchant.Description;
            }

            return View();
        }

        // POST: Merchants/Staffs/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,Guid,Name,PhoneNumber,Block")] Staff staff)
        {
            try
            {
                using (MerchantService _merchantService = new MerchantService(User.Identity))
                {
                    Merchant merchant = _merchantService.Merchant();
                    ViewBag.MerchantName = merchant.Name;
                    ViewBag.MerchantDescription = merchant.Description;
                    Guid guid = Guid.NewGuid();
                    staff.Guid = guid.ToString();
                    merchant.Staffs.Add(staff);
                    _merchantService._db.Entry(merchant).State = EntityState.Modified;
                    _merchantService._db.SaveChanges();
                    return RedirectToAction("../Merchants/Details/");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }

        // GET: Merchants/Staffs/Edit/5
        public ActionResult Edit(int id)
        {
            Merchant merchant = null;
            using (MerchantService _merchantService = new MerchantService(User.Identity))
            {
                merchant = _merchantService.Merchant();
            }

            Staff staff = merchant.Staffs.Find(s => s.Id == id);

            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // POST: Merchants/Staffs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Guid,Name,PhoneNumber,Block")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("../Merchants/Details");
            }
            return View(staff);
        }

        // GET: Merchants/Staffs/Delete/5
        public ActionResult Delete(int id)
        {
            Merchant merchant = null;
            using (MerchantService _merchantService = new MerchantService(User.Identity))
            {
                merchant = _merchantService.Merchant();
            }

            Staff staff = merchant.Staffs.Find(s => s.Id == id);

            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // POST: Merchants/Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Staff staff = db.Staffs.Find(id);
            db.Staffs.Remove(staff);
            db.SaveChanges();
            return RedirectToAction("../Merchants/Details");
        }
    }
}
