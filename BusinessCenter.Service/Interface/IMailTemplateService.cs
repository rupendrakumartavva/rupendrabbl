using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Data;
using BusinessCenter.Data.Model;

namespace BusinessCenter.Service.Interface
{
  public  interface IMailTemplateService
    {
        IEnumerable<MailTemplate> FindByID(MailTemplateModel mailTemplateModel);
        bool FindByStatus(MailTemplateModel mailTemplateModel);
        int InsertUpdateMailTemplate(MailTemplateModel mailTemplateModel);
        IEnumerable<MailTemplate> FindByMailStatusCheck(MailTemplateModel mailTemplateModel);
    }
}
