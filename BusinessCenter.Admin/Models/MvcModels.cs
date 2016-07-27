using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BusinessCenter.Data.Models;
using BusinessCenter.Data.Model;
namespace BusinessCenter.Admin.Models
{
    public class ForgotPasswordModel
    {
        public string FullName { get; set; }
        public string PasswordLink { get; set; }
        public string Role { get; set; }
    }

    public class SecurityQuestionModel
    {
        public int id { get; set; }
        public string Question { get; set; }
    }

    public class SearchKeywordMvcModel
    {
        public string CreatedDate { get; set; }
        public int KeyCount { get; set; }
        public string KeyId { get; set; }
        public string KeywordDid { get; set; }
        public string Keywords { get; set; }
        public string TypeID { get; set; }
        public int fullKeyCount { get; set; }

    }

    public enum UserRole
    {
        SuperAdmin,
        Admin,
        User
    }

    public enum UserStatus
    {
        All,
        Active,
        InActive,
    }

    public class UserTypeModel
    {
        public string UserType { get; set; }
        public string UserStatus { get; set; }
        public string searchText { get; set; }
    }
    public class DisplayType
    {
        public string DisplayValue { get; set; }
    }

    public class LoginHistoryModel
    {
        public string UserRole { get; set; }
        public string Count { get; set; }
        public DateTime LastLoginDate { get; set; }
        public long LoginHisId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }

        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int totalPages { get; set; }
        public int currentPageIndex { get; set; }
    }

    public class KewordsCount
    {
        public string Businesscount { get; set; }
        public string Licensecount { get; set; }
        public string Firstnamecount { get; set; }
        public string Lastnamecount { get; set; }
    }

    public class DashBoardViewModel
    {
        public List<LoginHistoryModel> loginhistory { get; set; }
        public List<SearchKeywordMvcModel> SearchedKeywords { get; set; }
        public List<KewordsCount> KeywordCount { get; set; }
        public List<RegulatorCount> RegulatorCountList { get; set; }
        public List<Roledeatils> RoleDetails { get; set; }

        public List<CreateDeleteSection> UserCreateDelete { get; set; }
    }


    public class Roledeatils
    {

        public int cnt { get; set; }
        public string Role { get; set; }
        public string CreatedDate { get; set; }
    }

    public class CreateDeleteSection
    {

        public int cnt { get; set; }
        public string status { get; set; }
        public string roleId { get; set; }
        public string CreatedDate { get; set; }
    }
    public class RegulatorCount
    {
        public int RegularCount { get; set; }
        public string Regulator { get; set; }
    }
    public class ResetPasswordModel
    {
        public string userId { get; set; }

        [Required(ErrorMessage = "Password required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [RegularExpression(@"((?=.*?[A-Z])(?=.*?[a-z])(?=.*?\d)|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?\d)(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?\d)(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Please make sure that your password contains all of the following: 1. The password is at least eight (8) characters long. 2. The password contains characters from at least three of the following four categories: 3. English uppercase characters (A- Z) 4. English lowercase characters (a- z) 5. Base 10 digits (0- 9) 6. Special or Punctuation Characters (Example: !, $,#,or%)")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords Do Not Match")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class AccountViewModel
    {
        public List<RegisterUserModel> AllSuperAdmins { get; set; }
        public List<RegisterUserModel> AllManagers { get; set; }
        public List<RegisterUserModel> AllAdmins { get; set; }
        public List<RegisterUserModel> AllEmployees { get; set; }

        public string UserType { get; set; }

        public RegisterUserModel registerUserModel { get; set; }
    }

    public class UserViewModel
    {
        public List<RegisterUserModel> ActiveUsers { get; set; }
        public List<RegisterUserModel> InActiveUsers { get; set; }
    }

    public class ResetPasswordViewModel
    {
        public string userId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 6)]
        // [RegularExpression(@"^(?=.{6,})(?=.*[a-z])(?=.*[A-Z])(?=.*[?/{}'!()*^`:@#$%^&+=]).*$", ErrorMessage = "Password must contain atleast one uppercase ('A'-'Z'),one lowercase('a'-'z'),one Numeric and one special character.")]
        [DataType(DataType.Password)]

        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password required")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Passwords Do Not Match")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPassword
    {
        [Required(ErrorMessage = "Username is required")]
        [Display(Name = "User Name")]
        [RegularExpression(@"(^[a-zA-Z0-9]{8,250}$)", ErrorMessage = "Invalid Username")]
        //  [RegularExpression(@"(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,250})$", ErrorMessage = "Invalid Username")]
        public string UserName { get; set; }

    }

    public class RegisterUserModel
    {
        public string Title { get; set; }

        [Required(ErrorMessage = " First Name is required")]
        [Display(Name = "First Name")]
        [StringLength(80, ErrorMessage = "First Name should be less than 80 characters", MinimumLength = 1)]
        //[RegularExpression(@"[a-zA-Z\.s]+$", ErrorMessage = "First Name accepts only alphabets.")]
        [RegularExpression(@"[a-zA-Z\s]+$", ErrorMessage = "First Name accepts only alphabets")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [Display(Name = "Last Name")]
        [StringLength(80, ErrorMessage = "Last Name should be less than 80 characters", MinimumLength = 1)]
        [RegularExpression(@"[a-zA-Z\s]+$", ErrorMessage = "Last Name accepts only alphabets")]
        public string LastName { get; set; }

        [Required(ErrorMessage = " Email is required")]
        [Display(Name = "Email Address")]
        //  [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please enter a valid email address.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+(@dc\.gov)$", ErrorMessage = "Please enter a valid email address.")]
        //[EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [Display(Name = "User Name")]
        [RegularExpression(@"(^[a-zA-Z0-9]{8,250}$)", ErrorMessage = "Username must be at least eight (8) characters and must be alphanumeric")]
        public string UserName { get; set; }

        [Required(ErrorMessage = " Password is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [RegularExpression(@"((?=.*?[A-Z])(?=.*?[a-z])(?=.*?\d)|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?\d)(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?\d)(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Please make sure that your password contains all of the following: 1. The password is at least eight (8) characters long. 2. The password contains characters from at least three of the following four categories: 3. English uppercase characters (A- Z) 4. English lowercase characters (a- z) 5. Base 10 digits (0- 9) 6. Special or Punctuation Characters (Example: !, $,#,or%)")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Passwords Do Not Match")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Security Question 1")]
        public string SecurityQuestion1 { get; set; }

        [Display(Name = "Security Question 2")]
        public string SecurityQuestion2 { get; set; }

        [Display(Name = "Security Question 3")]
        public string SecurityQuestion3 { get; set; }

        [Display(Name = "Security Answer 1")]
        public string SecurityAnswer1 { get; set; }

        [Display(Name = "Security Answer 2")]
        public string SecurityAnswer2 { get; set; }

        [Display(Name = "Security Answer 3")]
        public string SecurityAnswer3 { get; set; }
        public string IsActive { get; set; }
        public int AccessFailedCount { get; set; }
        public string Gui { get; set; }
        public int length { get; set; }
        public string UserId { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsForgot { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public DateTime? LastLoginDateandTime { get; set; }

        //[Required(ErrorMessage = " Address is required")]
        [StringLength(250, ErrorMessage = "Address should be less than 250 characters", MinimumLength = 1)]
        public string Address { get; set; }

        //[Required(ErrorMessage = "City is required")]
        [RegularExpression(@"[a-zA-Z\s]+$", ErrorMessage = " City accepts only alphabets")]
        [StringLength(100, ErrorMessage = "City should be less than 100 characters", MinimumLength = 1)]
        public string City { get; set; }

        //[Required(ErrorMessage = "Mobile Number is required")]
        // [RegularExpression(@"^[0-9- +]+$", ErrorMessage = "Mobile Number accepts only numbers")]
        [RegularExpression("([0-9][0-9]*)", ErrorMessage = "Mobile Number accepts only numbers")]
        [StringLength(15, ErrorMessage = "Mobile Number should be a minimum of six (6) digits and a maximum of fifteen (15) digits", MinimumLength = 6)]
        public string MobileNumber { get; set; }

        //[Required(ErrorMessage = "Postal Code is required")]

        // [RegularExpression(@"^[0-9- +]+$", ErrorMessage = "Postal Code accepts only numbers")]
        [RegularExpression("([0-9][0-9]*)", ErrorMessage = "Postal Code accepts only numbers")]
        [StringLength(5, ErrorMessage = "Postal Code should be five (5) digits", MinimumLength = 5)]
        public string PostalCode { get; set; }

        //[Required(ErrorMessage = "State is required")]
        [RegularExpression(@"[a-zA-Z\s]+$", ErrorMessage = " State accepts only alphabets")]
        [StringLength(100, ErrorMessage = "State should be less than 100 characters", MinimumLength = 1)]
        public string State { get; set; }

        public string Role { get; set; }
        public string StatusMsg { get; set; }
        public string EmailStatusMsg { get; set; }
    }

    public class UserAccountDeleteModel
    {
        public string UserId { get; set; }
        public string DeleteComment { get; set; }

    }

    public class UserAccountLockModel
    {
        public string UserId { get; set; }
    }

    public class UserAccountById
    {
        public string UserId { get; set; }
    }

    public class UserProfileUpdate
    {
        [StringLength(80, ErrorMessage = "First Name should be minimum 1 characters.", MinimumLength = 1)]
        public string FirstName { get; set; }
        [StringLength(80, ErrorMessage = "Last Name should be minimum 1 characters.", MinimumLength = 1)]
        public string LastName { get; set; }
        public int length { get; set; }
        public string UserId { get; set; }
        [Required]
        [Display(Name = "User Name")]
        [RegularExpression(@"(^[a-zA-Z0-9]{8,250}$)", ErrorMessage = "Username must be at least 8 characters.")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [EmailAddress]
        [Display(Name = "Secondary Email Address")]
        public string SecondaryEmail { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [RegularExpression(@"^(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?=.*[?/{}'!()*^`:@#$%^&+=]).*$", ErrorMessage = "Password must contain atleast one uppercase ('A'-'Z'),one lowercase('a'-'z'),one Numeric and one special character.")]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string Password { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [RegularExpression(@"^(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?=.*[?/{}'!()*^`:@#$%^&+=]).*$", ErrorMessage = "Password must contain atleast one uppercase ('A'-'Z'),one lowercase('a'-'z'),one Numeric and one special character.")]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [RegularExpression(@"^(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?=.*[?/{}'!()*^`:@#$%^&+=]).*$", ErrorMessage = "Password must contain atleast one uppercase ('A'-'Z'),one lowercase('a'-'z'),one Numeric and one special character.")]
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
        public string Id { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public DateTime? LastLoginDateandTime { get; set; }

        [Required(ErrorMessage = "Address required")]
        public string Address { get; set; }

        [Required]
        [RegularExpression(@"[a-zA-Z\s]+$", ErrorMessage = " City accepts only alphabets.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Mobile Number required")]
        [RegularExpression(@"^[0-9- +]+$", ErrorMessage = "Mobile Number accepts only numbers")]
        [StringLength(15, ErrorMessage = "Mobile Number should be minimum 6 characters and maximum 15 characters", MinimumLength = 6)]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "Postalcode required")]
        [RegularExpression(@"^[0-9- +]+$", ErrorMessage = "Postalcode accepts only numbers")]
        [StringLength(5, ErrorMessage = "Postalcode should be 5 characters", MinimumLength = 5)]
        public string PostalCode { get; set; }

        [Required]
        [RegularExpression(@"[a-zA-Z\s]+$", ErrorMessage = " State accepts only alphabets")]
        public string State { get; set; }

        public bool IsDelete { get; set; }
    }

    public class ChangePassword
    {
        public int userId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 8)]
        // [RegularExpression(@"^(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?=.*[?/{}'!()*^`:@#$%^&+=]).*$", ErrorMessage = "Password must contain atleast one uppercase ('A'-'Z'),one lowercase('a'-'z'),one Numeric and one special character.")]
        [DataType(DataType.Password)]
        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 8)]
        // [RegularExpression(@"^(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?=.*[?/{}'!()*^`:@#$%^&+=]).*$", ErrorMessage = "Password must contain atleast one uppercase ('A'-'Z'),one lowercase('a'-'z'),one Numeric and one special character.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 8)]
        // [RegularExpression(@"^(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?=.*[?/{}'!()*^`:@#$%^&+=]).*$", ErrorMessage = "Password must contain atleast one uppercase ('A'-'Z'),one lowercase('a'-'z'),one Numeric and one special character.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class UsersCount
    {
        public string status { get; set; }
        public string Count { get; set; }
        public string Ecount { get; set; }
        public string Acount { get; set; }
        public string Scount { get; set; }
    }

    public class JsonModel
    {
        public string status { get; set; }
        public string exception { get; set; }
        public string mailid { get; set; }
        public string userId { get; set; }
        public string code { get; set; }
    }

    public class LoginJsonModel
    {
        public string Status { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string RoleCount { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class ReSendMail
    {
        public string Status { get; set; }
        public int UserId { get; set; }
        public string Code { get; set; }
        public string EmailAddress { get; set; }
        public string FullName { get; set; }
        public string RoleCount { get; set; }
        public string StatusMsg { get; set; }
    }

    public class RegisterJsonModel
    {
        [Required(ErrorMessage = "First Name field cannot be blank")]
        [Display(Name = "First Name")]
        [StringLength(80, ErrorMessage = "First Name should be less than 80 characters", MinimumLength = 1)]
        [RegularExpression(@"[a-zA-Z\s]+$", ErrorMessage = " First Name accepts only alphabets")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name field cannot be blank")]
        [Display(Name = "Last Name")]
        [StringLength(80, ErrorMessage = "Last Name should be less than 80 characters", MinimumLength = 1)]
        [RegularExpression(@"[a-zA-Z\s]+$", ErrorMessage = " Last Name accepts only alphabets")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username field is required and can't be empty")]
        [Display(Name = "Username")]
        // [RegularExpression(@"(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,250})$", ErrorMessage = "User name allow alphanumeric character with minimum of 8 characters")]
        [RegularExpression(@"(^[a-zA-Z0-9]{8,250}$)", ErrorMessage = "Username must be at least eight (8) characters and must be alphanumeric")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "The Email ID field is required and can't be empty")]
        [Display(Name = "Primary Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please select three security questions")]
        [Display(Name = "QUESTION 1 (Choose a question from list below)")]
        public string SecurityQuestion1 { get; set; }

        [Required(ErrorMessage = "Please select three security questions")]
        [Display(Name = "QUESTION 2 (Choose a question from list below)")]
        public string SecurityQuestion2 { get; set; }

        [Required(ErrorMessage = "Please select three security questions")]
        [Display(Name = "QUESTION 3 (Choose a question from list below)")]
        public string SecurityQuestion3 { get; set; }

        [Required(ErrorMessage = "Please answer all Security questions")]
        [Display(Name = "ANSWER 1 (Enter your answer)")]
        public string SecurityAnswer1 { get; set; }

        [Required(ErrorMessage = "Please answer all Security questions")]
        [Display(Name = "ANSWER 2 (Enter your answer)")]
        public string SecurityAnswer2 { get; set; }

        [Required(ErrorMessage = "Please answer all Security questions")]
        [Display(Name = "ANSWER 3 (Enter your answer)")]
        public string SecurityAnswer3 { get; set; }

        public string UserId { get; set; }
        public int Id { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public DateTime? LastLoginDateandTime { get; set; }

        //[Required(ErrorMessage = "Address is required")]
        [StringLength(250, ErrorMessage = "Address should be less than 250 characters", MinimumLength = 1)]
        public string Address { get; set; }

        //[Required(ErrorMessage = "City is required")]
        [RegularExpression(@"[a-zA-Z\s]+$", ErrorMessage = " City accepts only alphabets")]
        [StringLength(100, ErrorMessage = "City should be less than 100 characters", MinimumLength = 1)]
        public string City { get; set; }

        //[Required(ErrorMessage = "Mobile Number is required")]
        [RegularExpression("([0-9][0-9]*)", ErrorMessage = "Mobile Number accepts only numbers")]
        [StringLength(15, ErrorMessage = "Mobile Number should be a minimum of six (6) digits and a maximum of fifteen (15) digits", MinimumLength = 6)]
        public string MobileNumber { get; set; }

        //[Required(ErrorMessage = "Postal Code is required")]
        [RegularExpression("([0-9][0-9]*)", ErrorMessage = "Postal Code accepts only numbers")]
        [StringLength(5, ErrorMessage = "Postal Code should be five (5) digits", MinimumLength = 5)]
        public string PostalCode { get; set; }

        //[Required(ErrorMessage = "State is required")]
        [RegularExpression(@"[a-zA-Z\s]+$", ErrorMessage = " State accepts only alphabets")]
        [StringLength(100, ErrorMessage = "State should be less than 100 characters", MinimumLength = 1)]
        public string State { get; set; }
        public bool IsActive { get; set; }

        // [StringLength(100, ErrorMessage = "The Password must be at least 8 characters long", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        //  [RegularExpression(@"((?=.*?[A-Z])(?=.*?[a-z])(?=.*?\d)|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?\d)(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?\d)(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Please make sure that your password contains all of the following: 1. The password is at least eight (8) characters long. 2. The password contains characters from at least three of the following four categories: 3. English uppercase characters (A- Z) 4. English lowercase characters (a- z) 5. Base 10 digits (0- 9) 6. Special or Punctuation Characters (Example: !, $,#,or%)")]
        public string Password { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "NewPassword")]
        [RegularExpression(@"((?=.*?[A-Z])(?=.*?[a-z])(?=.*?\d)|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?\d)(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?\d)(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Please make sure that your password contains all of the following: 1. The password is at least eight (8) characters long. 2. The password contains characters from at least three of the following four categories: 3. English uppercase characters (A- Z) 4. English lowercase characters (a- z) 5. Base 10 digits (0- 9) 6. Special or Punctuation Characters (Example: !, $,#,or%)")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        //  [Compare("NewPassword", ErrorMessage = "Passwords Do Not Match")]
        public string ConfirmPassword { get; set; }

        public bool IsDelete { get; set; }

        public string UserType { get; set; }
    }

    public class SubmissionTransferViewModel
    {
        public RegisterUserModel User { get; set; }
        public List<RegisterUserModel> UsersList { get; set; }
        public SubmissiontransferModel submissiontransfer { get; set; }
        public string UserId { get; set; }

        public string SubmissionLicense { get; set; }
        public string LoggedUserName { get; set; }
        public string TransferredFromUserName { get; set; }

        public string TransferToUserName { get; set; }

        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
        public string ReasonForTransfer { get; set; }
        public string MasterId { get; set; }
    }

    public class SubmissiontransferModel
    {
        public string MasterId { get; set; }
        public string FromUserId { get; set; }
        //[Required(ErrorMessage = "Please complete all required fields")]
      //[Required(ErrorMessage="User to Transfer is required")]
        public string ToUserId { get; set; }
        public string CreatedBy { get; set; }
        //[Required(ErrorMessage = "Please complete all required fields")]
        [DataType(DataType.MultilineText)]
      // [Required(ErrorMessage = "ReasonForTransfer is required")]
        public string ReasonForTransfer { get; set; }

        public string TransferToUserName { get; set; }
    }
}