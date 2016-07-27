using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Identity.IdentityModels
{
  

    public class ApplicationUserEmail
    {
        public DateTime? ChangeEmailValidate { get; set; }

        public bool ChangeEmailConfirmed { get; set; }

        public DateTime? PreviousEmailValidate { get; set; }

        public bool PreviousEmailConfirmed { get; set; }

        public string OldMailId { get; set; }
        public string NewMailId { get; set; }
       public string MailType { get; set; }
       public virtual int UserId { get; set; }
		
    }

   
}
