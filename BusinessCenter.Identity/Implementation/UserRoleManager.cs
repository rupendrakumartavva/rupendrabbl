using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using BusinessCenter.Identity.Common;
using BusinessCenter.Identity.IdentityExtension;
using BusinessCenter.Identity.IdentityModels;
using BusinessCenter.Identity.Interfaces;
using Microsoft.AspNet.Identity;

namespace BusinessCenter.Identity.Implementation
{
    public class UserRoleManager : IUserRoleManager
    {
        private readonly ApplicationRoleManager _roleManager;

       // private readonly RoleManager<ApplicationRole, int> _roleManager;
        private bool _disposed;

        public UserRoleManager(ApplicationRoleManager roleManager)
        {
            _roleManager = roleManager;
        }

        /// <summary>
        /// Gets the List of All Roles available in RoleStore
        /// </summary>
        /// <returns></returns>
        public  IEnumerable<ApplicationRole> GetRoles()
        {
    //   var h=     _roleManager.FindByName("Administrator").Users;
            return _roleManager.Roles.ToList();
        }

        /// <summary>
        /// Gets the List of All Roles asynchronously which are available in RoleStore 
        /// </summary>
        /// <returns></returns>
        public  async Task<IEnumerable<ApplicationRole>> GetRolesAsync()
        {
            var applicationRoles = await _roleManager.Roles.ToListAsync().ConfigureAwait(false);

            List<ApplicationRole> roles = new List<ApplicationRole>();

            foreach (var items in applicationRoles.ToList())
            {
                ApplicationRole roleModel = new ApplicationRole();
                roleModel.Id = items.Id;
                roleModel.Name = items.Name;
                roles.Add(roleModel);
               
            }
            return roles.ToList();
        }
       
        /// <summary>
        /// Diallocates the memory alloted to Managed and UnManaged Resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                if (_roleManager != null)
                {
                    _roleManager.Dispose();
                }
            }
            _disposed = true;
        }

    }
}