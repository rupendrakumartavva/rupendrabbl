using BusinessCenter.Api.Common;
using BusinessCenter.Api.Models;
using BusinessCenter.Email;
using BusinessCenter.Identity.IdentityModels;
using BusinessCenter.Identity.Interfaces;
using BusinessCenter.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace BusinessCenter.Api.Controllers
{
    [System.Web.Http.RoutePrefix("api/Admin")]
    public class AdminController : ApiController
    {
        private readonly IEmailTemplate _userMail;
        private readonly IUserManager _userManager;
        private readonly ISecurityQuestionsService _securityQuestions;
        public static IEnumerable<UserViewModel> _getUserDetails;
        private readonly IUserRoleManager _userRoleManager;

        public AdminController(IUserManager userManager, IEmailTemplate userMail,
             ISecurityQuestionsService service, IUserRoleManager userRoleManager)
        {
            _userRoleManager = userRoleManager;
            _userManager = userManager;
            _userMail = userMail;
            _securityQuestions = service;
        }
        
       

        [System.Web.Http.HttpPost]
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.Route("profile")]
        [ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> Profile(UserAccountById userDetail)
        {
            try
            {
                var userIDs = await _userManager.FindUserDetailsByIdAsync(userDetail.UserId);
                if (userIDs == null)
                {
                    return NotFound();
                }
                var adminUser = new RegisterUserModel
                {
                    FirstName = userIDs.FirstName,
                    LastName = userIDs.LastName,
                    UserName = userIDs.UserName,
                    Email = userIDs.Email,
                    Address = userIDs.Address,
                    City = userIDs.City,
                    MobileNumber = userIDs.MobileNumber,
                    PostalCode = userIDs.PostalCode,
                    State = userIDs.State
                };
                return Json(new { UserName = userIDs.UserName, Email = userIDs.Email.ToLower().Trim(), FirstName = userIDs.FirstName, LastName = userIDs.LastName, Address = userIDs.Address, City = userIDs.City, MobileNumber = userIDs.MobileNumber, PostalCode = userIDs.PostalCode, State = userIDs.State });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.Route("UserTypeBasedList")]
        [ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> EmployeeDetails(UserTypeModel usertypemodel)
        {
            try
            {
                var userIDs = await _userManager.AllUsers();
                if (userIDs == null)
                {
                    return NotFound();
                }
                List<UserViewModel> employeeAdminUser = new List<UserViewModel>();
                foreach (var items in userIDs)
                {
                    UserViewModel getListOfUsers = new UserViewModel();
                    var userRoles = await _userManager.GetRolesAsync(items.Id);
                    getListOfUsers.UserId = items.Id;
                    getListOfUsers.FirstName = items.FirstName;
                    getListOfUsers.LastName = items.LastName;
                    getListOfUsers.Email = items.Email;
                    getListOfUsers.IsActive = items.IsActive;
                    getListOfUsers.LockoutEnabled = items.LockoutEnabled;
                    getListOfUsers.LockoutEndDateUtc = items.LockoutEndDateUtc;
                    getListOfUsers.UserName = items.UserName;
                    getListOfUsers.LastLoginDateandTime = items.LastLoginDateandTime;
                    getListOfUsers.SecurityQuestion1 = items.SecurityQuestion1;
                    getListOfUsers.SecurityQuestion2 = items.SecurityQuestion2;
                    getListOfUsers.SecurityQuestion3 = items.SecurityQuestion3;
                    getListOfUsers.SecurityAnswer1 = items.SecurityAnswer1;
                    getListOfUsers.SecurityAnswer2 = items.SecurityAnswer2;
                    getListOfUsers.SecurityAnswer3 = items.SecurityAnswer3;
                    getListOfUsers.IsDelete = items.IsDelete;
                    if (usertypemodel.UserStatus == "All")
                    {
                        if ((userRoles.Contains("Employee")))
                        {
                            if ((userRoles.Count == 1))
                            {
                                employeeAdminUser.Add(getListOfUsers);
                            }
                        }
                    }
                    else if (usertypemodel.UserStatus == "Active")
                    {
                        if ((userRoles.Contains("Employee") && (items.IsDelete == false)))
                        {
                            if ((userRoles.Count == 1))
                            {
                                employeeAdminUser.Add(getListOfUsers);
                            }
                        }
                    }
                    else
                    {
                        if ((userRoles.Contains("Employee") && (items.IsDelete == true)))
                        {
                            if ((userRoles.Count == 1))
                            {
                                employeeAdminUser.Add(getListOfUsers);
                            }
                        }
                    }
                }
                _getUserDetails = employeeAdminUser.OrderByDescending(x => x.CreatedDate);
                return Ok(_getUserDetails);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.Route("UserDetails")]
        [ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> UserDetails()
        {
            try
            {
                var userIDs = await _userManager.AllUsers();

                if (userIDs == null)
                {
                    return NotFound();
                }

                List<UserViewModel> EmployeeAdminUser = new List<UserViewModel>();
                foreach (var items in userIDs)
                {
                    UserViewModel getListOfUsers = new UserViewModel();

                    var userRoles = await _userManager.GetRolesAsync(items.Id);
                    getListOfUsers.UserId = items.Id;
                    getListOfUsers.FirstName = items.FirstName;
                    getListOfUsers.LastName = items.LastName;
                    getListOfUsers.Email = items.Email;

                    getListOfUsers.IsActive = items.IsActive;
                    getListOfUsers.LockoutEnabled = items.LockoutEnabled;
                    getListOfUsers.LockoutEndDateUtc = items.LockoutEndDateUtc;

                    getListOfUsers.UserName = items.UserName;
                    getListOfUsers.LastLoginDateandTime = items.LastLoginDateandTime;

                    getListOfUsers.SecurityQuestion1 = items.SecurityQuestion1;
                    getListOfUsers.SecurityQuestion2 = items.SecurityQuestion2;
                    getListOfUsers.SecurityQuestion3 = items.SecurityQuestion3;

                    getListOfUsers.SecurityAnswer1 = items.SecurityAnswer1;
                    getListOfUsers.SecurityAnswer2 = items.SecurityAnswer2;
                    getListOfUsers.SecurityAnswer3 = items.SecurityAnswer3;
                    if ((userRoles.Contains("Employee") && (items.IsDelete == true)))
                    {
                        if ((userRoles.Count == 1))
                        {
                            EmployeeAdminUser.Add(getListOfUsers);
                        }
                    }
                }
                _getUserDetails = EmployeeAdminUser;

                return Ok(EmployeeAdminUser);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //TODO:Need to Modify this with get only count.

        [System.Web.Http.HttpGet]
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.Route("UsersCount")]
        [ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> CountofUsers()
        {
            try
            {
                var userIDs = await _userManager.AllUsers();
                if (userIDs == null)
                {
                    return NotFound();
                }
                List<UserViewModel> EmployeeAdminUser = new List<UserViewModel>();
                foreach (var items in userIDs)
                {
                    UserViewModel getListOfUsers = new UserViewModel();
                    var userRoles = await _userManager.GetRolesAsync(items.Id);
                    getListOfUsers.UserId = items.Id;
                    getListOfUsers.FirstName = items.FirstName;
                    getListOfUsers.LastName = items.LastName;
                    getListOfUsers.Email = items.Email;
                    getListOfUsers.IsActive = items.IsActive;
                    getListOfUsers.LockoutEnabled = items.LockoutEnabled;
                    getListOfUsers.LockoutEndDateUtc = items.LockoutEndDateUtc;
                    getListOfUsers.UserName = items.UserName;
                    getListOfUsers.LastLoginDateandTime = items.LastLoginDateandTime;
                    getListOfUsers.SecurityQuestion1 = items.SecurityQuestion1;
                    getListOfUsers.SecurityQuestion2 = items.SecurityQuestion2;
                    getListOfUsers.SecurityQuestion3 = items.SecurityQuestion3;
                    getListOfUsers.SecurityAnswer1 = items.SecurityAnswer1;
                    getListOfUsers.SecurityAnswer2 = items.SecurityAnswer2;
                    getListOfUsers.SecurityAnswer3 = items.SecurityAnswer3;
                    if ((userRoles.Contains("Employee") && (items.IsDelete == false)))
                    {
                        if ((userRoles.Count == 1))
                        {
                            EmployeeAdminUser.Add(getListOfUsers);
                        }
                    }
                }
                _getUserDetails = EmployeeAdminUser;
                int count = EmployeeAdminUser.Count;
                return Json(new { Count = count.ToString() });
            }
            catch (Exception)
            {
                throw;
            }
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.Route("TodayRegisteredUsersforSuperAdmin")]
        public async Task<IHttpActionResult> TodayRegisteredUsersforSuperAdmin()
        {
            int userCount = 0;
            int adminCount = 0;
            int superadmincount = 0;
            try
            {
                var userIDs = await _userManager.AllUsers();
                DateTime date = DateTime.Now;

                DateTime startdate = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, DateTime.Now.Date.Day, 00, 00, 00, 000);
                DateTime fromdate = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, DateTime.Now.Date.Day, 23, 59, 59, 999);

                var abc = from users in userIDs where users.CreatedDate >= startdate && users.CreatedDate <= fromdate && users.IsDelete == false select users;

                int i = abc.Count();

                if (abc == null)
                {
                    return NotFound();
                }
                List<UserViewModel> empUser = new List<UserViewModel>();
                List<UserViewModel> AdminUser = new List<UserViewModel>();
                List<UserViewModel> superAdminUser = new List<UserViewModel>();
                foreach (var items in abc)
                {
                    UserViewModel getListOfUsers = new UserViewModel();

                    var userRoles = await _userManager.GetRolesAsync(items.Id);

                   
                    if (userRoles.Contains("Employee"))
                    {
                        if (userRoles.Count == 1)
                        {
                            empUser.Add(getListOfUsers);
                            userCount = empUser.Count;
                        }
                    }
                    if (userRoles.Contains("Admin"))
                    {
                        if (userRoles.Count == 2)
                        {
                            AdminUser.Add(getListOfUsers);
                            adminCount = AdminUser.Count;
                        }
                    }
                    if (userRoles.Contains("Super Admin"))
                    {
                        if (userRoles.Count == 3)
                        {
                            superAdminUser.Add(getListOfUsers);
                            superadmincount = superAdminUser.Count;
                        }
                    }
                }
                _getUserDetails = superAdminUser;

                // int count = superAdminUser.Count;
                return Json(new { Ecount = userCount.ToString(), Acount = adminCount.ToString(), Scount = superadmincount.ToString() });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.Route("TodayDeletedUsersforSuperadmin")]
        public async Task<IHttpActionResult> TodayDeletedUsersforSuperadmin()
        {
            int userCount = 0;
            int adminCount = 0;
            int superadmincount = 0;
            try
            {
                var userIDs = await _userManager.AllUsers();
                DateTime date = DateTime.Now;
                DateTime startdate = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, DateTime.Now.Date.Day, 00, 00, 00, 000);
                DateTime fromdate = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, DateTime.Now.Date.Day, 23, 59, 59, 999);
                var abc = from users in userIDs where users.UpdatedDate >= startdate && users.UpdatedDate <= fromdate && users.IsDelete == true select users;
                int i = abc.Count();
                List<UserViewModel> superAdminUser = new List<UserViewModel>();
                List<UserViewModel> empUser = new List<UserViewModel>();
                List<UserViewModel> AdminUser = new List<UserViewModel>();
                foreach (var items in abc)
                {
                    UserViewModel getListOfUsers = new UserViewModel();
                    var userRoles = await _userManager.GetRolesAsync(items.Id);

                    if ((userRoles.Contains("Super Admin")))
                    {
                        if ((userRoles.Count == 3))
                        {
                            superAdminUser.Add(getListOfUsers);
                            superadmincount = superAdminUser.Count;
                        }
                    }
                    if (userRoles.Contains("Admin"))
                    {
                        if (userRoles.Count == 2)
                        {
                            AdminUser.Add(getListOfUsers);
                            adminCount = AdminUser.Count;
                        }
                    }
                    if (userRoles.Contains("Employee"))
                    {
                        if (userRoles.Count == 1)
                        {
                            empUser.Add(getListOfUsers);
                            userCount = empUser.Count;
                        }
                    }
                }
                _getUserDetails = superAdminUser;

               

                return Json(new { Ecount = userCount.ToString(), Acount = adminCount.ToString(), Scount = superadmincount.ToString() });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.Route("TodayRegisteredUsersforAdmin")]
        public async Task<IHttpActionResult> TodayRegisteredUsersforAdmin()
        {
            int userCount = 0;
            int adminCount = 0;
            int superadmincount = 0;
            try
            {
                var userIDs = await _userManager.AllUsers();
                DateTime date = DateTime.Now;

                DateTime startdate = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, DateTime.Now.Date.Day, 00, 00, 00, 000);
                DateTime fromdate = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, DateTime.Now.Date.Day, 23, 59, 59, 999);

                var abc = from users in userIDs where users.CreatedDate >= startdate && users.CreatedDate <= fromdate && users.IsDelete == false select users;

                int i = abc.Count();

                if (abc == null)
                {
                    return NotFound();
                }
                List<UserViewModel> empUser = new List<UserViewModel>();
                List<UserViewModel> adminUser = new List<UserViewModel>();
                List<UserViewModel> superAdminUser = new List<UserViewModel>();
                foreach (var items in abc)
                {
                    UserViewModel getListOfUsers = new UserViewModel();

                    var userRoles = await _userManager.GetRolesAsync(items.Id);

                    if (userRoles.Contains("Employee"))
                    {
                        if (userRoles.Count == 1)
                        {
                            empUser.Add(getListOfUsers);
                            userCount = empUser.Count;
                        }
                    }
                    if (userRoles.Contains("Admin"))
                    {
                        if (userRoles.Count == 2)
                        {
                            adminUser.Add(getListOfUsers);
                            adminCount = adminUser.Count;
                        }
                    }
                    if (userRoles.Contains("Super Admin"))
                    {
                        if (userRoles.Count == 3)
                        {
                            superAdminUser.Add(getListOfUsers);
                            superadmincount = superAdminUser.Count;
                        }
                    }
                }
                _getUserDetails = superAdminUser;

                // int count = superAdminUser.Count;
                return Json(new { Ecount = userCount.ToString(), Acount = adminCount.ToString(), Scount = superadmincount.ToString() });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.Route("TodayDeletedUsersforAdmin")]
        public async Task<IHttpActionResult> TodayDeletedUsersforAdmin()
        {
            int userCount = 0;
            int adminCount = 0;
            int superadmincount = 0;
            try
            {
                var userIDs = await _userManager.AllUsers();
                DateTime date = DateTime.Now;

                DateTime startdate = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, DateTime.Now.Date.Day, 00, 00, 00, 000);
                DateTime fromdate = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, DateTime.Now.Date.Day, 23, 59, 59, 999);

                var abc = from users in userIDs where users.UpdatedDate >= startdate && users.UpdatedDate <= fromdate && users.IsDelete == true select users;

                var appUsers = abc as AppUser[] ?? abc.ToArray();

                int i = appUsers.Count();

                List<UserViewModel> superAdminUser = new List<UserViewModel>();
                List<UserViewModel> empUser = new List<UserViewModel>();
                List<UserViewModel> AdminUser = new List<UserViewModel>();
                foreach (var items in appUsers)
                {
                    UserViewModel getListOfUsers = new UserViewModel();

                    var userRoles = await _userManager.GetRolesAsync(items.Id);

                    if (userRoles.Contains("Employee"))
                    {
                        if (userRoles.Count == 1)
                        {
                            empUser.Add(getListOfUsers);
                            userCount = empUser.Count;
                        }
                    }
                    if (userRoles.Contains("Admin"))
                    {
                        if (userRoles.Count == 2)
                        {
                            AdminUser.Add(getListOfUsers);
                            adminCount = AdminUser.Count;
                        }
                    }
                    if (userRoles.Contains("Super Admin"))
                    {
                        if (userRoles.Count == 3)
                        {
                            superAdminUser.Add(getListOfUsers);
                            superadmincount = superAdminUser.Count;
                        }
                    }
                }
                _getUserDetails = superAdminUser;

                //int count = superAdminUser.Count;
                return Json(new { Ecount = userCount.ToString(), Acount = adminCount.ToString(), Scount = superadmincount.ToString() });
            }
            catch (Exception)
            {
                throw;
            }
        }



        // Below 4 methods are for Superadmin and admin users count

        [System.Web.Http.HttpGet]
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.Route("ActiveUsersforSuperadmin")]
        [ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> ActiveUsersforSuperadmin()
        {
            int empCount = 0;
            int adminCount = 0;
            int superadminCount = 0;
            List<UserViewModel> employeeAdminUser = new List<UserViewModel>();
            var AdminUser = new List<UserViewModel>();
            var SuperAdminUser = new List<UserViewModel>();
            try
            {
                var userIDs = await _userManager.AllUsers();


                if (userIDs == null)
                {
                    return NotFound();
                }


                foreach (var items in userIDs)
                {
                    var getListOfUsers = new UserViewModel();

                    var userRoles = await _userManager.GetRolesAsync(items.Id);
                    getListOfUsers.UserId = items.Id;
                    getListOfUsers.FirstName = items.FirstName;
                    getListOfUsers.LastName = items.LastName;
                    getListOfUsers.Email = items.Email;

                    getListOfUsers.IsActive = items.IsActive;
                    getListOfUsers.LockoutEnabled = items.LockoutEnabled;
                    getListOfUsers.LockoutEndDateUtc = items.LockoutEndDateUtc;

                    getListOfUsers.UserName = items.UserName;
                    getListOfUsers.LastLoginDateandTime = items.LastLoginDateandTime;

                    getListOfUsers.SecurityQuestion1 = items.SecurityQuestion1;
                    getListOfUsers.SecurityQuestion2 = items.SecurityQuestion2;
                    getListOfUsers.SecurityQuestion3 = items.SecurityQuestion3;

                    getListOfUsers.SecurityAnswer1 = items.SecurityAnswer1;
                    getListOfUsers.SecurityAnswer2 = items.SecurityAnswer2;
                    getListOfUsers.SecurityAnswer3 = items.SecurityAnswer3;
                    if ((userRoles.Contains("Employee") && (items.IsDelete == false)))
                    {

                        if (userRoles.Count == 1)
                        {
                            employeeAdminUser.Add(getListOfUsers);
                            empCount = employeeAdminUser.Count;

                        }

                    }
                    if ((userRoles.Contains("Admin") && (items.IsDelete == false)))
                    {


                        if (userRoles.Count == 2)
                        {
                            AdminUser.Add(getListOfUsers);
                            adminCount = AdminUser.Count;

                        }

                    }
                    if ((userRoles.Contains("Super Admin") && (items.IsDelete == false)))
                    {

                        if ((userRoles.Count == 3))
                        {
                            SuperAdminUser.Add(getListOfUsers);
                            superadminCount = SuperAdminUser.Count;
                        }
                    }


                }
                // _getUserDetails = employeeAdminUser;



                return Json(new { Ecount = empCount.ToString(), Acount = adminCount.ToString(), Scount = superadminCount.ToString() });
            }
            catch (Exception)
            {

                throw;
            }
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.Route("InActiveUsersforSuperadmin")]
        [ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> InActiveUsersforSuperadmin()
        {
            int empCount = 0;
            int adminCount = 0;
            int superadminCount = 0;
            try
            {
                var userIDs = await _userManager.AllUsers();


                if (userIDs == null)
                {
                    return NotFound();
                }



                var employeeAdminUser = new List<UserViewModel>();
                var AdminUser = new List<UserViewModel>();
                var SuperAdminUser = new List<UserViewModel>();

                foreach (var items in userIDs)
                {
                    UserViewModel getListOfUsers = new UserViewModel();

                    var userRoles = await _userManager.GetRolesAsync(items.Id);
                    getListOfUsers.UserId = items.Id;
                    getListOfUsers.FirstName = items.FirstName;
                    getListOfUsers.LastName = items.LastName;
                    getListOfUsers.Email = items.Email;

                    getListOfUsers.IsActive = items.IsActive;
                    getListOfUsers.LockoutEnabled = items.LockoutEnabled;
                    getListOfUsers.LockoutEndDateUtc = items.LockoutEndDateUtc;

                    getListOfUsers.UserName = items.UserName;
                    getListOfUsers.LastLoginDateandTime = items.LastLoginDateandTime;

                    getListOfUsers.SecurityQuestion1 = items.SecurityQuestion1;
                    getListOfUsers.SecurityQuestion2 = items.SecurityQuestion2;
                    getListOfUsers.SecurityQuestion3 = items.SecurityQuestion3;

                    getListOfUsers.SecurityAnswer1 = items.SecurityAnswer1;
                    getListOfUsers.SecurityAnswer2 = items.SecurityAnswer2;
                    getListOfUsers.SecurityAnswer3 = items.SecurityAnswer3;
                    if ((userRoles.Contains("Employee") && (items.IsDelete == true)))
                    {

                        if ((userRoles.Count == 1))
                        {
                            employeeAdminUser.Add(getListOfUsers);
                            empCount = employeeAdminUser.Count;


                        }
                    }
                    if ((userRoles.Contains("Admin") && (items.IsDelete == true)))
                    {

                        if ((userRoles.Count == 2))
                        {
                            AdminUser.Add(getListOfUsers);
                            adminCount = AdminUser.Count;

                        }
                    }

                    if ((userRoles.Contains("Super Admin") && (items.IsDelete == true)))
                    {

                        if ((userRoles.Count == 3))
                        {
                            SuperAdminUser.Add(getListOfUsers);
                            superadminCount = SuperAdminUser.Count;
                        }
                    }

                }
                _getUserDetails = employeeAdminUser;



                //int count = employeeAdminUser.Count;
                return Json(new { Ecount = empCount.ToString(), Acount = adminCount.ToString(), Scount = superadminCount.ToString() });
            }
            catch (Exception)
            {

                throw;
            }


        }



        [System.Web.Http.HttpGet]
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.Route("ActiveUsersforadmin")]
        [ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> ActiveUsersforadmin()
        {
            int empCount = 0;
            int adminCount = 0;
            int superadminCount = 0;
            try
            {
                var userIDs = await _userManager.AllUsers();
                if (userIDs == null)
                {
                    return NotFound();
                }

                var employeeAdminUser = new List<UserViewModel>();
                var AdminUser = new List<UserViewModel>();
                var SuperAdminUser = new List<UserViewModel>();
                foreach (var items in userIDs)
                {
                    var getListOfUsers = new UserViewModel();

                    var userRoles = await _userManager.GetRolesAsync(items.Id);
                    getListOfUsers.UserId = items.Id;
                    getListOfUsers.FirstName = items.FirstName;
                    getListOfUsers.LastName = items.LastName;
                    getListOfUsers.Email = items.Email;

                    getListOfUsers.IsActive = items.IsActive;
                    getListOfUsers.LockoutEnabled = items.LockoutEnabled;
                    getListOfUsers.LockoutEndDateUtc = items.LockoutEndDateUtc;

                    getListOfUsers.UserName = items.UserName;
                    getListOfUsers.LastLoginDateandTime = items.LastLoginDateandTime;

                    getListOfUsers.SecurityQuestion1 = items.SecurityQuestion1;
                    getListOfUsers.SecurityQuestion2 = items.SecurityQuestion2;
                    getListOfUsers.SecurityQuestion3 = items.SecurityQuestion3;

                    getListOfUsers.SecurityAnswer1 = items.SecurityAnswer1;
                    getListOfUsers.SecurityAnswer2 = items.SecurityAnswer2;
                    getListOfUsers.SecurityAnswer3 = items.SecurityAnswer3;
                    if ((userRoles.Contains("Employee") && (items.IsDelete == false)))
                    {

                        if (userRoles.Count == 1)
                        {
                            employeeAdminUser.Add(getListOfUsers);
                            empCount = employeeAdminUser.Count;

                        }

                    }
                    if ((userRoles.Contains("Admin") && (items.IsDelete == false)))
                    {


                        if (userRoles.Count == 2)
                        {
                            AdminUser.Add(getListOfUsers);
                            adminCount = AdminUser.Count;

                        }

                    }
                    if ((userRoles.Contains("Super Admin") && (items.IsDelete == false)))
                    {

                        if ((userRoles.Count == 3))
                        {
                            SuperAdminUser.Add(getListOfUsers);
                            superadminCount = SuperAdminUser.Count;
                        }
                    }


                }
                // _getUserDetails = employeeAdminUser;



                return Json(new { Ecount = empCount.ToString(), Acount = adminCount.ToString(), Scount = superadminCount.ToString() });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.Route("InActiveUsersforadmin")]
        [ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> InActiveUsersforadmin()
        {
            int empCount = 0;
            int adminCount = 0;
            int superadminCount = 0;
            try
            {
                var userIDs = await _userManager.AllUsers();
                if (userIDs == null)
                {
                    return NotFound();
                }
                var employeeAdminUser = new List<UserViewModel>();
                var AdminUser = new List<UserViewModel>();
                var SuperAdminUser = new List<UserViewModel>();
                foreach (var items in userIDs)
                {
                    UserViewModel getListOfUsers = new UserViewModel();

                    var userRoles = await _userManager.GetRolesAsync(items.Id);
                    getListOfUsers.UserId = items.Id;
                    getListOfUsers.FirstName = items.FirstName;
                    getListOfUsers.LastName = items.LastName;
                    getListOfUsers.Email = items.Email;

                    getListOfUsers.IsActive = items.IsActive;
                    getListOfUsers.LockoutEnabled = items.LockoutEnabled;
                    getListOfUsers.LockoutEndDateUtc = items.LockoutEndDateUtc;

                    getListOfUsers.UserName = items.UserName;
                    getListOfUsers.LastLoginDateandTime = items.LastLoginDateandTime;

                    getListOfUsers.SecurityQuestion1 = items.SecurityQuestion1;
                    getListOfUsers.SecurityQuestion2 = items.SecurityQuestion2;
                    getListOfUsers.SecurityQuestion3 = items.SecurityQuestion3;

                    getListOfUsers.SecurityAnswer1 = items.SecurityAnswer1;
                    getListOfUsers.SecurityAnswer2 = items.SecurityAnswer2;
                    getListOfUsers.SecurityAnswer3 = items.SecurityAnswer3;
                    if ((userRoles.Contains("Employee") && (items.IsDelete == true)))
                    {

                        if (userRoles.Count == 1)
                        {
                            employeeAdminUser.Add(getListOfUsers);
                            empCount = employeeAdminUser.Count;

                        }

                    }
                    if ((userRoles.Contains("Admin") && (items.IsDelete == true)))
                    {


                        if (userRoles.Count == 2)
                        {
                            AdminUser.Add(getListOfUsers);
                            adminCount = AdminUser.Count;

                        }

                    }
                    if ((userRoles.Contains("Super Admin") && (items.IsDelete == true)))
                    {

                        if ((userRoles.Count == 3))
                        {
                            SuperAdminUser.Add(getListOfUsers);
                            superadminCount = SuperAdminUser.Count;
                        }
                    }


                }
                // _getUserDetails = employeeAdminUser;



                return Json(new { Ecount = empCount.ToString(), Acount = adminCount.ToString(), Scount = superadminCount.ToString() });
            }
            catch (Exception)
            {

                throw;
            }
        }





        // Below 2 methods brought from Superadmin controller

        //TODO: we will Make this some other way.with out Static Key words.

        [System.Web.Http.HttpPost]
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.Route("TypeBasedList")]
        [ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> TypeBasedList(UserTypeModel usertypemodel)
        {
            try
            {
                var userIDs = await _userManager.AllUsers();
                if (userIDs == null)
                {
                    return NotFound();
                }
                List<UserViewModel> superAdminUser = new List<UserViewModel>();
                foreach (var items in userIDs)
                {
                    UserViewModel getListOfUsers = new UserViewModel();
                    var userRoles = await _userManager.GetRolesAsync(items.Id);
                    getListOfUsers.UserId = items.Id;
                    getListOfUsers.FirstName = items.FirstName;
                    getListOfUsers.LastName = items.LastName;
                    getListOfUsers.Email = items.Email;
                    getListOfUsers.IsActive = items.IsActive;
                    getListOfUsers.LockoutEnabled = items.LockoutEnabled;
                    getListOfUsers.LockoutEndDateUtc = items.LockoutEndDateUtc;
                    getListOfUsers.UserName = items.UserName;
                    getListOfUsers.LastLoginDateandTime = items.LastLoginDateandTime;
                    getListOfUsers.SecurityQuestion1 = items.SecurityQuestion1;
                    getListOfUsers.SecurityQuestion2 = items.SecurityQuestion2;
                    getListOfUsers.SecurityQuestion3 = items.SecurityQuestion3;
                    getListOfUsers.SecurityAnswer1 = items.SecurityAnswer1;
                    getListOfUsers.SecurityAnswer2 = items.SecurityAnswer2;
                    getListOfUsers.SecurityAnswer3 = items.SecurityAnswer3;
                    getListOfUsers.CreatedDate = items.CreatedDate;
                    getListOfUsers.IsDelete = items.IsDelete;
                    if (usertypemodel.UserType == "Super Admin")
                    {
                        if (usertypemodel.UserStatus == "All")
                        {
                            if ((userRoles.Contains("Super Admin")))
                            {
                                if ((userRoles.Count == 3))
                                {
                                    superAdminUser.Add(getListOfUsers);
                                }
                            }
                        }
                        else if (usertypemodel.UserStatus == "Active")
                        {
                            if ((userRoles.Contains("Super Admin")) && (items.IsDelete == false))
                            {
                                if ((userRoles.Count == 3))
                                {
                                    superAdminUser.Add(getListOfUsers);
                                }
                            }
                        }
                        else
                        {
                            if ((userRoles.Contains("Super Admin")) && (items.IsDelete == true))
                            {
                                if ((userRoles.Count == 3))
                                {
                                    superAdminUser.Add(getListOfUsers);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (usertypemodel.UserStatus == "All")
                        {
                            if ((userRoles.Contains("Admin")))
                            {
                                if ((userRoles.Count == 2))
                                {
                                    superAdminUser.Add(getListOfUsers);
                                }
                            }
                        }
                        else if (usertypemodel.UserStatus == "Active")
                        {
                            if ((userRoles.Contains("Admin")) && (items.IsDelete == false))
                            {
                                if ((userRoles.Count == 2))
                                {
                                    superAdminUser.Add(getListOfUsers);
                                }
                            }
                        }
                        else
                        {
                            if ((userRoles.Contains("Admin")) && (items.IsDelete == true))
                            {
                                if ((userRoles.Count == 2))
                                {
                                    superAdminUser.Add(getListOfUsers);
                                }
                            }
                        }
                    }
                }
                _getUserDetails = superAdminUser.OrderByDescending(x => x.CreatedDate);
                return Ok(_getUserDetails);
            }
            catch (Exception)
            {
                throw;
            }
        }

     
        //TODO: This need to remove and redesign with Json object.

        [System.Web.Http.HttpPost]
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.Route("Userprofile")]
        [ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> UserProfile(UserAccountById userDetail)
        {
            try
            {
                var userIDs = await _userManager.FindUserDetailsByIdAsync(userDetail.UserId);
                if (userIDs == null)
                {
                    return NotFound();
                }
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
                return Json(new { UserName = userIDs.UserName, Email = userIDs.Email, FirstName = userIDs.FirstName, LastName = userIDs.LastName, Address = userIDs.Address, City = userIDs.City, MobileNumber = userIDs.MobileNumber, PostalCode = userIDs.PostalCode, State = userIDs.State, SecurityQuestion1 = userIDs.SecurityQuestion1, SecurityQuestion2 = userIDs.SecurityQuestion2, SecurityQuestion3 = userIDs.SecurityQuestion3, SecurityAnswer1 = userIDs.SecurityAnswer1, SecurityAnswer2 = userIDs.SecurityAnswer2, SecurityAnswer3 = userIDs.SecurityAnswer3, IsDelete = userIDs.IsDelete });
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}