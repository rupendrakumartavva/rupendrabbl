using BusinessCenter.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace BusinessCenter.Service.Interface
{
    public interface IMyServiceDetails
    {
        int  UserServiceAdd(UserServiceModel userService);
        int UserSaveListCount(string userId);
        IQueryable<CommonData> GetSearchData(UserServiceModel userService);
        IEnumerable<SearchDataViewModel> GetCount(UserServiceModel userService);
        IQueryable<CommonData> GetAllData(UserServiceModel searchServiceInput);
        //IEnumerable<CommonData> GetAbra(UserServiceModel userService);
        //IEnumerable<CommonData> GetBbl(UserServiceModel userService);
        //IEnumerable<CommonData> GetCbe(UserServiceModel userService);
        //IEnumerable<CommonData> GetOpla(UserServiceModel userService);
      
        //IEnumerable<CommonData> GetCorp(UserServiceModel userService);
        void DeleteUserService(UserServiceModel userService);
        void DeleteUserService(List<UserServiceModel> userService);
        void DeleteSingleUerService(string userId);
    }
}