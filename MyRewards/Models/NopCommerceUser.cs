using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyRewards.Entities;

namespace MyRewards.Models
{
    public class NopCommerceUser
    {
        public int Customer_Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public virtual List<string> Roles { get; set; }
    }
}