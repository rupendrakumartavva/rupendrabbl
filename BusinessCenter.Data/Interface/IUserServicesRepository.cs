using System;
using System.Threading.Tasks;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BusinessCenter.Data.Common;

namespace BusinessCenter.Data.Interface
{
    public interface IUserServicesRepository 
    {
        int UserServiceAdd(UserServiceModel userService);

        int UserSaveListCount(string userId);
        IQueryable<CommonData> GetSearchData(UserServiceModel userService);
        IEnumerable<SearchDataViewModel> GetCount(UserServiceModel userService);
    //    IQueryable<UserService> FindBy(Expression<Func<UserService, bool>> predicate);
        IQueryable<CommonData> GetAllData(UserServiceModel searchinput);
        void DeleteUserService(UserServiceModel userService);
        void DeleteSingleUerService(string userId);
         void DeleteUserService(List<UserServiceModel> userService);
         IQueryable<UserService> FindBy(Expression<Func<UserService, bool>> predicate);
         IEnumerable<UserService> GetUserServiceAll();
    }
}