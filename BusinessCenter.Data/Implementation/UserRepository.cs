using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessCenter.Data.Implementation
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public IUserRoleRepository rolerep;

        public UserRepository(IUnitOfWork context, IUserRoleRepository irole)
            : base(context)
        {
            rolerep = irole;
        }
        /// <summary>
        /// This method is used to Get All Users
        /// </summary>
        /// <returns>Return Users</returns>
        public IEnumerable<User> GetUserLookupAll()
        {
            var users = GetAll();
            return users;
        }
        /// <summary>
        /// This method is used to Get particular User Data Based on User Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Return User data</returns>
        public IEnumerable<User> FindByID(string userId)
        {
            var userdata = FindBy(x => x.Id == userId);
            return userdata;
        }
        /// <summary>
        /// This method is used to get User Data based on User name check with start with
        /// </summary>
        /// <param name="term"></param>
        /// <returns>Get User Data</returns>
        public IEnumerable<string> FindByUserName(string term)
        {
            var users = FindBy(x => x.UserName.StartsWith(term)).Select(y => y.UserName);
            return users;
        }
        /// <summary>
        /// This method is used to Get Bool Value based on User Id and Statis
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="status"></param>
        /// <returns>Return Bool Status</returns>
        public bool UpdateLoggedInStatus(string userId, bool status)
        {
            var users = FindBy(x => x.Id == userId).ToList();
            if (users.Any())
            {
                var getUserDetails = users.FirstOrDefault();
                getUserDetails.IsLoggedIn = status;
                getUserDetails.LoginSessionId = string.Empty;
                Update(getUserDetails, userId);
                Save();
                return true;
            }
            return false;
        }
        /// <summary>
        /// This method is used to Get User data Based on UserStatus and Role Id
        /// </summary>
        /// <param name="userStatus"></param>
        /// <param name="roleId"></param>
        /// <returns>Return User Data</returns>
        public IEnumerable<User> GetUsersBasedOnId(string userStatus, int roleId)
        {
            List<User> usersBasedontype = new List<User>();
            if (userStatus.ToUpper() == "ALL")
            {
                usersBasedontype = (from user in FindBy(x => x.IsActive == true)
                                    join role in rolerep.FindBy(roleId.ToString()) on user.Id equals role.UserId
                                    select user).ToList();
            }
            if (userStatus.ToUpper() == "ACTIVE")
            {
                usersBasedontype = (from user in FindBy(x => x.IsDelete == false && x.IsActive == true)
                                    join role in rolerep.FindBy(roleId.ToString()) on user.Id equals role.UserId
                                    select user).ToList();
            }
            if (userStatus.ToUpper() == "INACTIVE")
            {
                usersBasedontype = (from user in FindBy(x => x.IsDelete == true && x.IsActive == true)
                                    join role in rolerep.FindBy(roleId.ToString()) on user.Id equals role.UserId
                                    select user).ToList();
            }
            return usersBasedontype.ToList();
        }
        /// <summary>
        /// This Method is used to Get user Role Id based on User name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>Retrun Role Id</returns>
        public int GetuserByUserName(string userName)
        {
            int roleid = 0;
            var data = (from userdata in GetUserLookupAll().AsQueryable()
                        join role in rolerep.GetUserRoleLookupAll().AsQueryable() on userdata.Id equals role.UserId
                        where userdata.UserName == userName
                        select role.RoleId).ToList();
            if (!data.Any())
            {
                roleid = 0;
            }
            else
            { roleid = Convert.ToInt32(data.FirstOrDefault()); }
            return roleid;
        }
        /// <summary>
        /// This method is used to check user name exist or not based on user name.
        /// </summary>
        /// <param name="userdetail"></param>
        /// <returns>Return Bool Value</returns>
        public bool UpdateLoggedTime(Userdetails userdetail)
        {
            var users = FindBy(x => x.UserName.ToUpper().Trim() == userdetail.UserName.ToUpper().Trim()).ToList();
            if (users != null)
            {
                var getUserDetails = users.FirstOrDefault();
                getUserDetails.LastLoginDateandTime = Convert.ToDateTime(System.DateTime.Now);
                getUserDetails.IsLoggedIn = userdetail.IsLogedIn;
                getUserDetails.LoginSessionId = userdetail.SessionId;
                Update(getUserDetails, getUserDetails.Id);
                Save();
                return true;
            }
            return false;
        }
        /// <summary>
        /// This method is used to Get User Data based on User Name.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>Return User data</returns>
        public IEnumerable<User> FindByLoginUsername(string userName)
        {
            var userdata = FindBy(x => x.UserName.ToUpper().Trim() == userName.ToUpper().Trim());
            return userdata;
        }
    }
}