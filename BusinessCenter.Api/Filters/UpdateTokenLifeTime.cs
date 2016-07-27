using BusinessCenter.Data;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Identity.IdentityModels;
using BusinessCenter.Identity.Implementation;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace BusinessCenter.Api.Filters
{
    public class UpdateTokenLifeTime : ActionFilterAttribute
    {
        private RefreshTokensRepository _refreshTokensRepository;
        private IdentityConnectionContext _ctx;

        public UpdateTokenLifeTime()
        {
            _ctx = new IdentityConnectionContext();
            _refreshTokensRepository = new RefreshTokensRepository(new UnitOfWork(_ctx));
        }

        public async override void OnActionExecuted(HttpActionExecutedContext actionContext)
        {
            try
            {
                var refreshTokenId = actionContext.Request.Headers.GetValues("refreshTokenId").ElementAt(0);
                if (refreshTokenId != null)
                {
                    var _refreshtoken = await _refreshTokensRepository.FindRefreshToken(Helper.GetHash(refreshTokenId));
                    var _refreshTokenObject = _refreshtoken.ToList().FirstOrDefault();
                    if (_refreshTokenObject != null)
                    {
                        _refreshTokenObject.IssuedUtc = DateTime.UtcNow;
                        _refreshTokenObject.ExpiresUtc = DateTime.UtcNow.AddMinutes(30);
                        var result = await _refreshTokensRepository.UpdateRefreshTokenTime(_refreshTokenObject, _refreshTokenObject.Id);
                        if (result)
                        {
                            actionContext.ActionContext.Response.Headers.Add("expiretime", _refreshTokenObject.ExpiresUtc.ToString());
                            //actionContext.ActionContext.Response.Content.Headers.Add("expiretime", _refreshTokenObject.ExpiresUtc.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in OnActionExecuted", ex);
            }
        }
    }
}