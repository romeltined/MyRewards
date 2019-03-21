using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyRewards.Entities;
using MyRewards.Models;

namespace MyRewards.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Administrators")]
    public class NopProductsController : Controller
    {
        private NopCommerceContext ndb = new NopCommerceContext();

        // GET: Admin/NopProducts
        public ActionResult Index()
        {
            var products = ndb.Database.SqlQuery<NopProduct>("SELECT Id, Name, ShortDescription, Price FROM [Product]").ToList();
            return View(products);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ndb.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
