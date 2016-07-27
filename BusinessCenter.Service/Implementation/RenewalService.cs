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
    public class RenewalService : IRenewalService
    {
        protected IRenewRepository _renewRepo;
        protected IBblRepository _bblRepo;
        private readonly ISubmissionMasterRenewalRepository _submissionMasterRenewalRepository;

        /// <summary>
        ///
        /// </summary>
        /// <param name="renewReposiotry"></param>
        /// <param name="_bblRepository"></param>
        /// <param name="submissionMasterRenewalRepository"></param>
        public RenewalService(IRenewRepository renewReposiotry, IBblRepository _bblRepository, ISubmissionMasterRenewalRepository submissionMasterRenewalRepository)
        {
            _renewRepo = renewReposiotry;
            _bblRepo = _bblRepository;
            _submissionMasterRenewalRepository = submissionMasterRenewalRepository;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="renewModel"></param>
        /// <returns></returns>
        public IEnumerable<DCBC_ENTITY_BBL> GetRenewData(RenewModel renewModel)
        {
            var commandata = _bblRepo.GetRenewData(renewModel);
            return commandata.ToList();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="renewModel"></param>
        /// <returns></returns>
        public RenewModel CheckRenewal(RenewModel renewModel)
        {
            var commandata = _renewRepo.CheckRenewal(renewModel);
            return commandata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="documentCheck"></param>
        /// <returns></returns>
        public bool CheckDocument(DocumentCheck documentCheck)
        {
            var commandata = _renewRepo.CheckDocument(documentCheck);
            return commandata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="documentCheck"></param>
        /// <returns></returns>
        public bool UpdateRenwalDocumentType(DocumentCheck documentCheck)
        {
            var commandata = _renewRepo.UpdateRenwalDocumentType(documentCheck);
            return commandata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="renewModel"></param>
        /// <returns></returns>
        public bool DeleteRenewal(RenewModel renewModel)
        {
            var commandata = _renewRepo.DeleteRenewal(renewModel);
            return commandata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns></returns>
        public IEnumerable<SubmissionMasterRenewal> FindByID(string masterId)
        {
            var commandata = _submissionMasterRenewalRepository.FindByID(masterId);
            return commandata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="renewModel"></param>
        /// <returns></returns>
        public RenewModel CheckDocument(RenewModel renewModel)
        {
            var commandata = _renewRepo.CheckDocument(renewModel);
            return commandata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="renewModel"></param>
        /// <returns></returns>
        public RenewModel CheckAmount(RenewModel renewModel)
        {
            var commandata = _renewRepo.CheckAmount(renewModel);
            return commandata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="entityID"></param>
        /// <param name="renewallicenseNumber"></param>
        /// <param name="renewallrenNumber"></param>
        /// <returns></returns>
        public string CheckCategoryStatus(string entityID, string renewallicenseNumber, string renewallrenNumber)
        {
            var commandata = _renewRepo.CheckCategoryStatus(entityID, renewallicenseNumber, renewallrenNumber);
            return commandata;
        }
    }
}