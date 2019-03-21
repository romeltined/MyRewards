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
    public class RefreshTokensController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/RefreshTokens
        public ActionResult Index()
        {
            return View(db.RefreshTokens.ToList());
        }

        // GET: Admin/RefreshTokens/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RefreshToken refreshToken = db.RefreshTokens.Find(id);
            if (refreshToken == null)
            {
                return HttpNotFound();
            }
            return View(refreshToken);
        }

        // GET: Admin/RefreshTokens/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/RefreshTokens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Subject,ClientId,IssuedUtc,ExpiresUtc,ProtectedTicket")] RefreshToken refreshToken)
        {
            if (ModelState.IsValid)
            {
                db.RefreshTokens.Add(refreshToken);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(refreshToken);
        }

        // GET: Admin/RefreshTokens/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RefreshToken refreshToken = db.RefreshTokens.Find(id);
            if (refreshToken == null)
            {
                return HttpNotFound();
            }
            return View(refreshToken);
        }

        // POST: Admin/RefreshTokens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Subject,ClientId,IssuedUtc,ExpiresUtc,ProtectedTicket")] RefreshToken refreshToken)
        {
            if (ModelState.IsValid)
            {
                db.Entry(refreshToken).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(refreshToken);
        }

        // GET: Admin/RefreshTokens/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RefreshToken refreshToken = db.RefreshTokens.Find(id);
            if (refreshToken == null)
            {
                return HttpNotFound();
            }
            return View(refreshToken);
        }

        // POST: Admin/RefreshTokens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            RefreshToken refreshToken = db.RefreshTokens.Find(id);
            db.RefreshTokens.Remove(refreshToken);
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
