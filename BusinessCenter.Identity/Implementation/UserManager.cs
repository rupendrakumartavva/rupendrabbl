using BusinessCenter.Data;
using BusinessCenter.Identity.Common;
using BusinessCenter.Identity.IdentityExtension;
using BusinessCenter.Identity.IdentityModels;
using BusinessCenter.Identity.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Identity.Implementation
{
    public class UserManager : IUserManager
    {
        private readonly ApplicationUserManager _userManager;
        private readonly IAuthenticationManager _authenticationManager;

        // private ApplicationDbContext gg = new ApplicationDbContext();
        /// <summary>
        ///
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="authenticationManager"></param>
        public UserManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
        {
            _userManager = userManager;
            _authenticationManager = authenticationManager;
        }

        /// <summary>
        /// user can register here by passing the respective parameters
        /// </summary>
        /// <param name="userDetails">User details</param>
        /// <param name="password">password</param>
        /// <param name="selectedRoles">rolenames are passed by list</param>
        /// <returns></returns>
        ///
        public async Task<RegistartionStatus> UserRegistartion(AppUser userDetails, string password, string[] selectedRoles)
        {
            var checkUser = CheckUserAvailableAsync(userDetails.UserName);

            if (checkUser.Result == false)
            {
                return RegistartionStatus.Exists;
            }
            else
            {
                var chckEmail = CheckUserEmailAvailableAsync(userDetails.Email);

                if (chckEmail.Result)
                {
                    var userCreateResult = await _userManager.CreateAsync(userDetails, password);
                    if (userCreateResult.Succeeded)
                    {
                        var rolesAdd = await UserToRolesAsync(userDetails.Id, selectedRoles);
                        if (rolesAdd.Succeeded)
                        {
                            var code = await EmailConfirmationTokenGenaration(userDetails.Id);
                            var applicationUser = await _userManager.FindByIdAsync(userDetails.Id);

                            var regUserDetails = new AppUser();
                            regUserDetails.InjectFrom(applicationUser);

                            regUserDetails.Address = string.Empty;
                            regUserDetails.City = string.Empty;
                            regUserDetails.MobileNumber = string.Empty;
                            regUserDetails.PostalCode = string.Empty;
                            regUserDetails.State = string.Empty;
                            regUserDetails.LastLoginDateandTime = Convert.ToDateTime(System.DateTime.Now);
                            regUserDetails.PhoneNumberConfirmed = true;
                            regUserDetails.TwoFactorEnabled = false;
                            regUserDetails.LockoutEnabled = false;
                            regUserDetails.AccessFailedCount = 0;
                            regUserDetails.ActivationCode = code;
                            regUserDetails.ActivationDate = Convert.ToDateTime(System.DateTime.Now.AddDays(1));
                            regUserDetails.IsLoggedIn = userDetails.IsLoggedIn;
                            regUserDetails.LoginSessionId = userDetails.LoginSessionId;
                            var updateUseProfile = await UpdateAsync(regUserDetails, applicationUser.Id);
                        }
                    }
                    else
                    {
                        return RegistartionStatus.Failure;
                    }
                    return RegistartionStatus.Success;
                }
                else
                {
                    return RegistartionStatus.EmailExists;
                }
            }
        }

        public async Task<AppUser> FindUser(string userName, string password)
        {
            AppUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public async Task<List<string>> GetUserRoleName(string userId)
        {
            var roles = await _userManager.GetRolesAsync(userId);
            return roles.ToList();
        }

        /// <summary>
        /// user can register here by passing the respective parameters
        /// </summary>
        /// <param name="userDetails">User details</param>
        /// <param name="password">password</param>
        /// <param name="selectedRoles">rolenames are passed by list</param>
        /// <returns></returns>
        public async Task<RegistartionStatus> AdminRegistartion(AppUser userDetails, string password, string[] selectedRoles)
        {
            //var checkUser = CheckUserAvailableAsync(userDetails.UserName);

            //if (checkUser.Result == false)
            //{
            //    return RegistartionStatus.Exists;
            //}
            //else
            //{
            var chckEmail = CheckUserEmailAvailableAsync(userDetails.Email);

            if (chckEmail.Result)
            {
                var checkUser = CheckUserAvailableAsync(userDetails.UserName);

                if (checkUser.Result == false)
                {
                    return RegistartionStatus.Exists;
                }
                else
                {
                    var userCreateResult = await _userManager.CreateAsync(userDetails, password);
                    if (userCreateResult.Succeeded)
                    {
                        var rolesAdd = await UserToRolesAsync(userDetails.Id, selectedRoles);
                        if (rolesAdd.Succeeded)
                        {
                            var code = await EmailConfirmationTokenGenaration(userDetails.Id);
                            var applicationUser = await _userManager.FindByIdAsync(userDetails.Id);
                            var regUserDetails = new AppUser();
                            regUserDetails.InjectFrom(applicationUser);
                            //  regUserDetails.Id = Guid.NewGuid().ToString().Substring(0, 15);
                            regUserDetails.Address = userDetails.Address;
                            regUserDetails.City = userDetails.City;
                            regUserDetails.MobileNumber = userDetails.MobileNumber;
                            regUserDetails.PostalCode = userDetails.PostalCode;
                            regUserDetails.State = userDetails.State;
                            regUserDetails.LastLoginDateandTime = Convert.ToDateTime(System.DateTime.Now);
                            regUserDetails.PhoneNumberConfirmed = true;
                            regUserDetails.TwoFactorEnabled = false;
                            regUserDetails.LockoutEnabled = false;
                            regUserDetails.AccessFailedCount = 0;
                            regUserDetails.ActivationCode = code;
                            regUserDetails.IsLoggedIn = userDetails.IsLoggedIn;
                            regUserDetails.LoginSessionId = userDetails.LoginSessionId;
                            regUserDetails.ActivationDate = Convert.ToDateTime(System.DateTime.Now.AddDays(1));
                            var updateUseProfile = await UpdateAsync(regUserDetails, applicationUser.Id);
                        }
                    }
                    else
                    {
                        return RegistartionStatus.Failure;
                    }
                    return RegistartionStatus.Success;
                }
            }
            else
            {
                return RegistartionStatus.EmailExists;
            }

            // }
        }

        /// <summary>
        /// User roles can be added in the userroles section.by using UserToRolesAsync method.
        /// </summary>
        /// <param name="userId">pass user id</param>
        /// <param name="selectedRoles">pass role names</param>
        /// <returns></returns>
        public async Task<ApplicationIdentityResult> UserToRolesAsync(string userId, string[] selectedRoles)
        {
            var result = await _userManager.AddToRolesAsync(userId, selectedRoles);
            return result.ToApplicationIdentityResult();
        }

        /// <summary>
        /// Email token code is  genarating at the  time of user registaion.This will be sent to user registaion time mail.
        /// </summary>
        /// <param name="userId">passing user id .</param>
        /// <returns>it will return system genarated encrypt code .</returns>
        public async Task<string> EmailConfirmationTokenGenaration(string userId)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(userId);
            return code;
        }

        /// <summary>
        /// user activation with user id and security token code
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<ApplicationIdentityResult> UserMailConfirmation(string userId, string token)
        {
            var result = await _userManager.ConfirmEmailAsync(userId, token);
            return result.ToApplicationIdentityResult();
        }

        /// <summary>
        /// Base on the UserId Parameter At first verifies User exist or not,
        /// If User is active then User Details can be Updated
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public virtual async Task<ApplicationIdentityResult> UpdateAsync(AppUser user, string userId)
        {
            var applicationUser = await _userManager.FindByIdAsync(userId).ConfigureAwait(false);
            if (applicationUser == null)
            {
                return new ApplicationIdentityResult(new[] { "Invalid user Id" }, false);
            }

            applicationUser.Address = user.Address ?? "";
            applicationUser.FirstName = user.FirstName;
            applicationUser.LastName = user.LastName.Trim();
            applicationUser.MobileNumber = user.MobileNumber ?? "";
            applicationUser.PostalCode = user.PostalCode ?? "";
            applicationUser.State = user.State ?? "";
            applicationUser.City = user.City ?? "";
            applicationUser.IsActive = user.IsActive;
            applicationUser.Email = user.Email;
            applicationUser.LastLoginDateandTime = user.LastLoginDateandTime;
            applicationUser.MobileNumber = user.MobileNumber ?? "";
            applicationUser.PostalCode = user.PostalCode ?? "";
            applicationUser.LastLoginDateandTime = Convert.ToDateTime(System.DateTime.Now);
            applicationUser.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
            applicationUser.TwoFactorEnabled = user.TwoFactorEnabled;
            applicationUser.LockoutEnabled = user.LockoutEnabled;
            applicationUser.LockoutEndDateUtc = user.LockoutEndDateUtc;
            applicationUser.AccessFailedCount = user.AccessFailedCount;
            applicationUser.ActivationCode = user.ActivationCode;
            applicationUser.SecurityQuestion1 = user.SecurityQuestion1;
            applicationUser.SecurityQuestion2 = user.SecurityQuestion2;
            applicationUser.SecurityQuestion3 = user.SecurityQuestion3;
            applicationUser.SecurityAnswer1 = user.SecurityAnswer1.Trim();
            applicationUser.SecurityAnswer2 = user.SecurityAnswer2.Trim();
            applicationUser.SecurityAnswer3 = user.SecurityAnswer3.Trim();
            applicationUser.ActivationDate = Convert.ToDateTime(user.ActivationDate);
            applicationUser.ChangeEmailConfirmed = user.ChangeEmailConfirmed;
            applicationUser.ChangeEmailValidate = user.ChangeEmailValidate;
            applicationUser.PreviousEmailConfirmed = user.PreviousEmailConfirmed;
            applicationUser.PreviousEmailValidate = user.PreviousEmailValidate;
            applicationUser.IsDelete = user.IsDelete;
            applicationUser.IsForgot = user.IsForgot;
            applicationUser.CreatedDate = user.CreatedDate;
            applicationUser.UpdatedDate = user.UpdatedDate;
            applicationUser.DeleteComment = user.DeleteComment;
            applicationUser.IsLoggedIn = user.IsLoggedIn;
            applicationUser.LoginSessionId = user.LoginSessionId;
            if (user.SecondaryEmail != null)
            {
                applicationUser.SecondaryEmail = user.SecondaryEmail.Trim();
            }
            if (user.PasswordHash != null)
                applicationUser.PasswordHash = user.PasswordHash;
            //       var userDetails = ToConvertUser.ToDataConvert(user);

            var identityResult = await _userManager.UpdateAsync(applicationUser).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }

        /// <summary>
        /// Based on the UserId, that particular UserDetails is taken from the UserStores
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public virtual async Task<AppUser> FindUserDetailsByIdAsync(string userId)
        {
            var applicationUser = await _userManager.FindByIdAsync(userId).ConfigureAwait(false);
            return applicationUser;
        }

        public virtual async Task<AppUser> FindByEmailAddress(string userEmail)
        {
            var applicationUser = await _userManager.FindByEmailAsync(userEmail).ConfigureAwait(false);
            return applicationUser;
        }

        /// <summary>
        /// If Checks for the User in UserStore based on the UserId. If the User is not found,
        /// then it throws an error Invalid User or If User is Found in UserStore then that
        /// Particular user is deleted
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ApplicationIdentityResult> UserDeleteAsync(string userId)
        {
            var applicationUser = await _userManager.FindByIdAsync(userId);
            if (applicationUser == null)
            {
                return new ApplicationIdentityResult(new[] { "Invalid user Id" }, false);
            }
            var identityResult = await _userManager.DeleteAsync(applicationUser).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }

        /// <summary>
        /// It checks the UserStore, If there is an User With the UserName or not which is specified as Parameter
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<bool> CheckUserAvailableAsync(string userName)
        {
            try
            {
                var validate = _userManager.FindByName(userName.Trim());

                if (validate == null)
                {
                    return true;
                }
                else
                {
                    bool chcekDate = GenaralConvertion.CheckDateExpire(validate.CreatedDate.ToString(),
                     System.DateTime.Now.ToString());
                    if (validate.IsDelete == true)
                    {
                        return false;
                    }
                    if ((validate.IsActive == false) && (validate.IsDelete == false) && chcekDate == false)
                    {
                        await UserDeleteAsync(validate.Id);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// It Enables the User to Signin based on Username, Password as Parameters. If the User is not Locked
        /// If the parameter should lockout is true. Then User is getting Locked
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="isPersistent"></param>
        /// <param name="shouldLockout"></param>
        /// <returns></returns>
        public virtual async Task<UserSignInStatus> PasswordSignIn(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            var user = await FindByNameAsync(userName);
            if (user == null)
            {
                return UserSignInStatus.Nodata;
            }

            if (await IsLockedOutAsync(user.Id))
            {
                return UserSignInStatus.LockedOut;
            }
            if (user.IsDelete == true)
            {
                return UserSignInStatus.Delete;
            }
            if (user.IsActive == false)
            {
                bool chcekDate = GenaralConvertion.CheckExpireDate(user.ActivationDate.ToString(),
                     System.DateTime.Now.ToString());
                if ((user.IsActive == false) && (user.IsDelete == false) && chcekDate == true)
                {
                    await UserDeleteAsync(user.Id);
                    return UserSignInStatus.Nodata;
                }
                else
                {
                    return UserSignInStatus.In_Active;
                }
            }
            if (await CheckPasswordAsync(user, password))
            {
                if (user.IsActive == true)
                {
                    //if (user.IsLoggedIn == false)
                    //{
                    if (user.IsActive == true && user.IsDelete == false)
                    {
                        return await SignInOrTwoFactor(user, isPersistent);
                    }
                    else
                    {
                        return UserSignInStatus.Delete;
                    }
                    //}
                    //else
                    //{
                    //    return UserSignInStatus.AlreadyLoggedIn;
                    //}
                }
                else
                {
                    //DateTime expireDate = GenaralConvertion.ValidateGivenTime(user.LockoutEndDateUtc.ToString());
                    bool chcekDate = GenaralConvertion.CheckDateExpire(user.CreatedDate.ToString(),
                       System.DateTime.Now.ToString());
                    if ((user.IsActive == false) && (user.IsDelete == false) && chcekDate == false)
                    {
                        // UserSignInStatus.In_Active;
                        await UserDeleteAsync(user.Id);
                        return UserSignInStatus.Nodata;
                    }
                    else
                    {
                        return UserSignInStatus.In_Active;
                    }
                }
            }
            if (shouldLockout)
            {
                if (user.LockoutEndDateUtc != null)
                {
                    bool chcekDate = GenaralConvertion.CheckDateExpireForLockout(user.LockoutEndDateUtc.ToString(),
                        System.DateTime.UtcNow.ToString());
                    if (chcekDate == false)
                    {
                        await LoginUpdate(user, user.Id);
                    }
                }
                // If lockout is requested, increment access failed count which might lock out the user
                await AccessFailedAsync(user.Id);
                if (await IsLockedOutAsync(user.Id))
                {
                    return UserSignInStatus.LockedOut;
                }
            }
            return UserSignInStatus.Failure;
        }

        /// <summary>
        /// If the User Logged in using ThirdParty Authenctication such as Gmail
        /// then Sigin is done based on the Parameter Appuser which redirects to Claims
        /// </summary>
        /// <param name="user"></param>
        /// <param name="isPersistent"></param>
        /// <returns></returns>
        public virtual async Task<UserSignInStatus> SignInOrTwoFactor(AppUser user, bool isPersistent)
        {
            //if (await GetTwoFactorEnabledAsync(user.Id) &&
            //    !await TwoFactorBrowserRememberedAsync(user.Id))
            //{
            //    var identity = new ClaimsIdentity(DefaultAuthenticationTypes.TwoFactorCookie);
            //    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            //    SignIn(identity);
            //    return UserSignInStatus.RequiresTwoFactorAuthentication;
            //}

            await SignInAsync(user, isPersistent, false);

            await LoginUpdate(user, user.Id);
            return UserSignInStatus.Success;
        }

        private async Task<string> LoginUpdate(AppUser user, string userId)
        {
            var userDetails = ToConvertUser.ToDataConvert(user);
            var regUserDetails = new AppUser();

            var userrole = await GetUserRoleName(user.Id);
            string name = userrole[0].ToString();

            regUserDetails.InjectFrom(userDetails);
            regUserDetails.AccessFailedCount = 0;
            regUserDetails.LockoutEnabled = false;
            regUserDetails.LastLoginDateandTime = System.DateTime.Now;
            regUserDetails.IsForgot = false;
            regUserDetails.UpdatedDate = System.DateTime.Now;

            regUserDetails.LockoutEndDateUtc = System.DateTime.UtcNow;
            //if (name == "Employee")
            //    regUserDetails.IsLoggedIn = false;
            //else
            //    regUserDetails.IsLoggedIn = true;
            // regUserDetails.IsLoggedIn = true;
            regUserDetails.IsLoggedIn = false;
            await UpdateAsync(regUserDetails, user.Id);
            return user.FirstName + " " + user.LastName;
        }

        /// <summary>
        /// To Sigin using External Authentication such as Gmail, this methods also clears
        /// cookies which are created by External Authentication
        /// </summary>
        /// <param name="user"></param>
        /// <param name="isPersistent"></param>
        /// <param name="rememberBrowser"></param>
        /// <returns></returns>
        public virtual async Task SignInAsync(AppUser user, bool isPersistent, bool rememberBrowser)
        {
            // Clear any partial cookies from external or two factor partial sign ins
            SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
            var userIdentity = await CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            //if (rememberBrowser)
            //{
            //    var rememberBrowserIdentity = CreateTwoFactorRememberBrowserIdentity(user.Id);
            //    _authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, userIdentity, rememberBrowserIdentity);
            //}
            //else
            //{
            _authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, userIdentity);
            //}
        }

        /// <summary>
        /// Creates a ClaimsIdentity which is representing the User by accepting AppUser and AuthenticationType as Parameter
        /// </summary>
        /// <param name="user"></param>
        /// <param name="authenticationType"></param>
        /// <returns></returns>
        public virtual async Task<ClaimsIdentity> CreateIdentityAsync(AppUser user, string authenticationType)
        {
            var claimsIdentity = await _userManager.CreateIdentityAsync(user, authenticationType).ConfigureAwait(false);

            return claimsIdentity;
        }

        /// <summary>
        /// Creates a TwoFactorRememberBrowser cookie for a User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public virtual ClaimsIdentity CreateTwoFactorRememberBrowserIdentity(int userId)
        {
            return _authenticationManager.CreateTwoFactorRememberBrowserIdentity(userId.ToString());
        }

        /// <summary>
        /// This method accepts AuthenticationType as Parameter and Enables user to Logout
        /// </summary>
        /// <param name="authenticationTypes"></param>
        public void SignOut(params string[] authenticationTypes)
        {
            _authenticationManager.SignOut(authenticationTypes);
        }

        /// <summary>
        /// Get whether Two Factor Authentication is enabled for a User or not based on the Userid
        /// which is provided as a Parameter
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public virtual async Task<bool> GetTwoFactorEnabledAsync(string userId)
        {
            //
            return await _userManager.GetTwoFactorEnabledAsync(userId).ConfigureAwait(false);
        }

        /// <summary>
        /// If accepts UserId as Parameter and Checks if there is a TwoFactorBrowserRemember cookie for a user or not.
        /// If cookie is there then it returns true. If not returns false
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public virtual async Task<bool> TwoFactorBrowserRememberedAsync(string userId)
        {
            return await _authenticationManager.TwoFactorBrowserRememberedAsync(userId.ToString()).ConfigureAwait(false);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="identities"></param>
        public virtual void SignIn(params ClaimsIdentity[] identities)
        {
            _authenticationManager.SignIn(identities);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public virtual async Task<ApplicationIdentityResult> AccessFailedAsync(string userId)
        {
            int count = await _userManager.GetAccessFailedCountAsync(userId);

            // int valiDateCount = count + 1;
            int valiDateCount = count + 1;
            string configvalue1 = ConfigurationManager.AppSettings["DefaultAccountLockoutTimeSpan"];
            int lockOutValue = Convert.ToInt32(configvalue1);
            if (valiDateCount >= lockOutValue)
            {
                _userManager.SetLockoutEnabled(userId, true);
            }

            var identityResult = await _userManager.AccessFailedAsync(userId).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }

        /// <summary>
        /// If Accepts UserName as a Parameter and Checks whether that UserName is present in UserStore or not
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public virtual async Task<AppUser> FindByNameAsync(string userName)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// It checks whether the User is Locked or Not.
        /// It returns true If the User is already locked out or Returns false.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public virtual async Task<bool> IsLockedOutAsync(string userId)
        {
            return await _userManager.IsLockedOutAsync(userId).ConfigureAwait(false);
        }

        /// <summary>
        /// It takes AppUser and Password as Parameters and compares Password with
        /// the Password in UserStore for that Particular Username
        /// Returns true if the Password is valid for that Username or Returns false
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public virtual async Task<bool> CheckPasswordAsync(AppUser user, string password)
        {
            // var applicationUser = user.ToApplicationUser();
            var flag = await _userManager.CheckPasswordAsync(user, password);
            //  user.CopyApplicationIdentityUserProperties(applicationUser);
            return flag;
        }

        /// <summary>
        /// Based on the UserId which is taken as a Parameter.
        /// It gets all the roles available for that user from the UserStore
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public virtual async Task<IList<string>> GetRolesAsync(string userId)
        {
            return await _userManager.GetRolesAsync(userId).ConfigureAwait(false);
        }

        /// <summary>
        /// This method returns true If the User's email has been confirmed
        /// or it returns false based on the UserId which is taken as a Parameter
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public virtual async Task<bool> IsEmailConfirmedAsync(string userId)
        {
            return await _userManager.IsEmailConfirmedAsync(userId).ConfigureAwait(false);
        }

        /// <summary>
        /// Generates a Password reset token for the user using the UserTokenProvider based on UserId
        /// which is taken as a Parameter
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public virtual async Task<string> GeneratePasswordResetTokenAsync(string userId)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(userId).ConfigureAwait(false);
        }

        /// <summary>
        /// It resets a User's password by using a reset password token.
        /// This method takes UserId, PasswordToken and New Password as Parameters
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public virtual async Task<ApplicationIdentityResult> ResetPasswordAsync(string userId, string token,
           string newPassword)
        {
            var identityResult = await _userManager.ResetPasswordAsync(userId, token, newPassword).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }

        /// <summary>
        /// It generates a Password Reset token for the User using the UserTokenProvider and
        /// then it accepts that token, UserId and New Password as Parameters and Resets a User Password
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="currentPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public virtual async Task<ApplicationIdentityResult> ChangePasswordAsync(string userId, string currentPassword,
            string newPassword)
        {
            var code = await _userManager.GeneratePasswordResetTokenAsync(userId);
            var identityResult = await _userManager.ResetPasswordAsync(userId, code, newPassword).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }

        /// <summary>
        /// It Checks if the Password given is valid for that user or not in the UserStore.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> IsCheckPasswordAsync(AppUser user, string password)
        {
            var identityResult = await _userManager.CheckPasswordAsync(user, password).ConfigureAwait(false); ;
            return identityResult;
        }

        //public async Task<bool> IsCheckPasswordAsync1(UserPasswordTracking user, string password)
        //{
        //    var identityResult = await _userManager.CheckPasswordAsync(user, password).ConfigureAwait(false); ;
        //    return identityResult;
        //}

        //Created by Srinivas
        /// <summary>
        ///
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        public async Task<bool> CheckUserEmailAvailableAsync(string Email)
        {
            try
            {
                var validate = await _userManager.FindByEmailAsync(Email.Trim());
                return validate == null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<AppUser>> AllUsers()
        {
            var users = await _userManager.Users.ToListAsync().ConfigureAwait(false);

            List<AppUser> user = new List<AppUser>();

            foreach (var applicationIdentityUser in users.ToList())
            {
                AppUser appUser = new AppUser();
                appUser.Id = applicationIdentityUser.Id;
                appUser.FirstName = applicationIdentityUser.FirstName;
                appUser.LockoutEnabled = applicationIdentityUser.LockoutEnabled;

                appUser.UserName = applicationIdentityUser.UserName;
                appUser.Id = applicationIdentityUser.Id;
                appUser.AccessFailedCount = applicationIdentityUser.AccessFailedCount;
                appUser.Email = applicationIdentityUser.Email;
                appUser.EmailConfirmed = applicationIdentityUser.EmailConfirmed;

                appUser.LockoutEndDateUtc = applicationIdentityUser.LockoutEndDateUtc;
                appUser.PasswordHash = applicationIdentityUser.PasswordHash;
                appUser.PhoneNumber = applicationIdentityUser.PhoneNumber;
                appUser.PhoneNumberConfirmed = applicationIdentityUser.PhoneNumberConfirmed;
                appUser.SecurityStamp = applicationIdentityUser.SecurityStamp;
                appUser.TwoFactorEnabled = applicationIdentityUser.TwoFactorEnabled;

                appUser.FirstName = applicationIdentityUser.FirstName;
                appUser.LastName = applicationIdentityUser.LastName;
                appUser.MobileNumber = applicationIdentityUser.MobileNumber;
                appUser.Address = applicationIdentityUser.Address;
                appUser.City = applicationIdentityUser.City;
                appUser.State = applicationIdentityUser.State;
                appUser.PostalCode = applicationIdentityUser.PostalCode;

                appUser.IsActive = applicationIdentityUser.IsActive;
                appUser.LastLoginDateandTime = applicationIdentityUser.LastLoginDateandTime;

                appUser.ActivationCode = applicationIdentityUser.ActivationCode;
                appUser.ChangeEmailValidate = applicationIdentityUser.ChangeEmailValidate;

                appUser.ChangeEmailConfirmed = applicationIdentityUser.ChangeEmailConfirmed;
                appUser.PreviousEmailValidate = applicationIdentityUser.PreviousEmailValidate;

                appUser.PreviousEmailConfirmed = applicationIdentityUser.PreviousEmailConfirmed;

                appUser.SecurityQuestion1 = applicationIdentityUser.SecurityQuestion1;
                appUser.SecurityQuestion2 = applicationIdentityUser.SecurityQuestion2;
                appUser.SecurityQuestion3 = applicationIdentityUser.SecurityQuestion3;

                appUser.SecurityAnswer1 = applicationIdentityUser.SecurityAnswer1;
                appUser.SecurityAnswer2 = applicationIdentityUser.SecurityAnswer2;
                appUser.SecurityAnswer3 = applicationIdentityUser.SecurityAnswer3;

                appUser.Title = applicationIdentityUser.Title;

                appUser.ActivationDate = applicationIdentityUser.ActivationDate;

                appUser.SecondaryEmail = applicationIdentityUser.SecondaryEmail;

                appUser.IsDelete = applicationIdentityUser.IsDelete;

                appUser.CreatedDate = applicationIdentityUser.CreatedDate;

                appUser.IsForgot = applicationIdentityUser.IsForgot;

                appUser.UpdatedDate = applicationIdentityUser.UpdatedDate;
                appUser.IsLoggedIn = applicationIdentityUser.IsLoggedIn;
                appUser.LoginSessionId = applicationIdentityUser.LoginSessionId;
                foreach (var claim in applicationIdentityUser.Claims)
                {
                    appUser.Claims.Add(new ApplicationUserClaim
                    {
                        ClaimType = claim.ClaimType,
                        ClaimValue = claim.ClaimValue,
                        Id = claim.Id,
                        UserId = claim.UserId
                    });
                }
                foreach (var role in applicationIdentityUser.Roles)
                {
                    appUser.Roles.Add(role.ToApplicationUserRole());
                }
                foreach (var login in applicationIdentityUser.Logins)
                {
                    appUser.Logins.Add(new ApplicationUserLogin
                    {
                        LoginProvider = login.LoginProvider,
                        ProviderKey = login.ProviderKey,
                        UserId = login.UserId
                    });
                }

                user.Add(appUser);
            }

            return user.ToList();
        }

        public async Task<bool> CheckUserSecondaryEmailAvailableAsync(string Email, string userid)
        {
            try
            {
                var validate = (from user in _userManager.Users where user.SecondaryEmail.Contains(Email) select user).FirstOrDefault();

                if (validate != null)
                {
                    if (validate.Id == userid)
                    {
                        return await Task.FromResult(true);
                    }
                    // ReSharper disable once RedundantIfElseBlock
                    else
                    {
                        return await Task.FromResult(false);
                    }
                }
                // ReSharper disable once RedundantIfElseBlock
                else
                {
                    return await Task.FromResult(true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception Occurs in CheckUserSecondaryEmailAvailableAsync", ex);
            }
        }
    }
}