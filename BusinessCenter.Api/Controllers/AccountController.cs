using BusinessCenter.Api.Common;
using BusinessCenter.Api.Models;
using BusinessCenter.Email;
using BusinessCenter.Identity.IdentityExtension;
using BusinessCenter.Identity.IdentityModels;
using BusinessCenter.Identity.Interfaces;
using BusinessCenter.Service.Interface;
using Omu.ValueInjecter;
using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using BusinessCenter.Api.Utility;
using BusinessCenter.Api.Filters;
//using System.Web.Mvc;Utility
using RazorEngine;
using ToConvertUser = BusinessCenter.Identity.ToConvertUser;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;



namespace BusinessCenter.Api.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private readonly IUserManager _userManager;
        private readonly IEmailTemplate _userMail;
        private readonly IUserManagerService _userManagerDbService;
        private readonly IUserPasswordTrackingService _userPasswordTrackingService;
        private readonly IRefreshTokensRepository _refreshTokensRepository;
        private readonly IMailTemplateService _MailTemplateService;
        public int LoginFailedAttems = 0;

        public AccountController(IUserManager userManager, IEmailTemplate userMail, IUserManagerService userManagerDbService,
            IUserPasswordTrackingService userPasswordTrackingService, IRefreshTokensRepository refreshTokensRepository, IMailTemplateService mailTemplateService)
        {
            _userManager = userManager;
            _userMail = userMail;
            _userManagerDbService = userManagerDbService;
            _userPasswordTrackingService = userPasswordTrackingService;
            _refreshTokensRepository = refreshTokensRepository;
            _MailTemplateService = mailTemplateService;
        
        }
       
        /// <summary>
        /// Login method accepts LoginViewModel as parameters
        /// It invokes FindByNameAsync from IUserManager to get UserDetails based on UserName
        /// If User is active then sigin is done for user by using PasswordSignIn method from IUserManager
        /// and after Sigin It updates AccessFailedCount and LastLoginDateandTime
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
     
        [Route("Login")]
     
        [HttpPost]
     public async Task<IHttpActionResult> Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
         
     
            string resultData;
            string userId =string.Empty;
            var userFullName = string.Empty;
            var userFirstName = string.Empty;
            var userLastName = string.Empty;
            string roleCount = "0";
            var user = await _userManager.FindByNameAsync(login.UserName);

            if (user == null)
            {
                roleCount = "4";
                resultData = ResultMessages.nouser;
                userId = "0";
                userFullName = "";
                userFirstName = "";
                userLastName = "";
                LoginFailedAttems = 0;
                return
                    Json(
                        new
                        {
                            status = resultData,
                            userID = 0,
                            userName = userFullName,
                            RoleCount = roleCount,
                            FirstName = userFirstName,
                            LastName = userLastName,
                            failCount = LoginFailedAttems
                        });
            }
            else
            {

                var userRoles = await _userManager.GetUserRoleName(user.Id);
                if (userRoles.Contains("Employee"))
                {
                    roleCount = "3";
                    var status =
                    await
                        _userManager.PasswordSignIn(login.UserName, login.Password, login.RememberMe,
                            shouldLockout: true);
                    switch (status)
                    {
                        case UserSignInStatus.Success:
                            LoginFailedAttems = 0;
                            userId = user.Id.ToString();
                            var uLhModel = new UserLoginHistoryModel
                            {
                                Count = 0,
                                LastLoginDate = Convert.ToDateTime(System.DateTime.Now),
                                UserId =userId.Trim(),
                                LoginHisId = 0
                            };
                            var userLoginHistrory = new Data.Models.UserLoginHistoryModel();
                            userLoginHistrory.InjectFrom(uLhModel);

                            _userManagerDbService.UserLoginHistory(userLoginHistrory);

                            userFullName = user.FirstName + " " + user.LastName;
                            userFirstName = user.FirstName;
                            userLastName = user.LastName;
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
                            var user1 = await _userManager.FindByNameAsync(login.UserName);
                            if (user1 != null)
                            {
                                LoginFailedAttems = user1.AccessFailedCount;
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



                return
                    Json(
                        new
                        {
                            status = resultData,
                            userID = userId,
                            userName = userFullName,
                            RoleCount = roleCount,
                            FirstName = userFirstName,
                            LastName = userLastName,
                            failCount = LoginFailedAttems
                        });
            }
        }



        /// <summary>
        /// It clears the Cookies with Session variables.
        /// and It also Invokes the SignOut method from IUserManager Interface
        /// </summary>
        /// <returns></returns>
        //[Authorize]
     [Route("Logout")]
        [HttpPost]
        public async Task<IHttpActionResult> Logout(ModelFactory.DeleteRefreshToken tokenId)
        {
            bool result = false;
            try
            {
                var delete = await _refreshTokensRepository.RemoveRefreshToken(Helper.GetHash(tokenId.RefreshTokenId));
                if (delete)
                {

                }
               //
                _userManager.SignOut();
                System.Web.HttpContext.Current.Response.Cookies.Clear();
                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Json(new { status = result.ToString() });
        }
        [Authorize]
      //  [ValidateAntiForgeryToken]
        [Route("ResetPasswordByToken")]
        [HttpPost]
        public async Task<IHttpActionResult> ResetPasswordByToken(ResetPasswordModel user)
        {
            string resultData;
            try
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user.Userid);
                if (token == null)
                {
                    //resultData = "Token not generated";
                    resultData = ResultMessages.tokenmsg;
                }

                var result = await _userManager.ResetPasswordAsync(user.Userid, token, user.Password);
                resultData = result.Succeeded ? ResultMessages.success : ResultMessages.error;
            }
            catch (Exception ex)
            {
                resultData = ex.Message.ToString();
            }
            return Json(new { status = resultData });
        }

        /// <summary>
        /// FindByNameAsync method in IUserManager is executed which Gets the UserDetails based on
        /// the UserName provided. If It able to get the userdetails and email is confirmed
        /// then it invokes a method GeneratePasswordResetTokenAsync in IUserManager which redirects to a link,
        /// to Take new Password from User
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [Route("ForgotPassword")]
        [HttpPost]
        public async Task<IHttpActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            string result = string.Empty;
            string userMail = string.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByNameAsync(model.UserName);

                    if (user == null)
                    {
                        result = ResultMessages.NoUser;
                    }
                    else
                    {
                        bool getvalue = GenaralConvertion.CheckDateExpire(System.DateTime.Now.ToString(),
                          GenaralConvertion.ValidateGivenTime(user.ActivationDate.ToString()).ToString());

                        if ((user.IsActive == true) && (user.IsDelete == false))
                        {
                            DateTime expireDate = GenaralConvertion.ValidateGivenTime(user.LockoutEndDateUtc.ToString());
                            string h = System.DateTime.UtcNow.ToString();
                            if (((user.LockoutEnabled == true && (user.AccessFailedCount == 0 || user.AccessFailedCount == 5))) &&
                                (expireDate > System.DateTime.UtcNow))
                            {
                                result = ResultMessages.lockout;
                            }
                            else
                            {
                                var code = await _userManager.GeneratePasswordResetTokenAsync(user.Id);
                                ForgotPasswordMailSend(user.Id, code, user.Email.ToLower().Trim(), user.FirstName + " " + user.LastName);
                                var getUser = await _userManager.FindUserDetailsByIdAsync(user.Id);
                                var regUserDetails = new AppUser();
                                regUserDetails.InjectFrom(getUser);
                                regUserDetails.IsForgot = true;
                                regUserDetails.UpdatedDate = System.DateTime.Now;
                                await _userManager.UpdateAsync(regUserDetails, user.Id);
                                result = ResultMessages.success;
                                userMail = user.Email;
                            }
                        }
                        else if ((user.IsActive == false))
                        {
                            if (user.ActivationDate != null)
                            {
                                bool chcekDate = GenaralConvertion.CheckExpireDate(user.ActivationDate.ToString(),
                                    System.DateTime.Now.ToString());
                                if ((user.IsActive == false) && (user.IsDelete == false) && chcekDate == true)
                                {
                                    await _userManager.UserDeleteAsync(user.Id);
                                    result = ResultMessages.NoUser;
                                }
                                else
                                {
                                    result = ResultMessages.inactive;
                                }
                            }
                            else
                            {
                                result = ResultMessages.inactive;
                            }

                            //   result = ResultMessages.inactive;
                        }
                        else if ((user.IsActive == true) && (user.IsDelete == true))
                        {
                            result = ResultMessages.delete;
                        }
                    }
                }
                catch (Exception ex)
                {
                    result = ex.Message.ToString();
                }
            }
            return Json(new { status = result.ToString(), userMail = userMail.ToLower().Trim() });
        }

        [AllowAnonymous]
        [Route("ForgotPasswordAdmin")]
        [HttpPost]
        public async Task<IHttpActionResult> ForgotPasswordAdmin(ForgotPasswordViewModel model)
        {
            string result = string.Empty;
            string userMail = string.Empty;
            string Activecode = string.Empty;
            string userid = string.Empty;
            string fullname = string.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByNameAsync(model.UserName);

                    if (user == null)
                    {
                        result = ResultMessages.NoUser;
                    }
                    else
                    {
                        bool getvalue = GenaralConvertion.CheckDateExpire(System.DateTime.Now.ToString(),
                          GenaralConvertion.ValidateGivenTime(user.ActivationDate.ToString()).ToString());

                        if ((user.IsActive == true) && (user.IsDelete == false))
                        {
                            DateTime expireDate = GenaralConvertion.ValidateGivenTime(user.LockoutEndDateUtc.ToString());
                            string h = System.DateTime.UtcNow.ToString();
                            if (((user.LockoutEnabled == true && (user.AccessFailedCount == 0 || user.AccessFailedCount == 5))) &&
                                (expireDate > System.DateTime.UtcNow))
                            {
                                result = ResultMessages.lockout;
                            }
                            else
                            {
                                var code = await _userManager.GeneratePasswordResetTokenAsync(user.Id);
                                AdminForgotPasswordMailSend(user.Id, code, user.Email.ToLower().Trim(), user.FirstName + " " + user.LastName);
                                var getUser = _userManager.FindUserDetailsByIdAsync(user.Id);
                                var regUserDetails = new AppUser();
                                regUserDetails.InjectFrom(getUser);
                                regUserDetails.IsForgot = true;
                                regUserDetails.UpdatedDate = System.DateTime.Now;
                                result = ResultMessages.success;
                                userMail = user.Email;
                                userid = user.Id;
                                Activecode = code;
                                fullname = user.FirstName + " " + user.LastName;
                            }
                        }
                        else if ((user.IsActive == true) && (user.IsDelete == true))
                        {
                            result = ResultMessages.delete;
                        }
                        else if ((user.IsActive == false) && (user.IsDelete == false))
                        {
                            result = getvalue == true ? ResultMessages.inactive : ResultMessages.reregister;
                        }
                    }
                }
                catch (Exception ex)
                {
                    result = ex.Message.ToString();
                }
            }
            return Json(new { status = result.ToString(), EmailAddress = userMail.ToLower().Trim(), userid = userid, code = Activecode, fullname = fullname });
        }

        [Authorize]
        [Route("NewForgotPassword")]
        [HttpPost]     
        public async Task<IHttpActionResult> NewForgotPassword(ForgotPasswordViewModel model)
        {
            string result = string.Empty;
            string userMail = string.Empty;
            string question1 = string.Empty;
            string question2 = string.Empty;
            string question3 = string.Empty;
            string randomQuestion = string.Empty;
            string rolecount = "0";
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user.Id)))
                {
                    result = ResultMessages.NoUser;
                    rolecount = "4";
                }

                if (user != null)
                {
                    //  rolecount = user.Roles.Count.ToString();
                    //Added The Role Count.
                    var userRoles = await _userManager.GetUserRoleName(user.Id);
                    if (userRoles.Contains("Employee"))
                    {
                        rolecount = "3";

                        bool getvalue = GenaralConvertion.CheckDateExpire(System.DateTime.Now.ToString(),
                            GenaralConvertion.ValidateGivenTime(user.ActivationDate.ToString()).ToString());

                        if ((user.IsActive == true) && (user.IsDelete == false))
                        {
                            DateTime expireDate = GenaralConvertion.ValidateGivenTime(user.LockoutEndDateUtc.ToString());
                            //  string h = System.DateTime.UtcNow.ToString();
                            if (((user.LockoutEnabled == true &&
                                  (user.AccessFailedCount == 0 || user.AccessFailedCount == 5))) &&
                                (expireDate > System.DateTime.UtcNow))
                            {
                                result = ResultMessages.lockout;
                            }
                            else
                            {
                                Random getQuestion = new Random();
                                int getnum = getQuestion.Next(1, 4);

                                if (getnum == 1)
                                {
                                    randomQuestion = user.SecurityQuestion1;
                                }
                                else if (getnum == 2)
                                {
                                    randomQuestion = user.SecurityQuestion2;
                                }
                                else if (getnum == 3)
                                {
                                    randomQuestion = user.SecurityQuestion3;
                                }

                                question1 = user.SecurityQuestion1;
                                question2 = user.SecurityQuestion2;
                                question3 = user.SecurityQuestion3;

                                result = ResultMessages.success;
                                userMail = user.Email.ToLower().Trim();
                            }
                        }
                        else if ((user.IsActive == false))
                        {
                            if (user.ActivationDate != null)
                            {
                                bool chcekDate = GenaralConvertion.CheckExpireDate(user.ActivationDate.ToString(),
                                    System.DateTime.Now.ToString());
                                if ((user.IsActive == false) && (user.IsDelete == false) && chcekDate == true)
                                {
                                    await _userManager.UserDeleteAsync(user.Id);
                                    result = ResultMessages.NoUser;
                                }
                                else
                                {
                                    result = ResultMessages.inactive;
                                }
                            }
                            else
                            {
                                result = ResultMessages.inactive;
                            }
                        }
                        else if ((user.IsActive == true) && (user.IsDelete == true))
                        {
                            result = ResultMessages.delete;
                        }
                    }

                    else
                    {
                        rolecount = "0";
                    }
                }
                else
                {
                    result = ResultMessages.nullinput;
                }

            }


            //question1, question2 = question2, question3 = question3
            return Json(new { status = result.ToString(), userMail = userMail.ToLower().Trim(), question1 = randomQuestion, Rcount = rolecount });
        }

        [Authorize]
        [Route("ForgotUserName")]
        [HttpPost]
       
        public async Task<IHttpActionResult> ForgotUserName(ForgotUserNameViewModel model)
        {
            string result = string.Empty;
            int userId = 0;
            string question1 = string.Empty;
            string question2 = string.Empty;
            string question3 = string.Empty;
            string userName = string.Empty;
            string fullName = string.Empty;
            string rolecount = "0";
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByEmailAddress(model.Email);

                    if (user == null)
                    {
                        result = ResultMessages.NoUser;
                        rolecount = "4";
                    }
                    else
                    {
                        var userRoles = await _userManager.GetUserRoleName(user.Id);
                        if (userRoles.Contains("Employee"))
                        {
                            rolecount = "3";
                        }
                        else
                        {
                            rolecount = "1";
                        }
                        bool getvalue = GenaralConvertion.CheckExpireDate(System.DateTime.Now.ToString(),
                           GenaralConvertion.ValidateGivenTime(user.ActivationDate.ToString()).ToString());
                        if ((user.IsActive == true) && (user.IsDelete == false))
                        {
                            // rolecount = user.Roles.Count.ToString();
                            //Added The Role Count.

                            DateTime expireDate = GenaralConvertion.ValidateGivenTime(user.LockoutEndDateUtc.ToString());
                            string h = System.DateTime.UtcNow.ToString();
                            if (((user.LockoutEnabled == true && (user.AccessFailedCount == 0 || user.AccessFailedCount == 5))) &&
                                (expireDate > System.DateTime.UtcNow))
                            {
                                result = ResultMessages.lockout;
                            }
                            else
                            {

                                result = ResultMessages.success;

                                string siteAddress = ConfigurationManager.AppSettings["siteAddress"] + "#/login";
                                fullName = user.FirstName + " " + user.LastName;
                                userName = user.UserName;
                                var body = ForgotUserNameBody(user.FirstName + " " + user.LastName, siteAddress,
                                    user.UserName);

                                var mailTemplateModel = new MailTemplateModel
                                {
                                    UserId = user.Id,
                                    CustomApplicationId = user.Id,
                                    Type = "USERNAME RESET",
                                    Subject = "USERNAME RESET",
                                    MailSentFailCount = 0,
                                    MailContent = body,
                                    IsMailSent = true
                                };
                                _MailTemplateService.InsertUpdateMailTemplate(mailTemplateModel);
                                _userMail.MailSending("USERNAME RESET", body,
                                     user.Email.ToLower().Trim());
                            }
                        }
                        else if ((user.IsActive == true) && (user.IsDelete == true))
                        {
                            rolecount = user.Roles.Count.ToString();
                            result = ResultMessages.delete;
                        }
                        else if ((user.IsActive == false) && (user.IsDelete == false))
                        {
                            bool chcekDate = GenaralConvertion.CheckExpireDate(user.ActivationDate.ToString(),
                                             System.DateTime.Now.ToString());

                            if ((user.IsActive == false) && (user.IsDelete == false) && chcekDate == true)
                            {
                                await _userManager.UserDeleteAsync(user.Id);
                                result = ResultMessages.reregister;
                            }
                            else
                            {
                                result = ResultMessages.inactive;
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    result = ex.Message.ToString();
                }
            }
            return Json(new { status = result.ToString(), userID = userId, question1 = question1, question2 = question2, question3 = question3, FullName = fullName, UserName = userName, Rcount = rolecount });
        }

        private string ForgotUserNameBody(string fullName, string url, string myDcUserName)
        {
            string body;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplates/ForgotUserName.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", fullName);
            body = body.Replace("{Url}", url);
            body = body.Replace("{MyDcUserName}", myDcUserName);
            return body;
        }

        [Authorize]
        [Route("ForgotUserNameValidation")]
        [HttpPost]
        public async Task<IHttpActionResult> ForgotUserNameValidation(ForgotPasswordValidationViewModel model)
        {
            string result = string.Empty;
            string userMail = string.Empty;
            var userName = string.Empty;
            bool question1 = false;
            //bool question2 = false;
            //bool question3 = false;
            bool wrongFailCount = false;
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindUserDetailsByIdAsync(model.UserId);

                if (user == null)
                {
                    result = ResultMessages.NoUser;
                }

                if (user != null && user.SecurityAnswer1 == model.SecurityAnswer1)
                {
                    question1 = true;
                }

                if ((question1 == true))
                {
                    result = ResultMessages.success;
                    userMail = user.Email;
                    userName = user.UserName;
                    string siteAddress = ConfigurationManager.AppSettings["siteAddress"];
                    var body = ForgotUserNameBody(user.FirstName + " " + user.LastName, siteAddress,
                        user.UserName);
                    var mailTemplateModel = new MailTemplateModel
                    {
                        UserId = user.Id,
                        CustomApplicationId = user.Id,
                        Type = "USERNAME RESET",
                        Subject = "USERNAME RESET",
                        MailSentFailCount = 0,
                        MailContent = body,
                        IsMailSent = true
                    };
                    _MailTemplateService.InsertUpdateMailTemplate(mailTemplateModel);
                    _userMail.MailSending("USERNAME RESET", body,
                         user.Email.ToLower().Trim());

                }
                else
                {
                    if (user != null)
                    {
                        DateTime expireDate = GenaralConvertion.ValidateGivenTime(user.LockoutEndDateUtc.ToString());

                        //   string h = System.DateTime.UtcNow.ToString();

                        if ((user.LockoutEnabled == false ||
                             (expireDate < System.DateTime.UtcNow)))
                        {
                            var result1 = await _userManager.AccessFailedAsync(model.UserId);

                            if (result1.Succeeded == true)
                            {
                                wrongFailCount = false;
                            }
                            else
                            {
                                wrongFailCount = true;
                            }
                            //_userManager.AccessFailedAsync()
                            result = ResultMessages.fail;
                        }
                        else
                        {
                            result = ResultMessages.locked;
                        }
                    }
                }
            }
            return Json(new { status = result.ToString(), userMail = userMail.ToLower().Trim(), userName = userName, wrongFailCount = wrongFailCount });
        }

        [Authorize]
        [Route("ForgotValidation")]
        [HttpPost]
        public async Task<IHttpActionResult> ForgotValidation(ForgotPasswordViewModel model)
        {
            string result = string.Empty;
            string userId = string.Empty;
            string userMail = string.Empty;
            string question1 = string.Empty;
            string question2 = string.Empty;
            string question3 = string.Empty;
            string useranswer = "";
            string userCode = string.Empty;
            bool wrongFailCount = false;
            int SecurityQFailedCount1 = 0;
            string rolecount = "0";
            if (ModelState.IsValid)
            {
                // var user = await _userManager.FindUserDetailsByIdAsync(Convert.ToInt32(model.UserName));FindByNameAsync
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user.Id)))
                {
                    result = ResultMessages.NoUser;
                    rolecount = "4";
                }

                else
                {
                    // rolecount = user.Roles.Count.ToString();
                    var userRoles = await _userManager.GetUserRoleName(user.Id);
                    if (userRoles.Contains("Employee"))
                    {
                        rolecount = "3";


                        model.SecurityQuestion1 = model.SecurityQuestion1 == null ? "" : model.SecurityQuestion1.Trim();
                        model.SecurityAnswer1 = model.SecurityAnswer1 == null ? "" : model.SecurityAnswer1.Trim();
                        if (model.SecurityQuestion1.ToUpper().Trim() == user.SecurityQuestion1.ToUpper().Trim())
                        {
                            model.SecurityAnswer1 = model.SecurityAnswer1 == null ? "" : model.SecurityAnswer1.Trim();
                            useranswer = user.SecurityAnswer1;
                        }
                        else if (model.SecurityQuestion1.ToUpper().Trim() == user.SecurityQuestion2.ToUpper().Trim())
                        {
                            model.SecurityAnswer1 = model.SecurityAnswer1 == null ? "" : model.SecurityAnswer1.Trim();
                            useranswer = user.SecurityAnswer2;
                        }

                        else if (model.SecurityQuestion1.ToUpper().Trim() == user.SecurityQuestion3.ToUpper().Trim())
                        {
                            model.SecurityAnswer1 = model.SecurityAnswer1 == null ? "" : model.SecurityAnswer1.Trim();
                            useranswer = user.SecurityAnswer3;
                        }

                        if (user != null && useranswer.Trim() == model.SecurityAnswer1.Trim() && useranswer.Trim() != "")
                        {
                            question1 = "1";
                        }

                        if (user != null)
                        {
                            bool getvalue = GenaralConvertion.CheckExpireDate(System.DateTime.Now.ToString(),
                                  GenaralConvertion.ValidateGivenTime(user.ActivationDate.ToString()).ToString());

                            if ((user.IsActive == true) && (user.IsDelete == false))
                            {
                                DateTime expireDate;
                                if (user.LockoutEndDateUtc != null)
                                {
                                    expireDate = GenaralConvertion.ValidateGivenTime(user.LockoutEndDateUtc.ToString());
                                }
                                else
                                {
                                    expireDate = System.DateTime.UtcNow;
                                }


                                if (((user.LockoutEnabled == true && (user.AccessFailedCount == 0 || user.AccessFailedCount == 5))) &&
                                    (expireDate > System.DateTime.UtcNow))
                                {
                                    result = ResultMessages.lockout;
                                }
                                else
                                {
                                    var code = await _userManager.GeneratePasswordResetTokenAsync(user.Id);
                                    userCode = code;
                                    userId = user.Id.ToString();
                                    if (string.IsNullOrEmpty(model.SecurityAnswer1))
                                    {
                                        SecurityQFailedCount1 = 0;
                                        ForgotPasswordMailSend(user.Id, code, user.Email.ToLower().Trim(), user.FirstName + " " + user.LastName);
                                        var getUser = await _userManager.FindUserDetailsByIdAsync(user.Id);
                                        var regUserDetails = new AppUser();
                                        regUserDetails.InjectFrom(getUser);
                                        regUserDetails.IsForgot = true;
                                        regUserDetails.UpdatedDate = System.DateTime.Now;
                                        regUserDetails.Id = user.Id;
                                        userMail = user.Email;
                                        regUserDetails.Address = user.Address.Trim();

                                        regUserDetails.MobileNumber = user.MobileNumber.Trim();
                                        regUserDetails.PostalCode = user.PostalCode.Trim();
                                        regUserDetails.State = user.State.Trim();
                                        regUserDetails.City = user.City.Trim();
                                        await _userManager.UpdateAsync(regUserDetails, user.Id);
                                        result = ResultMessages.success;
                                    }
                                    else
                                    {
                                        if ((question1 == "1"))
                                        {
                                            SecurityQFailedCount1 = 0;
                                            //  ForgotPasswordMailSend(user.Id, code, user.Email, user.FirstName + " " + user.LastName); && (question2 == "1") && (question3 == "1")
                                            var getUser = await _userManager.FindUserDetailsByIdAsync(user.Id);
                                            var regUserDetails = new AppUser();
                                            regUserDetails.InjectFrom(getUser);
                                            regUserDetails.IsForgot = true;
                                            regUserDetails.UpdatedDate = System.DateTime.Now;
                                            regUserDetails.Id = user.Id;
                                            userMail = user.Email;
                                            regUserDetails.Address = user.Address.Trim();

                                            regUserDetails.MobileNumber = user.MobileNumber.Trim();
                                            regUserDetails.PostalCode = user.PostalCode.Trim();
                                            regUserDetails.State = user.State.Trim();
                                            regUserDetails.City = user.City.Trim();
                                            await _userManager.UpdateAsync(regUserDetails, user.Id);
                                            result = ResultMessages.success;
                                            SecurityQFailedCount1 = 0;
                                            result = ResultMessages.movenewpassword;
                                            userMail = user.Email;

                                        }
                                        else
                                        {
                                            if ((user.LockoutEnabled == false ||
                                                            (expireDate < System.DateTime.UtcNow)))
                                            {
                                                var result1 = await _userManager.AccessFailedAsync(user.Id);

                                                wrongFailCount = result1.Succeeded != true;
                                                SecurityQFailedCount1 = user.AccessFailedCount;
                                                result = ResultMessages.invalidans;
                                            }
                                            else
                                            {
                                                result = ResultMessages.lockout;
                                            }
                                        }
                                    }
                                }
                            }
                            else if ((user.IsActive == true) && (user.IsDelete == true))
                            {
                                // rolecount = rolecount;
                                result = ResultMessages.delete;
                            }
                            else if ((user.IsActive == false) && (user.IsDelete == false))
                            {
                                //rolecount = user.Roles.Count.ToString();
                                if (user.ActivationDate != null)
                                {
                                    bool chcekDate = GenaralConvertion.CheckExpireDate(user.ActivationDate.ToString(),
                                        System.DateTime.Now.ToString());
                                    if ((user.IsActive == false) && (user.IsDelete == false) && chcekDate == true)
                                    {
                                        await _userManager.UserDeleteAsync(user.Id);
                                        result = ResultMessages.reregister;
                                    }
                                    else
                                    {
                                        result = ResultMessages.inactive;
                                    }
                                }
                                else
                                {
                                    result = ResultMessages.inactive;
                                }

                                // result = getvalue == true ? ResultMessages.inactive : ResultMessages.reregister;
                            }

                        }
                    }
                    else
                    {
                        rolecount = "0";
                    }

                }

                // model.SecurityQuestion1







            }

            return Json(new { status = result.ToString(), code = userCode, userMail = userMail, UserId = userId, failCount = SecurityQFailedCount1, Rcount = rolecount });
        }

        [AllowAnonymous]
        [Route("ForgotValidationAdmin")]
        [HttpPost]
        public async Task<IHttpActionResult> ForgotValidationAdmin(ForgotPasswordViewModel model)
        {
            string result = string.Empty;
            string userId = string.Empty;
            string userMail = string.Empty;
            string userCode = string.Empty;
            string roleCount = string.Empty;
            if (ModelState.IsValid)
            {
                // var user = await _userManager.FindUserDetailsByIdAsync(Convert.ToInt32(model.UserName));FindByNameAsync
                var user = await _userManager.FindByNameAsync(model.UserName);
                roleCount = user.Roles.Count.ToString();
                if (roleCount == "1")
                {
                    return Json(new { RoleCount = roleCount });

                }

                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user.Id)))
                {
                    result = ResultMessages.NoUser;

                }

                if (user != null)
                {
                    bool getvalue = GenaralConvertion.CheckDateExpire(System.DateTime.Now.ToString(),
                          GenaralConvertion.ValidateGivenTime(user.ActivationDate.ToString()).ToString());

                    if ((user.IsActive == true) && (user.IsDelete == false))
                    {
                        DateTime expireDate = GenaralConvertion.ValidateGivenTime(user.LockoutEndDateUtc.ToString());
                        string h = System.DateTime.UtcNow.ToString();
                        if (((user.LockoutEnabled == true && (user.AccessFailedCount == 0 || user.AccessFailedCount == 5))) &&
                            (expireDate > System.DateTime.UtcNow))
                        {
                            result = ResultMessages.lockout;
                        }
                        else
                        {
                            var code = await _userManager.GeneratePasswordResetTokenAsync(user.Id);
                            userCode = code;
                            userId = user.Id.ToString();

                            AdminForgotPasswordMailSendNew(user.Id, code, user.Email.ToLower().Trim(), user.FirstName + " " + user.LastName);
                            var getUser = _userManager.FindUserDetailsByIdAsync(user.Id);
                            var regUserDetails = new AppUser();
                            regUserDetails.InjectFrom(getUser);
                            regUserDetails.IsForgot = true;
                            regUserDetails.UpdatedDate = System.DateTime.Now;
                            userMail = user.Email;
                            result = ResultMessages.success;
                        }
                    }
                    else if ((user.IsActive == true) && (user.IsDelete == true))
                    {
                        result = ResultMessages.delete;
                    }
                    else if ((user.IsActive == false) && (user.IsDelete == false))
                    {
                        result = getvalue == true ? ResultMessages.inactive : ResultMessages.reregister;
                    }
                }
            }
            return Json(new { status = result.ToString(), code = userCode, userMail = userMail.ToLower().Trim(), UserId = userId });
        }

        private void ForgotPasswordMailSend(string userId, string code, string email, string userFullName)
        {
            //  var templateFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EmailTemplates");

            var callBackUrl = new StringBuilder();
            callBackUrl.Append(ConfigurationManager.AppSettings["siteAddress"]);
            callBackUrl.Append("#");
            callBackUrl.Append("/getnewpassword?");
            callBackUrl.Append("userId=");
            callBackUrl.Append(userId);
            callBackUrl.Append("&code=");
            callBackUrl.Append(code);
            var boday = PopulateBody(userFullName, callBackUrl.ToString());
            var mailTemplateModel = new MailTemplateModel
            {
                UserId = userId,
                CustomApplicationId = userId,
                Type = "PASSWORD RESET",
                Subject = "PASSWORD RESET",
                MailSentFailCount = 0,
                MailContent = boday,
                IsMailSent = true
            };
            _MailTemplateService.InsertUpdateMailTemplate(mailTemplateModel);
            _userMail.MailSending("PASSWORD RESET", boday,
                 email.ToLower().Trim());
        }

        private void AdminForgotPasswordMailSendNew(string userId, string code, string email, string userFullName)
        {
            var callBackUrl = new StringBuilder();
            callBackUrl.Append(ConfigurationManager.AppSettings["siteAddress"]);
            callBackUrl.Append("Account");
            callBackUrl.Append("/ResetPassword?");
            callBackUrl.Append("userId=");
            callBackUrl.Append(userId);
            callBackUrl.Append("&code=");
            callBackUrl.Append(code);

            var boday = PopulateBody(userFullName.ToUpper(), callBackUrl.ToString());
            var mailTemplateModel = new MailTemplateModel
            {
                UserId = userId,
                CustomApplicationId = userId,
                Type = "FORGOT PASSWORD",
                Subject = "Forgot your password on My DC Business Center",
                MailSentFailCount = 0,
                MailContent = boday,
                IsMailSent = true
            };
            _MailTemplateService.InsertUpdateMailTemplate(mailTemplateModel);
            _userMail.MailSending("Forgot your password on My DC Business Center", boday,
                 email.ToLower().Trim());

            //var sb = new StringBuilder();
            //sb.Append("Hi " + userFullName);
            //sb.Append("Please reset your password by click the link:");
            //sb.Append("<a href=\"" + callBackUrl.ToString() + "\">link</a>");
            //_userMail.UserRegistartionMail("codeitindia@gmail.com", " ForgotPassword", sb.ToString(),
            //   email);
        }

        private void AdminForgotPasswordMailSend(string userId, string code, string email, string userFullName)
        {
            var callBackUrl = new StringBuilder();
            callBackUrl.Append(ConfigurationManager.AppSettings["siteAddress"]);
            callBackUrl.Append("Account");
            callBackUrl.Append("/ResetPassword?");
            callBackUrl.Append("userId=");
            callBackUrl.Append(userId);
            callBackUrl.Append("&code=");
            callBackUrl.Append(code);
            var sb = new StringBuilder();
            sb.Append("Hi " + userFullName);
            sb.Append("Please reset your password by click the link:");
            sb.Append("<a href=\"" + callBackUrl.ToString() + "\">link</a>");
            var mailTemplateModel = new MailTemplateModel
            {
                UserId = userId,
                CustomApplicationId = userId,
                Type = "FORGOTPASSWORD",
                Subject = "ForgotPassword",
                MailSentFailCount = 0,
                MailContent = sb.ToString(),
                IsMailSent = true
            };
            _MailTemplateService.InsertUpdateMailTemplate(mailTemplateModel);
            _userMail.MailSending("ForgotPassword", sb.ToString(),
               email.ToLower().Trim());
        }

        private string PopulateBody(string userName, string url)
        {

            var template = File.ReadAllText(HttpContext.Current.Server.MapPath("~/EmailTemplates/ForgotPassword.cshtml"));

            var viewModel = new ForgotPasswordModel
            {
                FullName = userName,
                PasswordLink = url

            };

            string body1;
#pragma warning disable 618
            body1 = Razor.Parse(template, viewModel);
#pragma warning restore 618


            //string body;
            //using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplates/ForgotPassword.html")))
            //{
            //    body = reader.ReadToEnd();
            //}
            //body = body.Replace("{UserName}", userName);
            //body = body.Replace("{Url}", url);

            return body1;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("ResendAdminPasswordMailAdmin")]
        public IHttpActionResult UserActivationResendMailAdmin(ReSendMail regUserDetails)
        {
            // var response = HttpContext.Current.Request.RequestContext.RouteData.Values["g-recaptcha-response"];
            ForgotPasswordMailSend(regUserDetails.UserId, regUserDetails.Code, regUserDetails.EmailAddress.ToLower().Trim(), regUserDetails.FullName);
            return Json(new { status = ResultMessages.success, mailid = regUserDetails.EmailAddress.ToLower().Trim(), userId = regUserDetails.UserId, code = regUserDetails.Code });
        }

        [Authorize]
        [HttpPost]
        [Route("ResendPasswordMail")]
        public IHttpActionResult UserActivationResendMail(ReSendMail regUserDetails)
        {
            // var response = HttpContext.Current.Request.RequestContext.RouteData.Values["g-recaptcha-response"];
            ForgotPasswordMailSend(regUserDetails.UserId, regUserDetails.Code, regUserDetails.EmailAddress.ToLower().Trim(), regUserDetails.FullName);
            return Json(new { status = ResultMessages.success, mailid = regUserDetails.EmailAddress.ToLower().Trim(), userId = regUserDetails.UserId, code = regUserDetails.Code });
        }

        /// <summary>
        /// It invokes ResetPasswordAsync in IUserManager, which based on the UserId
        /// It sends an Email.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [Route("ConfirmForgotPassword")]
        [HttpPost]
        public async Task<IHttpActionResult> ConfirmForgotPassword(ResetPasswordViewModel model)
        {
            string resultState;

            string userId = model.userId.ToString();
            var user = await _userManager.FindUserDetailsByIdAsync(userId);

            if (user.IsForgot == true)
            {
                var result = await _userManager.ResetPasswordAsync(userId, model.Code, model.Password);
                if (result.Succeeded)
                {
                    
                    resultState = ResultMessages.success;
                    var getuser = await _userManager.FindUserDetailsByIdAsync(userId);
                    _userPasswordTrackingService.InsertUserPasswordTracking(userId, getuser.PasswordHash);
                }
                else
                {
                    var result1 =
                        await
                    _userManager.ResetPasswordAsync(userId, model.Code + "==", model.Password);
                    resultState = result1.Succeeded ? ResultMessages.success : ResultMessages.error;
                    var userDetails = ToConvertUser.ToDataConvert(user);
                    var regUserDetails = new AppUser();
                    regUserDetails.InjectFrom(userDetails);
                    if (model.SelectedType == "S")
                    {

                        regUserDetails.IsForgot = user.IsForgot;
                        regUserDetails.UpdatedDate = user.UpdatedDate;
                    }
                    else
                    {

                        regUserDetails.IsForgot = false;
                        regUserDetails.UpdatedDate = System.DateTime.Now;
                    }


                    await _userManager.UpdateAsync(regUserDetails, user.Id);
                    resultState = ResultMessages.success;
                    var getuser = await _userManager.FindUserDetailsByIdAsync(userId);
                    _userPasswordTrackingService.InsertUserPasswordTracking(userId, getuser.PasswordHash);
                }
            }
            else
            {
                resultState = ResultMessages.passwordReseted;
            }
            return Json(new { status = resultState.ToString() });
        }

        /// <summary>
        /// It invokes ResetPasswordAsync in IUserManager, which based on the UserId
        /// It sends an Email.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        [Authorize]
        [Route("ForgetPasswordCheck")]
        [HttpPost]
        public async Task<IHttpActionResult> ForgetPasswordCheck(ResetPasswordViewModel model)
        {
            string resultState = string.Empty;
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            string userId = model.userId.ToString();
            var user = await _userManager.FindUserDetailsByIdAsync(userId);
            bool getvalue = GenaralConvertion.CheckDateExpire(GenaralConvertion.ValidateGivenTime(user.UpdatedDate.ToString()).ToString(),
                System.DateTime.Now.ToString());
            if (model.SelectedType == "S")
            {
                getvalue = true;
            }

            if (!getvalue && user.IsForgot == true)
            {
                resultState = ResultMessages.linkExpire;
            }
            else if (user.LockoutEndDateUtc != null)
            {
                bool chcekDate = GenaralConvertion.CheckDateExpireForLockout(
                       System.DateTime.UtcNow.ToString(), user.LockoutEndDateUtc.ToString());
                if (chcekDate == true && user.LockoutEnabled == true)
                {
                    //  await LoginUpdate(user, user.Id);
                    resultState = ResultMessages.lockout;
                }
                else
                {
                    resultState = user.IsForgot == true ? ResultMessages.success : ResultMessages.passwordReseted;
                }
            }

            else
            {
                resultState = user.IsForgot == true ? ResultMessages.success : ResultMessages.passwordReseted;
            }



            return Json(new { status = resultState.ToString() });
        }


        [Authorize]
        [Route("TraceTimeout")]
        [HttpPost]
        public String TraceTimeout()
        {
            try
            {
                var date = DateTime.Now;
                System.IO.File.WriteAllText(HttpContext.Current.Server.MapPath("~/App_Data/logtimeout.txt"), "Hours:"+ date.Hour + " Minutes:" + date.Minute + " Seconds:" + date.Second);
                return HttpContext.Current.Server.MapPath("~/App_Data/logtimeout.txt");
            }
            catch (Exception )
            {
               
                return "";
            }

        }

        [Authorize]
        [HttpPost]
        [Route("ValidatePassword")]
        [DeflateCompression]
        public async Task<IHttpActionResult> ValidatePassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            var result = _userPasswordTrackingService.PasswordStatus(resetPasswordViewModel.userId, resetPasswordViewModel.Password);
            return await Task.FromResult(Json(result));
        }
    }
}