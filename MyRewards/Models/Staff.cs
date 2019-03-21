using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyRewards.Models
{
    public class Staff
    {
        public int Id { get; set; }
        public string Guid { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public bool Block { get; set; }
        //public virtual Merchant Merchant { get; set; }
    }
}