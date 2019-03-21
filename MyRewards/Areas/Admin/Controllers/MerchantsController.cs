using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MyRewards.Models;
using System.Threading.Tasks;
using MyRewards.Services;
using Newtonsoft.Json;
using MyRewards.Entities;
using System.Configuration;
using MyRewards.Extensions;

namespace MyRewards.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class MerchantsController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Merchants
        public ActionResult Index()
        {
            return View(db.Merchants.ToList());
        }

        // GET: Merchants/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Merchant merchant = db.Merchants.Find(id);
            if (merchant == null)
            {
                return HttpNotFound();
            }
            return View(merchant);
        }

        // GET: Merchants/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Merchants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description")] Merchant merchant)
        {
            if (ModelState.IsValid)
            {
                Guid guid = Guid.NewGuid();
                merchant.Guid = guid.ToString();
                db.Merchants.Add(merchant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(merchant);
        }

        // GET: Merchants/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Merchant merchant = db.Merchants.Find(id);
            if (merchant == null)
            {
                return HttpNotFound();
            }

            return View(merchant);
        }

        // POST: Merchants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Guid,Name,Description")] Merchant merchant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(merchant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(merchant);
        }

        // GET: Merchants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Merchant merchant = db.Merchants.Find(id);
            if (merchant == null)
            {
                return HttpNotFound();
            }
            return View(merchant);
        }

        // POST: Merchants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Merchant merchant = db.Merchants.Find(id);
            db.Merchants.Remove(merchant);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Merchants/Create
        public ActionResult QRCode(int? merchantId)
        {
            var merchant = db.Merchants.Where(m => m.Id == merchantId).ToList().First();

            List<QRCodeModel> qRCodelist = new List<QRCodeModel>();

            foreach(var s in merchant.Staffs)
            {
                QRCodeModel qrCodes = new QRCodeModel();
                //MerchantStaff merchantStaff = new MerchantStaff();
                //merchantStaff.MerchantGuid = merchant.Guid;
                //merchantStaff.MerchantName = merchant.Name;
                //merchantStaff.StaffGuid = s.Guid;
                //List<MerchantStaff> list = new List<MerchantStaff>();
                string merchantStaff = merchant.Guid + ":" + s.Guid + ":" + merchant.Name;
                List<string> list = new List<string>();
                list.Add(merchantStaff);
                var content = JsonConvert.SerializeObject(list);
                qrCodes.QRCode= QRCodeService.GenerateQRCode(content);
                qrCodes.Merchant = merchant.Name;
                qrCodes.Staff = s.Name;
                qrCodes.PhoneNumber = s.PhoneNumber;
                qRCodelist.Add(qrCodes);
            }

            //string encrypted = ConfigurationManager.AppSettings["encryptedvalue"];
            //string decrypted = CryptoService.Decrypt(encrypted);
            ViewBag.MerchantId = merchantId;
            return View(qRCodelist);


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
