using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web;
//using 
//using System.Web.Mvc;
using BusinessCenter.Api.App_Start;
using BusinessCenter.Api.Common;
using BusinessCenter.Api.Models;
using BusinessCenter.Email;
using BusinessCenter.Identity.IdentityExtension;
using BusinessCenter.Identity.IdentityModels;
using BusinessCenter.Identity.Interfaces;
using BusinessCenter.Service.Interface;
using BussinessCenter.reCaptcha;
using Omu.ValueInjecter;
using System.Web.Http;
using BusinessCenter.Api.Filters;
using BusinessCenter.Api.Utility;
using BusinessCenter.Data.Model;
namespace BusinessCenter.Api.Controllers
{
    /// <summary>
    ///     Api Controller which contains the information of Registration
    /// </summary>
    [RoutePrefix("api/UserAccounts")]
    public class UserAccountsController : BaseApiController
    {
        private readonly IReCaptcha _googleReCaptcha;
        private readonly ISecurityQuestionsService _securityQuestions;
        private readonly IEmailTemplate _userMail;
        private readonly IUserManager _userManager;
        private String _reCaptchaResult;
        private readonly IUserPasswordTrackingService _userPasswordTrackingService;
        private readonly IMailTemplateService _MailTemplateService;
        public UserAccountsController(IUserManager userManager, IEmailTemplate userMail,
            IReCaptcha googleReCaptcha, ISecurityQuestionsService service, IUserPasswordTrackingService userPasswordTrackingService, IMailTemplateService mailTemplateService)
        {
            _googleReCaptcha = googleReCaptcha;
            _userManager = userManager;
            _userMail = userMail;
            _securityQuestions = service;
            _userPasswordTrackingService = userPasswordTrackingService;
            _MailTemplateService = mailTemplateService;
        }


      



        /// <summary>
        ///     This Action is used to create a user by taking Register view model as parameter.
        /// </summary>
        /// <param name="createUserModel">Object of RegisterUserModel which contains registration information</param>
        /// <returns>Returns ActionResult object</returns>
        //[Authorize]
        [HttpPost]
        [Route("create")]
      //  [ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> CreateUser(RegisterUserModel createUserModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string resultData;
            string emailAddress;
            string exception;
            string activationCode;
            string fullName;
            string userId = string.Empty ;

            var regUserDetails = new AppUser();
            regUserDetails.InjectFrom(createUserModel);
            regUserDetails.CreatedDate = DateTime.Now;
            regUserDetails.IsActive = false;
            regUserDetails.EmailConfirmed = true;
            regUserDetails.Id = Guid.NewGuid().ToString();
              //  appUser.Id = Guid.NewGuid().ToString();
            if (createUserModel.Title == "")
            {
                regUserDetails.Title = "Mr.";
            }

            string[] selectedRoles = { "Employee" };
            var status =
                await _userManager.UserRegistartion(regUserDetails, createUserModel.Password, selectedRoles);

            switch (status)
            {
                case RegistartionStatus.Success:
                    
                    UserMailSend(regUserDetails.Id, regUserDetails.Email.ToLower().Trim(), regUserDetails.ActivationCode, regUserDetails.FirstName + " " + regUserDetails.LastName);
                    resultData = ResultMessages.success;
                    emailAddress = regUserDetails.Email;
                    userId = regUserDetails.Id;
                    activationCode = regUserDetails.ActivationCode;
                    exception = string.Empty;
                    fullName = regUserDetails.FirstName + " " + regUserDetails.LastName;
                    break;
                case RegistartionStatus.Exists:

                    resultData = ResultMessages.resultstate;
                    emailAddress = string.Empty;
                    userId = string.Empty;
                    exception = ResultMessages.resultstate;
                    activationCode = string.Empty;
                    fullName = string.Empty;
                    break;
                case RegistartionStatus.Failure:
                    resultData = ResultMessages.email;
                    emailAddress = string.Empty;
                    userId = string.Empty;
                    activationCode = string.Empty;
                    exception = ResultMessages.emailresultstate;
                    fullName = string.Empty;
                    break;

                default:
                    resultData = status.ToString();
                    emailAddress = string.Empty;
                    userId = string.Empty;
                    activationCode = string.Empty;
                    exception = string.Empty;
                    fullName = string.Empty;
                    break;
            }


            return Json(new { status = resultData, mailid = emailAddress.ToLower().Trim(), userId, code = activationCode, exception, FullName = fullName });
        }

        /// <summary>
        ///     This method Sends an Email to the User Email Account Based on the UserId and EmailAddress
        ///     provided by the User.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userEmail"></param>
        /// <param name="activationCode"></param>
        /// <param name="userFullName"></param>
        private void UserMailSend(string userId, string userEmail, string activationCode, string userFullName)
        {
            var callBackUrl = new StringBuilder();
            //TODO:Remove Configuration setting and use current domain
            callBackUrl.Append(ConfigurationManager.AppSettings["siteAddress"]);
            callBackUrl.Append("#");
            callBackUrl.Append("/emailverified?");
            callBackUrl.Append("userId=");
            callBackUrl.Append(userId);
            callBackUrl.Append("&code=");
            callBackUrl.Append(activationCode);
            var body = LoginEmailBody(userFullName, callBackUrl.ToString());
            var mailTemplateModel = new MailTemplateModel
            {
                UserId = userId,
                CustomApplicationId = userId,
                Type = "CONFIRM ACCOUNT",
                Subject = "Welcome to My DC Business Center. Please confirm your account",
                MailSentFailCount=0,
                MailContent=body,
                IsMailSent=true
            };
            _MailTemplateService.InsertUpdateMailTemplate(mailTemplateModel);
            _userMail.MailSending("Welcome to My DC Business Center. Please confirm your account", body,
                userEmail.ToLower().Trim());
        }

        //TODO:Update the approach to use razor engine
        private string LoginEmailBody(string userName, string url)
        {
            string body;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplates/UserRegistration.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", userName ?? "");
            body = body.Replace("{Url}", url);

            return body;
        }

        /// <summary>
        ///     After User registration. User Email can be verified by sending an Email to the User
        ///     This method below is used to Resend an Email for Verifying
        /// </summary>
        /// <param name="regUserDetails"></param>
        /// <returns></returns>
       [Authorize]
        [HttpPost]
        [Route("ResendMail")]
        public IHttpActionResult UserActivationResendMail(ReSendMail regUserDetails)
        {
            // var response = HttpContext.Current.Request.RequestContext.RouteData.Values["g-recaptcha-response"];
            UserMailSend(regUserDetails.UserId, regUserDetails.EmailAddress.ToLower().Trim(), regUserDetails.Code, regUserDetails.FullName);
            return
                Json(
                    new
                    {
                        status = "success",
                        mailid = regUserDetails.EmailAddress,
                        userId = regUserDetails.UserId,
                        code = regUserDetails.Code,
                        fullName = regUserDetails.FullName
                    });
        }

        /// <summary>
        ///     It gets the Security token and Key from GoogleApi and It Generates ReCaptcha numbers which Gets
        ///     expired for every 5 Minutes by Default and Even if the User TypeIn recaptcha It will get expired
        /// </summary>
        /// <param name="userVaidate"></param>
        /// <returns></returns>
        //[Authorize]
        [AllowAnonymous]
        [HttpPost]
        [Route("UserReCaptcha")]
        public IHttpActionResult UserReCaptcha(ReCapchaResponse userVaidate)
        {
            // var response = HttpContext.Current.Request.RequestContext.RouteData.Values["g-recaptcha-response"];
            _reCaptchaResult = _googleReCaptcha.ReCaptchaValidation(
                ConfigurationManager.AppSettings["googleSecretToken"], userVaidate.responseValue);
            if (_reCaptchaResult == "true")
            {
                return Json(new { status = ResultMessages.success });
            }
            return NotFound();
        }

        /// <summary>
        ///     This Method Checks If the User available or not by taking UserCheckViewModel as Parameter
        /// </summary>
        /// <param name="userVaidate"></param>
        /// <returns></returns>
       [AllowAnonymous]
        [Route("CheckUser")]
        public IHttpActionResult CheckUserAvailable(UserCheckViewModel userVaidate)
        {
            var result = _userManager.CheckUserAvailableAsync(userVaidate.UserName);
            var final = true;
            if (result.Result)
            {
                final = true;
            }
            else
            {
                final = false;
            }

            return Json(new { status = final.ToString() });
        }

        /// <summary>
        ///     It gets all available Security Questions from the Database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("Questions")]
      //  [ValidateAntiForgeryToken]
        [DeflateCompression]
        public async Task<IHttpActionResult> GetSecurityQuestions()
        {
           var securityQuestions = _securityQuestions.GetAll();
          //return   Json(securityQuestions);
          if (securityQuestions == null)
          {
              return await Task.FromResult(NotFound());
          }
          return Ok(securityQuestions);
        }

        /// <summary>
        ///     It Enables SuperAdmin or Admin users to Delete a user by taking UserAccountById as Parameters
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("delete")]
        [Authorize]
        public async Task<IHttpActionResult> DeleteUser(UserAccountById userInfo)
        {
            string userId = string.Empty;
            userId = userInfo.UserId;
            //Only SuperAdmin or Admin can delete users (Later when implement roles)
            var resultMessage = string.Empty;
            var appUser = await _userManager.FindUserDetailsByIdAsync(userId);

            if (appUser != null)
            {
                var userDetails = ToConvertUser.ToDataConvert(appUser);
                var userUpdateProfile = new AppUser();
                userUpdateProfile.InjectFrom(userDetails);
                userUpdateProfile.IsDelete = true;
                userUpdateProfile.DeleteComment = userInfo.DeleteComment;
                userUpdateProfile.CreatedDate = appUser.CreatedDate;
                userUpdateProfile.UpdatedDate = Convert.ToDateTime(System.DateTime.Now);
                await _userManager.UpdateAsync(userUpdateProfile, appUser.Id);

                // return Json(new {status = "Success"});
                return Json(new { status = ResultMessages.success });
            }

            return NotFound();
        }


        /// <summary>
        ///     It Enables SuperAdmin or Admin users to lock a user by taking UserAccountById as Parameters
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("lock")]
        [Authorize]
        public async Task<IHttpActionResult> LockUser(UserAccountById userInfo)
        {
        
            //Only SuperAdmin or Admin can lock users (Later when implement roles)
            var resultMessage = string.Empty;
            var appUser = await _userManager.FindUserDetailsByIdAsync(userInfo.UserId);

            if (appUser != null)
            {
                var userDetails = ToConvertUser.ToDataConvert(appUser);
                var userUpdateProfile = new AppUser();
                userUpdateProfile.InjectFrom(userDetails);
                userUpdateProfile.LockoutEnabled = false;
                userUpdateProfile.IsDelete = false;
                userUpdateProfile.CreatedDate = appUser.CreatedDate;

                await _userManager.UpdateAsync(userUpdateProfile, appUser.Id);

                // return Json(new { status = "Success" });
                return Json(new { status = ResultMessages.success });
            }

            return NotFound();
        }

        /// <summary>
        ///     If the User is in InActive states then it Confirms the Email if it is Exactly in 24hours
        ///     If not User gets deleted If the validation is done after 24hours or before 24hours
        ///     If the User is already Active then It displays a Message that User is already active
        /// </summary>
        /// <param name="verifyEmail"></param>
        /// <returns></returns>
       //[Authorize]
        [HttpPost]
        [Route("ConfirmEmail")]
        public async Task<IHttpActionResult> ConfirmEmail(EmailVerification verifyEmail)
        {
            if (string.IsNullOrWhiteSpace(verifyEmail.userId) || string.IsNullOrWhiteSpace(verifyEmail.code))
            {
                ModelState.AddModelError("", "User Id and Code are required");
                return BadRequest(ModelState);
            }


            var resultMessage = string.Empty;
            var user = await _userManager.FindUserDetailsByIdAsync(verifyEmail.userId);

            if (user != null)
            {
                var validateUser = new AppUser
                {
                    IsActive = user.IsActive,
                    ActivationDate = user.ActivationDate
                };
                if (validateUser.IsActive)
                {
                    resultMessage = ResultMessages.useractive;
                }
                else
                {
                    var getvalue = GenaralConvertion.CheckDateExpire(DateTime.Now.ToString(),
                      GenaralConvertion.ValidateGivenTime(validateUser.ActivationDate.ToString()).ToString());


                    if (getvalue)
                    {
                        var result =
                            await
                                _userManager.UserMailConfirmation(verifyEmail.userId, verifyEmail.code);
                        if (result.Succeeded)
                        {
                            var regUserDetails = new AppUser();
                            regUserDetails.InjectFrom(user);
                            regUserDetails.IsActive = true;
                            await _userManager.UpdateAsync(regUserDetails,verifyEmail.userId);
                            //  resultMessage = "Success";
                            resultMessage = ResultMessages.success;
                        }
                        else
                        {
                            var result1 =
                                await
                                    _userManager.UserMailConfirmation(verifyEmail.userId,
                                        verifyEmail.code + "==");
                            if (result1.Succeeded)
                            {
                                var regUserDetails = new AppUser();
                                regUserDetails.InjectFrom(user);
                                regUserDetails.IsActive = true;
                                await _userManager.UpdateAsync(regUserDetails, verifyEmail.userId);
                                // resultMessage = "Success";
                                resultMessage = ResultMessages.success;
                            }
                        }
                    }
                    else
                    {
                        await _userManager.UserDeleteAsync(verifyEmail.userId);


                        resultMessage = ResultMessages.accountexpired;
                    }
                }
            }
            return Json(new { status = resultMessage });
        }

        /// <summary>
        ///     ONly User Profile can be Updated or
        ///     Only Old Password can be updated with new password by taking the Old Password as parameter or
        ///     Both Profile and Old Password can be updated at once by taking Old Password as Parameter
        /// </summary>
        /// <param name="profileUpdate"></param>
        /// <returns></returns>
       [Authorize]
        [Route("ProfileUpdate")]
        public async Task<IHttpActionResult> UserProfileUpdate(UserProfileUpdate profileUpdate)
        {
            var changePassword = false;
            var validateOldPassword = true;
            string emailChange = string.Empty;

            var user = await _userManager.FindUserDetailsByIdAsync(profileUpdate.UserId);


            var userDetails = ToConvertUser.ToDataConvert(user);
            var userUpdateProfile = new AppUser();
            userUpdateProfile.InjectFrom(userDetails);
            userUpdateProfile.FirstName = profileUpdate.FirstName;
            userUpdateProfile.LastName = profileUpdate.LastName;
            userUpdateProfile.Email = userUpdateProfile.Email;
            if (profileUpdate.Password != null)
            {
                profileUpdate.Password = profileUpdate.Password.Trim();
            }
            //   
           //userUpdateProfile.SecondaryEmail = profileUpdate.SecondaryEmail;
            if (profileUpdate.Email.ToString().ToUpper() == userUpdateProfile.Email.ToString().ToUpper())
            { userUpdateProfile.SecondaryEmail = string.Empty; }
            else
            {
                userUpdateProfile.SecondaryEmail = profileUpdate.Email;
            }
            userUpdateProfile.SecurityQuestion1 = profileUpdate.SecurityQuestion1;
            userUpdateProfile.SecurityQuestion2 = profileUpdate.SecurityQuestion2;
            userUpdateProfile.SecurityQuestion3 = profileUpdate.SecurityQuestion3;
            userUpdateProfile.SecurityAnswer1 = profileUpdate.SecurityAnswer1;
            userUpdateProfile.SecurityAnswer2 = profileUpdate.SecurityAnswer2;
            userUpdateProfile.SecurityAnswer3 = profileUpdate.SecurityAnswer3;
            userUpdateProfile.CreatedDate = userDetails.CreatedDate;
            userUpdateProfile.PreviousEmailConfirmed = true;
            userUpdateProfile.ChangeEmailConfirmed = false;
            userUpdateProfile.PreviousEmailValidate = DateTime.Now;
            userUpdateProfile.ChangeEmailValidate = DateTime.Now;

            var type = string.Empty;
            profileUpdate.Password = profileUpdate.Password == null ? "" : profileUpdate.Password;
            if (profileUpdate.Password != "")
            {
                validateOldPassword = await _userManager.IsCheckPasswordAsync(user, profileUpdate.Password);
            }


            if (validateOldPassword)
            {
                if ((profileUpdate.Password != null) && profileUpdate.NewPassword != null)
                {
                    if (((profileUpdate.Password != profileUpdate.NewPassword) ||
                         (profileUpdate.Password == profileUpdate.NewPassword))
                        && (profileUpdate.NewPassword != string.Empty))
                    {
                        //userUpdateProfile.PasswordHash = profileUpdate.NewPassword;
                        await
                            ProfilePasswordUpdate(profileUpdate.UserId, profileUpdate.Password,
                                profileUpdate.NewPassword);


                        var getuser = await _userManager.FindUserDetailsByIdAsync(profileUpdate.UserId);
                        _userPasswordTrackingService.InsertUserPasswordTracking(profileUpdate.UserId, getuser.PasswordHash);
                        changePassword = true;
                    }
                }
                var final = true;
                if (user.Email == profileUpdate.Email)
                {
                    final = true;
                }
                else
                {
                    var result1 = await _userManager.CheckUserEmailAvailableAsync(profileUpdate.Email);
                    final = result1 == true;
                }


                if (final == true)
                {
                    var result = await _userManager.UpdateAsync(userUpdateProfile, profileUpdate.UserId);
                    if (!result.Succeeded)
                    {
                        return GetErrorResult(result);
                    }
                    if (user.Email == profileUpdate.Email)
                    {

                    }
                    else
                    {
                      //  type = user.Email;
                    //    SendMailTolUser(user.Id, user.Email, profileUpdate.Email, type, "P", user.FirstName + " " + user.LastName, "CHANGE REGISTRATION EMAIL");
                        type = profileUpdate.Email;
                        SendMailTolUser(user.Id, user.Email, profileUpdate.Email, type, "N", user.FirstName + " " + user.LastName, "CHANGE REGISTRATION EMAIL");
                        emailChange = ResultMessages.emailchange;

                    }
                }
                else
                {
                    return Json(new { status = ResultMessages.emailresultstate, FirstName = userUpdateProfile.FirstName, LastName = userUpdateProfile.LastName });
                }
            }
            else
                // return Json(new { status = "success", logout = "logout" });
                return Json(new { status = "InvalidOldPassword", FirstName = userUpdateProfile.FirstName, LastName = userUpdateProfile.LastName });

            if (changePassword)
                return Json(new { status = ResultMessages.success, logout = "logout", FirstName = userUpdateProfile.FirstName, LastName = userUpdateProfile.LastName });

            return Json(new { status = ResultMessages.success, Email = emailChange, FirstName = userUpdateProfile.FirstName, LastName = userUpdateProfile.LastName });
        }

        [Authorize]
        [AllowAnonymous]
        [HttpPost]
        [Route("UserPreviousEmail")]
        public async Task<IHttpActionResult> UserPreviousEmailUpdate(UserMailAddressUpdate userPreviousEmail)
        {
            string returnResult;
            var user = await _userManager.FindUserDetailsByIdAsync(userPreviousEmail.UserId);
            userPreviousEmail.UserId = userPreviousEmail.UserId.Trim();
            if (userPreviousEmail.Type == "P")
            {
                if (user.PreviousEmailConfirmed != true)
                {
                    var userDetails = ToConvertUser.ToDataConvert(user);
                    var userUpdateProfile = new AppUser();
                    userUpdateProfile.InjectFrom(userDetails);
                    var expireDate = GenaralConvertion.ValidateGivenTime(user.PreviousEmailValidate.ToString());
                    var getvalue = GenaralConvertion.CheckDateExpire(DateTime.Now.ToString(),
                     expireDate.AddDays(1).ToString());
                    if ((user.PreviousEmailConfirmed == false) && (getvalue == true))
                    {
                        userUpdateProfile.PreviousEmailConfirmed = true;
                        userUpdateProfile.PreviousEmailValidate = DateTime.Now;
                        userUpdateProfile.ChangeEmailConfirmed = user.ChangeEmailConfirmed;
                        userUpdateProfile.ChangeEmailValidate = user.ChangeEmailValidate;
                        if (user.ChangeEmailConfirmed)
                        {
                            userUpdateProfile.Email = userPreviousEmail.NewEmail;
                           
                        }
                        var result =
                            await _userManager.UpdateAsync(userUpdateProfile, userPreviousEmail.UserId);
                        ;
                        // returnResult = "Success";
                        returnResult = ResultMessages.success.Trim();
                    }
                    else
                    {
                        returnResult = ResultMessages.linkExpire;

                    }

                }
                else
                {
                    // returnResult = "alreadyExists";
                    returnResult = ResultMessages.alreadyExistsEmail;
                }
            }
            else
            {
                if (user.ChangeEmailConfirmed != true)
                {
                    var userDetails = ToConvertUser.ToDataConvert(user);
                    var userUpdateProfile = new AppUser();
                    userUpdateProfile.InjectFrom(userDetails);
                    DateTime expireDate = GenaralConvertion.ValidateGivenTime(user.ChangeEmailValidate.ToString());
                    var getvalue = GenaralConvertion.CheckDateExpire(DateTime.Now.ToString(),
                     expireDate.AddDays(1).ToString());
                    if ((user.ChangeEmailConfirmed == false) && (getvalue == true))
                    {
                        userUpdateProfile.ChangeEmailConfirmed = true;
                        userUpdateProfile.ChangeEmailValidate = DateTime.Now;
                        userUpdateProfile.PreviousEmailConfirmed = user.PreviousEmailConfirmed;
                        userUpdateProfile.PreviousEmailValidate = user.PreviousEmailValidate;
                        if (user.PreviousEmailConfirmed)
                        {
                            userUpdateProfile.Email = userPreviousEmail.NewEmail;
                            userUpdateProfile.SecondaryEmail = string.Empty;
                        }
                        var result =
                            await _userManager.UpdateAsync(userUpdateProfile, userPreviousEmail.UserId);
                        // returnResult = "Success";
                        returnResult = ResultMessages.success.Trim();
                    }
                    else
                    {
                       
                            userUpdateProfile.SecondaryEmail = string.Empty;
                      
                        var result =
                            await _userManager.UpdateAsync(userUpdateProfile, userPreviousEmail.UserId);
                        returnResult = ResultMessages.linkExpire;
                    }

                }
                else
                {
                    //  returnResult = "alreadyExists";
                    returnResult = ResultMessages.alreadyExistsEmail;
                }
            }
            return Json(new { status = returnResult.Trim(), type = userPreviousEmail.Type });
        }

        [Authorize]
        [AllowAnonymous]
        [HttpPost]
        [Route("UserNewEmail")]
        public async Task<IHttpActionResult> UserNewEmailUpdate(UserMailAddressUpdate userNewEmail)
        {
            var user = await _userManager.FindUserDetailsByIdAsync(userNewEmail.UserId);

            userNewEmail.UserId = userNewEmail.UserId.Trim();
            var userDetails = ToConvertUser.ToDataConvert(user);
            var userUpdateProfile = new AppUser();
            userUpdateProfile.InjectFrom(userDetails);
            if (user.ChangeEmailConfirmed == false)
            {
                userUpdateProfile.PreviousEmailConfirmed = true;
                userUpdateProfile.PreviousEmailValidate = DateTime.Now;
            }
            if (user.PreviousEmailConfirmed)
            {
                userUpdateProfile.Email = userNewEmail.NewEmail;
            }
            var result = await _userManager.UpdateAsync(userUpdateProfile, userNewEmail.UserId);
            return Json(new { status = ResultMessages.success });
        }

        private void SendMailTolUser(string userId, string userEmail, string newEmail, string type, string emailType, string userFullName, string heading)
        {
            var callBackUrl = new StringBuilder();
            callBackUrl.Append(ConfigurationManager.AppSettings["siteAddress"]);
            callBackUrl.Append("#");
            callBackUrl.Append("/PreviousEmailVerification?");
            callBackUrl.Append("userId=");
            callBackUrl.Append(userId);
            callBackUrl.Append("&userEmail=");
            callBackUrl.Append(userEmail);
            callBackUrl.Append("&newEmail=");
            callBackUrl.Append(newEmail);
            callBackUrl.Append("&Type=");
            callBackUrl.Append(emailType);
            string emailBody = string.Empty;
            if (emailType == "P")
            {
                string description =
                    "Also, confirm the change by clicking the link sent to your New Primary email account if not done yet. ";
                emailBody = ProfileEmailBody(userFullName, callBackUrl.ToString(), ConfigurationManager.AppSettings["siteAddress"].ToString(), heading, description);
            }
            else
            {
                string description1 =
                    "Also, confirm the change by clicking the link sent to your Old Primary email account if not done yet ";
                emailBody = ProfileEmailBody(userFullName, callBackUrl.ToString(), ConfigurationManager.AppSettings["siteAddress"].ToString(), heading, description1);
            }

            var mailTemplateModel = new MailTemplateModel
            {
                UserId = userId,
                CustomApplicationId = userId,
                Type = "REGISTRATION EMAIL",
                Subject = "Change Registration Email",
                MailSentFailCount = 0,
                MailContent = emailBody.ToString().Trim(),
                IsMailSent = true
            };
            _MailTemplateService.InsertUpdateMailTemplate(mailTemplateModel);
            //var sb = new StringBuilder();
            //sb.Append("Please click on the following link to confirm changing your primary email address of My DCBC Account.");
            //sb.Append("<a href=\"" + callBackUrl + "\">link</a>");
            _userMail.MailSending("Change Registration Email", emailBody.ToString(), type);
        }

        private string ProfileEmailBody(string fullName, string url, string siteAddress, string heading, string description)
        {
            string body;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplates/PrimaryEmailChange.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", fullName);
            body = body.Replace("{Url}", url);
            body = body.Replace("{mySite}", siteAddress);
            body = body.Replace("{Heading}", heading);
            body = body.Replace("{Description}", description);
            return body;
        }

        /// <summary>
        ///     Used to change the Profile Password by taking Old password and UserId as Parameters
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public async Task<bool> ProfilePasswordUpdate(string userId, string oldPassword, string newPassword)
        {
            var result = true;
            var reseult = await _userManager.ChangePasswordAsync(userId,
                oldPassword, newPassword);

            if (!reseult.Succeeded)
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        ///     It resets the Old Password with the NewPassword by taking ChangePassword as Parameter
        /// </summary>
        /// <param name="resetPassword"></param>
        /// <returns></returns>
        [Authorize]
        [AllowAnonymous]
        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IHttpActionResult> ResetPassword(ChangePassword resetPassword)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var reseult = await _userManager.ChangePasswordAsync(resetPassword.userId,
                resetPassword.OldPassword, resetPassword.Password);

            if (!reseult.Succeeded)
            {
                return GetErrorResult(reseult);
            }

            return Ok();
        }

        /// <summary>
        ///     Get the UserDetails in to a List based on the UserId which is passed as Parameters
        /// </summary>
        /// <param name="userDetail"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("UserDetails")]
        public IHttpActionResult UserDetails(UserAccountById userDetail)
        {
            var userDetails = _userManager.FindUserDetailsByIdAsync(userDetail.UserId);


            if (userDetails == null)
            {
                return NotFound();
            }
            return Json(new { userDetails });
        }

        //Created by Srinivas
        [Authorize]
        [AllowAnonymous]
        [Route("checkemail")]
        public async Task<IHttpActionResult> CheckUserEmailAvailable(ForgotViewModel registermodel)
        {
            var result = await _userManager.CheckUserEmailAvailableAsync(registermodel.Email);
            var final = true;
            if (result == true)
            {
                int userid = 0;
              
                if (await _userManager.CheckUserSecondaryEmailAvailableAsync(registermodel.Email, userid.ToString()))
                { final = true; }
                else
                {

                    final = false;
                }
              //  final = true;
            }
            else
            {
               
                    final = false;
               
            }

            return Json(new { status = final.ToString() });
        }
        //changed by rupendra 237-2015
        // check with secondary emailexist or not
        [Authorize]
        [AllowAnonymous]
        [Route("CheckUserEmailProfile")]
        public async Task<IHttpActionResult> CheckUserEmailProfile(UserPrimaryEmailValidation registermodel)
        {
            var final = true;
          //  var userDetails = await _userManager.FindUserDetailsByIdAsync(registermodel.UserId);

            var result = await _userManager.CheckUserEmailAvailableAsync(registermodel.Email);
            if (result == true)
            {
                if (await _userManager.CheckUserSecondaryEmailAvailableAsync(registermodel.Email, registermodel.UserId))
                { final = true; }
                else
                {

                    final = false;
                }
            }
            else
            {
                //if (!await _userManager.CheckUserSecondaryEmailAvailableAsync(registermodel.Email, registermodel.UserId))
                //{ final = true; }
                //else
                //{

                    final = false;
              //  }
            }




            return Json(new { status = final.ToString() });
        }

        [Authorize]
        [AllowAnonymous]
        [Route("ValidatePassword")]
        public async Task<IHttpActionResult> ValidatePassword(PasswordCheckViewModel profileUpdate)
        {
            var validateOldPassword = false;
            var user = await _userManager.FindUserDetailsByIdAsync(profileUpdate.userId);
            validateOldPassword = await _userManager.IsCheckPasswordAsync(user, profileUpdate.Password);
            var final = true;
            if (validateOldPassword)
            {
                final = false;
            }
            else
            {
                final = true;
            }


            return Json(new { status = final.ToString() });
        }

        /// <summary>
        ///     ONly User Profile can be Updated or
        ///     Only Old Password can be updated with new password by taking the Old Password as parameter or
        ///     Both Profile and Old Password can be updated at once by taking Old Password as Parameter
        /// </summary>
        /// <param name="profileUpdate"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("AdminProfileUpdate")]
        public async Task<IHttpActionResult> AdminProfileUpdate(UserProfileUpdate profileUpdate)
        {
            var changePassword = false;
            var validateOldPassword = true;
            string emailChange = string.Empty;

            var user = await _userManager.FindUserDetailsByIdAsync(profileUpdate.UserId);


            var userDetails = ToConvertUser.ToDataConvert(user);
            var userUpdateProfile = new AppUser();
            userUpdateProfile.InjectFrom(userDetails);
            userUpdateProfile.FirstName = profileUpdate.FirstName;
            userUpdateProfile.LastName = profileUpdate.LastName;
            userUpdateProfile.Email = userUpdateProfile.Email;
            userUpdateProfile.SecondaryEmail = profileUpdate.SecondaryEmail;
            userUpdateProfile.SecurityQuestion1 = profileUpdate.SecurityQuestion1;
            userUpdateProfile.SecurityQuestion2 = profileUpdate.SecurityQuestion2;
            userUpdateProfile.SecurityQuestion3 = profileUpdate.SecurityQuestion3;
            userUpdateProfile.SecurityAnswer1 = profileUpdate.SecurityAnswer1;
            userUpdateProfile.SecurityAnswer2 = profileUpdate.SecurityAnswer2;
            userUpdateProfile.SecurityAnswer3 = profileUpdate.SecurityAnswer3;
            userUpdateProfile.CreatedDate = userDetails.CreatedDate;
            userUpdateProfile.PreviousEmailConfirmed = false;
            userUpdateProfile.ChangeEmailConfirmed = false;
            userUpdateProfile.PreviousEmailValidate = DateTime.Now;
            userUpdateProfile.ChangeEmailValidate = DateTime.Now;

            userUpdateProfile.Address = profileUpdate.Address;
            userUpdateProfile.City = profileUpdate.City;
            userUpdateProfile.State = profileUpdate.State;
            userUpdateProfile.PostalCode = profileUpdate.PostalCode;
            userUpdateProfile.MobileNumber = profileUpdate.MobileNumber;
            userUpdateProfile.IsDelete = profileUpdate.IsDelete;
            var type = string.Empty;
            if (profileUpdate.Password != null)
            {
                validateOldPassword = await _userManager.IsCheckPasswordAsync(user, profileUpdate.Password);
            }


            if (validateOldPassword)
            {
                if ((profileUpdate.Password != null) && profileUpdate.NewPassword != null)
                {
                    if (((profileUpdate.Password != profileUpdate.NewPassword) ||
                         (profileUpdate.Password == profileUpdate.NewPassword))
                        && (profileUpdate.NewPassword != string.Empty))
                    {
                        //userUpdateProfile.PasswordHash = profileUpdate.NewPassword;
                        await
                            ProfilePasswordUpdate(profileUpdate.UserId, profileUpdate.Password,
                                profileUpdate.NewPassword);
                        changePassword = true;
                    }
                }
                var result = await _userManager.UpdateAsync(userUpdateProfile, profileUpdate.UserId);
                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }
                if (user.Email == profileUpdate.Email)
                {
                }
                else
                {
                    type = user.Email;
                    SendMailTolUser(user.Id, user.Email, profileUpdate.Email.ToLower().Trim(), type, "P", user.FirstName + " " + user.LastName, "CHANGE REGISTRATION EMAIL");
                    type = profileUpdate.Email;
                    SendMailTolUser(user.Id, user.Email, profileUpdate.Email.ToLower().Trim(), type, "N", user.FirstName + " " + user.LastName, "CHANGE REGISTRATION EMAIL");
                    emailChange = ResultMessages.emailchange;

                }
            }
            else
                // return Json(new { status = "success", logout = "logout" });
                return Json(new { status = "InvalidOldPassword" });

            if (changePassword)
                return Json(new { status = ResultMessages.success, logout = ResultMessages.logout });

            return Json(new { status = ResultMessages.success, Email = ResultMessages.emailchange });
        }
    }
}