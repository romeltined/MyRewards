using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyRewards.Models
{
    public class Manager
    {
        public int Id { get; set; }
        public int Customer_Id { get; set; }
        public string Username { get; set; }
        //public virtual Merchant Merchant { get; set; }
    }
}