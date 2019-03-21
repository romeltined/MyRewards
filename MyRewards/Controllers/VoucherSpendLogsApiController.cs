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
using MyRewards.Entities;
using Microsoft.AspNet.Identity;

namespace MyRewards.Controllers
{
    [ClaimsAuthorization]
    public class VoucherSpendLogsApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // POST: api/VoucherSpendLogs
        [ResponseType(typeof(VoucherSpendLog))]
        [Authorize]
        public IHttpActionResult PostVoucherSpendLog(VoucherSpendLog VoucherSpendLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = VoucherSpendService.PostVoucher(VoucherSpendLog, db, User.Identity);

            return CreatedAtRoute("DefaultApi", new { id = result.Id }, result);

            //var json = SimulatorService.GenerateVCObject();
            //return Ok(json);
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