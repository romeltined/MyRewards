using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyRewards.Entities;
using MyRewards.Models;
using Newtonsoft.Json;

namespace MyRewards.Services
{
    public  static class SimulatorService
    {

        public static string GenerateVCObject()
        {

            VoucherSpend vc = new VoucherSpend();
            Entities.MerchantStaff ms = new Entities.MerchantStaff();

            ms.MerchantGuid = "043e7e84-a1a4-4c85-bece-c0d8ad8f2ca6";
            ms.MerchantName = "Fair price";
            ms.StaffGuid = "lkjsadoi098";

            VoucherType vt = new VoucherType();
            vt.Id = 1;
            vt.Name = "MyRewards";
            vt.Description = "$10 Party now";
            vt.Amount = 10;

            VoucherType vt2 = new VoucherType();
            vt2.Id = 2;
            vt2.Name = "MyRewards";
            vt2.Description = "$20 Party now";
            vt2.Amount = 20;

            Guid guid = Guid.NewGuid();
            var voucher = new Voucher
            {
                Id = 1,
                VoucherType = vt,
                Guid = guid.ToString(),
                Sender_Id = 1,
                Receiver_Id = 1,
                ActionType_Id = 0,
                SpendFlag = false,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };

            Guid guid2 = Guid.NewGuid();
            var voucher2 = new Voucher
            {
                Id=2,
                VoucherType = vt,
                Guid = guid2.ToString(),
                Sender_Id = 1,
                Receiver_Id = 1,
                ActionType_Id = 0,
                SpendFlag = false,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };

            vc.MerchantStaff = ms;
            vc.VoucherList.Add(voucher);
            vc.VoucherList.Add(voucher2);

            List<VoucherSpend> list = new List<VoucherSpend>();
            list.Add(vc);
            var content = JsonConvert.SerializeObject(list);

            return content;

        }


        public static string GenerateVTObject()
        {

            VoucherTransfer vc = new VoucherTransfer();

            VoucherType vt = new VoucherType();
            vt.Id = 1;
            vt.Name = "MyRewards";
            vt.Description = "$10 Party now";
            vt.Amount = 10;

            Guid guid = Guid.NewGuid();
            var voucher = new Voucher
            {
                VoucherType = vt,
                Guid = guid.ToString(),
                Sender_Id = 1,
                Receiver_Id = 1,
                ActionType_Id = 0,
                SpendFlag = false,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };

            Guid guid2 = Guid.NewGuid();
            var voucher2 = new Voucher
            {
                VoucherType = vt,
                Guid = guid2.ToString(),
                Sender_Id = 1,
                Receiver_Id = 1,
                ActionType_Id = 0,
                SpendFlag = false,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };

            vc.ReceiverUserName = "a@a.com";
            vc.VoucherList.Add(voucher);
            vc.VoucherList.Add(voucher2);

            List<VoucherTransfer> list = new List<VoucherTransfer>();
            list.Add(vc);
            var content = JsonConvert.SerializeObject(list);

            return content;

        }

        public static string GetUser()
        {
            var user = HttpContext.Current.User.Identity;

            return "me";
        }
    }
}