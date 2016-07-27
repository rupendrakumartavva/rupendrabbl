using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using System.Collections.Generic;
using System.Linq;

namespace BusinessCenter.Data.Implementation
{
    public class UserRoleRepository : GenericRepository<UserRole>, IUserRoleRepository
    {
       public UserRoleRepository(IUnitOfWork context)
            : base(context)
        {
        }
        /// <summary>
        /// This method is used to Get All User Role 
        /// </summary>
        /// <returns>Return User Role</returns>
       public IEnumerable<UserRole> GetUserRoleLookupAll()
       {
           return GetAll().AsQueryable();
       }
        /// <summary>
        /// This method is used to Get User Role based on User Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Return User Roles</returns>
       public IEnumerable<UserRole> FindByID(string userId)
       {
           var userdata =FindBy(x => x.UserId == userId).ToList();
           return userdata;
       }
        /// <summary>
        /// This method is used to Get User Role Based on Role Id
        /// </summary>
        /// <param name="roleId"></param>
       /// <returns>Return User Roles</returns>
       public IEnumerable<UserRole> FindBy(string roleId)
       {
           var userdata = FindBy(x => x.RoleId == roleId).ToList();
           return userdata;
       }
    }
}