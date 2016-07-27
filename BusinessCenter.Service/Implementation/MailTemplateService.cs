using BusinessCenter.Data;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using BusinessCenter.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Service.Implementation
{
    public class MailTemplateService : IMailTemplateService
    {
        public readonly IMailTemplateRepository _mailRepository;

        /// <summary>
        ///
        /// </summary>
        /// <param name="mailRepository"></param>
        public MailTemplateService(IMailTemplateRepository mailRepository)
        {
            _mailRepository = mailRepository;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="mailTemplateModel"></param>
        /// <returns></returns>
        public IEnumerable<MailTemplate> FindByID(MailTemplateModel mailTemplateModel)
        {
            var commondata = _mailRepository.FindByID(mailTemplateModel);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="mailTemplateModel"></param>
        /// <returns></returns>
        public bool FindByStatus(MailTemplateModel mailTemplateModel)
        {
            var commondata = _mailRepository.FindByStatus(mailTemplateModel);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="mailTemplateModel"></param>
        /// <returns></returns>
        public int InsertUpdateMailTemplate(MailTemplateModel mailTemplateModel)
        {
            var commondata = _mailRepository.InsertUpdateMailTemplate(mailTemplateModel);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="mailTemplateModel"></param>
        /// <returns></returns>
        public IEnumerable<MailTemplate> FindByMailStatusCheck(MailTemplateModel mailTemplateModel)
        {
            var commondata = _mailRepository.FindByMailStatusCheck(mailTemplateModel);
            return commondata;
        }
    }
}