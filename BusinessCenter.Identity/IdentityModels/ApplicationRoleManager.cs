using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace BusinessCenter.Identity.IdentityModels
{
    /// <summary>
    /// This Class Enables us To Manage Roles For the Users
    /// by creating Roles in the RoleStore
    /// </summary>
    public class ApplicationRoleManager : RoleManager<ApplicationRole, string>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole, string> store)
            : base(store)
        {

        }


        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            return new ApplicationRoleManager(new RoleStore<ApplicationRole, string, ApplicationUserRole>(context.Get<ApplicationDbContext>()));
        }
    }
}