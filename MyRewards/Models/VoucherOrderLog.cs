using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyRewards.Models
{
    public class VoucherOrderLog
    {
        public int Id { get; set; }
        public int Order_Id { get; set; }
        public int Customer_Id { get; set; }
        public string Content { get; set; }
        public int Status_Id { get; set; }
        public DateTime? PaidDateUtc { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}