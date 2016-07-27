using System;
using System.Net.Mail;

namespace BusinessCenter.Email
{
    public class EmailSending
    {
        //private MailAddress _addressFrom;
        //private MailAddress _addressTo;
        private SmtpClient _mailClient = new SmtpClient();

        //public void SendingMail(string toEmailAddress, string inputSubject, string fileAttach)
        //{
        //    try
        //    {
        //        MailAddress addressFrom = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["FromEmailID"]);
        //        MailAddress addressTo = new MailAddress(toEmailAddress);
        //        MailMessage mailMessage = new MailMessage(addressFrom, addressTo);

        //        mailMessage.IsBodyHtml = true;
        //        mailMessage.Subject = inputSubject;
        //        SmtpClient mailClient = new SmtpClient();
        //        mailClient.UseDefaultCredentials = false;
        //        mailClient.Host = System.Configuration.ConfigurationManager.AppSettings["host"].ToString();
        //        // mailClient.Port = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["port"].ToString());
        //        mailClient.Credentials = new System.Net.NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["username"].ToString(), System.Configuration.ConfigurationManager.AppSettings["password"].ToString());
        //        // mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        //        if (fileAttach != string.Empty)
        //        {
        //            // Attach the newly created email attachment
        //            mailMessage.Attachments.Add(new Attachment(fileAttach));

        //        }

        //        // mailClient.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis ;
        //        mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        //        mailClient.Send(mailMessage);

        //        // mailClient.Send(mailMessage);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public void SendingMail(string inputSubject, string strHtml, string toEmailAddress)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            string msg = string.Empty;
            try
            {
                var displayName = System.Configuration.ConfigurationManager.AppSettings["displayName"].ToString().Trim();
                var fromMail = System.Configuration.ConfigurationManager.AppSettings["fromMail"].ToString().Trim();
                MailAddress fromAddress = new MailAddress(fromMail, displayName);
                message.From = fromAddress;

                message.To.Add(toEmailAddress);
                message.Subject = inputSubject;
                message.IsBodyHtml = true;
                message.Body = strHtml;
                smtpClient.Host = System.Configuration.ConfigurationManager.AppSettings["host"].ToString().Trim(); // "CodeIT@123" We use gmail as our smtp client//587; "smtp.gmail.com";
                smtpClient.Port = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["port"].ToString());

                if (System.Configuration.ConfigurationManager.AppSettings["SSL"].ToString().Trim() == "true")
                {
                    smtpClient.EnableSsl = true;
                }
                else
                {
                    smtpClient.EnableSsl = false;
                }

                smtpClient.UseDefaultCredentials = false;
                //  smtpClient.UseDefaultCredentials = true;

                var userName = System.Configuration.ConfigurationManager.AppSettings["username"].ToString().Trim();
                var password = System.Configuration.ConfigurationManager.AppSettings["password"].ToString().Trim();

                smtpClient.Credentials = new System.Net.NetworkCredential(userName, password);

                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
        }

        public void SendMailHtml(string toEmailAddress, string inputSubject,
                                 string strHtmlL, string host, string userName,
                                 string password, string fromMailId)
        {
            try
            {
                // MailAddress addressFrom = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["FromEmailID"]);
                MailAddress addressFrom = new MailAddress(fromMailId.Trim());
                MailAddress addressTo = new MailAddress(toEmailAddress);
                MailMessage mailMessage = new MailMessage(addressFrom, addressTo);
                mailMessage.Body = strHtmlL;
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = inputSubject;
                SmtpClient mailClient = new SmtpClient();
                mailClient.UseDefaultCredentials = false;
                mailClient.Host = host;
                mailClient.Credentials = new System.Net.NetworkCredential(userName.Trim(), password.Trim());
                mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                mailClient.Send(mailMessage);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public void SendMailAttachment(string toEmailAddress, string inputSubject, Attachment attachment)
        {
            try
            {
                MailAddress addressFrom = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["FromEmailID"]);
                MailAddress addressTo = new MailAddress(toEmailAddress);
                MailMessage mailMessage = new MailMessage(addressFrom, addressTo);
                mailMessage.Body = inputSubject + " enclosed.";
                mailMessage.Subject = inputSubject;
                mailMessage.IsBodyHtml = true;
                mailMessage.Attachments.Add(attachment);
                SmtpClient mailClient = new SmtpClient();
                mailClient.UseDefaultCredentials = false;
                mailClient.Host = System.Configuration.ConfigurationManager.AppSettings["host"].ToString();
                mailClient.Credentials = new System.Net.NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["username"].ToString(), System.Configuration.ConfigurationManager.AppSettings["password"].ToString());
                mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                mailClient.Send(mailMessage);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
    }
}