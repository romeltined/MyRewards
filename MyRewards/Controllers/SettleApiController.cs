using MyRewards.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyRewards.Services;

namespace MyRewards.Controllers
{
    public class SettleApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Settle
        public IEnumerable<string> Get()
        {

            //db.Database.ExecuteSqlCommand("DELETE FROM [RefreshTokens] WHERE Subject = 'a@a.com'");

            SettlementService service = new SettlementService();
            service.Settle(null);


            return new string[] { "value1", "value2" };
        }

        // GET: api/Settle/5
        public IHttpActionResult Get(int id)
        {
            //return "value";

            var datestring = DateTime.UtcNow.ToString();
            db.Database.ExecuteSqlCommand("UPDATE Vouchers SET SettleFlag = 1, SettledOn='" + datestring + "', UpdatedOn='" + datestring + "' WHERE SpendFlag=1 AND SettleFlag=0");

            var report =
            db.Vouchers
              .Where(d => d.SettleFlag == true)
             .GroupBy(l => new { l.Merchant_Id, l.SettledOn })
                .Select(cl => new
                {
                    Merchant_Id = cl.Max(c => c.Merchant_Id),
                    Count = cl.Count(),
                    Total = cl.Sum(c => c.VoucherType.Amount).ToString(),
                    SettleDate = cl.Max(c => c.SettledOn)
                }).ToList();

            var content = JsonConvert.SerializeObject(report);
            return Ok(content);
        }

        // POST: api/Settle
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Settle/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Settle/5
        public void Delete(int id)
        {
        }
    }
}
