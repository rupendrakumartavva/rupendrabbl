using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Models;
using Omu.ValueInjecter;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Implementation
{
    public class UserManagerRepository : GenericRepository<UserLoginHistory>, IUserManagerRepository
    {
        public IUserRoleRepository UserRoleRep;
        public IUserRepository Userrep;
        public IRoleRepository RoleRepo;
         public UserManagerRepository(IUnitOfWork context, IUserRoleRepository iuserrolerep,IUserRepository iuser,IRoleRepository rolerep)
            : base(context)
        {
            UserRoleRep = iuserrolerep;
            Userrep = iuser;
            RoleRepo = rolerep;
        }
         public IEnumerable<UserLoginHistory> GetUserLoginHistory()
        {
            return GetAll().AsQueryable().AsNoTracking(); 
        }
        /// <summary>
        /// This method is used to Insert based on user login history model 
        /// </summary>
        /// <param name="userlogin"></param>
        /// <returns>Return Integer value</returns>
        public int AddUserLoginHistory(UserLoginHistoryModel userlogin)
        {
            int resultValue = 0;
                var count =(from x in
                  GetAll().AsQueryable().Where(UserValidate(userlogin.UserId).And(UserDate(userlogin.LastLoginDate)))
              select new { x.LoginHisId, x.Count }).ToList();
                if (!count.Any())
                {
                    try
                    {
                        var userLoginHistory1 = new UserLoginHistory();
                        userLoginHistory1.InjectFrom(userlogin);
                        userLoginHistory1.Count = 1;
                        Add(userLoginHistory1);
                        Save();
                        resultValue = 1;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Exception occurs in AddUserLoginHistory", ex);
                    }
                }
                else
                {
                    foreach (var item in count)
                    {
                        var h = item.LoginHisId;
                        var userlcount = (int)item.Count;
                        var userLoginHistory1 = new UserLoginHistory();
                        userLoginHistory1.InjectFrom(userlogin);
                        userLoginHistory1.LoginHisId = h;
                        userLoginHistory1.Count = userlcount + 1;
                        Update(userLoginHistory1, Convert.ToInt64(item.LoginHisId));
                        Save();
                       resultValue= Convert.ToInt32(h);
                    }
                }
            return resultValue;
        }
        /// <summary>
        /// This method is used to get User login history based on user id
        /// </summary>
        /// <param name="dataSourceInput"></param>
        /// <returns>Return User Login History</returns>
        private static Expression<Func<UserLoginHistory, bool>> UserValidate(
          string dataSourceInput)
        {
            return lookUpIndex => lookUpIndex.UserId == dataSourceInput;
        }
        /// <summary>
        /// This methos is used to get user login history based on last login date
        /// </summary>
        /// <param name="dataSourceInput"></param>
        /// <returns>Return User Login History</returns>
        private static Expression<Func<UserLoginHistory, bool>> UserDate(
          DateTime?  dataSourceInput)
        {
#pragma warning disable 618
            return lookUpIndex => EntityFunctions.TruncateTime(lookUpIndex.LastLoginDate) == EntityFunctions.TruncateTime(dataSourceInput);
#pragma warning restore 618
        }
        /// <summary>
        /// This method is used to get all user login history data
        /// </summary>
        /// <returns>Return get user history</returns>
        public IEnumerable<UserLoginHistory> GetUserHistory()
        {

            return GetAll().AsQueryable().AsNoTracking();
        }
        /// <summary>
        /// This method is used to get login history between the dates 
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns>Retrun user history</returns>
        public async Task<IQueryable<LoginHistory>> GetLoginHistoryCount(DateTime fromDate, DateTime toDate)
        {
            var hisCount = ((from uhis in GetUserHistory().AsQueryable()
                             join uData in Userrep.GetUserLookupAll() on uhis.UserId equals uData.Id                         
                         join roleid in UserRoleRep.GetUserRoleLookupAll() on uData.Id equals roleid.UserId
                        join role in RoleRepo.GetUserRoles() on roleid.RoleId equals role.Id
                             where (true)
                            orderby uhis.LastLoginDate descending
                             select new
                             {
                                 uhis.LoginHisId,
                                 uhis.UserId,
                                 uhis.LastLoginDate,
                                 uhis.Count,
                                 uData.FirstName,
                                 uData.LastName,
                                 role.Name
                                 
                             }).AsQueryable().AsNoTracking()).ToList();
            var finalldata = hisCount.Select(hisData => new LoginHistory
            {
                LoginHisId = hisData.LoginHisId, UserId = hisData.UserId.ToString(),
                LastLoginDate = Convert.ToDateTime(hisData.LastLoginDate), 
                Count = hisData.Count.ToString(), 
                UserName = hisData.FirstName.ToString().Trim() + " " + hisData.LastName.ToString().Trim(),
                UserRole = hisData.Name
            }).ToList();
            return  await Task.FromResult(finalldata.AsQueryable());
        }
        /// <summary>
        /// This methos is used to get user created delete data based on role id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns>Return User created delete</returns>
        public async Task<IQueryable<UserCreatedDelete>> GetCreatedDeleteDateWiseCount(string roleId)
        {
            var hisCount = ((from uhis in Userrep.GetUserLookupAll().AsQueryable()
                             join roleid in UserRoleRep.GetUserRoleLookupAll() on uhis.Id equals roleid.UserId
                          orderby uhis.CreatedDate descending
                             select new
                             {
                              roleid.RoleId,
                                 uhis.CreatedDate,
                                 uhis.IsDelete
                             }).AsQueryable().AsNoTracking()).ToList();
            if (roleId == "2")
            {
                hisCount =( from hisCount1 in hisCount.Where(hisCount1 => hisCount1.RoleId != "1") select hisCount1).ToList();
            }
            var finalldata = hisCount.Select(hisData => new UserCreatedDelete
            {
                roleId=  hisData.RoleId,
                CreatedDate = Convert.ToDateTime(hisData.CreatedDate),
               IsDelete = Convert.ToBoolean(hisData.IsDelete)
            }).ToList();
            return  await Task.FromResult(finalldata.AsQueryable());
        }
    }
}