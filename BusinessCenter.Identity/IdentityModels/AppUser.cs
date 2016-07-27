using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace BusinessCenter.Identity.IdentityModels
{
    /// <summary>
    /// AppUser Class Resembles the Default class ApplicationUser.
    /// AppUser Contains Properties that are added to the UserStore
    /// </summary>
    public partial class AppUser : IdentityUser<string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        #region properties


        public virtual string Title { get; set; }

        public virtual string Address { get; set; }

        public virtual string City { get; set; }


        public virtual string FirstName { get; set; }


        public virtual string LastName { get; set; }


        public virtual string MobileNumber { get; set; }


        public virtual string PostalCode { get; set; }

        public virtual string State { get; set; }

        public DateTime? LastLoginDateandTime { get; set; }
        public  bool IsActive { get; set; }

        public string SecurityQuestion1 { get; set; }
       
        public string SecurityQuestion2 { get; set; }
        
        public string SecurityQuestion3 { get; set; }
        
        public string SecurityAnswer1 { get; set; }
        public string SecurityAnswer2 { get; set; }

        public string SecurityAnswer3 { get; set; }
        public string ActivationCode { get; set; }

        public DateTime? ActivationDate { get; set; }
        public string SecondaryEmail { get; set; }
   
          public  DateTime? ChangeEmailValidate { get; set; }

         public  bool ChangeEmailConfirmed { get; set; }
          public  DateTime? PreviousEmailValidate { get; set; }

          public bool PreviousEmailConfirmed { get; set; }


          public bool IsDelete { get; set; }

          public DateTime? CreatedDate { get; set; }

          public bool IsForgot { get; set; }
          public string DeleteComment { get; set; }
          public DateTime? UpdatedDate { get; set; }
          public bool IsLoggedIn { get; set; }
          public string LoginSessionId { get; set; }
        
        #endregion

        #region methods

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager userManager)
        {
            var userIdentity = await userManager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }



        #endregion
    }
}
