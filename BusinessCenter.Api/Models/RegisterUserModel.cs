using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusinessCenter.Api.Models
{
    /// <summary>
    /// This Model class contains Fields Required for Registration.
    /// </summary>
    public class RegisterUserModel
    {

        public string Title { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }


        [Required]
        [Display(Name = "User Name")]
     //   [RegularExpression(@"(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,250})$", ErrorMessage = "User name allow alphanumeric character with minimum of 8 characters")]
        public string UserName { get; set; }


        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
         public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Security Question 1")]
        public string SecurityQuestion1 { get; set; }
        [Required]
        [Display(Name = "Security Question 2")]
        public string SecurityQuestion2 { get; set; }

        [Required]
        [Display(Name = "Security Question 3")]
        public string SecurityQuestion3 { get; set; }

        [Required]
        [Display(Name = "Security Answer 1")]
        public string SecurityAnswer1 { get; set; }
        [Required]
        [Display(Name = "Security Answer 1")]
        public string SecurityAnswer2 { get; set; }

        [Required]
        [Display(Name = "Security Answer 1")]
        public string SecurityAnswer3 { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> RolesList { get; set; }

        public bool IsDelete { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool IsForgot { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public int UserId { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string MobileNumber { get; set; }

        public string PostalCode { get; set; }

        public string State { get; set; }
        public string DeleteComment { get; set; }

        public string Role { get; set; }
    }
}