using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessCenter.Identity.IdentityModels;

namespace BusinessCenter.Api.Common
{
    public static class ToConvertUser
    {
        public static AppUser ToDataConvert(AppUser userDetails)
        {
            var user1 = new AppUser
            {

                Id = userDetails.Id,
                FirstName = userDetails.FirstName,
                LastName = userDetails.LastName,
                MobileNumber = userDetails.MobileNumber,
                UserName = userDetails.UserName,
                Email = userDetails.Email,
                Address = userDetails.Address,
                City = userDetails.City,
                State = userDetails.State,
                LastLoginDateandTime = userDetails.LastLoginDateandTime,
                PhoneNumberConfirmed = userDetails.PhoneNumberConfirmed,
                TwoFactorEnabled = userDetails.TwoFactorEnabled,
                LockoutEnabled = userDetails.LockoutEnabled,
                AccessFailedCount = userDetails.AccessFailedCount,
                PostalCode = userDetails.PostalCode,
                IsActive = userDetails.IsActive,
                SecurityQuestion1 = userDetails.SecurityQuestion1,
                SecurityQuestion2 = userDetails.SecurityQuestion2,
                SecurityQuestion3 = userDetails.SecurityQuestion3,
                SecurityAnswer1 = userDetails.SecurityAnswer1,
                SecurityAnswer2 = userDetails.SecurityAnswer2,
                SecurityAnswer3 = userDetails.SecurityAnswer3,
                ActivationCode = userDetails.ActivationCode,
                ActivationDate = userDetails.ActivationDate,
                IsDelete = userDetails.IsDelete,
                CreatedDate = userDetails.CreatedDate,
                PreviousEmailConfirmed =userDetails.PreviousEmailConfirmed,
            ChangeEmailConfirmed = userDetails.ChangeEmailConfirmed,
            PreviousEmailValidate =userDetails.PreviousEmailValidate,
                ChangeEmailValidate = userDetails.ChangeEmailValidate,
                IsForgot = userDetails.IsForgot,
                UpdatedDate = userDetails.UpdatedDate
              
            };

            return user1;
        }

    }
}