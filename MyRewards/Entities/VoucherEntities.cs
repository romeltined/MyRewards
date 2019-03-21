using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyRewards.Models;

namespace MyRewards.Entities
{
    public class VoucherSpend
    {
        public MerchantStaff MerchantStaff { get; set; }
        public List<Voucher> VoucherList { get; set; }
        public VoucherSpend()
        {
            VoucherList = new List<Voucher>();
        }
    }

    public class VoucherTransfer
    {
        public string ReceiverUserName { get; set; }
        public List<Voucher> VoucherList { get; set; }

        public VoucherTransfer()
        {
            VoucherList = new List<Voucher>();
        }
    }

    public class VoucherOderItem
    {
        public int OrderItem_Id { get; set; }
        public int Order_Id { get; set; }
        public int Product_Id { get; set; }
        public int Quantity { get; set; }
    }

}