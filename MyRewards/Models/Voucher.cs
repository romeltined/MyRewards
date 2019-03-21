using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyRewards.Models
{
    public class Voucher
    {
        public int Id { get; set; }
        public string Guid { get; set; }
        public int Sender_Id { get; set; }
        public int Receiver_Id { get; set; }
        public int ActionType_Id { get; set; }
        public bool SpendFlag { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public DateTime? Expiry { get; set; }
        public bool SettleFlag { get; set; }
        public DateTime? SettledOn { get; set; }
        public int Order_Id { get; set; }
        public int VoucherOrder_Id { get; set; }
        public int Merchant_Id { get; set; }
        public int VoucherSpend_Id { get; set; }
        public virtual VoucherType VoucherType { get; set; }
    }
}