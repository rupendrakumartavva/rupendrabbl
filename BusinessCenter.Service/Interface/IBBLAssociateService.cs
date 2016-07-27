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
    public interface IBBLAssociateService
    {
        IEnumerable<DCBC_ENTITY_BBL_Renewals> FindByPin(BblAsscoiatePin bblAssociate);

        bool CheckAssociate(BblAsscoiatePin bblAssociate);

        IEnumerable<DCBC_ENTITY_BBL> GetAssociteData(string licenseNumber);

        //  void UpdateUserAssociate(BblAsscoiateService bblassociate);
        string InsertAssociateBbl(BblAsscoiateService bblService);

        bool DeleteUserService(BblAsscoiateService bblService);

        UploadStatus InsertServiceDocuments(BblServiceDocuments bblServiceDocuments);

        IEnumerable<BblDocuments> DocumentList(BblDocuments bbldoc);

        bool UpdateSubmissionMaster(BblDocuments bbldoc);

        bool InsertPhysicallocation(CofoHopDetailsModel generalbusiness);

        bool UpdateIsCofoinChecklistApp(CofoHopDetailsModel cofoHopDetailsModel);

        bool UpdateAllCheckListConditions(CofoHopDetailsModel cofoHopDetailsModel);

        bool InsertCorporationDetails(GeneralBusiness DetailsModel);

        bool InsertEHopEligibility(EligibilityModel eligibilityModel);

        GeneralBusiness GetHeadQuarterAddress(GeneralBusiness subCorpAgentModel);

        GeneralBusiness GetPrimisessAddress(GeneralBusiness subCorpAgentModel);

        IEnumerable<GeneralBusiness> GetCorpBusinessDetails(GeneralBusiness generalBusiness);

        EhopModel MasterHopEligibility(EhopModel ehopModel);

        SubmissionVerfication SubmissionDetails(SubmissionVerfication SubVerfication);

        bool DeleteDocuments(BblServiceDocuments bbldoc);

        string InsertPaymentDetails(PaymentDetailsModel pDetails);

        IEnumerable<PaymentDetails> FindByPaymentID(PaymentDetails paymentDetails);

        List<GeneralBusiness> GetCorpAgent(GeneralBusiness generalBusiness);

        bool UpdatePaymentDetails(PaymentDetailsModel pDetails);

        ReceiptModel GetReceiptData(ReceiptModel RModel);

        bool DeleteCofo(CofoHopDetailsModel cofoModel);

        IEnumerable<MasterCategoryDocument> FindByDocID(int documentid);

        IEnumerable<MasterCategoryDocument> FindByDocName(string categoryname);

        IEnumerable<MasterCategoryDocument> FindByID(string categoryname);

        IEnumerable<MasterCategoryDocument> FindByRenewID(string categoryname);

        int InsertUpdateCategoryDocuments(MasterCategoryDocumentModel categoryDocumentModel);

        bool DeleteCategoryDocument(MasterCategoryDocumentModel categoryDocumentModel);

        int InsertUpdateCategoryFees(OSub_Category_FeesEntity oSub_Category_FeesEntity);

        IEnumerable<OSub_Category_Fees> FindFeesByPrimaryCategory(string PrimaryId);

        IEnumerable<OSub_Category_Fees> FindFeesBySecondaryCategory(string secondaryId);

        IEnumerable<OSub_Category_Fees> FindByCategoryFeeId(string categoryFeeId);

        IEnumerable<OSub_Category_Fees> AllSubCategoryFees();

        IEnumerable<UserBBLService> CheckUserBBL(string entityID, string userid);

        IEnumerable<string> FindByUserName(string term);

        bool DeleteHopPrimsesAddrss(CofoHopDetailsModel cofoModel);

        bool DeleteSubmissionCorpEmpty(string masterId, string type);

        // void CofoServiceStatus(string Masterid);
        //   int InsertUpdateCategoryQuestions(CategoryQuestionModel categoryQuestionModel);
        IEnumerable<User> FindByAdminId(string userId);

        string InsertUpdatePrimaryCategory(PrimaryPhysicallocation primaryPhysicallocation);

        IEnumerable<OSub_Category_Fees> FindFeesByDescription(string Description);

        CorporationStatus CorpServiceStatus(string masterId);

        bool DocumentInsertion(string Masterid, string licenseNumber);

        bool InsertTransferLicense(Submissiontransfer tranferlic);

        IEnumerable<MasterCategoryQuestion> FindBySecondaryName(string secondaryname);

        IEnumerable<MasterCategoryDocument> FindByDocNameBasedonCategoryName(string categoryname);

        IEnumerable<MasterCategoryDocument> FindByDocBasedonDocId(int documentId);

        bool RenewalStatuUpdation(string masterid, string submissionLicense);

        IEnumerable<SubmissionCofo_Hop_Ehop> EhopNumberWithMasterId(string masterId);

        IEnumerable<SubmissionCofo_Hop_Ehop_Address> GetPrimisessAddress(int customTypeid);

        //bool UpdatePrimaryCategory(PrimaryPhysicallocation primaryPhysicallocation);
        SubmissionVerfication SubmissionPayDetails(SubmissionVerfication SubVerfication);

        TaxAndReneueInitailDisplay DisplayTaxAndRevenuWithPrimisessDetails(string masterId);

        IEnumerable<UserBBLService> UserbblServiceFindById(int serviceId);

        IEnumerable<DCBC_ENTITY_BBL> GetBblDataOnEntityId(int entityId);

        string BusinessOwnerName(string masterId);

        EhopData EhopData(EhopData ehopData);

        BasicBusinessLicense BusinessReceipt(BasicBusinessLicense basicBusinessLicense);

        RenewBasicBusinessLicense RenewBusinessReceipt(RenewBasicBusinessLicense renewBasicBusinessLicense);

        void UpdateCorpDisplayStatus(GeneralBusiness generalBusiness);

        IEnumerable<SubmissionMaster_ApplicationCheckList> FindByMasterId(string masterid);

        IEnumerable<StreetTypes> AllStreetTypes();

        string CorpOnlineSearch(CorporationDetails corporationdetails);

        IEnumerable<DCBC_ENTITY_BBL_Renewals> FindByLicense(string licenseNumber);

        PaymentTransactionDetails FindAddressByPaymentId(string masterid);

        string GetRenewalLicenseNumber(string entityId);

        string GetStateFullName(string stateCode, string countryCode);

        string GetCountryFullName(string countryCode);

        bool FindByLicenseTax(BblAsscoiatePin bblassociatepin);

        IEnumerable<DCBC_ENTITY_BBL_Renewals> RenewalData(string licenseNumber, string lrenNumber);

        IEnumerable<DCBC_ENTITY_BBL> DailyMailAlarmToBBlLicenseUserPriorToExpired();

        IEnumerable<UserBBLService> MailAlarmAssociateUsers();

        IEnumerable<User> GetUserDetails();

        string StateCode(string statename, string countryCode);

        IEnumerable<UserBBLService> GetTransferdata(BblAsscoiateService bblService);

        IEnumerable<SubmissionBblAssociationToUsers> GetTransferHistory();

        bool LastLoggedTimeUpdate(Userdetails userdetail);

        string GetRenewalLicense(string licensenumber);

        IEnumerable<DCBC_ENTITY_BBL> ValidateBblEntityWithLicense(string licenseNumber);

        IEnumerable<UserBBLService> GetUserBBLData(int serviceId);

        IEnumerable<SubmissionCorporation_Agent> SubmissionCorporation_Agent_ByMasterId(string MasterId);

        IEnumerable<SubmissionCorporation_Agent_Address> MailAddresses(int masterId, string type);
    }
}