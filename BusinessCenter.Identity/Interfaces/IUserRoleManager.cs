using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessCenter.Identity.IdentityModels;

namespace BusinessCenter.Identity.Interfaces
{
    public interface IUserRoleManager: IDisposable
    {

        IEnumerable<ApplicationRole> GetRoles();

        Task<IEnumerable<ApplicationRole>> GetRolesAsync();
    }
}