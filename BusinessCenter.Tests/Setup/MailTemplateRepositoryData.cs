using BusinessCenter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Setup
{
   public class MailTemplateRepositoryData
    {
       private readonly List<MailTemplate> _entities;
        public bool IsInitialized;

        public void AddMailTemplateEntity(MailTemplate obj)
        {
            _entities.Add(obj);
        }

        public List<MailTemplate> MailTemplateEntitiesList
        {
            get { return _entities; }
        }


        public MailTemplateRepositoryData()
        {
            IsInitialized = true;
            _entities = new List<MailTemplate>();

            AddMailTemplateEntity(new MailTemplate()
            {
                EmailTemplateId = "6eaf458c-1918-4c80-8820-6f63be9d6c3c",
                Type = "DailyMailAlert",
                Subject = "MailAlert",
                MailSentFailCount=0,
                MailContent="",
                IsMailSent=true,
                MailCreatedDate=DateTime.Now,
                MailSentDate=DateTime.Now,
                UserId = "B068CA9E-BF68-4E09-816E-C1135083880E",
                Custom_Application_Id = "LREN14005140"
            });
        }
    }
}
