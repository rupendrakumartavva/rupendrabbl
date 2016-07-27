using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
   public interface IUserRoleRepository
    {
       IEnumerable<UserRole> GetUserRoleLookupAll();
       IEnumerable<UserRole> FindByID(string userId);
       IEnumerable<UserRole> FindBy(string id);
    }
}
