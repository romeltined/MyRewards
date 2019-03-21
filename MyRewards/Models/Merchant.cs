using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyRewards.Models
{
    public class Merchant
    {
        public int Id { get; set; }
        public string Guid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public virtual List<Staff> Staffs { get; set; }
        public virtual List<Manager> Managers { get; set; }
    }
}