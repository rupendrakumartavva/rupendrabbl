using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Email
{
    public class EmailTemplate : IEmailTemplate
    {
        public EmailSending sendMail;

        public EmailTemplate(EmailSending SendMail)
        {
            sendMail = SendMail;
        }
        public bool MailSending(string subject, string mailBoday, string toMail)
        {
            bool result = false;
            try
            {
                sendMail.SendingMail( subject, mailBoday, toMail);
                result = true;
            }
            catch (Exception)
            {
                result = true; 
                throw;
            }

            return result;
        }
    }
}
