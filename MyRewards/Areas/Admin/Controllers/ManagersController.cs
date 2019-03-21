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
    public class ManagersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Managers
        public ActionResult Index()
        {
            return View(db.Managers.ToList());
        }

        // GET: Admin/Managers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manager manager = db.Managers.Find(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            return View(manager);
        }

        // GET: Admin/Managers/Create
        public ActionResult Create(int merchantId)
        {
            Merchant merchant = db.Merchants.Find(merchantId);
            ViewBag.MerchantId = merchant.Id;
            ViewBag.MerchantName = merchant.Name;
            ViewBag.MerchantDescription = merchant.Description;
            return View();
        }

        // POST: Admin/Managers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Username")] Manager manager, int merchantId)
        {
            if (ModelState.IsValid)
            {
                Merchant merchant = db.Merchants.Find(merchantId);
                ViewBag.MerchantId = merchant.Id;
                ViewBag.MerchantName = merchant.Name;
                ViewBag.MerchantDescription = merchant.Description;

                //TODO: Update with NopCommerce table
                var user = db.Database.SqlQuery<int>("SELECT Id FROM AspNetUsers WHERE Username = '" + manager.Username + "'").FirstOrDefault();
                if (user == 0)
                {
                    ModelState.AddModelError("", "Username not found.");
                }
                else
                {
                    try
                    {
                        //TODO: Update with NopCommerce table
                        var roleId = db.Database.SqlQuery<int>("SELECT Id FROM [AspNetRoles] WHERE Name = 'Managers'").FirstOrDefault();
                        var role = db.Database.ExecuteSqlCommand("INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES (" + user + "," + roleId + ")");
                        manager.Customer_Id = user;
                        merchant.Managers.Add(manager);
                        db.Entry(merchant).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("../Merchants/Details/", new { id = merchant.Id });
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", "Username is already a manager of another merchant.");
                    }

                }
            }

            return View();
        }


        // GET: Admin/Managers/Delete/5
        public ActionResult Delete(int? id, int merchantId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manager manager = db.Managers.Find(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            ViewBag.MerchantId = merchantId;
            return View(manager);
        }

        // POST: Admin/Managers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int merchantId)
        {
            Manager manager = db.Managers.Find(id);
            int Customer_Id = manager.Customer_Id;
            db.Managers.Remove(manager);
            db.SaveChanges();

            try
            {
                var result = db.Database.ExecuteSqlCommand("DELETE FROM AspNetUserRoles WHERE UserId=" + Customer_Id + " AND RoleId = 2");
            }
            catch
            {

            }
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
