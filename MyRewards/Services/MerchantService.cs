using Microsoft.AspNet.Identity;
using MyRewards.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace MyRewards.Services
{
    public class MerchantService:   IDisposable
    {
        public ApplicationDbContext _db;
        private IIdentity _user;
        public MerchantService(IIdentity currentUser)
        {
            _db = new ApplicationDbContext();
            _user = currentUser;
        }

        public Merchant Merchant()
        {
            try
            {
                int userId = int.Parse(_user.GetUserId());
                int merchantId = _db.Database.SqlQuery<int>("SELECT Merchant_Id FROM [Managers] WHERE Customer_Id=" + userId).First();
                Merchant merchant = _db.Merchants.Find(merchantId);
                return merchant;
            }
            catch
            {
            }
            return null;
        }


        public void Dispose()
        {
            //_db.Dispose();

        }
    }
}