using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusinessCenter.Admin.Models
{
    public class UserApiModel
    {
        public string Gui { get; set; }
        public int length { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string SecurityQuestion1 { get; set; }
        public string SecurityQuestion2 { get; set; }
        public string SecurityQuestion3 { get; set; }
        public string SecurityAnswer1 { get; set; }
        public string SecurityAnswer2 { get; set; }
        public string SecurityAnswer3 { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> RolesList { get; set; }
        public bool IsActive { get; set; }
        public bool ?IsDelete { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsForgot { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UserId { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public DateTime? LastLoginDateandTime { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string MobileNumber { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username required")]
        [Display(Name = "User Name")]
        [RegularExpression(@"(^[a-zA-Z0-9]{8,250}$)", ErrorMessage = "Username must be at least 8 characters and must be alphanumeric")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
       // [RegularExpression(@"((?=.*?[A-Z])(?=.*?[a-z])(?=.*?\d)|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?\d)(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?\d)(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Please make sure that your password contains all of the following: 1. The password is at least eight (8) characters long. 2. The password contains characters from at least three of the following four categories: 3. English uppercase characters (A- Z) 4. English lowercase characters (a- z) 5. Base 10 digits (0- 9) 6. Special or Punctuation Characters (Example: !, $,#,or%)")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public string StatusMsg { get; set; }
    }

    public class UserLoginHistoryModel
    {
        public long? LoginHisId { get; set; }
       // public int? UserId { get; set; }
        public string UserId { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public int? Count { get; set; }
    }

    public class UserModel
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
        public bool IsActive { get; set; }
        public string SecurityQuestion1 { get; set; }
        public string SecurityQuestion2 { get; set; }
        public string SecurityQuestion3 { get; set; }
        public string SecurityAnswer1 { get; set; }
        public string SecurityAnswer2 { get; set; }
        public string SecurityAnswer3 { get; set; }
        public string ActivationCode { get; set; }
        public DateTime? ActivationDate { get; set; }
        public string SecondaryEmail { get; set; }
        public DateTime? ChangeEmailValidate { get; set; }
        public bool ChangeEmailConfirmed { get; set; }
        public DateTime? PreviousEmailValidate { get; set; }
        public bool PreviousEmailConfirmed { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsForgot { get; set; }
        public string DeleteComment { get; set; }
        public DateTime? UpdatedDate { get; set; }
        #endregion
    }

    public class SearchDataMvcViewModel
    {
        public string ID { get; set; }
        public string RecordCount { get; set; }
        public int NoofRecords { get; set; }
        public int ABRAID { get; set; }
        public string Source { get; set; }
        public string Name { get; set; }
        public string ABRACount { get; set; }
        public string BBLCount { get; set; }
        public string CBECount { get; set; }
        public string CORPCount { get; set; }
        public string OPLACount { get; set; }
        public List<SearchDataMvcViewModel> TotalSearchList { get; set; }
        public List<CommonDataModel> FinalData { get; set; }
    }

    public class CommonDataModel
    {
        public bool WishList { get; set; }
        public int EntityID { get; set; }
        public string Source { get; set; }
        public string CompanyName { get; set; }
        public string LicenseNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LeftNameTop { get; set; }
        public string LeftNameMiddle { get; set; }
        public string LeftNameBottom { get; set; }
        public string MiddleNameTop { get; set; }
        public string MiddleNameMiddle { get; set; }
        public string MiddleNameBottom { get; set; }
        public string RightNameTop { get; set; }
        public string RightNameMiddle1 { get; set; }
        public string RightNameMiddle2 { get; set; }
        public string RightNameBottom { get; set; }
        public string Expantion1 { get; set; }
        public string Expantion2 { get; set; }
        public string Expantion3 { get; set; }
        public string Expantion4 { get; set; }
        public string Expantion5 { get; set; }
        public string Expantion6 { get; set; }
        public string LeftNameResultTop { get; set; }
        public string LeftNameResultMiddle { get; set; }
        public string LeftNameResultBottom { get; set; }
        public string MiddleNameResultTop { get; set; }
        public string MiddleNameResultMiddle { get; set; }
        public string MiddleNameResultBottom { get; set; }
        public string RightNameResultTop { get; set; }
        public string RightNameResultMiddle1 { get; set; }
        public string RightNameResultMiddle2 { get; set; }
        public string RightNameResultBottom { get; set; }
        public string ExpantionResult1 { get; set; }
        public string ExpantionResult2 { get; set; }
        public string ExpantionResult3 { get; set; }
        public string ExpantionResult4 { get; set; }
        public string ExpantionResult5 { get; set; }
        public string ExpantionResult6 { get; set; }
        public string SourceFullName { get; set; }
        public string LastUpdateDateName { get; set; }
        public string LastUpdateDate { get; set; }
        public string LeftNameMiddleLabel1 { get; set; }
        public string LeftNameMiddle1Text { get; set; }
    }

    public class UserCheckViewModel
    {
        [Required]
        [Display(Name = "User Name")]
        //  [RegularExpression(@"^[0-9a-zA-Z]{8,}$", ErrorMessage = "User name allow alphanumeric character with minimum of 8 characters")]
        public string UserName { get; set; }
    }
    public class ForgotViewModel
    {
        //  [RegularExpression(@"^[0-9a-zA-Z]{8,}$", ErrorMessage = "User name allow alphanumeric character with minimum of 8 characters")]
        public string Email { get; set; }
    }
}