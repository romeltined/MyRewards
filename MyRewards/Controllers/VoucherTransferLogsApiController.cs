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
using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace MyRewards.Controllers
{
    [ClaimsAuthorization]
    public class VoucherTransferLogsApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private NopCommerceContext ndb = null; //new NopCommerceContext();


        // POST: api/VoucherTransferLogs
        [ResponseType(typeof(VoucherTransferLog))]
        [Authorize]
        public IHttpActionResult PostVoucherTransferLog(VoucherTransferLog voucherTransferLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = VoucherTransferService.PostVoucher(voucherTransferLog, db, ndb, User.Identity);

            return CreatedAtRoute("DefaultApi", new { id = result.Id }, result);
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