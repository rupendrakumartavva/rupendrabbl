using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;

namespace BusinessCenter.Data.Implementation
{
    public class MailTemplateRepository : GenericRepository<MailTemplate>, IMailTemplateRepository
    {
      
        public MailTemplateRepository(IUnitOfWork context)
            :base(context)
        {
           
        }
        /// <summary>
        /// This method is to retrive particular MailTemplate data based on EmailTemplateId
        /// </summary>
        /// <param name="mailTemplateModel"></param>
        /// <returns>Retrun MailTemplate</returns>
        public IEnumerable<MailTemplate> FindByID(MailTemplateModel mailTemplateModel)
        {
            var mailTemplate = FindBy(x => x.EmailTemplateId == mailTemplateModel.EmailTemplateId);
            return mailTemplate;

        }
        /// <summary>
        /// This method is used to check the existence of MailTemplate based on Subject,UserId  and Custom_Application_Id
        /// </summary>
        /// <param name="mailTemplateModel"></param>
        /// <returns>Retrun Bool Status</returns>
        public bool FindByStatus(MailTemplateModel mailTemplateModel)
        {
            var mailTemplate = FindBy(x => x.Subject.Replace(System.Environment.NewLine, "").ToUpper().Trim() == mailTemplateModel.Subject.ToUpper().Trim()
                && x.UserId.Trim() == mailTemplateModel.UserId.Trim() && x.Custom_Application_Id.Trim() == mailTemplateModel.CustomApplicationId.Trim()).Any();
            return mailTemplate;

        }
        /// <summary>
        /// This method is used to retrive existence of MailTemplate based on Subject,UserId  and Custom_Application_Id
        /// </summary>
        /// <param name="mailTemplateModel"></param>
        /// <returns></returns>
        public IEnumerable<MailTemplate> FindByMailStatusCheck(MailTemplateModel mailTemplateModel)
        {
            var mailTemplate = FindBy(x => x.Subject.Replace(System.Environment.NewLine, "").ToUpper().Trim() == mailTemplateModel.Subject.ToUpper().Trim()
                && x.UserId.Trim() == mailTemplateModel.UserId.Trim() && x.Custom_Application_Id.Trim() == mailTemplateModel.CustomApplicationId.Trim());
            return mailTemplate;

        }
        /// <summary>
        /// This method is used to Insert/update MailTemplate based on inputs
        /// </summary>
        /// <param name="mailTemplateModel"></param>
        /// <returns>Retrun Numeric Value</returns>
        public int InsertUpdateMailTemplate(MailTemplateModel mailTemplateModel)
        {
            int result = 0;
            try
            {
                var mailTemplate = FindBy(x => x.Subject.Replace(System.Environment.NewLine, "").ToUpper().Trim() == mailTemplateModel.Subject.ToUpper().Trim()
               && x.UserId.Trim() == mailTemplateModel.UserId.Trim() && x.Custom_Application_Id.Trim() == mailTemplateModel.CustomApplicationId.Trim()
               && x.Type.Trim() == mailTemplateModel.Type.Trim());

                if (!mailTemplate.Any())
                {
                    var mailEntity = new MailTemplate
                    {
                        EmailTemplateId = Guid.NewGuid().ToString(),
                        Type = (mailTemplateModel.Type ?? "").Trim(),
                        Subject = (mailTemplateModel.Subject ?? "").Trim(),
                        MailSentFailCount = mailTemplateModel.MailSentFailCount,
                        MailContent = (mailTemplateModel.MailContent ?? "").Trim(),
                        IsMailSent = mailTemplateModel.IsMailSent,
                        UserId=mailTemplateModel.UserId.Trim(),
                        Custom_Application_Id=mailTemplateModel.CustomApplicationId.Trim(),
                        MailCreatedDate = DateTime.Now,
                        MailSentDate = DateTime.Now
                    };
                    Add(mailEntity);
                    Save();
                    result = 1;
                }
                else
                {
                    var templateMail = mailTemplate.FirstOrDefault();
                    templateMail.IsMailSent = true;
                    Update(templateMail, templateMail.EmailTemplateId);
                    Save();
                    result = 2;
                }
            }
            catch (Exception)
            {
                result = 0;
            }
            return result;
        }
    }
}
