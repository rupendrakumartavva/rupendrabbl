using BusinessCenter.Admin.Filters;
using BusinessCenter.Admin.Interface;
using BusinessCenter.Admin.Models;
using BusinessCenter.Audits;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Models;
using BusinessCenter.Identity.Interfaces;
using BusinessCenter.Service.Interface;
using Omu.ValueInjecter;
using PagedList;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BusinessCenter.Admin.Common;

namespace BusinessCenter.Admin.Controllers
{
    public class SuperAdminController : Controller
    {
        private readonly IUserManager _userManager;
        private readonly IGridBindings _gridBindings;
        private ISearchKeywordService _keywordCount;
        private IUserManagerService _loginCount;
        public static IEnumerable<UserApiModel> _getUserDetails;
        private readonly IMyServiceDetails _searchService;
        private readonly IUserRepository _usersManager;
        public readonly IBusinessCenterCommon _businesscentercommon;

        public SuperAdminController(IUserManager userManager,
          ISearchKeywordService searchKeywordService, IUserManagerService userService,
            IMyServiceDetails searchService, IUserRepository iuser, IGridBindings gridBindings, IBusinessCenterCommon businesscentercommon)
        {
            _userManager = userManager;
            _keywordCount = searchKeywordService;
            _loginCount = userService;
            _searchService = searchService;
            _usersManager = iuser;
            _gridBindings = gridBindings;
            _businesscentercommon = businesscentercommon;
        }
        /// <summary>
        /// This method is used to show the customer Dashboard
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
         [Authorize]
         [ConcurrentLogin]
        public async Task<ActionResult> CustomerDashboard(string UserId)
        {
            var userservicemodel = new UserServiceModel
            {
                UserId = UserId,
                PageSize = 10,
                PageIndex = 1,
                DisplayType = "All"
            };
            var user1 = new UserAccountById { UserId = userservicemodel.UserId };

            var userIDs = await _userManager.FindUserDetailsByIdAsync(user1.UserId);
            ViewBag.UserName = userIDs.UserName;
            var result = _searchService.GetCount(userservicemodel);
            foreach (var item in result)
            {
                ViewBag.RecordCount = item.RecordCount;
                ViewBag.Abracount = item.ABRACount;
                ViewBag.BBLCount = item.BBLCount;
                ViewBag.CORPCount = item.CORPCount;
                ViewBag.OPLACount = item.OPLACount;
                ViewBag.CBECount = item.CBECount;
                ViewBag.ABRACount = item.ABRACount;
            }
            SearchDataMvcViewModel model = new SearchDataMvcViewModel();
            model.ID = userservicemodel.UserId;
            TempData["UserDashboardId"] = model.ID;
            return View();
        }
        /// <summary>
        /// This method is used to show the Quick Search Dashboard Page
        /// </summary>
        /// <param name="id"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        [Audit]
        [Authorize]
        [ConcurrentLogin]
        public async Task<PartialViewResult> UserDashboardPartial(string id, string keyword)
        {
            var userservicemodel = new UserServiceModel();
            userservicemodel.UserId = id;
            userservicemodel.PageSize = 10;
            userservicemodel.PageIndex = 1;
            userservicemodel.DisplayType = "All";
            SearchDataMvcViewModel model = new SearchDataMvcViewModel();
            var commondata = _searchService.GetAllData(userservicemodel).ToList();
            List<CommonDataModel> records = new List<CommonDataModel>();
            if ((keyword == "All"))
            {
                records = commondata.Select(i => new CommonDataModel().InjectFrom(i)).Cast<CommonDataModel>().ToList();
                model.FinalData = records;
                if (model.FinalData.Count == 0)
                {
                    ViewBag.SearchStatus = "No Results Found";
                }
                return await Task.FromResult(PartialView("_UserDashboardPartial", model));
            }
            // ReSharper disable once RedundantIfElseBlock
            else if (keyword == "BBL")
            {
                records = commondata.Select(i => new CommonDataModel().InjectFrom(i)).Cast<CommonDataModel>().Where(j => j.Source == "BBL").ToList();
                model.FinalData = records;
                return await Task.FromResult(PartialView("_UserDashboardPartial", model));
            }
            // ReSharper disable once RedundantIfElseBlock
            else if (keyword == "OPLA")
            {
                records = commondata.Select(i => new CommonDataModel().InjectFrom(i)).Cast<CommonDataModel>().Where(j => j.Source == "OPLA").ToList();
                model.FinalData = records;
                return await Task.FromResult(PartialView("_UserDashboardPartial", model));
            }
            // ReSharper disable once RedundantIfElseBlock
            else if (keyword == "ABRA")
            {
                records = commondata.Select(i => new CommonDataModel().InjectFrom(i)).Cast<CommonDataModel>().Where(j => j.Source == "ABRA").ToList();
                model.FinalData = records;
                return await Task.FromResult(PartialView("_UserDashboardPartial", model));
            }
            // ReSharper disable once RedundantIfElseBlock
            else if (keyword == "CBE")
            {
                records = commondata.Select(i => new CommonDataModel().InjectFrom(i)).Cast<CommonDataModel>().Where(j => j.Source == "CBE").ToList();
                model.FinalData = records;
                return await Task.FromResult(PartialView("_UserDashboardPartial", model));
            }
            // ReSharper disable once RedundantIfElseBlock
            else if (keyword == "CORP")
            {
                records = commondata.Select(i => new CommonDataModel().InjectFrom(i)).Cast<CommonDataModel>().Where(j => j.Source == "CORP").ToList();
                model.FinalData = records;
                return await Task.FromResult(PartialView("_UserDashboardPartial", model));
            }
            // ReSharper disable once RedundantIfElseBlock
            else
            {
                records = commondata.Select(i => new CommonDataModel().InjectFrom(i)).Cast<CommonDataModel>().ToList();
                Session["UserData"] = records;
                List<CommonDataModel> li = new List<CommonDataModel>();
                li = (List<CommonDataModel>)Session["UserData"];
                List<CommonDataModel> recs = new List<CommonDataModel>();
                string key = keyword.ToUpper();
                try
                {
                    foreach (var item in li)
                    {
                        StringBuilder builder = new StringBuilder();
                        builder.Append(item.LeftNameResultTop == null ? "" : item.LeftNameResultTop.ToString());
                        builder.Append("δ");
                        builder.Append(item.LeftNameResultMiddle == null ? "" : item.LeftNameResultMiddle.ToString());
                        builder.Append("δ");
                        builder.Append(item.LeftNameResultBottom == null ? "" : item.LeftNameResultBottom.ToString());
                        builder.Append("δ");
                        builder.Append(item.LeftNameMiddle1Text == null ? "" : item.LeftNameMiddle1Text.ToString());
                        builder.Append("δ");
                        builder.Append(item.MiddleNameResultTop == null ? "" : item.MiddleNameResultTop.ToString());
                        builder.Append("δ");
                        builder.Append(item.RightNameResultTop == null ? "" : item.RightNameResultTop.ToString());
                        builder.Append("δ");
                        builder.Append(item.RightNameResultMiddle1 == null ? "" : item.RightNameResultMiddle1.ToString());
                        builder.Append("δ");
                        builder.Append(item.RightNameResultMiddle2 == null ? "" : item.RightNameResultMiddle2.ToString());
                        builder.Append("δ");
                        builder.Append(item.RightNameResultBottom == null ? "" : item.RightNameResultBottom.ToString());
                        builder.Append("δ");
                        builder.Append(item.LastUpdateDate == null ? "" : item.LastUpdateDate.ToString());
                        builder.Append("δ");
                        builder.Append(item.SourceFullName == null ? "" : item.SourceFullName.ToString());
                        builder.Append("δ");
                        builder.Append(item.ExpantionResult1 == null ? "" : item.ExpantionResult1.ToString());
                        builder.Append("δ");
                        builder.Append(item.ExpantionResult2 == null ? "" : item.ExpantionResult2.ToString());
                        builder.Append("δ");
                        builder.Append(item.ExpantionResult3 == null ? "" : item.ExpantionResult3.ToString());
                        builder.Append("δ");
                        builder.Append(item.ExpantionResult4 == null ? "" : item.ExpantionResult4.ToString());
                        builder.Append("δ");
                        builder.Append(item.ExpantionResult5 == null ? "" : item.ExpantionResult5.ToString());

                        if (builder.ToString().ToLower().Contains(key.ToLower()))
                        {
                            recs.Add(item);
                        }
                        records = recs.ToList();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Exception occurs in UserDashboardPartial", ex);
                }
            }
            model.FinalData = records;
            if (keyword == null || keyword == "" && model.FinalData.Count == 0)
            {
                ViewBag.SearchStatus = "No Results Found";
            }
            else if (keyword != null && keyword.Length > 100)
            {
                ViewBag.SearchStatus = "Your keyword search can be no longer than 100 characters maximum";
            }
            else if (keyword != null && model.FinalData.Count == 0)
            {
                ViewBag.SearchStatus = "Sorry no search data found.Please verify search keyword and try again";
            }
            return PartialView("_UserDashboardPartial", model);
        }

        [HttpGet]
        [Audit]
        [Authorize]
        [ConcurrentLogin]
        public async Task<PartialViewResult> Searchkeyword(string id, string recordtype, string kwd)
        {
            UserServiceModel userservicemodel = new UserServiceModel();
            userservicemodel.UserId = id;
            userservicemodel.PageSize = 10;
            userservicemodel.PageIndex = 1;
            userservicemodel.DisplayType = "All";
            userservicemodel.KeyType = kwd;
            SearchDataMvcViewModel model = new SearchDataMvcViewModel();
            var commondata = _searchService.GetAllData(userservicemodel).ToList();
            List<CommonDataModel> records = new List<CommonDataModel>();
            if (recordtype == null)
            {
                recordtype = "All";
            }
            if (recordtype == "All")
            {
                records = commondata.Select(i => new CommonDataModel().InjectFrom(i)).Cast<CommonDataModel>().ToList();
            }
            model.FinalData = records;
            return await Task.FromResult(PartialView("_UserDashboardPartial", model));
        }

        [HttpGet]
        [Audit]
        [Authorize]
        [ConcurrentLogin]
        public async Task<ActionResult> SearchWordsList(int? page)
        {
            string DisplayType = "ALL";
            NameValueCollection KV = HttpUtility.ParseQueryString(DisplayType);
            DateTime fromDate = DateTime.Now.AddDays(-2);
            DateTime toDate = DateTime.Now;
            var keywordCount = await _keywordCount.GetKeywordSearchCount(fromDate, toDate, KV.ToString());
            var keywords = keywordCount.Select(i => new SearchKeywordMvcModel().InjectFrom(i)).Cast<SearchKeywordMvcModel>().ToList();
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return await Task.FromResult<ActionResult>( View(keywords.ToPagedList(pageNumber, pageSize)));
        }
        /// <summary>
        /// This method Retruns SuperAdmin Dashboard view
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Audit]
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

        public static string SuperadminActive;
        public static string SuperadminInActive;
        public static string SuperadminRegister;
        public static string SuperadminDelete;

        public static string AdminActive;
        public static string AdminInActive;
        public static string AdminRegister;
        public static string AdminDelete;

        public static string EmployeeActive;
        public static string EmployeeInActive;
        public static string EmployeeRegister;
        public static string EmployeeDelete;

        public static string ManagerActive;
        public static string ManagerInActive;
        public static string ManagerRegister;
        public static string ManagerDelete;
        /// <summary>
        /// This method is used to get the counts of Active,Inactive,Register and Deleted Users
        /// </summary>
         [Authorize]
         [ConcurrentLogin]
        public void DashboardCount()
        {
            DateTime date = DateTime.Now;
            DateTime startdate = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, DateTime.Now.Date.Day, 00, 00, 00, 000);
            DateTime fromdate = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, DateTime.Now.Date.Day, 23, 59, 59, 999);

            var superadmin = _usersManager.GetUsersBasedOnId("ALL", 1);
            SuperadminActive = superadmin.Count(user => user.IsDelete == false).ToString();
            SuperadminInActive = superadmin.Count(user => user.IsDelete == true).ToString();
            SuperadminRegister = superadmin.Count(users => (users.CreatedDate >= startdate && users.CreatedDate <= fromdate)).ToString();
            SuperadminDelete = superadmin.Count(users => users.UpdatedDate >= startdate && users.UpdatedDate <= fromdate && users.IsDelete == true).ToString();

            var admin = _usersManager.GetUsersBasedOnId("ALL", 2);
            if (admin != null)
            {
                // ReSharper disable once PossibleMultipleEnumeration
                AdminActive = admin.Count(user => user.IsDelete == false).ToString();
                // ReSharper disable once PossibleMultipleEnumeration
                AdminInActive = admin.Count(user => user.IsDelete == true).ToString();
                // ReSharper disable once PossibleMultipleEnumeration
                AdminRegister = admin.Count(users => users.CreatedDate >= startdate && users.CreatedDate <= fromdate).ToString();

                // ReSharper disable once PossibleMultipleEnumeration
                AdminDelete = admin.Count(users => users.UpdatedDate >= startdate && users.UpdatedDate <= fromdate && users.IsDelete == true).ToString();
            }

            var Employee = _usersManager.GetUsersBasedOnId("ALL", 3);
            EmployeeActive = Employee.Where(user => user.IsDelete == false).Count().ToString();
            EmployeeInActive = Employee.Where(user => user.IsDelete == true).Count().ToString();
            EmployeeRegister = Employee.Where(users => users.CreatedDate >= startdate &&
                    users.CreatedDate <= fromdate).Count().ToString();
            EmployeeDelete = Employee.Where(users => users.UpdatedDate >= startdate && users.UpdatedDate <= fromdate
                    && users.IsDelete == true && users.IsActive == true).Count().ToString();


            var manager = _usersManager.GetUsersBasedOnId("ALL", 4);
            if (manager != null)
            {
                // ReSharper disable once PossibleMultipleEnumeration
                ManagerActive = manager.Count(user => user.IsDelete == false).ToString();
                // ReSharper disable once PossibleMultipleEnumeration
                ManagerInActive = manager.Count(user => user.IsDelete == true).ToString();
                // ReSharper disable once PossibleMultipleEnumeration
                ManagerRegister = manager.Count(users => users.CreatedDate >= startdate && users.CreatedDate <= fromdate).ToString();

                // ReSharper disable once PossibleMultipleEnumeration
                ManagerDelete = manager.Count(users => users.UpdatedDate >= startdate && users.UpdatedDate <= fromdate && users.IsDelete == true).ToString();
            }
        }
        /// <summary>
        /// This method is used to display the counts of Active SuperAdmin,Admin,Manager and Employee
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ConcurrentLogin]
        public async Task<ActionResult> DashboardActive()
        {
            ViewData["FullName"] = Session["FullName"];
            try
            {
                ViewBag.EmpActiveUsers = EmployeeActive;
                ViewBag.AdminActiveUsers = AdminActive;
                ViewBag.SuperadminActiveUsers = SuperadminActive;
                ViewBag.ManagerActiveUsers = ManagerActive;
                return await Task.FromResult<ActionResult>(PartialView("_DashboardActiveusers"));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in DashboardActive", ex); 
            }
        }
        /// <summary>
        /// This method is used to Display the counts of Inactive SuperAdmin,Admin,Manager and Employee
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ConcurrentLogin]
        public async Task<ActionResult> DashboardInactive()
        {
            try
            {
                ViewBag.EmpInActiveUsers = EmployeeInActive;
                ViewBag.AdminInActiveUsers = AdminInActive;
                ViewBag.SuperAdminInActiveUsers = SuperadminInActive;
                ViewBag.ManagerInActiveUsers = ManagerInActive;
                return await Task.FromResult<ActionResult>(PartialView("_DashboardInactiveusers"));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in DashboardInactive", ex); 
            }
        }
        /// <summary>
        /// This method is used to display the Todays Registered  SuperAdmin,Admin,Manager,Employee counts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ConcurrentLogin]
        public async Task<ActionResult> DashboardRegister()
        {
            try
            {
                ViewBag.EmpTodayRegistered = EmployeeRegister;
                ViewBag.AdminTodayRegistered = AdminRegister;
                ViewBag.SuperAdminTodayRegistered = SuperadminRegister;
                ViewBag.ManagerTodayRegistered = ManagerRegister;
                return await Task.FromResult <ActionResult>(PartialView("_DashboardRegisteredUsers"));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in DashboardRegister", ex);
            }
        }
        /// <summary>
        /// This method is used to Display the Today's Deleted SuperAdmin,Admin,Manager and Employee Counts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ConcurrentLogin]
        public async Task<ActionResult> DashboardDelete()
        {
            try
            {
                ViewBag.SuperAdminTodayDeleted = SuperadminDelete;
                ViewBag.AdminTodayDeleted = AdminDelete;
                ViewBag.EmpTodayDeleted = EmployeeDelete;
                ViewBag.ManagerTodayDeleted = ManagerDelete;
                return await Task.FromResult <ActionResult>(PartialView("_DashboardDeletedUsers"));
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// This method is used to display the Number of logins per day and respective last login details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ConcurrentLogin]
        public async Task<ActionResult> Dashboardloginhistory()
        {
            DateTime FromDate = DateTime.Now.AddDays(-2);
            DateTime ToDate = DateTime.Now;
            var Keywordcount = await _loginCount.GetLoginHistoryCount(FromDate, ToDate);
            DashBoardViewModel viewmodel = new DashBoardViewModel();
            viewmodel.loginhistory = Keywordcount.Select(i => new LoginHistoryModel().InjectFrom(i)).Cast<LoginHistoryModel>().ToList();

            foreach (var item in viewmodel.loginhistory)
            {
                if (item.UserRole == "Employee")
                {
                    item.UserRole = "User";
                }
                if (item.UserRole == "Super Admin")
                {
                    item.UserRole = "Superadmin";
                }
            }
            return PartialView("_DashboardLoginUsers", viewmodel);
        }
        /// <summary>
        /// This method is used to Display the details of Number of logins per day by role
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ConcurrentLogin]
        //  [Audit]
        public async Task<ActionResult> DashboardLoginByRole()
        {
            DateTime FromDate = DateTime.Now.AddDays(-2);
            DateTime ToDate = DateTime.Now;
            var Keywordcount = await _loginCount.GetLoginHistoryCount(FromDate, ToDate);
            DashBoardViewModel viewmodel = new DashBoardViewModel();
            viewmodel.loginhistory = Keywordcount.Select(i => new LoginHistoryModel().InjectFrom(i)).Cast<LoginHistoryModel>().ToList();
            foreach (var item in viewmodel.loginhistory)
            {
                if (item.UserRole == "Employee")
                {
                    item.UserRole = "User";
                }
                if (item.UserRole == "Super Admin")
                {
                    item.UserRole = "Superadmin";
                }
            }
            var keygroupcount = from r in Keywordcount
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
                if (item.Role == "Super Admin")
                {
                    item.Role = "Superadmin";
                }
            }
            return PartialView("_DashboardLoginsbyRole", viewmodel);
        }
        /// <summary>
        /// This method is used to get All SuperAdmin Details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Audit]
        [Authorize]
        [ConcurrentLogin]
        public async Task<PartialViewResult> All()
        {
            AccountViewModel accountViewModel = new AccountViewModel();
            return await CommonPartialList(accountViewModel, "All", "Super Admin");
        }
        /// <summary>
        /// This method is used to get Active SuperAdmin Details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Audit]
        [Authorize]
        [ConcurrentLogin]
        public async Task<PartialViewResult> Active()
        {
            AccountViewModel accountViewModel = new AccountViewModel();
            return await CommonPartialList(accountViewModel, "Active", "Super Admin");
        }
        /// <summary>
        /// This method is used to get Inactive SuperAdmin Details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Audit]
        [Authorize]
        [ConcurrentLogin]
        public async Task<PartialViewResult> InActive()
        {
            AccountViewModel accountViewModel = new AccountViewModel();
            return await CommonPartialList(accountViewModel, "InActive", "Super Admin");
        }
        /// <summary>
        /// This is the Commmon method for get SuperAdmin and admin Details
        /// </summary>
        /// <param name="viewmodel"></param>
        /// <param name="userType"></param>
        /// <param name="role"></param>
        /// <returns></returns>
         [Authorize]
         [ConcurrentLogin]
        public async Task<PartialViewResult> CommonPartialList(AccountViewModel viewmodel, string userType, string role)
        {
            var model = new UserTypeModel { UserType = role, UserStatus = userType };
            viewmodel.UserType = userType;
            switch (role)
            {
                case "Super Admin":
                    viewmodel.AllSuperAdmins = await _gridBindings.TypeBasedList(model, 1);
                    return PartialView("_HomePartial", viewmodel);

                case "Admin":
                    viewmodel.AllAdmins = await _gridBindings.TypeBasedList(model, 2);
                    return PartialView("_AdminHomePartial", viewmodel);
            }
            return PartialView();
        }
    /// <summary>
    ///This method is used to display the SuperAdmin grid
    /// </summary>
    /// <param name="accountViewModel"></param>
    /// <param name="type"></param>
    /// <returns></returns>
        [HttpGet]
        [Audit]
        [Authorize]
        [ConcurrentLogin]
        public async Task<ActionResult> Home(AccountViewModel accountViewModel, string type)
        {
            try
            {
                Session["count"] = 3;
                if (type != null)
                {
                    accountViewModel.UserType = type;
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
        /// This method is used to check Whether UserName exists or not
        /// </summary>
        /// <param name="userVaidate"></param>
        /// <returns></returns>
        [HttpPost]
        [Audit]
        [Authorize]
       // [ConcurrentLogin]
        public async Task<ActionResult> CheckUserAvailable(UserCheckViewModel userVaidate)
        {
            if (_businesscentercommon.ValidateSessionResult())
            {
                var result = await _userManager.CheckUserAvailableAsync(userVaidate.UserName);
                var final = true;
                try
                {
                    // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                    if (result != null)
                    {
                        final = result;
                    }
                }
                catch (AggregateException ex)
                {
                    throw new Exception("Exception occurs in Super Admin Controller", ex);
                }
                return Json(new {status = final.ToString()});
            }
            // ReSharper disable once RedundantIfElseBlock
            else
            { return Json(new { status = "SessionExipred" }); }
        }
        /// <summary>
        /// This method is used to check whether Email is Existr or not
        /// </summary>
        /// <param name="registermodel"></param>
        /// <returns></returns>
        [HttpPost]
        [Audit]
        [Authorize]
        public async Task<ActionResult> CheckUserEmailAvailable(ForgotViewModel registermodel)
        {
            if (_businesscentercommon.ValidateSessionResult())
            {
                var result = await _userManager.CheckUserEmailAvailableAsync(registermodel.Email);
                var final = true;
                try
                {
                    // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                    if (result != null)
                    {
                        if (result == false)
                        {
                            final = true;
                        }
                        else
                        {
                            final = false;
                        }
                    }
                }
                catch (AggregateException ex)
                {
                    throw new Exception("Exception occurs in Check User Email", ex);
                }
                return Json(new {status = final.ToString()});
            }
            // ReSharper disable once RedundantIfElseBlock
            else
            { return Json(new { status = "SessionExipred" }); }
        }
    }
}