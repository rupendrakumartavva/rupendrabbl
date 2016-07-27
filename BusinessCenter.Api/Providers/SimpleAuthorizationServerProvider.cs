using BusinessCenter.Api.Common;
using BusinessCenter.Api.Models;
using BusinessCenter.Data;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Identity.IdentityExtension;
using BusinessCenter.Identity.IdentityModels;
using BusinessCenter.Identity.Implementation;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;

namespace BusinessCenter.Api.Providers
{
    #region This is working but this will be use

    //    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    //{
    //    public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
    //    {
    //        context.Validated();
    //    }

    //    public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
    //    {
    //        bool isValidUser = false;
    //        context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

    //        if(context.UserName == "test" && context.Password == "test1")
    //        {
    //            isValidUser = true;
    //        }

    //        if (!isValidUser)
    //        {
    //            context.SetError("invalid_grant", "The user name or password is incorrect.");
    //            return;
    //        }

    //        var identity = new ClaimsIdentity(context.Options.AuthenticationType);
    //        identity.AddClaim(new Claim("sub", context.UserName));
    //        identity.AddClaim(new Claim("role", "user"));

    //        context.Validated(identity);
    //    }
    //}

    #endregion This is working but this will be use

    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private RefreshTokensRepository _refreshTokensRepository;
        private readonly ClientsRepository _clientsRepository;
        private readonly UserManagerRepository _userManagerDbService;
        private readonly UserRoleRepository _userRoleRepository;
        private readonly UserRepository _userRepository;
        private readonly RoleRepository _roleRepository;

        private IdentityConnectionContext _ctx;
        public int LoginFailedAttems = 0;

        public SimpleAuthorizationServerProvider()
        {
            _ctx = new IdentityConnectionContext();
            var testUnitOfWork = new UnitOfWork(_ctx);
            _clientsRepository = new ClientsRepository(testUnitOfWork);
            _refreshTokensRepository = new RefreshTokensRepository(testUnitOfWork);
            _userRoleRepository = new UserRoleRepository(testUnitOfWork);
            _userRepository = new UserRepository(testUnitOfWork, _userRoleRepository);
            _userManagerDbService = new UserManagerRepository(testUnitOfWork, _userRoleRepository, _userRepository, _roleRepository);
            _roleRepository=new RoleRepository(testUnitOfWork);
           
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId = string.Empty;
            string clientSecret = string.Empty;
            IEnumerable<Clients> client = null;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                //Remove the comments from the below line context.SetError, and invalidate context
                //if you want to force sending clientId/secrects once obtain access tokens.
                context.Validated();
                //context.SetError("invalid_clientId", "ClientId should be sent.");
                return Task.FromResult<object>(null);
            }

            client = _clientsRepository.FindClient(context.ClientId);

            if (client == null)
            {
                context.SetError("invalid_clientId", string.Format("Client '{0}' is not registered in the system.", context.ClientId));
                return Task.FromResult<object>(null);
            }

            if (client.FirstOrDefault().ApplicationType == (int)Models.ApplicationTypes.NativeConfidential)
            {
                if (string.IsNullOrWhiteSpace(clientSecret))
                {
                    context.SetError("invalid_clientId", "Client secret should be sent.");
                    return Task.FromResult<object>(null);
                }
                else
                {
                    if (client.FirstOrDefault().Secret != Helper.GetHash(clientSecret))
                    {
                        context.SetError("invalid_clientId", "Client secret is invalid.");
                        return Task.FromResult<object>(null);
                    }
                }
            }

            if (!client.FirstOrDefault().Active)
            {
                context.SetError("invalid_clientId", "Client is inactive.");
                return Task.FromResult<object>(null);
            }
            var refreshid = context.Parameters["refresh_token"];
            if (refreshid != null)
            {
                context.OwinContext.Set<string>("as:RefreshTokenvalue", context.Parameters["refresh_token"]);
            }

            context.OwinContext.Set<string>("as:clientAllowedOrigin", client.FirstOrDefault().AllowedOrigin);

            context.OwinContext.Set<string>("as:clientRefreshTokenLifeTime", client.FirstOrDefault().RefreshTokenLifeTime.ToString());

            //  context.OwinContext.Set<string>("as:Ref",);
            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            try
            {
                string resultData = string.Empty;
                string userId = string.Empty;
                var userFullName = string.Empty;
                var userFirstName = string.Empty;
                var userLastName = string.Empty;
                string roleCount = "0";
                var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin") ?? "*";

                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });
                string userName = System.Configuration.ConfigurationManager.AppSettings["freeuser"];
                if (context.UserName != userName)
                {
                    var userManager = new UserManager(context.OwinContext.GetUserManager<ApplicationUserManager>(),
                        HttpContext.Current.GetOwinContext().Authentication);
                    //   var user = await userManager.FindUser(context.UserName, context.Password);
                    //if (context.UserName != "")
                    //{
                    var findUser = await userManager.FindByNameAsync(context.UserName);
                    if (findUser == null)
                    {
                        roleCount = "4";
                        resultData = ResultMessages.nouser;
                        userId = "0";
                        userFullName = "";
                        userFirstName = "";
                        userLastName = "";
                        LoginFailedAttems = 0;
                    }
                    else
                    {
                        var userRoles = await userManager.GetUserRoleName(findUser.Id);
                        if (userRoles.Contains("Employee"))
                        {
                            roleCount = "3";
                            var status =
                                await
                                    userManager.PasswordSignIn(context.UserName, context.Password, false,
                                        shouldLockout: true);
                            switch (status)
                            {
                                case UserSignInStatus.Success:
                                    LoginFailedAttems = 0;
                                    userId = findUser.Id.ToString();
                                    var uLhModel = new UserLoginHistoryModel
                                    {
                                        Count = 0,
                                        LastLoginDate = Convert.ToDateTime(System.DateTime.Now),
                                        UserId = userId.Trim(),
                                        LoginHisId = 0
                                    };
                                    var userLoginHistrory = new Data.Models.UserLoginHistoryModel();
                                    userLoginHistrory.InjectFrom(uLhModel);

                                    _userManagerDbService.AddUserLoginHistory(userLoginHistrory);

                                    userFullName = findUser.FirstName + " " + findUser.LastName;
                                    userFirstName = findUser.FirstName;
                                    userLastName = findUser.LastName;
                                    resultData = status.ToString();
                                    break;

                                case UserSignInStatus.Delete:
                                    resultData = ResultMessages.delete;
                                    break;

                                case UserSignInStatus.Expire:
                                    resultData = ResultMessages.linkExpire;
                                    break;

                                case UserSignInStatus.In_Active:
                                    resultData = ResultMessages.inactive;
                                    break;

                                case UserSignInStatus.Nodata:
                                    roleCount = "4";
                                    resultData = ResultMessages.nouser;
                                    break;

                                case UserSignInStatus.LockedOut:
                                    resultData = status.ToString();
                                    break;

                                default:
                                    var user1 = await userManager.FindByNameAsync(context.UserName);
                                    if (user1 != null)
                                    {
                                        LoginFailedAttems = user1.AccessFailedCount;
                                        userFullName = "";
                                        userFirstName = "";
                                        userLastName = "";
                                    }

                                    resultData = status.ToString();
                                    break;
                            }
                        }
                        else
                        {
                            resultData = "Superadmin/admin";
                            roleCount = "1";
                            userId = "0";
                            userFullName = "";
                            userFirstName = "";
                            userLastName = "";
                            LoginFailedAttems = 0;
                        }
                    }

                    //}

                    string getClientId = string.Empty;
                    IEnumerable<Clients> getClient = null;
                    if (context.ClientId != null)
                    {
                        getClient = _clientsRepository.FindClient(context.ClientId);

                        if (getClient != null)
                        {
                            var firstOrDefault = getClient.FirstOrDefault();
                            if (firstOrDefault != null) getClientId = firstOrDefault.Id;
                        }
                    }

                    var identity = new ClaimsIdentity(context.Options.AuthenticationType);

                    var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                        "as:client_id", context.ClientId ?? string.Empty
                    }
                });
                    if (findUser == null)
                    {
                        props.Dictionary.Add("Ref", "");
                        props.Dictionary.Add("userName", "codeit");
                        props.Dictionary.Add("status", resultData);
                        props.Dictionary.Add("userID", "0");
                        props.Dictionary.Add("userFullName", userFullName);
                        props.Dictionary.Add("RoleCount", roleCount);
                        props.Dictionary.Add("FirstName", userFirstName);
                        props.Dictionary.Add("LastName", userLastName);
                        props.Dictionary.Add("failCount", LoginFailedAttems.ToString());
                        props.Dictionary.Add("GuiToken", string.Empty);
                    }
                    else
                    {
                        props.Dictionary.Add("Ref", "True");
                        props.Dictionary.Add("userName", context.UserName);
                        props.Dictionary.Add("status", resultData);
                        props.Dictionary.Add("userID", userId);
                        props.Dictionary.Add("userFullName", userFullName);
                        props.Dictionary.Add("RoleCount", roleCount);
                        props.Dictionary.Add("FirstName", userFirstName);
                        props.Dictionary.Add("LastName", userLastName);
                        props.Dictionary.Add("failCount", LoginFailedAttems.ToString());
                        props.Dictionary.Add("GuiToken", string.Empty);
                    }

                    var ticket = new AuthenticationTicket(identity, props);
                    context.Validated(ticket);
                }
                else
                {
                    roleCount = "4";
                    resultData = ResultMessages.nouser;
                    userId = "0";
                    userFullName = "";
                    userFirstName = "";
                    userLastName = "";
                    LoginFailedAttems = 0;
                    var identity = new ClaimsIdentity(context.Options.AuthenticationType);

                    var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                        "as:client_id", context.ClientId ?? string.Empty
                    }
                });

                    props.Dictionary.Add("Ref", "");
                    props.Dictionary.Add("userName", "codeit");
                    props.Dictionary.Add("status", resultData);
                    props.Dictionary.Add("userID", "0");
                    props.Dictionary.Add("userFullName", userFullName);
                    props.Dictionary.Add("RoleCount", roleCount);
                    props.Dictionary.Add("FirstName", userFirstName);
                    props.Dictionary.Add("LastName", userLastName);
                    props.Dictionary.Add("failCount", LoginFailedAttems.ToString());
                    props.Dictionary.Add("GuiToken", string.Empty);
                    var ticket = new AuthenticationTicket(identity, props);
                    context.Validated(ticket);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            string getClientId = string.Empty;
            IEnumerable<Clients> getClient = null;

            getClient = _clientsRepository.FindClient(context.ClientId);

            if (getClient != null)
            {
                getClientId = getClient.FirstOrDefault().Id;
            }

            var currentClient = context.ClientId;

            if (getClientId != currentClient)
            {
                context.SetError("invalid_clientId", "Refresh token is issued to a different clientId.");
                return Task.FromResult<object>(null);
            }

            // Change auth ticket for refresh token requests
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);

            var newClaim = newIdentity.Claims.FirstOrDefault(c => c.Type == "newClaim");
            if (newClaim != null)
            {
                newIdentity.RemoveClaim(newClaim);
            }
            newIdentity.AddClaim(new Claim("newClaim", "newValue"));
            context.Ticket.Properties.Dictionary["GuiToken"] = Helper.GetHash(Guid.NewGuid().ToString("n"));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (var property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
    }
}