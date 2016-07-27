using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Email
{
   public  interface IEmailTemplate
    {

       bool MailSending(string subject, string mailBoday, string toMail);

       

    }
}
