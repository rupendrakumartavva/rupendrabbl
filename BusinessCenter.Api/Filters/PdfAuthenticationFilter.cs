using BusinessCenter.Api.Utility;
using BusinessCenter.Data;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Results;
using System.Web.Mvc;
using ActionFilterAttribute = System.Web.Http.Filters.ActionFilterAttribute;

namespace BusinessCenter.Api.Filters
{
    public class PdfAuthenticationFilter : AuthorizationFilterAttribute
    {
        //public override void OnActionExecuting(HttpActionContext actionContext)
        //{
        //    var h = actionContext.ActionArguments["refreshtoken"].ToString();
        //    int BHARATH = 0;

        //    var host = actionContext.Request.RequestUri.DnsSafeHost;
        //    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
        //    actionContext.Response.Headers.Add("WWW-Authenticate", string.Format("Basic realm=\"{0}\"", host));
        //    base.OnActionExecuting(actionContext);
        //    // pre processing
        //}

        private RefreshTokensRepository _refreshTokensRepository;
        private IdentityConnectionContext _ctx;

        public PdfAuthenticationFilter()
        {
            _ctx = new IdentityConnectionContext();
            _refreshTokensRepository = new RefreshTokensRepository(new UnitOfWork(_ctx));
        }

        public async override void OnAuthorization(HttpActionContext actionContext)
        {
            var getInputValue = actionContext.Request.RequestUri.Query.Remove(0, 1);
            string[] GetParames = getInputValue.Split('&');

            var refreshTokenId = GetParames[0].ToString().Split('=');
            if (refreshTokenId[1].ToString() == "freeAccess")
            {
                return;
            }
            var decryptString = AESEncrytDecry.DecryptStringAES(refreshTokenId[1]).ToString();
            var _refreshtoken = await _refreshTokensRepository.FindRefreshTokenPdf(decryptString);
            if (_refreshtoken != null && (_refreshtoken.Any()))
            {
                var _refreshTokenObject = _refreshtoken.ToList().FirstOrDefault();

                var expireTime = _refreshTokenObject.ExpiresUtc;
                TimeSpan differenc = _refreshTokenObject.ExpiresUtc - DateTime.UtcNow;
                int expireTimeInSeconds = Convert.ToInt32(differenc.TotalSeconds);
                if (expireTimeInSeconds > 1800 || expireTimeInSeconds < 0)
                {
                    HandleUnauthorizedRequest(actionContext);
                }
            }
            else
            {
                HandleUnauthorizedRequest(actionContext);
            }
        }

        protected void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Content = new StringContent("SORRY!!! You are not authorized to perform this action.")
            };
            //actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            //actionContext.Response.Headers.Add("WWW-Authenticate", string.Format("Sorry you have to right to Access to this page.{0}", "0"));
        }
    }
}