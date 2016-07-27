using BusinessCenter.Common;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
    public interface ISubmissionMasterRepository
    {
        IEnumerable<SubmissionMaster> AllSubmissionMaster();
        IEnumerable<SubmissionMaster> FindByID(SubmissionMasterModel submissionMasterModel);
        string InsertAssociateBblService(SubmissionApplication submissionApp);
        IEnumerable<SubmissionMaster> FindByEntityID(string submissionLicense);
        IEnumerable<BBlService> BBlServiceList(BblAsscoiateService BblService);
        bool UpdateDocumentType(BblDocuments bbldoc);
        IEnumerable<SubmissionMaster> FindByMasterID(string masterId);
        bool UpdateUserSelect(GeneralBusiness bbldoc);
        GeneralBusiness GetMailType(GeneralBusiness detailsModel);
        bool UpdateSubmissionMaster(PaymentDetailsModel pDetails);
        bool UpdateBusinessStructure(GeneralBusiness detailsModel);
        bool UpdateEhopTotal(decimal grandTotal,string Masterid);
        bool UpdateEhop(CofoHopDetailsModel cofoModel);
        string InsertRenewData(RenewModel renewModel);
        string ValidateBblLicence(string licenceNumber);
        bool UpdateEhopNonSelect(string masterId);
        int GetUserAssociateBblListCount(BblAsscoiateService BblService);
        bool UpdateRenwalDocumentType(DocumentCheck documentCheck);
        //IEnumerable<SubmissionMasterDetails> AllSubmissions(string userId);
        SubmissionData MasterStatus(string masterId);
        bool UpdateUserAssociateExpiryDate(string  masterId);
        bool TransferSubmissions(Submissiontransfer bbldoc);
        string GetmasterId(string enitiyId, string userId);
        bool UpdateNoDocStatus(string masterId);
        IEnumerable<UserBBLService> GetBblServices(string userSericeId);
        RenewModel GetRenewalSubmissionData(RenewModel renewModel);
        IEnumerable<SubmissionMaster> GetRenewMasterUserAssociateID(RenewModel renewModel);
        // string UpdateFinalDocumentsToAccela(string licenseNumber, string masterId);
        ApplicationReviewCounts GetApplicationReviewCounts();
        TaxRevenueData GetAddressDetails(TaxRevenueData taxRevenueData);
        bool DeleteSubmissionMaster(string masterId);
        bool UpdateBusinessOwner(string masterId, string businessowner);
        bool UpdateBusinessName(string masterId, string businessname);
        bool UpdatePremisesAddress(string masterId, string premisesAddress);
        bool UpdateSubmissionOrder(string masterId, string ehopFileName, string noDocumentOrderPdf);
        IEnumerable<User> FindByUser();
        IEnumerable<UserBBLService> GetUserBblService();
        IEnumerable<User> FindUserDetails(string userId);
        bool UpdateUserSubmissionExpiryDate(string masterId);
        bool UpdateRenewNoDocStatus(string masterId);
        List<UserBBLService> GetUserBblDetails(string masterId);
        IEnumerable<SubmissionMaster> GetMasterId(string submissionLicense, string userid);
        IQueryable<DCBC_ENTITY_BBL> GetBblDetails(string licenceNumber);
        bool UpdateSubmissionsMasterExpirationDatewithStatus(string masterId, string status);
    }
}
