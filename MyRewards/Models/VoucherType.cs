using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyRewards.Models
{
    public class VoucherType
    {
       public int Id { get; set; }
       public int Product_Id { get; set; }
       public string Name { get; set; }
       public string Description { get; set; }
       public decimal Amount { get; set; }
    }
}