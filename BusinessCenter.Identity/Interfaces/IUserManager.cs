using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Identity.IdentityExtension;
using BusinessCenter.Identity.IdentityModels;

namespace BusinessCenter.Identity.Interfaces
{
  public  interface IUserManager
    {
      Task<RegistartionStatus> AdminRegistartion(AppUser userDetails, string password, string[] selectedRoles);
      Task<RegistartionStatus> UserRegistartion(AppUser userDetails, string password, string[] selectedRoles);
   
      Task<ApplicationIdentityResult> UserToRolesAsync(string userId, string[] roles);
      Task<string> EmailConfirmationTokenGenaration(string userId);

      Task<ApplicationIdentityResult> UserMailConfirmation(string userId, string token);

      Task<ApplicationIdentityResult> UpdateAsync(AppUser user, string userId);

      Task<AppUser> FindUserDetailsByIdAsync(string userId);

      Task<ApplicationIdentityResult> UserDeleteAsync(string userId);

      Task<bool> CheckUserAvailableAsync(string userName);
      Task<UserSignInStatus> PasswordSignIn(string userName, string password, bool isPersistent, bool shouldLockout);
      Task<UserSignInStatus> SignInOrTwoFactor(AppUser user, bool isPersistent);
      Task SignInAsync(AppUser user, bool isPersistent, bool rememberBrowser);
      Task<ApplicationIdentityResult> AccessFailedAsync(string userId);
      Task<AppUser> FindByNameAsync(string userName);
      Task<bool> CheckPasswordAsync(AppUser user, string password);
      Task<IList<string>> GetRolesAsync(string userId);
      void SignOut(params string[] authenticationTypes);
      Task<bool> IsEmailConfirmedAsync(string userId);
      Task<string> GeneratePasswordResetTokenAsync(string userId);
      Task<ApplicationIdentityResult> ResetPasswordAsync(string userId, string token, string newPassword);
      Task<ApplicationIdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword);

      Task<bool> IsCheckPasswordAsync(AppUser user, string password);
      Task<AppUser> FindByEmailAddress(string userEmail);

      //Created by Srinivas
      Task<bool> CheckUserEmailAvailableAsync(string Email);
      Task<IEnumerable<AppUser>> AllUsers();
      Task<AppUser> FindUser(string userName, string password);

      Task<bool> CheckUserSecondaryEmailAvailableAsync(string Email, string userid);

      Task<List<string>> GetUserRoleName(string userId);
    }
}

