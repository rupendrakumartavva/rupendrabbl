using BusinessCenter.Data;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Models;
using BusinessCenter.Service.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessCenter.Service.Implementation
{
    public class UserManagerService : IUserManagerService
    {
        //IUserManagerRepository
        protected IUserManagerRepository Repository;

        /// <summary>
        ///
        /// </summary>
        /// <param name="repo"></param>
        public UserManagerService(IUserManagerRepository repo)
        {
            Repository = repo;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userlogin"></param>
        /// <returns></returns>
        public int UserLoginHistory(UserLoginHistoryModel userlogin)
        {
            return Repository.AddUserLoginHistory(userlogin);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <returns></returns>
        public async Task<IQueryable<LoginHistory>> GetLoginHistoryCount(DateTime FromDate, DateTime ToDate)
        {
            var commandata = await Repository.GetLoginHistoryCount(FromDate, ToDate);
            return commandata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>

        public async Task<IQueryable<UserCreatedDelete>> GetCreatedDeleteDateWiseCount(string roleId)
        {
            var commandata = await Repository.GetCreatedDeleteDateWiseCount(roleId);
            return commandata;
        }
    }
}