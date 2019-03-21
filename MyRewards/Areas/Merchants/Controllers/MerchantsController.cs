using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyRewards.Models;
using Microsoft.AspNet.Identity;
using MyRewards.Services;
using Newtonsoft.Json;

namespace MyRewards.Areas.Merchants.Controllers
{
    [Authorize(Roles = "Managers")]
    public class MerchantsController : Controller
    {
        // GET: Merchants/Merchants/Details/5
        public ActionResult Details()
        {
            Merchant merchant = null;
            using (MerchantService _merchantService = new MerchantService(User.Identity))
            {
                merchant = _merchantService.Merchant();
            }
            if(merchant!=null)
            {
                return View(merchant);
            }
            else
            {
                object Nothing = null;
                return RedirectToAction("Unauthorized","Account",new { area = Nothing });
            }
        }

        // GET: Merchants/Create
        public ActionResult QRCode(int? merchantId)
        {
            Merchant merchant = null;
            using (MerchantService _merchantService = new MerchantService(User.Identity))
            {
                merchant = _merchantService.Merchant();
            }

            List<QRCodeModel> qRCodelist = new List<QRCodeModel>();

            foreach (var s in merchant.Staffs)
            {
                QRCodeModel qrCodes = new QRCodeModel();
                //MerchantStaff merchantStaff = new MerchantStaff();
                //merchantStaff.MerchantGuid = merchant.Guid;
                //merchantStaff.MerchantName = merchant.Name;
                //merchantStaff.StaffGuid = s.Guid;
                //List<MerchantStaff> list = new List<MerchantStaff>();
                string merchantStaff = merchant.Guid + ":" + s.Guid + ":" + merchant.Name;
                List<string> list = new List<string>();
                list.Add(merchantStaff);
                var content = JsonConvert.SerializeObject(list);
                qrCodes.QRCode = QRCodeService.GenerateQRCode(content);
                qrCodes.Merchant = merchant.Name;
                qrCodes.Staff = s.Name;
                qrCodes.PhoneNumber = s.PhoneNumber;
                qRCodelist.Add(qrCodes);
            }

            //string encrypted = ConfigurationManager.AppSettings["encryptedvalue"];
            //string decrypted = CryptoService.Decrypt(encrypted);
            ViewBag.MerchantId = merchantId;
            return View(qRCodelist);


        }

    }
}
