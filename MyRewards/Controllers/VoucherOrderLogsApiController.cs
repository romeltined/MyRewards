using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MyRewards.Models;
using MyRewards.Services;

namespace MyRewards.Controllers
{
    public class VoucherOrderLogsApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/VoucherOrderLogsApi
        //FOR POC PURPOSE ONLY JUST TO TRIGGER THE SERVICE
        public IQueryable<VoucherOrderLog> GetVoucherOrderLogs()
        {
            VoucherOderService svc = new VoucherOderService();
            svc.VoucherOrderFlow();
            return db.VoucherOrderLogs.OrderByDescending(v=>v.Id);
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