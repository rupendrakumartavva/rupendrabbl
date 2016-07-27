using BusinessCenter.Admin.Common;
using BusinessCenter.Admin.Filters;
using BusinessCenter.Admin.Models;
using BusinessCenter.Audits;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using BusinessCenter.Email;
using BusinessCenter.Identity.IdentityExtension;
using BusinessCenter.Identity.IdentityModels;
using BusinessCenter.Identity.Interfaces;
using BusinessCenter.Service.Interface;
using Omu.ValueInjecter;
using PagedList;
using RazorEngine;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
//using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BusinessCenter.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserManager _userManager;
        private readonly IEmailTemplate _userMail;
        private readonly IUserManagerService _userManagerDbService;
        private readonly ISecurityQuestionsService _securityQuestions;
        private readonly ISearchKeywordService _keywordCount;
        public int LoginFailedAttems = 0;
        public string ProfileId = string.Empty;
        public readonly IUserRepository Userrep;
        public readonly IBusinessCenterCommon Businesscentercommon;
        private readonly IMailTemplateService _mailTemplateService;

        public AccountController(IUserManager userManager, IEmailTemplate userMail,
            IUserManagerService userManagerDbService, ISecurityQuestionsService service,
            ISearchKeywordService searchKeywordService, IUserRepository userrep, IBusinessCenterCommon businesscentercommon,
            IMailTemplateService mailTemplateService)
        {
            _userManager = userManager;
            _userMail = userMail;
            _userManagerDbService = userManagerDbService;
            _securityQuestions = service;
            _keywordCount = searchKeywordService;
            Userrep = userrep;
            Businesscentercommon = businesscentercommon;
            _mailTemplateService = mailTemplateService;
        }
        /// <summary>
        /// This mehod is used to get Register page
        /// </summary>
        /// <param name="utype"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult Register(string utype)
        {
            try
            {
                if (Businesscentercommon.ValidateSessionResult())
                {
                    ViewBag.StatusMsg = "";
                    TempData["Registertype"] = utype;
                    return View();
                }
                else
                { return RedirectToAction("SessionExpiry", "Account"); }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occurred in Register Getmethod:-", ex);
            }
        }
        /// <summary>
        /// This method is used for Create admin,SuperAdmin and Manager
        /// </summary>
        /// <param name="registerUserModel"></param>
        /// <param name="utype"></param>
        /// <returns></returns>
        [HttpPost]
        //  [ConcurrentLogin]
        [Audit]
        public async Task<ActionResult> Register(RegisterUserModel registerUserModel, string utype)
        {
            try
            {
                if (Businesscentercommon.ValidateSessionResult())
                {
                    TempData["Registertype"] = utype;
                    if (ModelState.IsValid)
                    {
                        registerUserModel.SecurityQuestion1 = "NA";
                        registerUserModel.SecurityQuestion2 = "NA";
                        registerUserModel.SecurityQuestion3 = "NA";
                        registerUserModel.SecurityAnswer1 = "NA";
                        registerUserModel.SecurityAnswer2 = "NA";
                        registerUserModel.SecurityAnswer3 = "NA";
                        switch (utype)
                        {
                            case "S":
                                registerUserModel.Role = "SuperAdmin";
                                break;

                            case "M":
                                registerUserModel.Role = "Manager";
                                break;

                            case "A":
                                registerUserModel.Role = "Admin";
                                break;
                        }
                        string resultData;
                        string emailAddress;
                        string exception;
                        string activationCode;
                        string userId = string.Empty;
                        var appUser = new AppUser();
                        appUser.InjectFrom(registerUserModel);
                        appUser.Id = Guid.NewGuid().ToString();
                        appUser.CreatedDate = DateTime.Now;
                        appUser.IsActive = true;
                        appUser.EmailConfirmed = true;
                        appUser.Address = (registerUserModel.Address ?? "").Trim();
                        appUser.City = (registerUserModel.City ?? "").Trim();
                        appUser.State = (registerUserModel.State ?? "").Trim();
                        appUser.PostalCode = (registerUserModel.PostalCode ?? "").Trim();
                        appUser.MobileNumber = (registerUserModel.MobileNumber ?? "").Trim();
                        if (registerUserModel.Title == "")
                        {
                            appUser.Title = "Mr.";
                        }
                        string[] selectedRoles = { string.Empty };

                        if (registerUserModel.Role == "SuperAdmin")
                        {
                            selectedRoles = new string[] { "Super Admin" };
                        }
                        if (registerUserModel.Role == "Admin")
                        {
                            selectedRoles = new string[] { "Admin" };
                        }
                        if (registerUserModel.Role == "Manager")
                        {
                            selectedRoles = new string[] { "Manager" };
                        }
                        var status = await _userManager.AdminRegistartion(appUser, registerUserModel.Password, selectedRoles);
                        switch (status)
                        {
                            case RegistartionStatus.Success:
                                resultData = ResultMessages.success;
                                emailAddress = appUser.Email;
                                userId = appUser.Id;
                                activationCode = appUser.ActivationCode;
                                exception = string.Empty;
                                break;

                            case RegistartionStatus.Exists:
                                resultData = ResultMessages.resultstate;
                                emailAddress = string.Empty;
                                userId = string.Empty;
                                exception = ResultMessages.resultstate;
                                activationCode = string.Empty;
                                break;

                            case RegistartionStatus.Failure:
                                resultData = ResultMessages.email;
                                emailAddress = string.Empty;
                                userId = string.Empty;
                                activationCode = string.Empty;
                                exception = ResultMessages.emailresultstate;
                                break;

                            default:
                                resultData = status.ToString();
                                emailAddress = string.Empty;
                                userId = string.Empty;
                                activationCode = string.Empty;
                                exception = string.Empty;
                                break;
                        }
                        ViewBag.StatusMsg = resultData;
                        if (resultData == "success")
                        {
                            if (registerUserModel.Role == "SuperAdmin")
                            {
                                return JavaScript(" $('.registermodaldiv').modal('show');$('.modal-body .error_message').empty().append('Superadmin account has been created successfully');");
                            }
                            if (registerUserModel.Role == "Manager")
                            {
                                return JavaScript(" $('.registermodaldiv').modal('show');$('.modal-body .error_message').empty().append('Manager account has been created successfully');");
                            }
                            if (registerUserModel.Role == "Admin")
                            {
                                return JavaScript(" $('.registermodaldiv').modal('show');$('.modal-body .error_message').empty().append('Admin account has been created successfully');");
                            }
                        }
                        else if (resultData == "EmailExists")
                        {
                            ViewBag.StatusMsg = "This email is already in our system .Please select another email address";
                            registerUserModel.EmailStatusMsg = "This email is already in our system. Please select another email address";
                            registerUserModel.Password = "";
                            registerUserModel.ConfirmPassword = "";
                            return JavaScript(" $('.registermodaldiv').modal('show');$('.modal-body .error_message').empty().append('This email is already in our system .Please select another email address');$('.hiddencss').text('Email')");
                        }
                        else if (resultData == "User Already Exits")
                        {
                            ViewBag.StatusMsg = "This username is already in our system .Please select a new username.";
                            registerUserModel.StatusMsg = "This username is already registered in our system. Please select a new username";
                            registerUserModel.Password = "";
                            registerUserModel.ConfirmPassword = "";
                            return JavaScript(" $('.registermodaldiv').modal('show');$('.modal-body .error_message').empty().append('This username is already in our system .Please select a new username.');$('.hiddencss').text('Uname')");
                        }
                        return View();
                    }
                }
                else
                { return Json(new { status = "SessionExipred" }); }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occurred in Register PostMethod:-", ex);
            }
            return View();
        }
        /// <summary>
        /// This method is used to send an Email When forgotpassword link click
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="code"></param>
        /// <param name="email"></param>
        /// <param name="userFullName"></param>
        private void ForgotPasswordMailSend(string userId, string code, string email, string userFullName)
        {
            var callBackUrl = new StringBuilder();
            callBackUrl.Append(ConfigurationManager.AppSettings["anuglarAddress"]);
            callBackUrl.Append("#");
            callBackUrl.Append("/getnewpassword?");
            callBackUrl.Append("userId=");
            callBackUrl.Append(userId);
            callBackUrl.Append("&code=");
            callBackUrl.Append(code);
            var mailbody = PopulateBody(userFullName, callBackUrl.ToString(), "User");
            var mailTemplateModel = new MailTemplateModel
            {
                UserId = userId,
                CustomApplicationId = userId,
                Type = "PASSWORD RESET",
                Subject = "Password Reset",
                MailSentFailCount = 0,
                MailContent = mailbody.ToString().Trim(),
                IsMailSent = true
            };
            _mailTemplateService.InsertUpdateMailTemplate(mailTemplateModel);
            _userMail.MailSending("Password Reset", mailbody, email);
        }
        /// <summary>
        /// This method is used to send an Forgotpassword mail for admin and SuperAdmin
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="code"></param>
        /// <param name="email"></param>
        /// <param name="userFullName"></param>
        [Audit]
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
            var emailBody = PopulateBody(userFullName.ToUpper(), callBackUrl.ToString(), "Admin");
            var mailTemplateModel = new MailTemplateModel
            {
                UserId = userId,
                CustomApplicationId = userId,
                Type = "FORGOT PASSWORD",
                Subject = "Forgot your password on My DC Business Center Admin Portal",
                MailSentFailCount = 0,
                MailContent = emailBody.ToString().Trim(),
                IsMailSent = true
            };
            _mailTemplateService.InsertUpdateMailTemplate(mailTemplateModel);
            _userMail.MailSending("Forgot your password on My DC Business Center Admin Portal", emailBody, email);
        }
        /// <summary>
        /// This method is used to generate password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string HashPassword(string password)
        {
            if (password == null)
                throw new ArgumentNullException("password");
            byte[] salt;
            byte[] bytes;
            using (Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, 16, 1000))
            {
                salt = rfc2898DeriveBytes.Salt;
                bytes = rfc2898DeriveBytes.GetBytes(32);
            }
            byte[] inArray = new byte[49];
            Buffer.BlockCopy((Array)salt, 0, (Array)inArray, 1, 16);
            Buffer.BlockCopy((Array)bytes, 0, (Array)inArray, 17, 32);
            return Convert.ToBase64String(inArray);
        }
        /// <summary>
        /// This method is used for mailbody content
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="url"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        private string PopulateBody(string userName, string url, string role)
        {
            var template = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/EmailTemplates/ForgotPassword.cshtml"));

            var viewModel = new ForgotPasswordModel
            {
                FullName = userName,
                PasswordLink = url,
                Role = role
            };

            string body1;
#pragma warning disable 618
            body1 = Razor.Parse(template, viewModel);
#pragma warning restore 618
            return body1;
        }
        /// <summary>
        /// This method is used to get login page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            if (Session["Login"] != null)
            {
                Session["ApplicationStart"] = "Yes";
                if ((int)Session["Admincount"] == 3)
                {
                    return RedirectToAction("Dashboard", "SuperAdmin");
                }
                else if ((int)Session["Admincount"] == 2)
                {
                    return RedirectToAction("CustomerHome", "Admin");
                }
                else if ((int)Session["Admincount"] == 4)
                {
                    return RedirectToAction("Dashboard", "SuperAdmin");
                }
            }
            else
            {
                Session["ApplicationStart"] = "No";
                Session["Login"] = null;
                Session["UserName"] = null;
                ViewBag.StatusMsg = "";
                ViewData["mesage"] = "";
                Session["count"] = 0;
                return View();
            }
            return View();
        }
        /// <summary>
        /// This method is used for user logout from the application
        /// </summary>
        /// <returns></returns>
        public ActionResult Signout()
        {
            Session["ApplicationStart"] = "No";
            Session["Login"] = null;
            Session["UserName"] = null;
            ViewBag.StatusMsg = "";
            ViewData["mesage"] = "";
            Session["count"] = 0;
            Session["Login"] = null;
            Session["UserName"] = null;
            BusinessCenter.Admin.Common.BusinessCenterSession.BusinessCenterLoginUserSessionId = null;
            BusinessCenter.Admin.Common.BusinessCenterSession.BusinessCenterUserName = null;
            System.Web.HttpContext.Current.Response.Cookies.Clear();
            _userManager.SignOut();
            return RedirectToAction("Login", "Account");
        }
        /// <summary>
        /// This method is used for Admin or SuperAdmin login into application
        /// </summary>
        /// <param name="loginViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Audit]
        public async Task<ActionResult> Login(LoginViewModel loginViewModel)
        {
            int userrole = Userrep.GetuserByUserName(loginViewModel.UserName);
            loginViewModel.StatusMsg = "";
            try
            {
                if (userrole == 1 || userrole == 2 || userrole == 4)
                {
                    if (ModelState.IsValid)
                    {
                        ViewBag.StatusMsg = "";
                        string resultData;
                        string userId = "0";
                        var userFullName = string.Empty;
                        var userFirstName = string.Empty;
                        var userLastName = string.Empty;
                        var status = await
                                        _userManager.PasswordSignIn(loginViewModel.UserName, loginViewModel.Password, loginViewModel.RememberMe,
                                           shouldLockout: true);
                        switch (status)
                        {
                            case UserSignInStatus.Success:
                                var user = await _userManager.FindByNameAsync(loginViewModel.UserName);
                                Session["ApplicationStart"] = "Yes";
                                if (user != null)
                                {
                                    Session["ApplicationStart"] = "Yes";
                                    Session["ApplicationStartTime"] = DateTime.UtcNow;
                                    LoginFailedAttems = 0;
                                    userId = user.Id.ToString();
                                    var uLhModel = new UserLoginHistoryModel
                                    {
                                        Count = 0,
                                        LastLoginDate = Convert.ToDateTime(System.DateTime.Now),
                                        UserId = userId.Trim(),
                                        LoginHisId = 0
                                    };
                                    var userLoginHistrory = new Data.Models.UserLoginHistoryModel();
                                    userLoginHistrory.InjectFrom(uLhModel);
                                    _userManagerDbService.UserLoginHistory(userLoginHistrory);
                                    userFullName = user.FirstName + " " + user.LastName;
                                    userFirstName = user.FirstName;
                                    userLastName = user.LastName;
                                    BusinessCenter.Admin.Common.BusinessCenterSession.BusinessCenterLoginUserSessionId =
                            Guid.NewGuid().ToString();
                                    Userdetails userdetail = new Userdetails();
                                    userdetail.UserName = loginViewModel.UserName.Trim();
                                    userdetail.IsLogedIn = true;
                                    userdetail.SessionId = BusinessCenter.Admin.Common.BusinessCenterSession.BusinessCenterLoginUserSessionId.ToString();
                                    Userrep.UpdateLoggedTime(userdetail);
                                    BusinessCenter.Admin.Common.BusinessCenterSession.BusinessCenterUserName = loginViewModel.UserName.Trim();
                                }
                                var userRoles = await _userManager.GetUserRoleName(userId);
                                resultData = status.ToString();
                                Session["FirstName"] = user.FirstName;
                                Session["LastName"] = user.LastName;
                                Session["FullName"] = userFullName;
                                Session["UserId"] = user.Id;
                                ProfileId = user.Id;
                                if (userRoles.Contains("Super Admin"))
                                {
                                    Session["Login"] = "Success";
                                    Session["Admincount"] = 3;
                                    System.Web.HttpContext.Current.Session["UserName"] = loginViewModel.UserName;
                                    if (Session["UserName"] != null)
                                    {
                                        return RedirectToAction("Dashboard", "SuperAdmin");
                                    }
                                }
                                else if (userRoles.Contains("Manager"))
                                {
                                    Session["Login"] = "Success";
                                    Session["Admincount"] = 4;
                                    System.Web.HttpContext.Current.Session["UserName"] = loginViewModel.UserName;
                                    if (Session["UserName"] != null)
                                    {
                                        return RedirectToAction("Dashboard", "Admin");
                                    }
                                }
                                else if (userRoles.Contains("Admin"))
                                {
                                    Session["Login"] = "Success";
                                    Session["Admincount"] = 2;
                                    System.Web.HttpContext.Current.Session["UserName"] = loginViewModel.UserName;
                                    if (Session["UserName"] != null)
                                    {
                                        return RedirectToAction("CustomerHome", "Admin");
                                    }
                                }
                                else
                                {
                                    loginViewModel.StatusMsg = "Access is denied for the above user into this portal";
                                    ViewBag.StatusMsg = loginViewModel.StatusMsg;
                                }
                                break;

                            case UserSignInStatus.Delete:
                                resultData = ResultMessages.delete;
                                loginViewModel.StatusMsg = "Account has been deactivated";
                                ViewBag.StatusMsg = loginViewModel.StatusMsg;
                                break;

                            case UserSignInStatus.Expire:
                                resultData = ResultMessages.linkExpire;
                                break;

                            case UserSignInStatus.In_Active:
                                resultData = ResultMessages.inactive;
                                break;

                            case UserSignInStatus.AlreadyLoggedIn:
                                resultData = ResultMessages.UserAlreadyLoggedIn;
                                loginViewModel.StatusMsg = "User already logged in to the system.Please verify and try again ";
                                ViewBag.StatusMsg = loginViewModel.StatusMsg;
                                break;

                            case UserSignInStatus.Nodata:
                                resultData = ResultMessages.nouser;
                                loginViewModel.StatusMsg = "This site admin/superadmin is not currently registered. Please double check the username or send request to a new account";
                                ViewBag.StatusMsg = loginViewModel.StatusMsg;
                                break;

                            case UserSignInStatus.LockedOut:
                                resultData = status.ToString();
                                return RedirectToAction("Lockout", "Account");

                            default:
                                var user1 = await _userManager.FindByNameAsync(loginViewModel.UserName);
                                if (user1 != null)
                                {
                                    LoginFailedAttems = user1.AccessFailedCount;
                                }
                                if (user1 != null && (LoginFailedAttems == 0 && user1.LockoutEnabled == true))
                                {
                                    resultData = status.ToString();
                                    return RedirectToAction("Lockout", "Account");
                                }
                                resultData = status.ToString();
                                if (LoginFailedAttems == 1 || LoginFailedAttems == 2)
                                {
                                    loginViewModel.StatusMsg = "Either Username or Password Are Incorrect ";
                                    ViewBag.StatusMsg = loginViewModel.StatusMsg;
                                }
                                else if (LoginFailedAttems == 3)
                                {
                                    loginViewModel.StatusMsg = "Either Username or Password are incorrect. You currently have two (2) attempts left before your account will be frozen for five (5)minutes";
                                    ViewBag.StatusMsg = loginViewModel.StatusMsg;
                                }
                                else if (LoginFailedAttems == 4)
                                {
                                    loginViewModel.StatusMsg = "Either Username or Password are incorrect. You currently have one (1) attempt left before your account will be frozen for five (5) minutes";
                                    ViewBag.StatusMsg = loginViewModel.StatusMsg;
                                }
                                else { }
                                break;
                        }
                    }
                }
                else if (userrole == 0)
                {
                    loginViewModel.StatusMsg = "This site admin/superadmin is not currently registered. Please double check the username or send request to a new account";
                    ViewBag.StatusMsg = loginViewModel.StatusMsg;
                }
                else if (userrole == 3)
                {
                    loginViewModel.StatusMsg = "Access is denied for the above user into this portal";
                    ViewBag.StatusMsg = loginViewModel.StatusMsg;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occurred in Login PostMethod:-", ex);
            }
            return View();
        }
        /// <summary>
        /// This method is used to get lockout page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Lockout()
        {
            try
            {
                Session["Login"] = null;
                Session["UserName"] = null;
                System.Web.HttpContext.Current.Response.Cookies.Clear();
                _userManager.SignOut();
                return View();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public string KeepSessionAlive()
        {
            return "true";
        }
        /// <summary>
        /// This method is used to get logout from application
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Audit]
        public async Task<ActionResult> Logout()
        {
            try
            {
                if (Businesscentercommon.ValidateSessionResult())
                {
                    string userid = Session["UserId"].ToString();

                    bool result = Userrep.UpdateLoggedInStatus(userid, false);
                    if (result == true)
                    {
                        Session["Login"] = null;
                        Session["UserName"] = null;
                        BusinessCenter.Admin.Common.BusinessCenterSession.BusinessCenterLoginUserSessionId = null;
                        BusinessCenter.Admin.Common.BusinessCenterSession.BusinessCenterUserName = null;
                        System.Web.HttpContext.Current.Response.Cookies.Clear();
                        _userManager.SignOut();
                        result = true;
                    }
                    return await Task.FromResult(JavaScript(""));
                }
                else
                { return RedirectToAction("SessionExpiry", "Account"); }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occurred in Logout GetMethod:-", ex);
            }
        }
        /// <summary>
        /// This method is used to send an email for when forgot password link click
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Audit]
        public async Task<ActionResult> ForgotpasswordstausNew(String UserId, string FirstName, string LastName)
        {
            try
            {
                if (Businesscentercommon.ValidateSessionResult())
                {
                    if (ModelState.IsValid)
                    {
                        ForgotPassword forgotPassword = new ForgotPassword();
                        ReSendMail reSendMail = new ReSendMail();
                        string result = string.Empty;
                        string userId = string.Empty;
                        string userMail = string.Empty;
                        string userCode = string.Empty;
                        string roleCount = string.Empty;
                        if (ModelState.IsValid)
                        {
                            var user = await _userManager.FindUserDetailsByIdAsync(UserId);
                            if (user == null)
                            {
                                result = ResultMessages.NoUser;
                                reSendMail.Status = "Either Username or Password Are Incorrect";
                                TempData["StatusMsg"] = reSendMail.Status;
                                return RedirectToAction("ForgotPassword", "Account");
                            }
                            var userroles = await _userManager.GetRolesAsync(user.Id);
                            if (userroles.Contains("Employee"))
                            {
                                reSendMail.Status = "User is not allow access to this System";
                                TempData["StatusMsg"] = reSendMail.Status;
                                return RedirectToAction("ForgotPassword", "Account");
                            }
                            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user.Id)))
                            {
                                result = ResultMessages.NoUser;
                                reSendMail.Status = "Either Username or Password Are Incorrect";
                                TempData["StatusMsg"] = reSendMail.Status;
                                return RedirectToAction("ForgotPassword", "Account");
                            }
                            if (user != null)
                            {
                                bool getvalue = GenaralConvertion.CheckDateExpire(System.DateTime.Now.ToString(),
                                      GenaralConvertion.ValidateGivenTime(user.ActivationDate.ToString()).ToString());
                                if (getvalue == false && user.LockoutEnabled == true)
                                {
                                    user.LockoutEnabled = false;
                                }
                                if ((user.IsActive == true) && (user.IsDelete == false))
                                {
                                    DateTime expireDate = GenaralConvertion.ValidateGivenTime(user.LockoutEndDateUtc.ToString());
                                    string h = System.DateTime.UtcNow.ToString();
                                    if (((user.LockoutEnabled == true && (user.AccessFailedCount == 0 || user.AccessFailedCount == 5))) &&
                                        (expireDate.AddMinutes(5) > System.DateTime.UtcNow))
                                    {
                                        result = ResultMessages.lockout;
                                    }
                                    else
                                    {
                                        var code = await _userManager.GeneratePasswordResetTokenAsync(user.Id);
                                        userCode = code;
                                        userId = user.Id.ToString();
                                        AdminForgotPasswordMailSendNew(user.Id, code, user.Email, user.FirstName + " " + user.LastName);
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
                                        reSendMail.Status = "success";
                                        ViewBag.Statusmsg = user.FirstName + " " + user.LastName;
                                        return JavaScript("");
                                    }
                                }
                                else if ((user.IsActive == true) && (user.IsDelete == true))
                                {
                                    result = ResultMessages.delete;
                                    reSendMail.Status = "Account has been deleted";
                                    TempData["StatusMsg"] = reSendMail.Status;
                                    return JavaScript("");
                                }
                                else if ((user.IsActive == false) && (user.IsDelete == false))
                                {
                                    result = getvalue == true ? ResultMessages.inactive : ResultMessages.reregister;
                                    reSendMail.Status = "Inactive";
                                    return RedirectToAction("ForgotPassword", "Account");
                                }
                            }
                        }
                        if (reSendMail.Status == "success")
                        {
                            return View(reSendMail);
                        }
                    }
                    return View();
                }
                else
                { return RedirectToAction("SessionExpiry", "Account"); }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occurred in ForgotpasswordstausNew GetMethod:-", ex);
            }
        }
        /// <summary>
        /// This method is used to Display the Keyword counts
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Audit]
        [ConcurrentLogin]
        public async Task<ActionResult> DashboardSearch(string type = null)
        {
            try
            {
                DashBoardViewModel viewmodel = new DashBoardViewModel();
                DisplayType dt = new DisplayType();
                dt.DisplayValue = type;
                if (type == null)
                {
                    dt.DisplayValue = "ALL";
                }
                var KeywordCount = await _keywordCount.GetDashBoardInvdvidualcountsCount();
                viewmodel.KeywordCount = KeywordCount.Select(i => new KewordsCount().InjectFrom(i)).Cast<KewordsCount>().ToList();
                return PartialView("_DashboardSearch", viewmodel);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occurred in DashboardSearch GetMethod:-", ex);
            }
        }
        /// <summary>
        /// This method is used to show the KeywordSearchHistory Details
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Audit]
        [ConcurrentLogin]
        public async Task<ActionResult> DashboardSearchitem(string type = null)
        {
            try
            {
                DashBoardViewModel viewmodel = new DashBoardViewModel();
                DisplayType dt = new DisplayType();
                dt.DisplayValue = type;
                if (type == null)
                {
                    dt.DisplayValue = "ALL";
                }
                NameValueCollection KV = HttpUtility.ParseQueryString(dt.DisplayValue);
                DateTime FromDate = DateTime.Now.AddDays(-2);
                DateTime ToDate = DateTime.Now;
                var KeywordCount = await _keywordCount.GetKeywordSearchCount(FromDate, ToDate, KV.ToString());
                viewmodel.SearchedKeywords = KeywordCount.Select(i => new SearchKeywordMvcModel().InjectFrom(i)).Cast<SearchKeywordMvcModel>().OrderByDescending(x => Convert.ToDateTime(x.CreatedDate)).ToList();
                if (dt.DisplayValue == "ALL")
                {
                    Session["SearchedKeywords"] = viewmodel.SearchedKeywords;
                    Session["KeywordCount"] = viewmodel.SearchedKeywords;
                }
                return PartialView("_SuperadminSearchitem", viewmodel);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occurred in DashboardSearchitem GetMethod:-", ex);
            }
        }
        /// <summary>
        /// This method is used to display the 1000 Most Frequently Searched Items
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Audit]
        [ConcurrentLogin]
        public async Task<ActionResult> DashboardKeywords()
        {
            try
            {
                string type = null;
                DashBoardViewModel viewmodel = new DashBoardViewModel();
                DisplayType dt = new DisplayType();
                dt.DisplayValue = type;
                if (type == null)
                {
                    dt.DisplayValue = "ALL";
                }
                NameValueCollection KV = HttpUtility.ParseQueryString(dt.DisplayValue);
                DateTime FromDate = DateTime.Now.AddDays(-2);
                DateTime ToDate = DateTime.Now;
                var KeywordCount = await _keywordCount.AdminKeywordSearchCount(FromDate, ToDate, KV.ToString());
                viewmodel.SearchedKeywords = KeywordCount.Select(i => new SearchKeywordMvcModel().InjectFrom(i)).Cast<SearchKeywordMvcModel>().ToList();
                viewmodel.SearchedKeywords = viewmodel.SearchedKeywords.OrderByDescending(i => i.KeyCount).ToList();
                if (dt.DisplayValue == "ALL")
                {
                    Session["SearchedKeywords"] = viewmodel.SearchedKeywords;
                    Session["KeywordCount"] = viewmodel.SearchedKeywords;
                }
                return PartialView("_DashboardKeywordscount", viewmodel);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occurred in DashboardKeywords GetMethod:-", ex);
            }
        }
        /// <summary>
        /// This method is used to filter keywordSearchHistory Details  based on Date
        /// </summary>
        /// <param name="searchdate"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Audit]
        [ConcurrentLogin]
        public ActionResult DashboardSearchdate(string searchdate)
        {
            try
            {
                searchdate = searchdate.Replace("-", "/");
                DashBoardViewModel viewmodel = new DashBoardViewModel();
                viewmodel.SearchedKeywords = (List<SearchKeywordMvcModel>)Session["SearchedKeywords"];
                viewmodel.SearchedKeywords = viewmodel.SearchedKeywords.Where(c => c.CreatedDate.Trim() == searchdate.Trim()).ToList();
                return PartialView("_SuperadminSearchitem", viewmodel);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occurred in DashboardSearchdate GetMethod:-", ex);
            }
        }
        /// <summary>
        /// This method is used an forgot password mail to the user
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Audit]
        public async Task<ActionResult> UserForgotpasswordstausNew(String UserId)
        {
            try
            {
                if (Businesscentercommon.ValidateSessionResult())
                {
                    if (ModelState.IsValid)
                    {
                        ForgotPassword forgotPassword = new ForgotPassword();
                        ReSendMail reSendMail = new ReSendMail();
                        string result = string.Empty;
                        string userId = string.Empty;
                        string userMail = string.Empty;
                        string userCode = string.Empty;
                        string roleCount = string.Empty;
                        if (ModelState.IsValid)
                        {
                            var user = await _userManager.FindUserDetailsByIdAsync(UserId);
                            roleCount = user.Roles.Count.ToString();
                            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user.Id)))
                            {
                                result = ResultMessages.NoUser;
                            }
                            if (user != null)
                            {
                                bool getvalue = GenaralConvertion.CheckDateExpire(System.DateTime.Now.ToString(),
                                      GenaralConvertion.ValidateGivenTime(user.ActivationDate.ToString()).ToString());
                                if (getvalue == false && user.LockoutEnabled == true)
                                {
                                    user.LockoutEnabled = false;
                                }
                                if ((user.IsActive == true) && (user.IsDelete == false))
                                {
                                    DateTime expireDate = GenaralConvertion.ValidateGivenTime(user.LockoutEndDateUtc.ToString());
                                    string h = System.DateTime.UtcNow.ToString();
                                    if (((user.LockoutEnabled == true && (user.AccessFailedCount == 0 || user.AccessFailedCount == 5))) &&
                                        (expireDate.AddMinutes(5) > System.DateTime.UtcNow))
                                    {
                                        result = ResultMessages.lockout;
                                    }
                                    else
                                    {
                                        var code = await _userManager.GeneratePasswordResetTokenAsync(user.Id);
                                        userCode = code;
                                        userId = user.Id.ToString();
                                        ForgotPasswordMailSend(user.Id, code, user.Email, user.FirstName + " " + user.LastName);
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
                                        reSendMail.Status = "success";
                                        ViewBag.Statusmsg = user.UserName;
                                        reSendMail.StatusMsg = user.FirstName + " " + user.LastName;
                                        return JavaScript("");
                                    }
                                }
                                else if ((user.IsActive == true) && (user.IsDelete == true))
                                {
                                    result = ResultMessages.delete;
                                    reSendMail.Status = "deleted";
                                    return JavaScript("");
                                }
                                else if ((user.IsActive == false) && (user.IsDelete == false))
                                {
                                    result = getvalue == true ? ResultMessages.inactive : ResultMessages.reregister;
                                    reSendMail.Status = "Inactive";
                                }
                            }
                        }
                        if (reSendMail.Status == "success")
                        {
                            return View(reSendMail);
                        }
                    }
                    return View();
                }
                else
                { return RedirectToAction("SessionExpiry", "Account"); }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occurred in UserForgot password staus New GetMethod:-", ex);
            }
        }
        /// <summary>
        /// This method is used to display forgotpassword page
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [Authorize]
        [Audit]
        [ConcurrentLogin]
        public async Task<ActionResult> ResetPassword(string UserId)
        {
            ResetPasswordModel resetPasswordModel = new ResetPasswordModel();
            resetPasswordModel.userId = UserId;
            return await Task.FromResult(View(resetPasswordModel));
        }
        /// <summary>
        /// This method is used for Admin can Resetpassword
        /// </summary>
        /// <param name="resetPasswordModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [Audit]
        [ConcurrentLogin]
        public async Task<ActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            string resultData;
            try
            {
                if (Businesscentercommon.ValidateSessionResult())
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(resetPasswordModel.userId);
                    if (token == null)
                    {
                        resultData = ResultMessages.tokenmsg;
                    }
                    var result = await _userManager.ResetPasswordAsync(resetPasswordModel.userId, token, resetPasswordModel.Password);
                    resultData = result.Succeeded ? ResultMessages.success : ResultMessages.error;
                    if (resultData == "success")
                    {
                        ViewBag.status = "Password changed";
                        return RedirectToAction("ResetSuccess", "Account");
                    }
                    else
                    {
                        ViewBag.status = "Password not changed";
                        Session["Userid"] = null;
                    }
                }
                else
                { return RedirectToAction("SessionExpiry", "Account"); }
            }
            catch (Exception ex)
            {
                resultData = ex.Message.ToString();
            }
            return View(resetPasswordModel);
        }
        /// <summary>
        /// This method is used to show the ResetPasswordConfirmation view page
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Audit]
        [ConcurrentLogin]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }
        /// <summary>
        /// This method is used to show the ResetSuccess page
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Audit]
        [ConcurrentLogin]
        public ActionResult ResetSuccess()
        {
            return View();
        }
        /// <summary>
        /// This method is used to display the Profile page
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet]
        [Audit]
        [Authorize]
        [ConcurrentLogin]
        public async Task<ActionResult> Profile(string userId, string type)
        {
            try
            {

                var userAccountById = new UserAccountById { UserId = userId };
                var registerJsonModel = new RegisterJsonModel();
                var userIDs = await _userManager.FindUserDetailsByIdAsync(userAccountById.UserId);
                registerJsonModel.InjectFrom(userIDs);
                registerJsonModel.UserId = userAccountById.UserId;
                registerJsonModel.UserType = type;
                ViewBag.Status = "";
                Session["ProfileUserId"] = registerJsonModel.UserId;
                Session["AdminName"] = registerJsonModel.FirstName + " " + registerJsonModel.LastName;
                return await Task.FromResult<ActionResult>(View(registerJsonModel));
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occurred in Profile GetMethod:-", ex);
            }
        }
        /// <summary>
        /// This method is used to Update Proile for Admin and SuperAdmin
        /// </summary>
        /// <param name="registerJsonModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Audit]
        [Authorize]
        [ConcurrentLogin]
        public async Task<ActionResult> Profile(RegisterJsonModel registerJsonModel)
        {
            try
            {
                if (Businesscentercommon.ValidateSessionResult())
                {
                    string updatemsg = registerJsonModel.UserName + " " + "profile is updated";
                    registerJsonModel.UserId = Session["ProfileUserId"].ToString();
                    string userId = string.Empty;
                    bool profilestatus = false;
                    AppUser id = await _userManager.FindByNameAsync(registerJsonModel.UserName);
                    if (id != null)
                    {
                        userId = id.Id;
                    }
                    UserProfileUpdate userProfileUpdate = new UserProfileUpdate();
                    userProfileUpdate.SecurityQuestion1 = "NA";
                    userProfileUpdate.SecurityQuestion2 = "NA";
                    userProfileUpdate.SecurityQuestion3 = "NA";
                    userProfileUpdate.SecurityAnswer1 = "NA";
                    userProfileUpdate.SecurityAnswer2 = "NA";
                    userProfileUpdate.SecurityAnswer3 = "NA";
                    userProfileUpdate.UserId = userId.Trim();
                    JsonModel jsonModel = new JsonModel();
                    var changePassword = false;
                    var validateOldPassword = true;
                    string emailChange = string.Empty;
                    string status = string.Empty;
                    var user = await _userManager.FindUserDetailsByIdAsync(userProfileUpdate.UserId);
                    var userDetails = ToConvertUser.ToDataConvert(user);
                    var userUpdateProfile = new AppUser();
                    userUpdateProfile.InjectFrom(userDetails);
                    if (userUpdateProfile.FirstName != registerJsonModel.FirstName)
                    {
                        profilestatus = true;
                    }
                    else if (userUpdateProfile.LastName != registerJsonModel.LastName)
                    {
                        profilestatus = true;
                    }
                    else if (userUpdateProfile.Address != registerJsonModel.Address)
                    {
                        profilestatus = true;
                    }
                    else if (userUpdateProfile.City != registerJsonModel.City)
                    {
                        profilestatus = true;
                    }
                    else if (userUpdateProfile.State != registerJsonModel.State)
                    {
                        profilestatus = true;
                    }
                    else if (userUpdateProfile.MobileNumber != registerJsonModel.MobileNumber)
                    {
                        profilestatus = true;
                    }
                    else if (userUpdateProfile.PostalCode != registerJsonModel.PostalCode)
                    {
                        profilestatus = true;
                    }
                    else if (userUpdateProfile.IsDelete != registerJsonModel.IsDelete)
                    {
                        profilestatus = true;
                    }
                    if (profilestatus == false)
                    {
                        ViewBag.Status = "You have not made any changes to your profile. If needed, please do changes and then click on [SAVE & UPDATE]";
                        string msg = "You have not made any changes to" + " " + registerJsonModel.UserName + " " + "profile. If needed, please do changes and then click on [SAVE & UPDATE]";
                        return JavaScript(" $('.profilemodaldiv').modal('show');$('.profilemodal-body .error_message').empty().append('" + msg + "');");
                    }
                    userProfileUpdate.UserName = userUpdateProfile.UserName;
                    userUpdateProfile.FirstName = registerJsonModel.FirstName;
                    userUpdateProfile.LastName = registerJsonModel.LastName;
                    userUpdateProfile.Email = registerJsonModel.Email;
                    userUpdateProfile.SecurityQuestion1 = registerJsonModel.SecurityQuestion1;
                    userUpdateProfile.SecurityQuestion2 = registerJsonModel.SecurityQuestion2;
                    userUpdateProfile.SecurityQuestion3 = userProfileUpdate.SecurityQuestion3;
                    userUpdateProfile.SecurityAnswer1 = userProfileUpdate.SecurityAnswer1;
                    userUpdateProfile.SecurityAnswer2 = userProfileUpdate.SecurityAnswer2;
                    userUpdateProfile.SecurityAnswer3 = userProfileUpdate.SecurityAnswer3;
                    userUpdateProfile.CreatedDate = user.CreatedDate;
                    userUpdateProfile.LockoutEndDateUtc = user.LockoutEndDateUtc;
                    userUpdateProfile.UpdatedDate = Convert.ToDateTime(System.DateTime.Now);
                    userUpdateProfile.PreviousEmailConfirmed = false;
                    userUpdateProfile.ChangeEmailConfirmed = false;
                    userUpdateProfile.PreviousEmailValidate = DateTime.Now;
                    userUpdateProfile.ChangeEmailValidate = DateTime.Now;
                    userUpdateProfile.Address = (registerJsonModel.Address ?? "").Trim();
                    userUpdateProfile.City = (registerJsonModel.City ?? "").Trim();
                    userUpdateProfile.State = (registerJsonModel.State ?? "").Trim();
                    userUpdateProfile.PostalCode = (registerJsonModel.PostalCode ?? "").Trim();
                    userUpdateProfile.MobileNumber = (registerJsonModel.MobileNumber ?? "").Trim();
                    userUpdateProfile.IsDelete = registerJsonModel.IsDelete;
                    var type = string.Empty;
                    if (userProfileUpdate.Password != null)
                    {
                        validateOldPassword = await _userManager.IsCheckPasswordAsync(user, userProfileUpdate.Password);
                    }
                    if (validateOldPassword)
                    {
                        if ((userProfileUpdate.Password != null) && userProfileUpdate.NewPassword != null)
                        {
                            if (((userProfileUpdate.Password != userProfileUpdate.NewPassword) ||
                                 (userProfileUpdate.Password == userProfileUpdate.NewPassword))
                                && (userProfileUpdate.NewPassword != string.Empty))
                            {
                                await ProfilePasswordUpdate(userProfileUpdate.UserId, userProfileUpdate.Password,
                                        userProfileUpdate.NewPassword);
                                changePassword = true;
                            }
                        }
                        var result = await _userManager.UpdateAsync(userUpdateProfile, userProfileUpdate.UserId);
                        if (!result.Succeeded)
                        {
                            status = "error";
                            ViewBag.Status = "Error in Updating";
                            return JavaScript(" $('.profilemodaldiv').modal('show');$('.profilemodal-body .error_message').empty().append('Your profile is not updated');");
                        }
                    }
                    else
                        status = "InvalidOldPassword";
                    if (changePassword)
                    {
                        status = "success";
                        ViewBag.Status = "Profile updated successfully";
                        Session["AdminName"] = registerJsonModel.FirstName + " " + registerJsonModel.LastName;

                        return JavaScript(" $('.profilemodaldiv').modal('show');$('.profilemodal-body .error_message').empty().append(" + updatemsg + ");");
                    }
                    // ReSharper disable once RedundantIfElseBlock
                    else
                    {
                        status = "success";
                        ViewBag.Status = "Profile updated successfully";
                        Session["AdminName"] = registerJsonModel.FirstName + " " + registerJsonModel.LastName;
                        return JavaScript(" $('.profilemodaldiv').modal('show');$('.profilemodal-body .error_message').empty().append('" + updatemsg + "');");
                    }
                    // ReSharper disable once HeuristicUnreachableCode
                    return await Task.FromResult<ActionResult>(View());
                }
                // ReSharper disable once RedundantIfElseBlock
                else
                { return RedirectToAction("SessionExpiry", "Account"); }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occurred in Profile PostMethod:-", ex);
            }
        }
        /// <summary>
        /// This method is used to display the User Profile
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpGet]
        [Audit]
        [Authorize]
        [ConcurrentLogin]
        public async Task<ActionResult> CustomerProfile(string UserId)
        {
            try
            {
                UserAccountById UserAccountById = new UserAccountById();
                UserAccountById.UserId = UserId;
                RegisterJsonModel reg = new RegisterJsonModel();
                var userIDs = await _userManager.FindUserDetailsByIdAsync(UserAccountById.UserId);
                RegisterUserModel superAdminUser = new RegisterUserModel();
                reg.FirstName = userIDs.FirstName;
                reg.LastName = userIDs.LastName;
                reg.UserName = userIDs.UserName;
                reg.Email = userIDs.Email;
                reg.Address = userIDs.Address;
                reg.City = userIDs.City;
                reg.MobileNumber = userIDs.MobileNumber;
                reg.PostalCode = userIDs.PostalCode;
                reg.State = userIDs.State;
                reg.SecurityQuestion1 = userIDs.SecurityQuestion1;
                reg.SecurityQuestion2 = userIDs.SecurityQuestion2;
                reg.SecurityQuestion3 = userIDs.SecurityQuestion3;
                reg.SecurityAnswer1 = userIDs.SecurityAnswer1;
                reg.SecurityAnswer2 = userIDs.SecurityAnswer2;
                reg.SecurityAnswer3 = userIDs.SecurityAnswer3;
                reg.IsDelete = userIDs.IsDelete;
                ViewBag.Status = "";
                ViewBag.Sq1 = reg.SecurityQuestion1;
                ViewBag.Sq2 = reg.SecurityQuestion2;
                ViewBag.Sq3 = reg.SecurityQuestion3;
                var securityQuestions = _securityQuestions.GetAll();
                List<SecurityQuestionModel> securityModel = new List<SecurityQuestionModel>();
                securityModel = securityQuestions.Select(i => new SecurityQuestionModel().InjectFrom(i)).Cast<SecurityQuestionModel>().ToList();
                ViewBag.Questions = securityModel;
                Session["UserProfileUserId"] = UserId;
                return View(reg);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occurred in CustomerProfile GetMethod:-", ex);
            }
        }

        public async Task<ActionResult> SessionExpiry(RegisterJsonModel registerJsonModel)
        {
            Session["ApplicationStart"] = "No";
            return await Task.FromResult<ActionResult>(View());
        }
        /// <summary>
        /// This method is used to user Profile Updating
        /// </summary>
        /// <param name="registerJsonModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Audit]
        [Authorize]
        [ConcurrentLogin]
        public async Task<ActionResult> CustomerProfile(RegisterJsonModel registerJsonModel)
        {
            try
            {
                if (Businesscentercommon.ValidateSessionResult())
                {
                    string Updatemsg = registerJsonModel.UserName + " " + "profile is updated";
                    registerJsonModel.UserId = Session["UserProfileUserId"].ToString();
                    string userID = string.Empty;
                    bool profilestatus = false;
                    AppUser id = await _userManager.FindByNameAsync(registerJsonModel.UserName);
                    if (id != null)
                    {
                        userID = id.Id;
                    }
                    var securityQuestions = _securityQuestions.GetAll();
                    ViewBag.Questions = securityQuestions.Select(i => new SecurityQuestionModel().InjectFrom(i)).Cast<SecurityQuestionModel>();
                    UserProfileUpdate userProfileUpdate = new UserProfileUpdate();
                    userProfileUpdate.Email = registerJsonModel.Email;
                    userProfileUpdate.UserName = registerJsonModel.UserName;
                    userProfileUpdate.SecurityQuestion1 = registerJsonModel.SecurityQuestion1;
                    userProfileUpdate.SecurityQuestion2 = registerJsonModel.SecurityQuestion2;
                    userProfileUpdate.SecurityQuestion3 = registerJsonModel.SecurityQuestion3;
                    userProfileUpdate.SecurityAnswer1 = registerJsonModel.SecurityAnswer1;
                    userProfileUpdate.SecurityAnswer2 = registerJsonModel.SecurityAnswer2;
                    userProfileUpdate.SecurityAnswer3 = registerJsonModel.SecurityAnswer3;
                    userProfileUpdate.UserId = userID.Trim();
                    userProfileUpdate.Address = "NA";
                    userProfileUpdate.City = "NA";
                    userProfileUpdate.State = "NA";
                    userProfileUpdate.MobileNumber = "NA";
                    userProfileUpdate.PostalCode = "NA";
                    JsonModel jmodel = new JsonModel();
                    var changePassword = false;
                    var validateOldPassword = true;
                    string emailChange = string.Empty;
                    var user = await _userManager.FindUserDetailsByIdAsync(userProfileUpdate.UserId);
                    var userDetails = ToConvertUser.ToDataConvert(user);
                    var userUpdateProfile = new AppUser();
                    userUpdateProfile.InjectFrom(userDetails);
                    if (userUpdateProfile.FirstName != registerJsonModel.FirstName)
                    {
                        profilestatus = true;
                    }
                    else if (userUpdateProfile.LastName != registerJsonModel.LastName)
                    {
                        profilestatus = true;
                    }
                    else if (userUpdateProfile.IsDelete != registerJsonModel.IsDelete)
                    {
                        profilestatus = true;
                    }

                    if (profilestatus == false)
                    {
                        ViewBag.Status = "You have not made any changes to your profile. If needed, please do changes and then click on [SAVE & UPDATE]";
                        string msg = "You have not made any changes to" + " " + registerJsonModel.UserName + " " + "profile. If needed, please do changes and then click on [SAVE & UPDATE]";
                        return JavaScript(" $('.profilemodaldiv').modal('show');$('.profilemodal-body .error_message').empty().append('" + msg + "');");
                    }
                    userUpdateProfile.FirstName = registerJsonModel.FirstName;
                    userUpdateProfile.LastName = registerJsonModel.LastName;
                    userUpdateProfile.Email = userUpdateProfile.Email;
                    userUpdateProfile.SecondaryEmail = userProfileUpdate.SecondaryEmail;
                    userUpdateProfile.SecurityQuestion1 = userProfileUpdate.SecurityQuestion1;
                    userUpdateProfile.SecurityQuestion2 = userProfileUpdate.SecurityQuestion2;
                    userUpdateProfile.SecurityQuestion3 = userProfileUpdate.SecurityQuestion3;
                    userUpdateProfile.SecurityAnswer1 = userProfileUpdate.SecurityAnswer1;
                    userUpdateProfile.SecurityAnswer2 = userProfileUpdate.SecurityAnswer2;
                    userUpdateProfile.SecurityAnswer3 = userProfileUpdate.SecurityAnswer3;
                    userUpdateProfile.CreatedDate = user.CreatedDate;
                    userUpdateProfile.LockoutEndDateUtc = user.LockoutEndDateUtc;
                    userUpdateProfile.UpdatedDate = Convert.ToDateTime(System.DateTime.Now);
                    userUpdateProfile.PreviousEmailConfirmed = false;
                    userUpdateProfile.ChangeEmailConfirmed = false;
                    userUpdateProfile.PreviousEmailValidate = DateTime.Now;
                    userUpdateProfile.ChangeEmailValidate = DateTime.Now;
                    userUpdateProfile.Address = userProfileUpdate.Address;
                    userUpdateProfile.City = userProfileUpdate.City;
                    userUpdateProfile.State = userProfileUpdate.State;
                    userUpdateProfile.PostalCode = userProfileUpdate.PostalCode;
                    userUpdateProfile.MobileNumber = userProfileUpdate.MobileNumber;
                    userUpdateProfile.IsDelete = registerJsonModel.IsDelete;
                    var type = string.Empty;
                    if (userProfileUpdate.Password != null)
                    {
                        validateOldPassword = await _userManager.IsCheckPasswordAsync(user, userProfileUpdate.Password);
                    }
                    if (validateOldPassword)
                    {
                        if ((userProfileUpdate.Password != null) && userProfileUpdate.NewPassword != null)
                        {
                            if (((userProfileUpdate.Password != userProfileUpdate.NewPassword) ||
                                 (userProfileUpdate.Password == userProfileUpdate.NewPassword))
                                && (userProfileUpdate.NewPassword != string.Empty))
                            {
                                await ProfilePasswordUpdate(userProfileUpdate.UserId, userProfileUpdate.Password,
                                        userProfileUpdate.NewPassword);
                                changePassword = true;
                            }
                        }
                        var result = await _userManager.UpdateAsync(userUpdateProfile, userProfileUpdate.UserId);
                        if (!result.Succeeded)
                        {
                            ViewBag.Status = "Error in Updating";
                            return JavaScript(" $('.profilemodaldiv').modal('show');$('.profilemodal-body .error_message').empty().append('Your profile is not updated');");
                        }
                        if (user.Email == userProfileUpdate.Email)
                        {
                        }
                        else { }
                    }
                    else
                        ViewBag.Status = "Error in Updating";
                    if (changePassword)
                    {
                        ViewBag.Status = "Profile updated successfully";
                        return JavaScript(" $('.profilemodaldiv').modal('show');$('.profilemodal-body .error_message').empty().append('" + Updatemsg + "');");
                    }
                    // ReSharper disable once RedundantIfElseBlock
                    else
                    {
                        ViewBag.Status = "Profile updated successfully";
                        return JavaScript(" $('.profilemodaldiv').modal('show');$('.profilemodal-body .error_message').empty().append('" + Updatemsg + "');");
                    }
                    // ReSharper disable once HeuristicUnreachableCode
                    return View(registerJsonModel);
                }
                // ReSharper disable once RedundantIfElseBlock
                else
                { return RedirectToAction("SessionExpiry", "Account"); }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occurred in CustomerProfile PostMethod:-", ex);
            }
        }
        /// <summary>
        /// This method is used to display the login user Profile
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Audit]
        [Authorize]
        [ConcurrentLogin]
        public async Task<ActionResult> MyProfile()
        {
            try
            {
                var userAccountById = new UserAccountById();
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                userAccountById.UserId = Session["UserId"].ToString();
                RegisterJsonModel registerJsonModel = new RegisterJsonModel();
                var userIDs = await _userManager.FindUserDetailsByIdAsync(userAccountById.UserId);
                registerJsonModel.InjectFrom(userIDs);
                registerJsonModel.UserId = userAccountById.UserId;
                registerJsonModel.UserName = userIDs.UserName;
                registerJsonModel.Email = userIDs.Email;
                ViewBag.Status = "";
                ViewBag.PasswordStatus = "";
                return View(registerJsonModel);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occurred in MyProfile GetMethod:-", ex);
            }
        }
        /// <summary>
        /// This method is used to password update
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        [Audit]
        [Authorize]
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
        /// This method is used to update profile for login user
        /// </summary>
        /// <param name="registerJsonModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Audit]
        [Authorize]
        public async Task<ActionResult> MyProfile(RegisterJsonModel registerJsonModel)
        {
            try
            {
                if (Businesscentercommon.ValidateSessionResult())
                {
                    bool profilestatus = false;
                    var userProfileUpdate = new UserProfileUpdate { UserId = registerJsonModel.UserId };
                    var user = await _userManager.FindUserDetailsByIdAsync(userProfileUpdate.UserId);
                    registerJsonModel.Email = user.Email;
                    registerJsonModel.UserName = user.UserName;
                    userProfileUpdate.Email = user.Email;
                    userProfileUpdate.UserName = user.UserName;
                    userProfileUpdate.Password = registerJsonModel.Password;
                    userProfileUpdate.NewPassword = registerJsonModel.NewPassword;
                    userProfileUpdate.ConfirmPassword = registerJsonModel.ConfirmPassword;
                    userProfileUpdate.SecurityQuestion1 = "NA";
                    userProfileUpdate.SecurityQuestion2 = "NA";
                    userProfileUpdate.SecurityQuestion3 = "NA";
                    userProfileUpdate.SecurityAnswer1 = "NA";
                    userProfileUpdate.SecurityAnswer2 = "NA";
                    userProfileUpdate.SecurityAnswer3 = "NA";
                    var changePassword = false;
                    var validateOldPassword = true;
                    string emailChange = string.Empty;
                    string status = string.Empty;
                    var userDetails = ToConvertUser.ToDataConvert(user);
                    var userUpdateProfile = new AppUser();
                    userUpdateProfile.InjectFrom(userDetails);
                    if (userUpdateProfile.FirstName != registerJsonModel.FirstName)
                    {
                        profilestatus = true;
                    }
                    else if (userUpdateProfile.LastName != registerJsonModel.LastName)
                    {
                        profilestatus = true;
                    }
                    else if (userUpdateProfile.Address != registerJsonModel.Address)
                    {
                        profilestatus = true;
                    }
                    else if (userUpdateProfile.City != registerJsonModel.City)
                    {
                        profilestatus = true;
                    }
                    else if (userUpdateProfile.State != registerJsonModel.State)
                    {
                        profilestatus = true;
                    }
                    else if (userUpdateProfile.MobileNumber != registerJsonModel.MobileNumber)
                    {
                        profilestatus = true;
                    }
                    else if (userUpdateProfile.PostalCode != registerJsonModel.PostalCode)
                    {
                        profilestatus = true;
                    }
                    else if (userUpdateProfile.IsDelete != registerJsonModel.IsDelete)
                    {
                        profilestatus = true;
                    }
                    else if (registerJsonModel.Password != null)
                    {
                        profilestatus = true;
                    }
                    else if (registerJsonModel.NewPassword != null)
                    {
                        profilestatus = true;
                    }
                    else if (registerJsonModel.ConfirmPassword != null)
                    {
                        profilestatus = true;
                    }
                    if (profilestatus == false)
                    {
                        ViewBag.PasswordStatus = "You have not made any changes to your profile. If needed, please do changes and then click on [SAVE & UPDATE]";
                        return JavaScript(" $('.profilemodaldiv').modal('show');$('.profilemodal-body .error_message').empty().append('You have not made any changes to your profile. If needed, please do changes and then click on [SAVE & UPDATE]');");
                    }
                    userUpdateProfile.FirstName = registerJsonModel.FirstName;
                    userUpdateProfile.LastName = registerJsonModel.LastName;
                    userUpdateProfile.SecondaryEmail = userProfileUpdate.SecondaryEmail;
                    userUpdateProfile.SecurityQuestion1 = userProfileUpdate.SecurityQuestion1;
                    userUpdateProfile.SecurityQuestion2 = userProfileUpdate.SecurityQuestion2;
                    userUpdateProfile.SecurityQuestion3 = userProfileUpdate.SecurityQuestion3;
                    userUpdateProfile.SecurityAnswer1 = userProfileUpdate.SecurityAnswer1;
                    userUpdateProfile.SecurityAnswer2 = userProfileUpdate.SecurityAnswer2;
                    userUpdateProfile.SecurityAnswer3 = userProfileUpdate.SecurityAnswer3;
                    userUpdateProfile.CreatedDate = user.CreatedDate;
                    userUpdateProfile.UpdatedDate = Convert.ToDateTime(System.DateTime.Now);
                    userUpdateProfile.PreviousEmailConfirmed = false;
                    userUpdateProfile.ChangeEmailConfirmed = false;
                    userUpdateProfile.PreviousEmailValidate = DateTime.Now;
                    userUpdateProfile.ChangeEmailValidate = DateTime.Now;
                    userUpdateProfile.Address = registerJsonModel.Address;
                    userUpdateProfile.City = registerJsonModel.City;
                    userUpdateProfile.State = registerJsonModel.State;
                    userUpdateProfile.PostalCode = registerJsonModel.PostalCode;
                    userUpdateProfile.MobileNumber = registerJsonModel.MobileNumber;
                    userUpdateProfile.IsDelete = registerJsonModel.IsDelete;
                    userUpdateProfile.LoginSessionId = user.LoginSessionId;
                    var type = string.Empty;
                    if (userProfileUpdate.Password != null)
                    {
                        validateOldPassword = await _userManager.IsCheckPasswordAsync(user, userProfileUpdate.Password);
                    }
                    if (validateOldPassword)
                    {
                        if (userProfileUpdate.Password != null)
                        {
                            if (userProfileUpdate.NewPassword == null)
                            {
                                ViewBag.Status = "To change your password, please complete Current Password, New Password and Confirm New Password fields";
                                return Json(new { status = "Passwordempty" });
                            }
                        }
                        if (userProfileUpdate.NewPassword != null)
                        {
                            if (userProfileUpdate.Password == null)
                            {
                                ViewBag.Status = "To change your password, please complete Current Password, New Password and Confirm New Password fields";
                                return Json(new { status = "Passwordempty" });
                            }
                        }
                        if (userProfileUpdate.ConfirmPassword != null)
                        {
                            if (userProfileUpdate.Password == null || userProfileUpdate.NewPassword == null)
                            {
                                ViewBag.Status = "To change your password, please complete Current Password, New Password and Confirm New Password fields";
                                return Json(new { status = "Passwordempty" });
                            }
                        }
                        if ((userProfileUpdate.Password != null) && userProfileUpdate.NewPassword != null)
                        {
                            if (userProfileUpdate.Password == userProfileUpdate.NewPassword)
                            {
                                ViewBag.Status = "Current password and New password are same. Please enter new one";
                                return Json(new { status = "Newpasswordsame" });
                            }
                            if (userProfileUpdate.ConfirmPassword != null)
                            {
                                if (userProfileUpdate.NewPassword != userProfileUpdate.ConfirmPassword)
                                {
                                    ViewBag.confirmpwdmsg = "Passwords Do Not Match";
                                    return Json(new { status = "confirmpwd" });
                                }
                            }
                            else
                            {
                                ViewBag.Status = "To change your password, please complete Current Password, New Password and Confirm New Password fields";
                                return Json(new { status = "Passwordempty" });
                            }
                            if (((userProfileUpdate.Password != userProfileUpdate.NewPassword) ||
                                 (userProfileUpdate.Password == userProfileUpdate.NewPassword))
                                && (userProfileUpdate.NewPassword != string.Empty))
                            {
                                await ProfilePasswordUpdate(userProfileUpdate.UserId, userProfileUpdate.Password,
                                        userProfileUpdate.NewPassword);
                                changePassword = true;
                            }
                        }
                        if (userProfileUpdate.NewPassword != null)
                        {
                            if (userProfileUpdate.ConfirmPassword == null)
                            {
                                ViewBag.Status = "To change your password, please complete Current Password, New Password and Confirm New Password fields";
                                return Json(new { status = "Passwordempty" });
                            }
                        }
                        var result = await _userManager.UpdateAsync(userUpdateProfile, userProfileUpdate.UserId);
                        if (!result.Succeeded)
                        {
                            status = "error";
                            ViewBag.Status = "Error in Updating";
                            return JavaScript(" $('.profilemodaldiv').modal('show');$('.profilemodal-body .error_message').empty().append('You have not made any changes to your profile. If needed, please do changes and then click on [SAVE & UPDATE]');");
                        }
                    }
                    else
                    {
                        status = "InvalidOldPassword";
                        ViewBag.Status = "Current password is incorrect";
                        return Json(new { status = "InvalidOldPassword" });
                    }
                    if (changePassword)
                    {
                        status = "success";
                        ViewBag.PasswordStatus = "Profile updated successfully";
                        Session["FullName"] = registerJsonModel.FirstName + " " + registerJsonModel.LastName;
                        return JavaScript(" $('.profilemodaldiv').modal('show');$('.profilemodal-body .error_message').empty().append('Your profile is updated');");
                    }
                    // ReSharper disable once RedundantIfElseBlock
                    else
                    {
                        status = "success";
                        ViewBag.PasswordStatus = "Profile updated successfully";
                        Session["FirstName"] = userProfileUpdate.FirstName;
                        Session["LastName"] = userProfileUpdate.LastName;
                        Session["FullName"] = registerJsonModel.FirstName + " " + registerJsonModel.LastName;
                        return JavaScript(" $('.profilemodaldiv').modal('show');$('.profilemodal-body .error_message').empty().append('Your profile is updated');");
                    }

                    // ReSharper disable once HeuristicUnreachableCode
                    return View();
                }
                // ReSharper disable once RedundantIfElseBlock
                else
                { return RedirectToAction("SessionExpiry", "Account"); }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occurred in MyProfile PostMethod:-", ex);
            }
        }
        /// <summary>
        /// This mehod is used to validate the Current Password
        /// </summary>
        /// <param name="currentPassword"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ActionResult> CurrentPasswordValidation(string currentPassword, string userId)
        {
            var validateOldPassword = false;
            var user = await _userManager.FindUserDetailsByIdAsync(userId);
            validateOldPassword = await _userManager.IsCheckPasswordAsync(user, currentPassword);
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
        /// This method is used for paging
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        [Audit]
        [Authorize]
        [ConcurrentLogin]
        public async Task<ActionResult> SearchWordsList(int? page)
        {
            string DisplayType = "ALL";
            NameValueCollection KV = HttpUtility.ParseQueryString(DisplayType);
            DateTime FromDate = DateTime.Now.AddDays(-2);
            DateTime ToDate = DateTime.Now;
            var KeywordCount = await _keywordCount.GetKeywordSearchCount(FromDate, ToDate, KV.ToString());
            var Keywords = KeywordCount.Select(i => new SearchKeywordMvcModel().InjectFrom(i)).Cast<SearchKeywordMvcModel>().ToList();
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            // ReSharper disable once Mvc.ViewNotResolved
            return View(Keywords.ToPagedList(pageNumber, pageSize));
        }
        /// <summary>
        /// This method is used to display the Regulator Counts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Audit]
        [ConcurrentLogin]
        public async Task<ActionResult> RegulatorCount()
        {
            DashBoardViewModel viewmodel = new DashBoardViewModel();
            var KeywordCount = await _keywordCount.GetDashBoardRegulatorCount();
            viewmodel.RegulatorCountList = KeywordCount.Select(i => new RegulatorCount().InjectFrom(i)).Cast<RegulatorCount>().ToList();
            viewmodel.RegulatorCountList = viewmodel.RegulatorCountList.OrderByDescending(i => i.RegularCount).ToList();
            RegulatorCount regulatorcount = new RegulatorCount();
            if (viewmodel.RegulatorCountList.Any(r => r.Regulator == "BBL"))
            {
                ViewBag.BBL = viewmodel.RegulatorCountList.Find(item => item.Regulator == "BBL").RegularCount;
            }
            else
            {
                ViewBag.BBL = 0;
            }
            if (viewmodel.RegulatorCountList.Any(r => r.Regulator == "OPLA"))
            {
                ViewBag.OPLA = viewmodel.RegulatorCountList.Find(item => item.Regulator == "OPLA").RegularCount;
            }
            else
            {
                ViewBag.OPLA = 0;
            }
            if (viewmodel.RegulatorCountList.Any(r => r.Regulator == "ABRA"))
            {
                ViewBag.ABRA = viewmodel.RegulatorCountList.Find(item => item.Regulator == "ABRA").RegularCount;
            }
            else
            {
                ViewBag.ABRA = 0;
            }
            if (viewmodel.RegulatorCountList.Any(r => r.Regulator == "CBE"))
            {
                ViewBag.CBE = viewmodel.RegulatorCountList.Find(item => item.Regulator == "CBE").RegularCount;
            }
            else
            {
                ViewBag.CBE = 0;
            }
            if (viewmodel.RegulatorCountList.Any(r => r.Regulator == "CORP"))
            {
                ViewBag.CORP = viewmodel.RegulatorCountList.Find(item => item.Regulator == "CORP").RegularCount;
            }
            else
            {
                ViewBag.CORP = 0;
            }
            if (viewmodel.RegulatorCountList.Count != 0)
            {
                ViewBag.HighestEntity = viewmodel.RegulatorCountList.OrderByDescending(i => i.RegularCount).First().Regulator;
                ViewBag.HighestCount = viewmodel.RegulatorCountList.OrderByDescending(i => i.RegularCount).First().RegularCount;
            }
            else
            {
                ViewBag.HighestEntity = "No Result";
                ViewBag.HighestCount = "";
            }
            return PartialView("_RegulatorCountPartial", viewmodel);
        }
        /// <summary>
        /// This method is used to show the Customer Details 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        [HttpGet]
        [Audit]
        [ConcurrentLogin]
        public async Task<ActionResult> CustomerDetails(string UserId, string Type)
        {
            try
            {
                UserAccountById user1 = new UserAccountById();
                user1.UserId = UserId;
                RegisterJsonModel reg = new RegisterJsonModel();
                var userIDs = await _userManager.FindUserDetailsByIdAsync(user1.UserId);
                RegisterUserModel superAdminUser = new RegisterUserModel();
                superAdminUser.FirstName = userIDs.FirstName;
                superAdminUser.LastName = userIDs.LastName;
                superAdminUser.UserName = userIDs.UserName;
                superAdminUser.Email = userIDs.Email;
                superAdminUser.Address = userIDs.Address;
                superAdminUser.City = userIDs.City;
                superAdminUser.MobileNumber = userIDs.MobileNumber;
                superAdminUser.PostalCode = userIDs.PostalCode;
                superAdminUser.State = userIDs.State;
                superAdminUser.SecurityQuestion1 = userIDs.SecurityQuestion1;
                superAdminUser.SecurityQuestion2 = userIDs.SecurityQuestion2;
                superAdminUser.SecurityQuestion3 = userIDs.SecurityQuestion3;
                superAdminUser.SecurityAnswer1 = userIDs.SecurityAnswer1;
                superAdminUser.SecurityAnswer2 = userIDs.SecurityAnswer2;
                superAdminUser.SecurityAnswer3 = userIDs.SecurityAnswer3;
                superAdminUser.IsDelete = userIDs.IsDelete;
                reg.InjectFrom(superAdminUser);
                ViewBag.Status = "";
                ViewBag.UserType = Type;
                ViewBag.Sq1 = reg.SecurityQuestion1;
                ViewBag.Sq2 = reg.SecurityQuestion2;
                ViewBag.Sq3 = reg.SecurityQuestion3;
                var securityQuestions = _securityQuestions.GetAll();
                ViewBag.Questions = securityQuestions.Select(i => new SecurityQuestionModel().InjectFrom(i)).Cast<SecurityQuestionModel>();
                return View(reg);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occurred in CustomerDetails GetMethod:-", ex);
            }
        }
        /// <summary>
        /// This method is used to inactivate Admin,SuperAdmin,Manager and Customer
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Type"></param>
        /// <param name="userRole"></param>
        /// <returns></returns>
        [HttpPost]
        [Audit]
        [Authorize]
        public async Task<ActionResult> Delete(string UserId, string Type, string userRole)
        {
            try
            {
                if (Businesscentercommon.ValidateSessionResult())
                {
                    if (ModelState.IsValid)
                    {
                        var userAccountDeleteModel = new UserAccountDeleteModel { UserId = UserId };
                        TempData["Type"] = Type;
                        var status = string.Empty;
                        var userId = userAccountDeleteModel.UserId;
                        var appUser = await _userManager.FindUserDetailsByIdAsync(userId);
                        if (appUser != null)
                        {
                            var userDetails = ToConvertUser.ToDataConvert(appUser);
                            var userUpdateProfile = new AppUser();
                            userUpdateProfile.InjectFrom(userDetails);
                            userUpdateProfile.IsDelete = true;
                            userUpdateProfile.DeleteComment = userAccountDeleteModel.DeleteComment;
                            userUpdateProfile.CreatedDate = appUser.CreatedDate;
                            userUpdateProfile.LockoutEndDateUtc = appUser.LockoutEndDateUtc;
                            userUpdateProfile.UpdatedDate = Convert.ToDateTime(System.DateTime.Now);
                            await _userManager.UpdateAsync(userUpdateProfile, appUser.Id);
                            status = "success";
                        }
                        switch (status)
                        {
                            case "success":
                                switch (userRole)
                                {
                                    case "SuperAdmin":
                                        return RedirectToAction("Home", "SuperAdmin");

                                    case "Manager":
                                        return RedirectToAction("ManagerHome", "Admin");

                                    case "Admin":
                                        return RedirectToAction("Home", "Admin");

                                    default:
                                        return RedirectToAction("CustomerHome", "Admin");
                                }
                        }
                    }
                    // ReSharper disable once Mvc.ViewNotResolved
                    return View();
                }
                else
                { return RedirectToAction("SessionExpiry", "Account"); }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occurred in Delete PostMethod:-", ex);
            }
        }
        /// <summary>
        /// This method is used to lock the account of Admin,SuperAdmin and Manager
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Type"></param>
        /// <param name="userRole"></param>
        /// <returns></returns>
        [HttpGet, ActionName("Lock")]
        [Audit]
        public async Task<ActionResult> Lock(string id, string Type, string userRole)
        {
            try
            {
                if (Businesscentercommon.ValidateSessionResult())
                {
                    if (ModelState.IsValid)
                    {
                        var userAccountDeleteModel = new UserAccountDeleteModel();
                        userAccountDeleteModel.UserId = id;
                        TempData["Type"] = Type;
                        var status = string.Empty;
                        var userId = userAccountDeleteModel.UserId;
                        var appUser = await _userManager.FindUserDetailsByIdAsync(userId);
                        var userUpdateProfile = new AppUser();
                        if (appUser != null)
                        {
                            var userDetails = ToConvertUser.ToDataConvert(appUser);
                            userUpdateProfile.InjectFrom(userDetails);
                            userUpdateProfile.LockoutEnabled = false;
                            userUpdateProfile.AccessFailedCount = 0;
                            userUpdateProfile.LockoutEndDateUtc = System.DateTime.UtcNow;
                            userUpdateProfile.CreatedDate = appUser.CreatedDate;
                            await _userManager.UpdateAsync(userUpdateProfile, appUser.Id);
                            status = "success";
                        }
                        if (userUpdateProfile.LockoutEndDateUtc != null)
                        {
                            bool chcekDate = GenaralConvertion.CheckDateExpireForLockout(userUpdateProfile.LockoutEndDateUtc.ToString(),
                                System.DateTime.UtcNow.ToString());
                        }
                        switch (status)
                        {
                            case "success":
                                switch (userRole)
                                {
                                    case "SuperAdmin":
                                        return JavaScript("");

                                    case "Admin":
                                        return JavaScript("");

                                    default:
                                        return JavaScript("");
                                }
                        }
                    }
                    // ReSharper disable once Mvc.ViewNotResolved
                    return View();


                }
                else
                { return RedirectToAction("SessionExpiry", "Account"); }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occurred in Lock GetMethod:-", ex);
            }
        }
        /// <summary>
        /// This method is used to display forgotpassword Page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Audit]
        public async Task<ActionResult> ForgotPassword()
        {
            return await Task.FromResult<ActionResult>(View());
        }
        /// <summary>
        /// This method is used to implement forgotpassword functionality
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Audit]
        public async Task<ActionResult> ForgotPassword(string UserName)
        {
            try
            {
                if (Businesscentercommon.ValidateSessionResult())
                {
                    if (ModelState.IsValid)
                    {
                        var forgotPassword = new ForgotPassword { UserName = UserName };
                        var reSendMail = new ReSendMail();
                        string userId = string.Empty;
                        string userMail = string.Empty;
                        string userCode = string.Empty;
                        string roleCount = string.Empty;
                        if (ModelState.IsValid)
                        {
                            var user = await _userManager.FindByNameAsync(forgotPassword.UserName);
                            if (user == null)
                            {
                                reSendMail.Status = "The username you entered is not registered in our database. Double check the username and try again";
                                TempData["StatusMsg"] = reSendMail.Status;
                                return RedirectToAction("ForgotPassword", "Account");
                            }
                            // roleCount = user.Roles.Count.ToString();
                            var userroles = await _userManager.GetRolesAsync(user.Id);

                            if (userroles.Contains("Employee"))
                            {
                                reSendMail.Status = "Access is denied for the above user into this portal";
                                TempData["StatusMsg"] = reSendMail.Status;
                                return RedirectToAction("ForgotPassword", "Account");
                            }

                            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user.Id)))
                            {
                                reSendMail.Status = "The username you entered is not registered in our database. Double check the username and try again";
                                TempData["StatusMsg"] = reSendMail.Status;
                                return RedirectToAction("ForgotPassword", "Account");
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
                                        (expireDate.AddMinutes(5) > System.DateTime.UtcNow))
                                    {
                                    }
                                    else
                                    {
                                        var code = await _userManager.GeneratePasswordResetTokenAsync(user.Id);
                                        userCode = code;
                                        userId = user.Id.ToString();
                                        AdminForgotPasswordMailSendNew(user.Id, code, user.Email, user.FirstName + " " + user.LastName);
                                        var getUser = _userManager.FindUserDetailsByIdAsync(user.Id);
                                        var regUserDetails = new AppUser();
                                        regUserDetails.InjectFrom(getUser);
                                        regUserDetails.IsForgot = true;
                                        regUserDetails.UpdatedDate = System.DateTime.Now;
                                        userMail = user.Email;
                                        reSendMail.Status = "success";
                                        ViewBag.Statusmsg = user.FirstName + " " + user.LastName;
                                    }
                                }
                                else if ((user.IsActive == true) && (user.IsDelete == true))
                                {
                                    reSendMail.Status = "Account has been deleted";
                                    TempData["StatusMsg"] = reSendMail.Status;
                                    return RedirectToAction("ForgotPassword", "Account");
                                }
                                else if ((user.IsActive == false) && (user.IsDelete == false))
                                {
                                    reSendMail.Status = "Inactive";
                                    return RedirectToAction("ForgotPassword", "Account");
                                }
                            }
                        }
                        if (reSendMail.Status == "success")
                        {
                            return View("ForgotpasswordstausNew", reSendMail);
                        }
                    }
                    return View();
                }
                else
                { return RedirectToAction("SessionExpiry", "Account"); }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occurred in ForgotPassword PostMethod:-", ex);
            }
        }

        [HttpGet]
        [Audit]
        [Authorize]
        [ConcurrentLogin]
        public ActionResult DeleteStatus()
        {
            return View();
        }
        /// <summary>
        /// This method is used to clear the sessions
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SessionExpiry()
        {
            Session["Login"] = null;
            Session["UserName"] = null;
            ViewBag.StatusMsg = "";
            ViewData["mesage"] = "";
            Session["count"] = 0;
            return View();
        }
        /// <summary>
        /// This method is used to validate the conncurrentLogins
        /// </summary>
        /// <returns></returns>
        public string ValidateConcurrentLogin()
        {
            var validate = Businesscentercommon.ValidateSessionResult();
            return validate.ToString().ToUpper();
        }
    }
}
