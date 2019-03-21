using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyRewards.Models
{
    public class QRCodeViewModel
    {
        public List<QRCodeModel> QRCodes { get; set; }
    }

    public class QRCodeModel
    {
        public string Merchant { get; set; }
        public string Staff { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] QRCode { get; set; }
    }
}