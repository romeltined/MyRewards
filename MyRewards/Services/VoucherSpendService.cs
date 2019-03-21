using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using MyRewards.Models;
using MyRewards.Entities;
using Newtonsoft.Json;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using MyRewards.Services;

namespace MyRewards.Services
{
    public static class VoucherSpendService
    {
        public static VoucherSpendLog PostVoucher(VoucherSpendLog voucherSpendLog, ApplicationDbContext db, IIdentity currentUser)
        {
            try
            {
                var userId = int.Parse(currentUser.GetUserId());
                voucherSpendLog.User_Id = userId; 
                voucherSpendLog.CreatedOn = DateTime.UtcNow;
                db.VoucherSpendLogs.Add(voucherSpendLog);
                db.SaveChanges();

                VoucherSpend VoucherSpend = JsonConvert.DeserializeObject<List<VoucherSpend>>(voucherSpendLog.Content).ElementAt(0);

                var merchant = db.Merchants.Where(m => m.Guid == VoucherSpend.MerchantStaff.MerchantGuid).First();
                foreach (var vc in VoucherSpend.VoucherList)
                {
                    var voucher = db.Vouchers.Where(v => v.Id == vc.Id && v.Receiver_Id==userId && v.SpendFlag == false).First();
                    voucher.SpendFlag = true;
                    voucher.Merchant_Id = merchant.Id;
                    voucher.UpdatedOn = DateTime.UtcNow;
                    voucher.VoucherSpend_Id = voucherSpendLog.Id;
                    db.Entry(voucher).State = EntityState.Modified;
                    db.SaveChanges();
                }

                voucherSpendLog.Result = ResultTypes.Success;
                db.Entry(voucherSpendLog).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                voucherSpendLog.Result = ResultTypes.Failed;
                db.Entry(voucherSpendLog).State = EntityState.Modified;
                db.SaveChanges();
            }

            return voucherSpendLog;
        }



    }
}