using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyRewards.Models;

namespace MyRewards.Services
{
    public class SettlementService
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void Settle(DateTime? date)
        {
            var datestring = (date==null) ? DateTime.Now.Date : date.Value.Date;
            db.Database.ExecuteSqlCommand("DELETE FROM [Settlements] WHERE SettledOn >= '" + datestring + "' AND SettledOn <'" + datestring.AddDays(1) + "'");
            db.Database.ExecuteSqlCommand("UPDATE [Vouchers] SET SettleFlag = 1, SettledOn='" + datestring + "'" + " WHERE SpendFlag=1 AND SettleFlag=0");

            var result =
                db.Vouchers
                .Where(d => d.SettleFlag == true && d.SettledOn != null)
                .Join(db.Merchants, v => v.Merchant_Id, m => m.Id,
                (v, m) => new { Merchant_Id = m.Id, MerchantName = m.Name, SettledOn = v.SettledOn, VoucherType = v.VoucherType })
                .GroupBy(l => new { l.Merchant_Id, l.SettledOn })
                .Select(cl => new 
                {
                    Merchant_Id = cl.Max(c => c.Merchant_Id),
                    MerchantName = cl.Max(c => c.MerchantName),
                    Count = cl.Count(),
                    Total = cl.Sum(c => c.VoucherType.Amount),
                    SettledOn = cl.Max(c => c.SettledOn)
                }
                ).ToList();

           foreach(var item in result)
            {
                var entry = new Settlement { Merchant_Id = item.Merchant_Id, MerchantName = item.MerchantName, Count = item.Count, Total = item.Total, SettledOn = item.SettledOn };
                db.Settlements.Add(entry);
                db.SaveChanges();
            }
        }
    }
}