using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
  public  interface IMailTemplateRepository
    {
      IEnumerable<MailTemplate> FindByID(MailTemplateModel mailTemplateModel);
      bool FindByStatus(MailTemplateModel mailTemplateModel);
      int InsertUpdateMailTemplate(MailTemplateModel mailTemplateModel);
      IEnumerable<MailTemplate> FindByMailStatusCheck(MailTemplateModel mailTemplateModel);
    }
}
