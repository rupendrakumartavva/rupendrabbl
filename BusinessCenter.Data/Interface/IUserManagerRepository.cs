using BusinessCenter.Data.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
    public interface IUserManagerRepository
    {
        int AddUserLoginHistory(UserLoginHistoryModel userlogin);
        Task<IQueryable<LoginHistory>> GetLoginHistoryCount(DateTime fromDate, DateTime toDate);
        Task<IQueryable<UserCreatedDelete>> GetCreatedDeleteDateWiseCount(string roleId);
    }
}