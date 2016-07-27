using BusinessCenter.Admin.Common;
using BusinessCenter.Admin.Interface;
using BusinessCenter.Data.Interface;
using BusinessCenter.Identity.Interfaces;
using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Admin.Models
{
    public class GridBindings : IGridBindings
    {
        private readonly IUserManager _userManager;
        private readonly IUserRepository _usersManager;
        public static IEnumerable<UserApiModel> _getUserDetails;

        public GridBindings(IUserManager userManager, IUserRepository iuser)
        {
            _userManager = userManager;
            _usersManager = iuser;
        }

        public async Task<List<RegisterUserModel>> TypeBasedList(UserTypeModel usertypemodel, int roleId)
        {
            var userIDs = _usersManager.GetUsersBasedOnId(usertypemodel.UserStatus, roleId);
            var superAdminUser = new List<UserApiModel>();
            foreach (var items in userIDs)
            {
                UserApiModel getListOfUsers = new UserApiModel();

                //string s = GiuParse.GenarateGuiWithUserId(items.Id, 10);
                getListOfUsers.UserId = items.Id;
                // getListOfUsers.Gui = s;
                //int len = (items.Id).ToString().Length;
                // getListOfUsers.length = Convert.ToInt32(len);
                getListOfUsers.FirstName = items.FirstName;
                getListOfUsers.LastName = items.LastName;
                getListOfUsers.Email = items.Email;
                getListOfUsers.IsActive = items.IsActive;
                getListOfUsers.LockoutEndDateUtc = items.LockoutEndDateUtc;
                getListOfUsers.UserName = items.UserName;
                getListOfUsers.LastLoginDateandTime = items.LastLoginDateandTime;

                getListOfUsers.CreatedDate = items.CreatedDate;
                getListOfUsers.IsDelete = items.IsDelete;
                if (items.LockoutEndDateUtc != null)
                {
                    if (items.LockoutEnabled == false)
                    {
                        getListOfUsers.LockoutEnabled = false;
                    }
                    else
                    {
                        bool chcekDate = GenaralConvertion.CheckDateExpireForLockout(
                            System.DateTime.UtcNow.ToString(), items.LockoutEndDateUtc.ToString());
                        if (chcekDate == false && items.LockoutEnabled == true)
                        {
                            getListOfUsers.LockoutEnabled = false;
                        }
                        else
                        {
                            getListOfUsers.LockoutEnabled = true;
                        }
                    }
                }
                else
                {
                    getListOfUsers.LockoutEnabled = false;
                }
                superAdminUser.Add(getListOfUsers);
            }
            _getUserDetails = superAdminUser.OrderByDescending(x => x.CreatedDate);
            List<RegisterUserModel> registerUserModel = new List<RegisterUserModel>();
            registerUserModel = _getUserDetails.Select(i => new RegisterUserModel().InjectFrom(i)).Cast<RegisterUserModel>().ToList();
            return await Task.FromResult(registerUserModel.ToList());
        }

        public async Task<List<RegisterUserModel>> UserTypeBasedList(UserTypeModel usertypemodel)
        {
            try
            {
                //  var userIDs = await _userManager.AllUsers();
                // userIDs = userIDs.Where(x => x.Roles.Count == 1);
                var userIDs = _usersManager.GetUsersBasedOnId(usertypemodel.UserStatus, 3);
                var employeeAdminUser = new List<UserApiModel>();
                foreach (var items in userIDs)
                {
                    var getListOfUsers = new UserApiModel();
                    // var userRoles = await _userManager.GetRolesAsync(items.Id);

                    getListOfUsers.UserId = items.Id;
                    getListOfUsers.FirstName = items.FirstName;
                    getListOfUsers.LastName = items.LastName;
                    getListOfUsers.Email = items.Email;
                    getListOfUsers.IsActive = items.IsActive;
                    //getListOfUsers.LockoutEnabled = items.LockoutEnabled;
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
                    if (items.LockoutEndDateUtc != null)
                    {
                        if (items.LockoutEnabled == false)
                        {
                            getListOfUsers.LockoutEnabled = false;
                        }
                        else
                        {
                            bool chcekDate = GenaralConvertion.CheckDateExpireForLockout(
                                System.DateTime.UtcNow.ToString(), items.LockoutEndDateUtc.ToString());
                            if (chcekDate == false && items.LockoutEnabled == true)
                            {
                                getListOfUsers.LockoutEnabled = false;
                            }
                            else
                            {
                                getListOfUsers.LockoutEnabled = true;
                            }
                        }
                    }
                    else
                    {
                        getListOfUsers.LockoutEnabled = false;
                    }

                    if (usertypemodel.UserStatus == "All")
                    {
                        bool chcekDate = GenaralConvertion.CheckDateExpire(items.CreatedDate.ToString(),
                      System.DateTime.Now.ToString());
                        if (items.IsActive == false && chcekDate == false)
                        {
                        }
                        else
                        {
                            employeeAdminUser.Add(getListOfUsers);
                        }
                    }
                    else if (usertypemodel.UserStatus == "Active")
                    {
                        bool chcekDate = GenaralConvertion.CheckDateExpire(items.CreatedDate.ToString(),
                     System.DateTime.Now.ToString());
                        if (items.IsActive == false && chcekDate == false)
                        {
                        }
                        else
                        {
                            if ((items.IsDelete == false) && (items.IsActive == true))
                            {
                                employeeAdminUser.Add(getListOfUsers);
                            }
                        }
                    }
                    else
                    {
                        bool chcekDate = GenaralConvertion.CheckDateExpire(items.CreatedDate.ToString(),
                   System.DateTime.Now.ToString());
                        if (items.IsActive == false && chcekDate == false)
                        {
                        }
                        else
                        {
                            if ((items.IsDelete == true) && (items.IsActive == true))
                            {
                                employeeAdminUser.Add(getListOfUsers);
                            }
                        }
                    }
                }
                _getUserDetails = employeeAdminUser.OrderByDescending(x => x.UserId);
                // List<RegisterUserModel> registerUserModel = new List<RegisterUserModel>();
                List<RegisterUserModel> registermodel = new List<RegisterUserModel>();
                registermodel = _getUserDetails.Select(i => new RegisterUserModel().InjectFrom(i)).Cast<RegisterUserModel>().ToList();
                if (usertypemodel.searchText == "")
                {
                    return registermodel.ToList();
                }
                else
                {
                    object obj = registermodel;
                    var li = new List<RegisterUserModel>();
                    li = (List<RegisterUserModel>)obj;
                    var recs = new List<RegisterUserModel>();
                    string key = usertypemodel.searchText.ToUpper();
                    foreach (var item in li)
                    {
                        StringBuilder builder = new StringBuilder();
                        //builder.Append(item.Email == null ? "" : item.Email.ToString());
                        builder.Append(item.UserName == null ? "" : item.UserName.ToString());
                        builder.Append(item.FirstName == null ? "" : item.FirstName.ToString());
                        builder.Append(item.LastName == null ? "" : item.LastName.ToString());
                        if (builder.ToString().ToLower().Contains(key.ToLower()))
                        {
                            recs.Add(item);
                        }
                    }
                    registermodel = recs.ToList();
                    return await Task.FromResult(registermodel.ToList());
                    // return registermodel.Where(x => x.UserName.StartsWith(usertypemodel.searchText)).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}