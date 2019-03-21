using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MyRewards.Models
{
    public class SmartStoreContext : DbContext
    {
        public SmartStoreContext()
        : base("SmartStore")
        {
        }

        static SmartStoreContext()
        {

        }

        public static SmartStoreContext Create()
        {
            return new SmartStoreContext();
        }

    }
}