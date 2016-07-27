using BusinessCenter.Data;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Identity.IdentityModels;
using BusinessCenter.Identity.Implementation;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BusinessCenter.Api.Providers
{
    public class SimpleRefreshTokenProvider : IAuthenticationTokenProvider
    {
        private readonly RefreshTokensRepository _refreshTokensRepository;
        private ClientsRepository _clientsRepository;
        private IdentityConnectionContext _ctx;

        public SimpleRefreshTokenProvider()
        {
            _ctx = new IdentityConnectionContext();
            var testUnitOfWork = new UnitOfWork(_ctx);
            _clientsRepository = new ClientsRepository(testUnitOfWork);
            _refreshTokensRepository = new RefreshTokensRepository(testUnitOfWork);
        }

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var getRefreshToken = context.OwinContext.Get<string>("as:RefreshTokenvalue");

            var clientid = context.Ticket.Properties.Dictionary["as:client_id"];
            if (string.IsNullOrEmpty(clientid))
            {
                return;
            }
            string refreshTokenId = Guid.NewGuid().ToString("n");
            if (getRefreshToken != null)
            {
                refreshTokenId = getRefreshToken;
            }
            else
            {
                refreshTokenId = Guid.NewGuid().ToString("n");
            }
            var guiToken = context.Ticket.Properties.Dictionary["GuiToken"] != string.Empty
                ? context.Ticket.Properties.Dictionary["GuiToken"].ToString()
                : Helper.GetHash(Guid.NewGuid().ToString("n"));

            //  refreshTokenId = Guid.NewGuid().ToString("n");
            var refreshTokenLifeTime = context.OwinContext.Get<string>("as:clientRefreshTokenLifeTime");

            var token = new RefreshTokens()
            {
                Id = Helper.GetHash(refreshTokenId),
                ClientId = clientid,
                Subject = context.Ticket.Properties.Dictionary["userName"],
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeTime)),
                TokenGui = guiToken,
            };

            context.Ticket.Properties.IssuedUtc = token.IssuedUtc;
            context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc;
            context.Ticket.Properties.Dictionary["GuiToken"] = token.TokenGui;
            token.ProtectedTicket = context.SerializeTicket();
            var userManager = new UserManager(context.OwinContext.GetUserManager<ApplicationUserManager>(),
                HttpContext.Current.GetOwinContext().Authentication);
            var findUser = await userManager.FindByNameAsync(context.Ticket.Properties.Dictionary["userName"]);
            if (findUser != null)
            {
                var result = await _refreshTokensRepository.AddRefreshToken(token);

                if (result)
                {
                    context.SetToken(refreshTokenId);
                }
            }
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            string hashedTokenId = Helper.GetHash(context.Token);

            var refreshToken = await _refreshTokensRepository.FindRefreshToken(hashedTokenId);

            if (refreshToken != null)
            {
                //Get protectedTicket from refreshToken class
                // var g = context.DeserializeTicket(refreshToken.FirstOrDefault().ProtectedTicket).ToString();
                context.DeserializeTicket(refreshToken.FirstOrDefault().ProtectedTicket);
                var result = await _refreshTokensRepository.RemoveRefreshToken(hashedTokenId);
            }
        }

        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            //context.DeserializeTicket(context.Token);
            throw new NotImplementedException();
        }
    }
}