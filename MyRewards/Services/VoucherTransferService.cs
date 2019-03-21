using System;
using System.Collections.Generic;
using System.Linq;
using MyRewards.Entities;
using Newtonsoft.Json;
using MyRewards.Models;
using Microsoft.AspNet.Identity;
using System.Security.Principal;
using System.Data;
using System.Data.Entity;

namespace MyRewards.Services
{
    public static class VoucherTransferService
    {    
        public static VoucherTransferLog PostVoucher(VoucherTransferLog voucherTransferLog, ApplicationDbContext db, NopCommerceContext ndb,IIdentity currentUser)
        {
            try
            {
                var userId = int.Parse(currentUser.GetUserId());
                voucherTransferLog.User_Id = userId;
                voucherTransferLog.CreatedOn = DateTime.UtcNow;
                db.VoucherTransferLogs.Add(voucherTransferLog);
                db.SaveChanges();

                VoucherTransfer voucherTransfer = JsonConvert.DeserializeObject<List<VoucherTransfer>>(voucherTransferLog.Content).ElementAt(0);

                //NopCommerceUser receiver = ndb.Database.SqlQuery<NopCommerceUser>("SELECT CU.Id AS Customer_Id, Username, Email, null AS Password, null AS PasswordSalt FROM [Customer] CU WHERE CU.Username = '" + voucherTransfer.ReceiverUserName + "'").First();

                //FOR STUB PURPOSE ONLY
                var aspUser = db.Users.Where(u => u.UserName == voucherTransfer.ReceiverUserName).First();
                var receiver = new { Customer_Id = aspUser.Id };

                foreach (var vc in voucherTransfer.VoucherList)
                {
                    var voucher = db.Vouchers.Where(v => v.Id == vc.Id && v.Receiver_Id==userId && v.SpendFlag == false).First();
                    voucher.Receiver_Id = receiver.Customer_Id;
                    voucher.Sender_Id = userId;
                    voucher.UpdatedOn = DateTime.UtcNow;
                    db.Entry(voucher).State = EntityState.Modified;
                    db.SaveChanges();
                }

                voucherTransferLog.Result = ResultTypes.Success;
                db.Entry(voucherTransferLog).State = EntityState.Modified;
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                voucherTransferLog.Result = ResultTypes.Failed;
                db.Entry(voucherTransferLog).State = EntityState.Modified;
                db.SaveChanges();
            }
            return voucherTransferLog;
        }
    }
}