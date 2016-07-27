using BusinessCenter.Data.Model;
using BusinessCenter.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
  public  interface IUserRepository
    {
      IEnumerable<User> GetUserLookupAll();
      IEnumerable<User> FindByID(string userId);
      IEnumerable<User> GetUsersBasedOnId(string userStatus, int roleId);
      int GetuserByUserName(string userName);
      IEnumerable<string> FindByUserName(string term);
      bool UpdateLoggedInStatus(string userId, bool status);
      bool UpdateLoggedTime(Userdetails userdetail);
      IEnumerable<User> FindByLoginUsername(string userName);
    }
}
