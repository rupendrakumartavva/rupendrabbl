using BusinessCenter.Admin.Models;
using BusinessCenter.Email;
using BusinessCenter.Identity.IdentityModels;
using BusinessCenter.Identity.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Omu.ValueInjecter;
using BusinessCenter.Identity.IdentityExtension;
using BusinessCenter.Admin.Common;
using BusinessCenter.Service.Interface;
using BusinessCenter.Audits;
using BusinessCenter.Data.Interface;
using BusinessCenter.Admin.Interface;
using BusinessCenter.Data.Models;
using BusinessCenter.Admin.Filters;

namespace BusinessCenter.Admin.Controllers
{
    public class AdminController : Controller
    {
        private readonly IEmailTemplate _userMail;
        private readonly IUserManager _userManager;
        private readonly IUserRoleManager _userRoleManager;
        private ISearchKeywordService _keywordCount;
        private IUserManagerService _loginCount;
        public static IEnumerable<UserApiModel> _getUserDetails;
        private readonly IUserRepository _usersManager;
        private readonly IGridBindings _gridBindings;
        private readonly IBBLAssociateService _bblAssociateService;
       // public readonly IBusinessCenterCommon _businesscentercommon;

        public AdminController(IUserManager userManager, IEmailTemplate userMail,
            IUserRoleManager userRoleManager, ISearchKeywordService searchKeywordService,
            IUserManagerService userService, IUserRepository iuser, IGridBindings gridBindings,
               IBBLAssociateService bblAssociateService)
        {
            _userRoleManager = userRoleManager;
            _userManager = userManager;
            _userMail = userMail;
            _keywordCount = searchKeywordService;
            _loginCount = userService;
            _usersManager = iuser;
            _gridBindings = gridBindings;
             _bblAssociateService = bblAssociateService;
           //  _businesscentercommon = businesscentercommon;
          //  , IBusinessCenterCommon businesscentercommon
        }

        readonly string _angularAddress = ConfigurationManager.AppSettings["anuglarAddress"].ToString();
        /// <summary>
        /// This method is used to display the Customer DashBoard page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Audit]
        [Authorize]
        [ConcurrentLogin]
        public ActionResult CustomerDashboard(int? id)
        {
            Response.Redirect(_angularAddress + "UserSavedDashboard/?id=" + id);
            return View();
        }
        /// <summary>
        /// This method is used to shown the Admin  DashBoard Page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Audit]
        [Authorize]
        [ConcurrentLogin]
        public ActionResult Dashboard()
        {
            try
            {
                DashboardCount();
                ViewData["FullName"] = Session["FullName"];
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string AdminActive;
        public static string AdminInActive;
        public static string AdminRegister;
        public static string AdminDelete;


        public static string EmployeeActive;
        public static string EmployeeInActive;
        public static string EmployeeRegister;
        public static string EmployeeDelete;
        /// <summary>
        /// This method is used to display the counts of Active,inactive,Registered and Deleted Users
        /// </summary>
         [Authorize]
         [ConcurrentLogin]
        public void DashboardCount()
        {
            DateTime date = DateTime.Now;
            DateTime startdate = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, DateTime.Now.Date.Day, 00, 00, 00, 000);
            DateTime fromdate = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, DateTime.Now.Date.Day, 23, 59, 59, 999);




            var admin = _usersManager.GetUsersBasedOnId("ALL", 2);
            AdminActive = admin.Count(user => user.IsDelete == false).ToString();
            
            AdminInActive = admin.Count(user => user.IsDelete == true).ToString();
           
            AdminRegister = admin.Count(users => users.CreatedDate >= startdate && users.CreatedDate <= fromdate).ToString();

          
            AdminDelete = admin.Count(users => users.UpdatedDate >= startdate && users.UpdatedDate <= fromdate && users.IsDelete == true).ToString();

            var Employee = _usersManager.GetUsersBasedOnId("ALL", 3);
            EmployeeActive = Employee.Where(user => user.IsDelete == false).Count().ToString();
            EmployeeInActive = Employee.Where(user => user.IsDelete == true).Count().ToString();
            EmployeeRegister = Employee.Where(users => users.CreatedDate >= startdate &&
                    users.CreatedDate <= fromdate).Count().ToString();
            EmployeeDelete = Employee.Where(users => users.UpdatedDate >= startdate && users.UpdatedDate <= fromdate
                    && users.IsDelete == true && users.IsActive == true).Count().ToString(); 

        }
        /// <summary>
        /// This method is used to Display The counts of Active Admins and Customers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Audit]
        [Authorize]
        [ConcurrentLogin]
        protected async Task<ActionResult> DashboardActive()
        {
           try{
                ViewBag.EmpActiveUsers = EmployeeActive;
                ViewBag.AdminActiveUsers = AdminActive;
                return await Task.FromResult<ActionResult>(PartialView("_DashboardActiveusers"));
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// This method is used to Display the counts of Inactive Admins and Customers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Audit]
        [Authorize]
        [ConcurrentLogin]
        protected async Task<ActionResult> DashboardInactive()
        {
          
            try
            {

                ViewBag.EmpInActiveUsers = EmployeeInActive;
                ViewBag.AdminInActiveUsers = AdminInActive;
                return await Task.FromResult <ActionResult>(PartialView("_DashboardInactiveusers"));
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// This method is used to Display the counts of Registered Admins and Customers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Audit]
        [Authorize]
        [ConcurrentLogin]
        public async Task<ActionResult> DashboardRegister()
        {
            
            try
            {

                ViewBag.EmpTodayRegistered = EmployeeRegister;
                ViewBag.AdminTodayRegistered = AdminRegister;
                return await Task.FromResult <ActionResult>(PartialView("_DashboardRegisteredUsers"));
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// This method is used to Display the counts of Deleted Admins and Employees
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Audit]
        [Authorize]
        [ConcurrentLogin]
        public async Task<ActionResult> DashboardDelete()
        {           
            try
            {
                ViewBag.EmpTodayDeleted = EmployeeDelete;
                ViewBag.AdminTodayDeleted = AdminDelete;
                return await Task.FromResult <ActionResult>(PartialView("_DashboardDeletedUsers"));
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// This method is used to Display the Login History details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Audit]
        [ConcurrentLogin]
        public async Task<ActionResult> Dashboardloginhistory()
        {
            DateTime FromDate = DateTime.Now.AddDays(-2);
            DateTime ToDate = DateTime.Now;
            var KeywordCount = await _loginCount.GetLoginHistoryCount(FromDate, ToDate);
            var AdminKeywordCount = KeywordCount.Where(x => x.UserRole != "Super Admin");
            DashBoardViewModel viewmodel = new DashBoardViewModel();
            viewmodel.loginhistory = AdminKeywordCount.Select(i => new LoginHistoryModel().InjectFrom(i)).Cast<LoginHistoryModel>().ToList();
            foreach (var item in viewmodel.loginhistory)
            {
                if (item.UserRole == "Employee")
                {
                    item.UserRole = "User";
                }
            }
            var keygroupcount = from r in AdminKeywordCount

                                where (true)
                                orderby r.LastLoginDate.Date descending
                                group r by new { r.UserRole, r.LastLoginDate.Date } into grp
                                select new
                                {
                                    cnt = grp.Count(),
                                    Role = grp.Key.UserRole,
                                    CreatedDate = grp.Key.Date.ToString("MM/dd/yyyy"),
                                };
            viewmodel.RoleDetails = keygroupcount.Select(i => new Roledeatils().InjectFrom(i)).Cast<Roledeatils>().ToList();
            foreach (var item in viewmodel.RoleDetails)
            {
                if (item.Role == "Employee")
                {
                    item.Role = "User";
                }
            }
            return PartialView("_DashboardLoginUsers", viewmodel);
        }
        /// <summary>
        /// This method is used to display the KeyWord Counts based on Role
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Audit]
        [ConcurrentLogin]
        public async Task<ActionResult> DashboardLoginByRole()
        {
            DateTime FromDate = DateTime.Now.AddDays(-2);
            DateTime ToDate = DateTime.Now;
            var KeywordCount = await _loginCount.GetLoginHistoryCount(FromDate, ToDate);
            var AdminKeywordCount = KeywordCount.Where(x => x.UserRole != "Super Admin");
            DashBoardViewModel viewmodel = new DashBoardViewModel();
            viewmodel.loginhistory = AdminKeywordCount.Select(i => new LoginHistoryModel().InjectFrom(i)).Cast<LoginHistoryModel>().ToList();
            foreach (var item in viewmodel.loginhistory)
            {
                if (item.UserRole == "Employee")
                {
                    item.UserRole = "User";
                }
            }
            var keygroupcount = from r in AdminKeywordCount

                                where (true)
                                orderby r.LastLoginDate.Date descending
                                group r by new { r.UserRole, r.LastLoginDate.Date } into grp
                                select new
                                {
                                    cnt = grp.Count(),
                                    Role = grp.Key.UserRole,
                                    CreatedDate = grp.Key.Date.ToString("MM/dd/yyyy"),
                                };
            viewmodel.RoleDetails = keygroupcount.Select(i => new Roledeatils().InjectFrom(i)).Cast<Roledeatils>().ToList();
            foreach (var item in viewmodel.RoleDetails)
            {
                if (item.Role == "Employee")
                {
                    item.Role = "User";
                }
            }
            return PartialView("_DashboardLoginsbyRole", viewmodel);
        }
        /// <summary>
        /// This method is used to display the Number of new account creations and account deletions per day 
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Audit]
        [ConcurrentLogin]
        public async Task<ActionResult> DashboardCreatedDeleteDateWise(string roleId)
        {

            var keywordCount1 = await _loginCount.GetCreatedDeleteDateWiseCount(roleId);


            DashBoardViewModel viewmodel = new DashBoardViewModel();
     

            var keygroupcount = from y in keywordCount1
                            
                                orderby y.CreatedDate.Date descending 
                                group y by new {y.IsDelete,y.roleId, y.CreatedDate.Date } into grp1
                                select new
                                {
                                  
                                    cnt = grp1.Count(),
                                    status=grp1.Key.IsDelete,
                                    roleId = grp1.Key.roleId,
                                   CreatedDate = grp1.Key.Date.ToString("MM/dd/yyyy")
                                };

            var getDetails = new List<CreateDeleteSection>();

            foreach (var item in keygroupcount)
            {
                CreateDeleteSection getcreateSection = new CreateDeleteSection();
                getcreateSection.cnt = item.cnt;
                if (item.roleId == "1")
                {
                    getcreateSection.roleId = "Super Admin";
                }
                else if (item.roleId == "2")
                {
                    getcreateSection.roleId = "Admin";
                }
                else if (item.roleId == "3")
                {
                    getcreateSection.roleId = "User";
                }
                else 
                {
                    getcreateSection.roleId = "Manager";
                }
                getcreateSection.CreatedDate = item.CreatedDate;
                getcreateSection.status = item.status.ToString();
                getDetails.Add(getcreateSection);
            }
            viewmodel.UserCreateDelete = getDetails;
            return PartialView("DashboardCreatedDeleteDateWise", viewmodel);           
        }
/// <summary>
/// This method is used to Display the  All Admin Details
/// </summary>
/// <returns></returns>
        [HttpGet]
        [Audit]
        [Authorize]
        [ConcurrentLogin]
        public async Task<PartialViewResult> AllAdmins()
        {
            AccountViewModel accountViewModel = new AccountViewModel();
            Session["AdminType"] = "All";
            return await CommonPartialList(accountViewModel, "All", "Admin");
        }
/// <summary>
/// This method is used to Display the Active Admin Details
/// </summary>
/// <returns></returns>
        [HttpGet]
        [Audit]
        [Authorize]
        [ConcurrentLogin]
        public async Task<PartialViewResult> ActiveAdmins()
        {
            AccountViewModel accountViewModel = new AccountViewModel();
            Session["AdminType"] = "Active";
            return await CommonPartialList(accountViewModel, "Active", "Admin");
        }
/// <summary>
/// This method is used to Display the inactive Admin Details
/// </summary>
/// <returns></returns>
        [HttpGet]
        [Audit]
        [Authorize]
        [ConcurrentLogin]
        public async Task<PartialViewResult> InActiveAdmins()
        {
            AccountViewModel accountViewModel = new AccountViewModel();
            Session["AdminType"] = "InActive";
            return await CommonPartialList(accountViewModel, "InActive", "Admin");
        }
        /// <summary>
        /// This method is used to Display the All Manager Details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Audit]
        [Authorize]
        [ConcurrentLogin]
        public async Task<PartialViewResult> AllManagers()
        {
            AccountViewModel accountViewModel = new AccountViewModel();
            Session["AdminType"] = "All";
            return await CommonPartialList(accountViewModel, "All", "Manager");
        }
        /// <summary>
        /// This method is used to Display the Active Manager Details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Audit]
        [Authorize]
        [ConcurrentLogin]
        public async Task<PartialViewResult> ActiveManagers()
        {
            AccountViewModel accountViewModel = new AccountViewModel();
            Session["AdminType"] = "Active";
            return await CommonPartialList(accountViewModel, "Active", "Manager");
        }
        /// <summary>
        /// This method is used to Display the Inactive manager Details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Audit]
        [Authorize]
        [ConcurrentLogin]
        public async Task<PartialViewResult> InActiveManagers()
        {
            AccountViewModel accountViewModel = new AccountViewModel();
            Session["AdminType"] = "InActive";
            return await CommonPartialList(accountViewModel, "InActive", "Manager");
        }
        /// <summary>
        /// This is the common Method for getting the SuperAdmin,Admin and Manager Details.
        /// </summary>
        /// <param name="viewmodel"></param>
        /// <param name="UserType"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        [Audit]
        [Authorize]
        [ConcurrentLogin]
        public async Task<PartialViewResult> CommonPartialList(AccountViewModel viewmodel, string UserType, string role)
        {
            UserTypeModel model = new UserTypeModel();
            model.UserType = role;
            model.UserStatus = UserType;
            viewmodel.UserType = UserType;
            if (role == "Super Admin")
            {
                viewmodel.AllSuperAdmins = await _gridBindings.TypeBasedList(model,1);
                return PartialView("_HomePartial", viewmodel);
            }
            if (role == "Manager")
            {
                viewmodel.AllManagers = await _gridBindings.TypeBasedList(model, 4);
                return PartialView("_ManagerHomePartial", viewmodel);
            }
            if (role == "Admin")
            {
                viewmodel.AllAdmins = await _gridBindings.TypeBasedList(model,2);
                return PartialView("_AdminHomePartial", viewmodel);
            }
            return PartialView();
        }

       /// <summary>
       /// This method is used to show the Admin Deails grid
       /// </summary>
       /// <param name="accountViewModel"></param>
       /// <param name="Type"></param>
       /// <returns></returns>
        [HttpGet]
        [Audit]
        [Authorize]
        [ConcurrentLogin]
        public async Task<ActionResult> Home(AccountViewModel accountViewModel, string Type)
        {
            try
            {
                Session["count"] = 2;
                if (Type != null)
                {
                    accountViewModel.UserType = Type;
                }
                if (TempData["Type"] != null)
                {
                    accountViewModel.UserType = Convert.ToString(TempData["Type"]);
                    return await Task.FromResult <ActionResult>(View(accountViewModel));
                }
                if (accountViewModel.UserType == null)
                {
                    accountViewModel.UserType = "All";
                }
                return await Task.FromResult <ActionResult>(View(accountViewModel));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Audit]
        [Authorize]
        [ConcurrentLogin]
        public ActionResult Home(int i)
        {
            return View();
        }
        /// <summary>
        /// This Method is used to show the all the Users
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        [HttpGet]
        [Audit]
        [Authorize]
        [ConcurrentLogin]
        public async Task<PartialViewResult> AllUsers(string searchText)
        {
            AccountViewModel accountViewModel = new AccountViewModel();
            Session["UserType"] = "All";
            return await UserCommonPartialList(accountViewModel, "All", "Employee",searchText);
        }
        /// <summary>
        /// This method is used to show the Active User Details
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        [HttpGet]
        [Audit]
        [Authorize]
        [ConcurrentLogin]
        public async Task<PartialViewResult> ActiveUsers(string searchText)
        {
            AccountViewModel accountViewModel = new AccountViewModel();
            Session["UserType"] = "Active";
            return await UserCommonPartialList(accountViewModel, "Active", "Employee", searchText);
        }
        /// <summary>
        /// This method is Used to show the inActive User Details
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        [HttpGet]
        [Audit]
        [Authorize]
        [ConcurrentLogin]
        public async Task<PartialViewResult> InActiveUsers(string searchText)
        {
            AccountViewModel accountViewModel = new AccountViewModel();
            Session["UserType"] = "InActive";
            return await UserCommonPartialList(accountViewModel, "InActive", "Employee", searchText);
        }
        /// <summary>
        /// This method is used to show the User Partial page
        /// </summary>
        /// <param name="viewmodel"></param>
        /// <param name="UserType"></param>
        /// <param name="role"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        [Audit]
        [Authorize]
        [ConcurrentLogin]
        public async Task<PartialViewResult> UserCommonPartialList(AccountViewModel viewmodel, string UserType, string role,string searchText)
        {
            UserTypeModel model = new UserTypeModel();
            model.UserType = role;
            model.UserStatus = UserType;
            model.searchText = searchText;
            viewmodel.UserType = UserType;
            if (role == "Employee")
            {
                viewmodel.AllEmployees = await _gridBindings.UserTypeBasedList(model);
                return PartialView("_UserHomePartial", viewmodel);
            }
            return PartialView();
        }
        /// <summary>
        /// This method is used to show the Cutomer grid page
        /// </summary>
        /// <param name="accountViewModel"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        [HttpGet]
        [Audit]
        [Authorize]
        [ConcurrentLogin]
        public async Task<ActionResult> CustomerHome(AccountViewModel accountViewModel, string Type)
        {
            try
            {
                if (Type != null)
                {
                    accountViewModel.UserType = Type;

                }
                if (TempData["Type"] != null)
                {
                    accountViewModel.UserType = Convert.ToString(TempData["Type"]);
                    return await Task.FromResult <ActionResult>(View(accountViewModel));
                }
                if (accountViewModel.UserType == null)
                {
                    accountViewModel.UserType = "All";
                }
                return await Task.FromResult <ActionResult>(View(accountViewModel));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// This method is used for get all the users based on seach input
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
         [Authorize]
         [ConcurrentLogin]
        public JsonResult GetUsers(string term)
        {
            List<string> primaries = _bblAssociateService.FindByUserName(term).ToList();
            return Json(primaries, JsonRequestBehavior.AllowGet);
        }

        #region Manager
        /// <summary>
        /// This method is used to show the manager grid Page
        /// </summary>
        /// <param name="accountViewModel"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
         [HttpGet]
         [Audit]
         [Authorize]
         [ConcurrentLogin]
         public async Task<ActionResult> ManagerHome(AccountViewModel accountViewModel, string Type)
         {
             try
             {
                 Session["count"] = 4;
                 if (Type != null)
                 {
                     accountViewModel.UserType = Type;
                 }
                 if (TempData["Type"] != null)
                 {
                     accountViewModel.UserType = Convert.ToString(TempData["Type"]);
                     return await Task.FromResult <ActionResult>(View(accountViewModel));
                 }
            
                 if (accountViewModel.UserType == null)
                 {
                     accountViewModel.UserType = "All";
                 }
                 return await Task.FromResult <ActionResult>(View(accountViewModel));
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }

         [HttpPost]
         [ValidateAntiForgeryToken]
         [Audit]
         [Authorize]
         [ConcurrentLogin]
         public ActionResult ManagerHome(int i)
         {
             return View();
         }

        #endregion
    }

}