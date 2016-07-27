using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using System.Collections.Generic;
using System.Linq;

namespace BusinessCenter.Data.Implementation
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(IUnitOfWork context)
            : base(context)
        {

        }
        /// <summary>
        /// This method is used to get all user role 
        /// </summary>
        /// <returns>Return Role data</returns>
        public IEnumerable<Role> GetUserRoles()
        {
            return GetAll().AsQueryable(); 
        }
    }
}
