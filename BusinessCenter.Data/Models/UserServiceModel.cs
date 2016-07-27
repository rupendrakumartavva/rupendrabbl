using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Models
{
   public class UserServiceModel
    {
        public int ServiceId { get; set; }
        public string DataSource { get; set; }
        public int EntityId { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int CreatedBy { get; set; }
  
        public int UpdatedBy { get; set; }
        public string  UserId { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public string CompanyName { get; set; }

        public string LicenseNumber { get; set; }
        public bool IsChanged { get; set; }
        public string DisplayType { get; set; }
        public string KeyType { get; set; }
    }

   public class UserLoginHistoryModel
   {
       public long LoginHisId { get; set; }
       public string UserId { get; set; }
       public DateTime? LastLoginDate { get; set; }
       public int? Count { get; set; }
     
   }
    public class LoginHistory
    {
        public string UserRole { get; set; }
        public long LoginHisId { get; set; }
        public string UserId { get; set; }
        public DateTime LastLoginDate { get; set; }
        public string Count { get; set; }
        public string UserName { get; set; }
    }

    public class UserCreatedDelete
    {
        public string StatusDetails { get; set; }
       
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string roleId { get; set; }
        public bool  IsDelete { get; set; }
    }


    public class DeleteServiceSingle
    {    
        public string UserId { get; set; }      
    }

  
}
