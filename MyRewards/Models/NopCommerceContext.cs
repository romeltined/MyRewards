using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MyRewards.Models
{
    public class NopCommerceContext : DbContext
    {
        public NopCommerceContext()
        : base("NopCommerce")
        {
        }

        static NopCommerceContext()
        {

        }

        public static NopCommerceContext Create()
        {
            return new NopCommerceContext();
        }

    }
}