using System.Collections.Generic;
using System.Linq;
using BusinessCenter.Identity.IdentityModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BusinessCenter.Identity.IdentityExtension
{
    public static class IdentityExtensions
    {
        public static ApplicationIdentityResult ToApplicationIdentityResult(this IdentityResult identityResult)
        {
            return identityResult == null ? null : new ApplicationIdentityResult(identityResult.Errors, identityResult.Succeeded);
        }
        public static ApplicationUserRole ToApplicationUserRole(this IdentityUserRole<string> role)
        {
            return role == null ? null : new ApplicationUserRole { RoleId = role.RoleId, UserId = role.UserId };
        }
        //public static IEnumerable<AppUser> ToAppUserList(this IEnumerable<ApplicationUserManager> list)
        //{
        //    return list.Select(u => u.ToAppUser()).ToList();
        //}

        //public static AppUser ToAppUser(this ApplicationUserManager applicationIdentityUser)
        //{
        //    if (applicationIdentityUser == null)
        //    {
        //        return null;
        //    }
        //    var appUser = new AppUser();
        //    return appUser.CopyApplicationIdentityUserProperties(applicationIdentityUser);
        //}

        //public static AppUser CopyApplicationIdentityUserProperties(this AppUser appUser, ApplicationUserManager applicationIdentityUser)
        //{
        //    if (appUser == null)
        //    {
        //        return null;
        //    }
        //    if (applicationIdentityUser == null)
        //    {
        //        return null;
        //    }


        //    appUser.LockoutEnabled = applicationIdentityUser.;


        //    appUser.UserName = applicationIdentityUser.UserName;
        //    appUser.Id = applicationIdentityUser.Id;
        //    appUser.AccessFailedCount = applicationIdentityUser.AccessFailedCount;
        //    appUser.Email = applicationIdentityUser.Email;
        //    appUser.EmailConfirmed = applicationIdentityUser.EmailConfirmed;

        //    appUser.LockoutEndDateUtc = applicationIdentityUser.LockoutEndDateUtc;
        //    appUser.PasswordHash = applicationIdentityUser.PasswordHash;
        //    appUser.PhoneNumber = applicationIdentityUser.PhoneNumber;
        //    appUser.PhoneNumberConfirmed = applicationIdentityUser.PhoneNumberConfirmed;
        //    appUser.SecurityStamp = applicationIdentityUser.SecurityStamp;
        //    appUser.TwoFactorEnabled = applicationIdentityUser.TwoFactorEnabled;

        //    appUser.FirstName = applicationIdentityUser.FirstName;
        //    appUser.LastName = applicationIdentityUser.LastName;
        //    appUser.MobileNumber = applicationIdentityUser.MobileNumber;
        //    appUser.Address = applicationIdentityUser.Address;
        //    appUser.City = applicationIdentityUser.City;
        //    appUser.State = applicationIdentityUser.State;
        //    appUser.PostalCode = applicationIdentityUser.PostalCode;

        //    appUser.IsActive = applicationIdentityUser.IsActive;
        //    appUser.LastLoginDateandTime = applicationIdentityUser.LastLoginDateandTime;

        //    foreach (var claim in applicationIdentityUser.Claims)
        //    {
        //        appUser.Claims.Add(new ApplicationUserClaim
        //        {
        //            ClaimType = claim.ClaimType,
        //            ClaimValue = claim.ClaimValue,
        //            Id = claim.Id,
        //            UserId = claim.UserId
        //        });
        //    }
        //    foreach (var role in applicationIdentityUser.Roles)
        //    {
        //        appUser.Roles.Add(role.ToApplicationUserRole());
        //    }
        //    foreach (var login in applicationIdentityUser.Logins)
        //    {
        //        appUser.Logins.Add(new ApplicationUserLogin
        //        {
        //            LoginProvider = login.LoginProvider,
        //            ProviderKey = login.ProviderKey,
        //            UserId = login.UserId
        //        });
        //    }
        //    return appUser;
        //}
    }
}