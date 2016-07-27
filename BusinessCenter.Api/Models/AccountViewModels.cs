using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusinessCenter.Api.Models
{
    public class PdfModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class UserTypeModel
    {
        public string UserType { get; set; }
        public string UserStatus { get; set; }
    }
    public class ResetPasswordModel
    {
        public string Userid { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        // [RegularExpression(@"^(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?=.*[?/{}'!()*^`:@#$%^&+=]).*$", ErrorMessage = "Password must contain atleast one uppercase ('A'-'Z'),one lowercase('a'-'z'),one Numeric and one special character.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        //   [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }




    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class UserPrimaryEmailValidation
    {
       
       
        public string Email { get; set; }
        public string UserId { get; set; }
    }

    public class LoginViewModel
    {

        [Required]
        [Display(Name = "User Name")]
       // [RegularExpression(@"(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,250})$", ErrorMessage = "User name allow alphanumeric character with minimum of 8 characters")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {

        [Required]
        public string Title { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

       
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }


        [Required]
        [Display(Name = "User Name")]
       // [RegularExpression(@"(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,250})$", ErrorMessage = "User name allow alphanumeric character with minimum of 8 characters")]
        public string UserName { get; set; }


        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
       // [RegularExpression(@"^(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?=.*[?/{}'!()*^`:@#$%^&+=]).*$", ErrorMessage = "Password must contain atleast one uppercase ('A'-'Z'),one lowercase('a'-'z'),one Numeric and one special character.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
       // [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string SecurityQuestion1 { get; set; }
        [Required]
        public string SecurityQuestion2 { get; set; }

        [Required]
        public string SecurityQuestion3 { get; set; }

        [Required]
        public string SecurityAnswer1 { get; set; }
        [Required]
        public string SecurityAnswer2 { get; set; }

        [Required]
        public string SecurityAnswer3 { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> RolesList { get; set; }
        public DateTime? ChangeEmailValidate { get; set; }

        public bool ChangeEmailConfirmed { get; set; }
        public DateTime? PreviousEmailValidate { get; set; }

        public bool PreviousEmailConfirmed { get; set; }
        public bool IsDelete { get; set; }

        public DateTime? CreatedDate { get; set; }
        public string DeleteComment { get; set; }

    }

    public class ResetPasswordViewModel
    {
      
        public string userId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
        public string SelectedType { get; set; }
    }

    public class ChangePassword
    {
        //[Required]
        //[EmailAddress]
        //[Display(Name = "Email")]
        public string userId { get; set; }

        [Required]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
       // [RegularExpression(@"^(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?=.*[?/{}'!()*^`:@#$%^&+=]).*$", ErrorMessage = "Password must contain atleast one uppercase ('A'-'Z'),one lowercase('a'-'z'),one Numeric and one special character.")]
        [DataType(DataType.Password)]
        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }

        [Required]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
       // [RegularExpression(@"^(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?=.*[?/{}'!()*^`:@#$%^&+=]).*$", ErrorMessage = "Password must contain atleast one uppercase ('A'-'Z'),one lowercase('a'-'z'),one Numeric and one special character.")]
       [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
       // [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
       // [RegularExpression(@"^(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?=.*[?/{}'!()*^`:@#$%^&+=]).*$", ErrorMessage = "Password must contain atleast one uppercase ('A'-'Z'),one lowercase('a'-'z'),one Numeric and one special character.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

       
    }
    public class UserMailAddressUpdate
    {
        public string UserId { get; set; }


        public string UserEmail { get; set; }

        public string NewEmail { get; set; }


        public string Type { get; set; }


    }
    public class ForgotPasswordViewModel
    {
        [Required]
        [Display(Name = "User Name")]
     //   [RegularExpression(@"^[0-9a-zA-Z]{8,}$", ErrorMessage = "User name allow alphanumeric character with minimum of 8 characters")]
        public string UserName { get; set; }
        public string SecurityQuestion1 { get; set; }
       
        public string SecurityQuestion2 { get; set; }

      
        public string SecurityQuestion3 { get; set; }

      
        public string SecurityAnswer1 { get; set; }
        
        public string SecurityAnswer2 { get; set; }

      
        public string SecurityAnswer3 { get; set; }
      //  public bool RadioEmailValue { get; set; }
       // public bool RadioSecuValue { get; set; }
    }
    public class ForgotUserNameViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

      
    }
    public class ForgotPasswordValidationViewModel
    {
        
        public string UserId{ get; set; }
        [Required]
        public string SecurityQuestion1 { get; set; }
        [Required]
        public string SecurityQuestion2 { get; set; }

        [Required]
        public string SecurityQuestion3 { get; set; }

        [Required]
        public string SecurityAnswer1 { get; set; }
        [Required]
        public string SecurityAnswer2 { get; set; }

        [Required]
        public string SecurityAnswer3 { get; set; }
    }
    public class UserAccountById
    {
        
        public string UserId { get; set; }
        public string DeleteComment { get; set; }
    }
    public class ReCapchaResponse
    {
       public string responseValue { get; set; }
    }
    public class UserCheckViewModel
    {
        [Required]
        [Display(Name = "User Name")]
      //  [RegularExpression(@"^[0-9a-zA-Z]{8,}$", ErrorMessage = "User name allow alphanumeric character with minimum of 8 characters")]
        public string UserName { get; set; }
    }
    public class PasswordCheckViewModel
    {
        public string Password { get; set; }
        public string userId { get; set; }
        
    }
    public class UserProfileUpdate
    {
          public string FirstName { get; set; }
          public string LastName { get; set; }
        
          public string UserId { get; set; }
          [Required]
          [Display(Name = "User Name")]
       //   [RegularExpression(@"^[0-9a-zA-Z]{8,}$", ErrorMessage = "User name allow alphanumeric character with minimum of 8 characters")]
          public string UserName { get; set; }

          [Required]
          [EmailAddress]
          [Display(Name = "Email Address")]
          public string Email { get; set; }


          [EmailAddress]
          [Display(Name = "Secondary Email Address")]
          public string SecondaryEmail { get; set; }


       
          [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        //  [RegularExpression(@"^(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?=.*[?/{}'!()*^`:@#$%^&+=]).*$", ErrorMessage = "Password must contain atleast one uppercase ('A'-'Z'),one lowercase('a'-'z'),one Numeric and one special character.")]
          [DataType(DataType.Password)]
         [Display(Name = "Current Password")]
        public string Password { get; set; }


        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
     //   [RegularExpression(@"^(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?=.*[?/{}'!()*^`:@#$%^&+=]).*$", ErrorMessage = "Password must contain atleast one uppercase ('A'-'Z'),one lowercase('a'-'z'),one Numeric and one special character.")]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
         public string NewPassword { get; set; }

      
         [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        // [RegularExpression(@"^(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?=.*[?/{}'!()*^`:@#$%^&+=]).*$", ErrorMessage = "Password must contain atleast one uppercase ('A'-'Z'),one lowercase('a'-'z'),one Numeric and one special character.")]
         [DataType(DataType.Password)]
         [Display(Name = "Confirm password")]
         [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
         public string ConfirmPassword { get; set; }


         [Required]
         public string SecurityQuestion1 { get; set; }
         [Required]
         public string SecurityQuestion2 { get; set; }

         [Required]
         public string SecurityQuestion3 { get; set; }

         [Required]
         public string SecurityAnswer1 { get; set; }
         [Required]
         public string SecurityAnswer2 { get; set; }

         [Required]
         public string SecurityAnswer3 { get; set; }


         public DateTime? ChangeEmailValidate { get; set; }

         public bool ChangeEmailConfirmed { get; set; }
         public DateTime? PreviousEmailValidate { get; set; }

         public bool PreviousEmailConfirmed { get; set; }
         public bool IsForgot { get; set; }
         public DateTime? UpdatedDate { get; set; }

         public string Address { get; set; }
         public string City { get; set; }
         public string MobileNumber { get; set; }
         public string PostalCode { get; set; }
         public string State { get; set; }
         public string DeleteComment { get; set; }
         public bool IsDelete { get; set; }

    }

    public class EmailVerification
    {
        public string userId { get; set; }
        public string code { get; set; }
    }

    public class ReSendMail
    {
        public string UserId { get; set; }
        public string Code { get; set; }
        public string EmailAddress { get; set; }
        public string FullName { get; set; }
    }

    public class UserLoginHistoryModel
    {
        public long LoginHisId { get; set; }
        public string UserId { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public int? Count { get; set; }
    }
}