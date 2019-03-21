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
    public class SessionGuidsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/SessionGuids
        public ActionResult Index()
        {
            return View(db.SessionGuids.ToList());
        }

        // GET: Admin/SessionGuids/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SessionGuid sessionGuid = db.SessionGuids.Find(id);
            if (sessionGuid == null)
            {
                return HttpNotFound();
            }
            return View(sessionGuid);
        }

        // GET: Admin/SessionGuids/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/SessionGuids/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserName,Guid")] SessionGuid sessionGuid)
        {
            if (ModelState.IsValid)
            {
                db.SessionGuids.Add(sessionGuid);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sessionGuid);
        }

        // GET: Admin/SessionGuids/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SessionGuid sessionGuid = db.SessionGuids.Find(id);
            if (sessionGuid == null)
            {
                return HttpNotFound();
            }
            return View(sessionGuid);
        }

        // POST: Admin/SessionGuids/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,Guid")] SessionGuid sessionGuid)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sessionGuid).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sessionGuid);
        }

        // GET: Admin/SessionGuids/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SessionGuid sessionGuid = db.SessionGuids.Find(id);
            if (sessionGuid == null)
            {
                return HttpNotFound();
            }
            return View(sessionGuid);
        }

        // POST: Admin/SessionGuids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SessionGuid sessionGuid = db.SessionGuids.Find(id);
            db.SessionGuids.Remove(sessionGuid);
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
