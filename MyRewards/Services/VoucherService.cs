using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyRewards.Models;
using System.Security.Principal;
using Microsoft.AspNet.Identity;

namespace MyRewards.Services
{

    public interface IVoucherService
    {
        List<Voucher> GetVouchers();
    }

    public class VoucherService : IVoucherService
    {
        private readonly ApplicationDbContext db;
        private readonly IIdentity currentUser;

        public VoucherService()
        {
            this.db = new ApplicationDbContext();
            this.currentUser = HttpContext.Current.User.Identity;
        }

        public List<Voucher> GetVouchers()
        {
            var userId = int.Parse(currentUser.GetUserId());
            var vouchers = db.Vouchers.Where(v => v.Receiver_Id == userId && v.SpendFlag == false).ToList();
            return vouchers;
        }
    }
}