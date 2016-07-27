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
    public class Portal_Content_ErrorsService : IPortal_Content_ErrorsService
    {
        private readonly IPortal_Content_ErrorsRepository _portalcontentRepository;

        /// <summary>
        ///
        /// </summary>
        /// <param name="portalcontentRepository"></param>
        public Portal_Content_ErrorsService(IPortal_Content_ErrorsRepository portalcontentRepository)
        {
            _portalcontentRepository = portalcontentRepository;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Portal_Content_Errors> GetAllContentErrors()
        {
            var contentErrors = _portalcontentRepository.GetAllErrors();
            return contentErrors.ToList();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="portalContentErrors"></param>
        /// <returns></returns>
        public IEnumerable<Portal_Content_Errors> FindByMessageId(PortaContentErrorsModel portalContentErrors)
        {
            var commandata = _portalcontentRepository.FindByMessageId(portalContentErrors);
            return commandata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="portalContentErrors"></param>
        /// <returns></returns>
        public IEnumerable<Portal_Content_Errors> FindByMessageName(PortaContentErrorsModel portalContentErrors)
        {
            var commandata = _portalcontentRepository.FindByMessageName(portalContentErrors);
            return commandata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="portalContentErrors"></param>
        /// <returns></returns>
        public bool InsertUpdateContentErrors(PortaContentErrorsModel portalContentErrors)
        {
            var commondata = _portalcontentRepository.InsertUpdateContentErrors(portalContentErrors);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="portalContentErrors"></param>
        /// <returns></returns>
        public bool DeleteContentErrors(PortaContentErrorsModel portalContentErrors)
        {
            var commondata = _portalcontentRepository.DeleteContentErrors(portalContentErrors);
            return commondata;
        }
    }
}