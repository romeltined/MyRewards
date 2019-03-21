using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using MyRewards.Models;

namespace MyRewards.Services
{
    public class ClaimsAuthorization : AuthorizationFilterAttribute
    {
        public override Task OnAuthorizationAsync(HttpActionContext actionContext, System.Threading.CancellationToken cancellationToken)
        {

            var principal = actionContext.RequestContext.Principal as ClaimsPrincipal;

            if (!principal.Identity.IsAuthenticated)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                return Task.FromResult<object>(null);
            }

            using (ApplicationDbContext db = new ApplicationDbContext())
            { 
                var guid = principal.FindFirst("SessionGuid").Value.ToString();

                var dbGuid = db.SessionGuids.Where(s => s.UserName == principal.Identity.Name).FirstOrDefault();

                if (guid != dbGuid.Guid)
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }

            return Task.FromResult<object>(null);

        }
    }
}