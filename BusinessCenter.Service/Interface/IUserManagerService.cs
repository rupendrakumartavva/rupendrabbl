using BusinessCenter.Data.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessCenter.Service.Interface
{
    public interface IUserManagerService
    {
        int UserLoginHistory(UserLoginHistoryModel userlogin);
        Task<IQueryable<LoginHistory>> GetLoginHistoryCount(DateTime FromDate, DateTime ToDate);
        Task<IQueryable<UserCreatedDelete>> GetCreatedDeleteDateWiseCount(string roleId);
    }
}