using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Identity.IdentityModels
{
    public class ApplicationUserProfile
    {
      
        public virtual string Address { get; set; }

        public virtual string City { get; set; }
    
        public virtual string State { get; set; }
        public virtual string PostalCode { get; set; }
        public virtual string MobileNumber { get; set; }
        public virtual string ActivationCode { get; set; }

        public DateTime? ActivationDate { get; set; }
        public virtual string UserId { get; set; }
     
    }
}
