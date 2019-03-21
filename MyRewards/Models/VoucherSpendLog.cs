using System;
using MyRewards.Entities;

namespace MyRewards.Models
{
    public class VoucherSpendLog
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public string Content { get; set; }
        public ResultTypes? Result { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}