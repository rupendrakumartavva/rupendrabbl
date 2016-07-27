using BusinessCenter.Common;
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
    public class SubmissionMasterService : ISubmissionMasterService
    {
        protected ISubmissionMasterRepository _repository;

        /// <summary>
        ///
        /// </summary>
        /// <param name="repo"></param>
        public SubmissionMasterService(ISubmissionMasterRepository repo)
        {
            _repository = repo;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SubmissionMaster> GetAllSubmissionMaster()
        {
            var commandata = _repository.AllSubmissionMaster();
            return commandata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserBBLService> UserBblAssociationService()
        {
            var commandata = _repository.GetUserBblService();
            return commandata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="submissionLicense"></param>
        /// <returns></returns>
        public IEnumerable<SubmissionMaster> FindByEntityID(string submissionLicense)
        {
            var commondate = _repository.FindByEntityID(submissionLicense);
            return commondate;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="submissionMasterModel"></param>
        /// <returns></returns>
        public IEnumerable<SubmissionMaster> FindBySubmissionMasterId(SubmissionMasterModel submissionMasterModel)
        {
            var commondate = _repository.FindByID(submissionMasterModel);
            return commondate;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="submissionApp"></param>
        /// <returns></returns>
        public string InsertAssociateBblService(SubmissionApplication submissionApp)
        {
            string commondate = _repository.InsertAssociateBblService(submissionApp);
            return commondate;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="BblService"></param>
        /// <returns></returns>
        public IEnumerable<BBlService> GetBblService(BblAsscoiateService BblService)
        {
            var commandata = _repository.BBlServiceList(BblService);
            return commandata.ToList();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="bbldoc"></param>
        /// <returns></returns>
        public bool UpdateUserSelect(GeneralBusiness bbldoc)
        {
            var commondate = _repository.UpdateUserSelect(bbldoc);
            return commondate;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="detailsModel"></param>
        /// <returns></returns>

        public GeneralBusiness GetMailType(GeneralBusiness detailsModel)
        {
            var commondate = _repository.GetMailType(detailsModel);
            return commondate;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="pDetails"></param>
        /// <returns></returns>
        public bool UpdateSubmissionMaster(PaymentDetailsModel pDetails)
        {
            var commondata = _repository.UpdateSubmissionMaster(pDetails);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cofoModel"></param>
        /// <returns></returns>
        public bool UpdateEhop(CofoHopDetailsModel cofoModel)
        {
            var commondata = _repository.UpdateEhop(cofoModel);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="licenceNumber"></param>
        /// <returns></returns>
        public string ValidateBblLicence(string licenceNumber)
        {
            return _repository.ValidateBblLicence(licenceNumber);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns></returns>
        public bool UpdateEhopNonSelect(string masterId)
        {
            return _repository.UpdateEhopNonSelect(masterId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="BblService"></param>
        /// <returns></returns>
        public int GetUserAssociateBblCount(BblAsscoiateService BblService)
        {
            return _repository.GetUserAssociateBblListCount(BblService);
        }

        //public IEnumerable<SubmissionMasterDetails> AllSubmissions(string userId)
        //{
        //    return _repository.AllSubmissions(userId);
        //}
        /// <summary>
        ///
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns></returns>
        public SubmissionData MasterStatus(string masterId)
        {
            return _repository.MasterStatus(masterId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns></returns>
        public bool UpdateUserAssociateExpiryDate(string masterId)
        {
            return _repository.UpdateUserAssociateExpiryDate(masterId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="bbldoc"></param>
        /// <returns></returns>
        public bool TransferSubmissions(Submissiontransfer bbldoc)
        {
            return _repository.TransferSubmissions(bbldoc);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="enitiyId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetmasterId(string enitiyId, string userId)
        {
            return _repository.GetmasterId(enitiyId, userId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userSericeId"></param>
        /// <returns></returns>
        public IEnumerable<UserBBLService> GetBblServices(string userSericeId)
        {
            return _repository.GetBblServices(userSericeId).ToList();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="renewModel"></param>
        /// <returns></returns>
        public IEnumerable<SubmissionMaster> GetRenewMasterUserAssociateID(RenewModel renewModel)
        {
            return _repository.GetRenewMasterUserAssociateID(renewModel);
        }

        //IEnumerable<UserBBLService> GetBblServices(string userSericeId);
        public ApplicationReviewCounts GetApplicationReviewCounts()
        {
            return _repository.GetApplicationReviewCounts();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="taxRevenueData"></param>
        /// <returns></returns>
        public TaxRevenueData GetAddressDetails(TaxRevenueData taxRevenueData)
        {
            return _repository.GetAddressDetails(taxRevenueData);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns></returns>
        public IEnumerable<SubmissionMaster> FindByMasterID(string masterId)
        {
            var commondata = _repository.FindByMasterID(masterId);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="masterId"></param>
        /// <param name="ehopFileName"></param>
        /// <param name="noDocumentOrderPdf"></param>
        /// <returns></returns>
        public bool UpdateSubmissionOrder(string masterId, string ehopFileName, string noDocumentOrderPdf)
        {
            var commondata = _repository.UpdateSubmissionOrder(masterId, ehopFileName, noDocumentOrderPdf);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> GetUserDetails()
        {
            var commondata = _repository.FindByUser();
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns></returns>
        public bool UpdateUserSubmissionExpiryDate(string masterId)
        {
            return _repository.UpdateUserSubmissionExpiryDate(masterId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns></returns>
        public List<UserBBLService> UserBblDetails(string masterId)
        {
            var commondata = _repository.GetUserBblDetails(masterId);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="submissionLicense"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public IEnumerable<SubmissionMaster> SubmissionDetailsBasedonLicenseandUserId(string submissionLicense, string userid)
        {
            var commondata = _repository.GetMasterId(submissionLicense, userid);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="licenceNumber"></param>
        /// <returns></returns>
        public IQueryable<DCBC_ENTITY_BBL> BblDetails(string licenceNumber)
        {
            return _repository.GetBblDetails(licenceNumber).AsQueryable();
        }
    }
}