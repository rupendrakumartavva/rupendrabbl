using BusinessCenter.Common;
using BusinessCenter.Data;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Service.Interface
{
    public interface ISubmissionMasterService
    {
        IEnumerable<SubmissionMaster> GetAllSubmissionMaster();
        IEnumerable<SubmissionMaster> FindBySubmissionMasterId(SubmissionMasterModel submissionMasterModel);
        IEnumerable<SubmissionMaster> FindByEntityID(string submissionLicense);
        string InsertAssociateBblService(SubmissionApplication submissionApp);
        IEnumerable<BBlService> GetBblService(BblAsscoiateService BblService);
        bool UpdateUserSelect(GeneralBusiness bbldoc);
        GeneralBusiness GetMailType(GeneralBusiness detailsModel);
        bool UpdateSubmissionMaster(PaymentDetailsModel pDetails);
        bool UpdateEhop(CofoHopDetailsModel cofoModel);
        string ValidateBblLicence(string licenceNumber);
        bool UpdateEhopNonSelect(string masterId);
        int GetUserAssociateBblCount(BblAsscoiateService BblService);
     //   IEnumerable<SubmissionMasterDetails> AllSubmissions(string userId);
        SubmissionData MasterStatus(string masterId);
        bool UpdateUserAssociateExpiryDate(string masterId);
        bool TransferSubmissions(Submissiontransfer bbldoc);
        string GetmasterId(string enitiyId, string userId);
        IEnumerable<UserBBLService> GetBblServices(string userSericeId);
        IEnumerable<SubmissionMaster> GetRenewMasterUserAssociateID(RenewModel renewModel);
        ApplicationReviewCounts GetApplicationReviewCounts();
        TaxRevenueData GetAddressDetails(TaxRevenueData taxRevenueData);
        IEnumerable<SubmissionMaster> FindByMasterID(string masterId);
        bool UpdateSubmissionOrder(string masterId, string ehopFileName, string noDocumentOrderPdf);
        IEnumerable<User> GetUserDetails();
        IEnumerable<UserBBLService> UserBblAssociationService();
        bool UpdateUserSubmissionExpiryDate(string masterId);
        List<UserBBLService> UserBblDetails(string masterId);
        IEnumerable<SubmissionMaster> SubmissionDetailsBasedonLicenseandUserId(string submissionLicense, string userid);
        IQueryable<DCBC_ENTITY_BBL> BblDetails(string licenceNumber);
    }
}
