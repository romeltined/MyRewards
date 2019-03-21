using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyRewards.Models;
using System.Web.Http.Description;
using MyRewards.Services;

namespace MyRewards.Controllers
{
    [ClaimsAuthorization]
    public class VoucherApiController : ApiController
    {
        private readonly IVoucherService voucherService;

        public VoucherApiController(IVoucherService voucherService)
        {
            this.voucherService = voucherService;
        }
        // GET: api/VoucherApi
        [Authorize]
        public List<Voucher> Get()
        {
            var vouchers = voucherService.GetVouchers();
            return vouchers;
        }

    }
}
