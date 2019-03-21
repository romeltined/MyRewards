using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MyRewards.Models
{
    public class UserViewModel
    {
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Username")]
        public string Username { get; set; }
    }
}