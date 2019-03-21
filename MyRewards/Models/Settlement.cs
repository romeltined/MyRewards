using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyRewards.Models
{
    public class Settlement
    {
        public int Id { get; set; }
        public int Merchant_Id { get; set; }
        public string MerchantName { get; set; }
        public int Count { get; set; }
        public decimal Total { get; set; }
        public DateTime? SettledOn { get; set; }
    }
}