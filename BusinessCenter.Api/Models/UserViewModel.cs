using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessCenter.Api.Models
{
    public class UserViewModel
    {
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
        public bool IsDelete { get; set; }

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
}