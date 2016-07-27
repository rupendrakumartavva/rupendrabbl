using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Model
{
    public class BblAsscoiatePin
    {
        public string EntityId { get; set; }
        public string PinNumber { get; set; }
        public string LicenseNumber { get; set; }
        public string TaxNumber { get; set; }
        public string UserId { get; set; }
        public string UserBblAssociateId { get; set; }
        public string CleanHandsType { get; set; }
    }

    public class BblAsscoiateService
    {
        public string SubmissionLicense { get; set; }
        public string UserID { get; set; }
        public string PinNumber { get; set; }
        public DateTime LicenseExpirationDate { get; set; }
        public string Status { get; set; }
        public string CleanHandsType { get; set; }
        public string DCBC_ENTITY_ID { get; set; }
        public string B1_ALT_ID { get; set; }
        //public string Type { get; set; }
    }

    public class BblServiceList
    {
        public string MasterId { get; set; }
        public string EntityId { get; set; }
        public string UserBblServiceId { get; set; }
        public string LicNumber { get; set; }
        public string ExpDate { get; set; }
        public string LicenseeFirstLastName { get; set; }
        public string BusinessName { get; set; }
        public string TradeName { get; set; }
        public string PremiseAddress { get; set; }
        public string LicTypes { get; set; }
        public string Status { get; set; }
        public string MultipleLicense { get; set; }
        public bool IsEhop { get; set; }
        public string EhopNumber { get; set; }
        public string EhopType { get; set; }
        public string LrenNumber { get; set; }
        public string UserAssociateType { get; set; }
        public bool CategoryStatus { get; set; }
        public string PaymentDate { get; set; }
        public string SubCategory { get; set; }
        public string ExpirationDate { get; set; }
        public string UserName { get; set; }
        public string AppStatusdate { get; set; }
        public string DocumentSubmitType { get; set; }
        public decimal GrandTotal { get; set; }
        public string APP_Type { get; set; }
        public string ChcekEhopAllow { get; set; }
        public string ShowActivePdf { get; set; }
    }

    public class BBLServiceCount
    {
        public int Draft { get; set; }
        public int ExpirySoon { get; set; }
        public int UnderReview { get; set; }
        public int Active { get; set; }
        public int Expired { get; set; }
        public int Total { get; set; }
        public int Renew { get; set; }
        public int eHOP { get; set; }
        public int Lapsed { get; set; }
        public int RenewNotallowed { get; set; }
    }

    public class BBlService
    {
        public List<BblServiceList> BblServiceList { get; set; }
        public List<BBLServiceCount> BBLServiceCount { get; set; }
    }

    public class BusinessActivityModel
    {
        public string ActivityId { get; set; }
    }

    public class BusinessActivityEntity
    {
        public string ActivityID { get; set; }
        public string ActivityName { get; set; }
        public string APP_Type { get; set; }
    }

    public class PrimaryCategoryEntity
    {
        public string PrimaryID { get; set; }
        public string ActivityID { get; set; }
        public string Description { get; set; }
        public string Endorsement { get; set; }
        public string CategoryCode { get; set; }
        public string UnitOne { get; set; }
        public string UnitTwo { get; set; }
        public string App_Type { get; set; }
    }

    public class SlCategoryEntity
    {
        public string SecondaryId { get; set; }
        public string PrimaryId { get; set; }
        public string SecondaryLicenseCategory { get; set; }
        public string UnitOne { get; set; }
        public string UnitTwo { get; set; }
        public string Endorsement { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<bool> IsSuperSubCategoryAllow { get; set; }
    }

    public class SecondaryCategoryModel
    {
        public int EntityId { get; set; }
        public string PrimaryId { get; set; }
        public string SecondaryId { get; set; }
    }

    public class SubCategoryEntity
    {
        public string SubCatID { get; set; }
        public string SubCategoryName { get; set; }
        public string CustomCategoryName { get; set; }
        public Nullable<bool> Status { get; set; }
    }

    public class SubmissionModel
    {
        public int Id { get; set; }
        public string CustomerNumber { get; set; }
        public string ActivityId { get; set; }
    }

    public class SubmissionCategoryModel
    {
        public int SubmissionCategoryId { get; set; }
        public string MasterId { get; set; }
        public int CategoryTypeId { get; set; }
    }

    public class SubmissionMasterModel
    {
        public string MasterId { get; set; }
        public string SubmissionLicense { get; set; }
        public string UserId { get; set; }
        public int ActivityId { get; set; }
    }

    public class SubmissionQuestionModel
    {
        public int SubmQuestionsId { get; set; }
        public string MasterId { get; set; }
    }

    public class SubmissionCategoryList
    {
        public bool Status { get; set; }
        public string CategoryName { get; set; }
        public string SubCategory { get; set; }

        public List<CategoryDetails> CategoryDetailsList { get; set; }
        public string PrimaryCategoryCode { get; set; }
        public bool IsPDFShow { get; set; }
    }

    public class CategoryDetails
    {
        public string Endoresment { get; set; }
        public string CategoryName { get; set; }
    }

    public class SubmissionDocumentModel
    {
        public int SubmDocId { get; set; }
        public int SubmissionCategoryID { get; set; }
    }

    public class DetailedCategoryList
    {
        public string Endorsement { get; set; }
        public string CategoryId { get; set; }
        public string LicenseCategory { get; set; }
        public string Units { get; set; }
        public decimal ApplicationFee { get; set; }
        public decimal CategoryLicenseFee { get; set; }
        public decimal EndorsementFee { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TechFee { get; set; }
        public decimal TotalFee { get; set; }
        public string CategoryCode { get; set; }
        public bool IsRaoFeeApplied { get; set; }
        public string LicenseDuration { get; set; }
        public bool IsSubCategory { get; set; }
        public string SubCategoryName { get; set; }
        public decimal ExpiryFee { get; set; }
        public decimal LapsedFee { get; set; }
        public bool IsBackgroundInvestigation { get; set; }
    }

    public class SubmissionApplication
    {
        public bool IsRaoFeeApplied { get; set; }
        public string MasterId { get; set; }
        public string SubmissionLicense { get; set; }
        public string UserID { get; set; }
        public string ActivityID { get; set; }

        // public decimal ApplicationFee { get; set; }
        public decimal RAOFee { get; set; }

        public bool IseHOP { get; set; }
        public decimal eHOP { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string ApprovedBy { get; set; }
        public string Description { get; set; }
        public string App_Type { get; set; }
        public string DocSubmType { get; set; }
        public string FEIN { get; set; }
        public bool IsFEIN { get; set; }
        public string PrimaryID { get; set; }
        public string Secondary { get; set; }
        public string SubSubCategory { get; set; }
        public string ItemQty { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }

        //public bool Iscof { get; set; }
        public decimal Applicationfee { get; set; }

        public decimal Licensefee { get; set; }
        public decimal Endorsmentfee { get; set; }
        public decimal Techfee { get; set; }
        public List<ScreeningQuestion> SubQuestion { get; set; }
        public string LicenseCategory { get; set; }
        public string Endorsement { get; set; }
        public List<DetailedCategoryList> DetailedCategoryList { get; set; }
        public string Validation { get; set; }
        public decimal TotalFee { get; set; }
        public bool IsBusinessMustbeinDC { get; set; }
        public bool IsHomeBased { get; set; }
        public bool IsCofo { get; set; }
        public bool IsPhysicalLocationVerify { get; set; }
        public decimal GrandTotal { get; set; }
        public bool IsCorporationDivision { get; set; }
        public string BusinessStructure { get; set; }
        public string TradeName { get; set; }
    }

    public class SubmissionApplicationCategory
    {
        public int CategoryTypeID { get; set; }
        public decimal EndorsementFee { get; set; }
        public decimal LicenseCategoryFee { get; set; }
        public bool IsSecondaryCategory { get; set; }
        public int ItemQty { get; set; }
    }

    public class SubmissionApplicationQuestion
    {
        public string MasterId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }

    public class SubmissionServiceDocument
    {
        public int SubmissionApplicationID { get; set; }
        public string ApprovedBy { get; set; }
        public string DocRequired { get; set; }
        public string Agency { get; set; }
        public string Division { get; set; }
        public string FileName { get; set; }
        public string FileLocation { get; set; }
        public string DocStatus { get; set; }
        public string Description { get; set; }
    }

    public class SubmissionIndividual
    {
        public string MasterId { get; set; }
        public string CompanyBusinessLicense { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateofBirth { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string HairColor { get; set; }
        public string EyeColor { get; set; }
        public string IdentificationCard { get; set; }
        public string StateofIssuance { get; set; }
        public DateTime ExpirationDate { get; set; }
    }

    public class CategoryphysicallocationModel
    {
        public int PrimaryCategoryId { get; set; }
    }

    public class OSubCategoryFeesModel
    {
        public string Description { get; set; }
    }

    //public class ScreeningQuestion
    //{
    //    public List<CategoryQuestions> CategoryQuestionsList { get; set; }
    //    public List<BusinessStructure> BusinessStructureList { get; set; }

    //}

    //public class CategoryQuestions
    //{
    //    public string UnitOne { get; set; }
    //    public string UnitTwo { get; set; }
    //    public string Question { get; set; }
    //    public string Answers { get; set; }
    //}

    public class BusinessStructure
    {
        public string BusinessStructureOption { get; set; }
    }

    public class ScreeningQuestion
    {
        public string Question { get; set; }
        public List<BusinessStructure> Option { get; set; }
        public string Answer { get; set; }
        public string Type { get; set; }
        public int StartRange { get; set; }
        public int EndRange { get; set; }
        public string QuestionFor { get; set; }
        public string CategoryId { get; set; }
        public string keyIdentifying { get; set; }
    }

    public class TotalFees
    {
        public string ApplicationFee { get; set; }
        public string CategoryLicenseFee { get; set; }
        public string EndorsementFee { get; set; }
        public string PercentageTechFee { get; set; }
    }

    public class Endosementlist
    {
        public string CatId { get; set; }
        public string LicenseCategory { get; set; }
        public string Endorsement { get; set; }

        public string NumberUnit { get; set; }
    }

    public class BblServiceDocuments
    {
        public string MasterId { get; set; }
        public string SubmissionId { get; set; }
        public int SubmissionCategoryID { get; set; }
        public string ApprovedBy { get; set; }
        public string DocRequired { get; set; }
        public string Agency { get; set; }
        public string Division { get; set; }
        public string Div { get; set; }
        public string FileName { get; set; }
        public string FileLocation { get; set; }
        public string DocStatus { get; set; }
        public string Description { get; set; }
        public string Endorsement { get; set; }
        public string License { get; set; }
        public string CategoryID { get; set; }
        public string ShortName { get; set; }
        public bool IsUpload { get; set; }
        public string UploadFileName { get; set; }
        public string CheckListType { get; set; }
        public string CategoryCode { get; set; }
        public string LicenseName { get; set; }
    }

    public class BblDocuments
    {
        public string MasterId { get; set; }
        public List<BblServiceDocuments> BblServiceDoc { get; set; }
        public bool IsIndividual { get; set; }
        public bool IsFEIN { get; set; }
        public string DocSubType { get; set; }
        public bool IsHop { get; set; }
        public bool IsCof { get; set; }
        public string AppType { get; set; }
        public string BusinessStructure { get; set; }
        public string TradeName { get; set; }
        public string CategoryName { get; set; }
        public bool IsCorporationDivision { get; set; }
        public bool ISFEINSSN { get; set; }
        public bool IsCleanHandsVerify { get; set; }
        public bool IsCorporateRegistration { get; set; }
        public bool IsBHAddress { get; set; }
        public bool IsBPAddress { get; set; }
        public bool IsMailAddress { get; set; }
        public bool IsResidentAgent { get; set; }
        public bool IsDocforCleanHands { get; set; }
        public bool IsDocforCofo { get; set; }
        public bool IsDocforHop { get; set; }
        public bool IsDocforEhop { get; set; }

        public bool IsSubmissionCofo { get; set; }
        public bool IsSubmissionHop { get; set; }
        public bool IsSubmissioneHop { get; set; }
        public bool CheckedStatus { get; set; }
        public bool IsSubmissionCorpReg { get; set; }
        public bool IsSubmissionAgent { get; set; }
        public bool IsHomeBased { get; set; }
        public bool IsBusinessMustbeinDC { get; set; }
        public bool IsMcofo { get; set; }
        public string CategoryCode { get; set; }
        public bool IsIndividualValid { get; set; }
        public bool IsValidateTextRevenue { get; set; }
        public bool IsCategorySelfCertification { get; set; }
        public bool IsSelfCertification { get; set; }
        public string BusinessName { get; set; }
        public string PremisesAddress { get; set; }
        public bool physicalLocationValidate { get; set; }
        public bool IsTaxReattested { get; set; }
    }

    public class QuestionsList
    {
        public string MasterId { get; set; }
        public string CategoryName { get; set; }
        public string SubmissionLicense { get; set; }
        public int SubmissionCategoryID { get; set; }
        public string CategoryTypeID { get; set; }
        public string Endorsement { get; set; }
        public string License { get; set; }
        public string CategoryCode { get; set; }
        public string LicenseName { get; set; }
        public string Type { get; set; }
    }

    public class DeleteRecord
    {
        public int DeleteId { get; set; }
    }

    public class ServiceChecklist
    {
        public string MasterId { get; set; }
        public decimal ApplicationFee { get; set; }
        public decimal CategoryLicenseFee { get; set; }
        public decimal EndorsementFee { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TechFee { get; set; }
        public decimal TotalFee { get; set; }
        public bool Isehop { get; set; }
        public decimal ExtraAmount { get; set; }
        public string Extradays { get; set; }
        public string SubCategory { get; set; }
        public List<DetailedCategoryList> DetailedCategoryList { get; set; }
        public List<ScreeningQuestion> SubQuestion { get; set; }
    }

    public class UploadStatus
    {
        public bool Status { get; set; }
        public string FileName { get; set; }
    }

    //public class UploadStatus
    //{
    //    public bool Status { get; set; }
    //    public string FileName { get; set; }
    //}

    //public class SubmissionTaxRevenu
    //{
    //    public int SubmissionTaxRevenueId { get; set; }
    //    public string MasterId { get; set; }
    //    public string TaxRevenueFFIN { get; set; }
    //    public bool isDoc { get; set; }

    //}

    public class GeneralBusiness
    {
        public string StreetNumber { get; set; }
        public string AddressID { set; get; }
        public string AddressNumber { set; get; }
        public string AddressNumberSufix { get; set; }
        public string Anc { get; set; }
        public string Cluster { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Vote_Prcnct { get; set; }
        public string Ward { get; set; }
        public string Xcoord { get; set; }
        public string Ycoord { get; set; }
        public string UserType { get; set; }
        public string FileNumber { get; set; }
        public string MasterId { get; set; }
        public string CBusinessName { get; set; }
        public string TradeName { get; set; }
        public string BusinessStructure { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string BusinessName { get; set; }
        public string BusinessAddressLine1 { get; set; }
        public string BusinessAddressLine2 { get; set; }
        public string BusinessAddressLine3 { get; set; }
        public string BusinessAddressLine4 { get; set; }
        public string BusinessCity { get; set; }
        public string BusinessState { get; set; }
        public string BusinessCountry { get; set; }
        public string ZipCode { get; set; }

        //  public string Country { get; set; }
        public string Email { get; set; }

        public string EntityStatus { get; set; }
        public int SubCorporationRegId { get; set; }
        public string UserSelectTpe { get; set; }
        public string Quardrant { get; set; }
        public string UnitType { get; set; }
        public string Unit { get; set; }
        public string Telphone { get; set; }
        public bool IsValid { get; set; }
        public List<StreetDetails> Dropdownlist { get; set; }
        public string OccupancyAddssValidate { get; set; }
        public bool DonothaveCof { get; set; }
        public string CorpStatus { get; set; }
        public string HQStatus { get; set; }

        public string Zone { get; set; }
        public string Smd { get; set; }
        public string SSL { get; set; }
        public bool BusinessStructureStatus { get; set; }
        public bool IsDataChange { get; set; }
        //public List<RegisterAgent> AgentDetails { get; set; }
    }

    public class CofoHopDetailsModel
    {
        public string MasterId { get; set; }
        public int CofoHopId { get; set; }
        public string Number { get; set; }
        public string Type { get; set; }
        public string DateofIssue { get; set; }
        public string Street { get; set; }
        public string StreetName { get; set; }
        public Nullable<int> StreetTypeId { get; set; }
        public string StreetType { get; set; }
        public string Quadrant { get; set; }
        public string UnitType { get; set; }
        public string Unit { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Telephone { get; set; }
        public bool DonothaveCof { get; set; }
        public bool IsUploadSupportDoc { get; set; }
        public bool IsValid { get; set; }
        public bool IseHOPEligibility { get; set; }
        public string EHopEligibilityType { get; set; }
        public bool ConfirmeHOPEligibilityType { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public List<string> Dropdownlist { get; set; }
        public string OccupancyAddssValidate { get; set; }
        public string Country { get; set; }
        public string AddressId { get; set; }
        public string AddressNumber { get; set; }
        public string AddressNumberSufix { get; set; }
        public string Xcoord { get; set; }
        public string Ycoord { get; set; }
        public string Anc { get; set; }
        public string Ward { get; set; }
        public string Cluster { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Vote_Prcnct { get; set; }
        public string TradeName { get; set; }
        public string BusinessStructure { get; set; }
        public string Zone { get; set; }
        public string SMD { get; set; }
        public string SSL { get; set; }
        public string UnitNumber { get; set; }

        public bool IsQuadrant { get; set; }
        public bool IsStreetName { get; set; }
        public bool IsStreetType { get; set; }
        public bool IsStreetNumber { get; set; }
        public bool IsCofoHop { get; set; }
        public bool IsDataChange { get; set; }
        public bool IsSelfCertificationChange { get; set; }
    }

    public class StreetDetails
    {
        public string StreetType { get; set; }
        public string StreetCode { get; set; }
    }

    public class SubmissionCorpAgentModel
    {
        public int SCAId { get; set; }
        public string BusinessName { get; set; }
        public string FirstName { get; set; }
        public string MiddelName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string Email { get; set; }
        public bool isCorrect { get; set; }
        public bool isDocupload { get; set; }
        public Nullable<int> SubCorporationRegId { get; set; }
    }

    public class EligibilityModel
    {
        public string MasterId { get; set; }
        public string EhopIds { get; set; }
        public int TypeId { get; set; }
        public string UserId { get; set; }
    }

    public class RegisterAgent
    {
        // public int RegisterID { get; set; }
        public string Name { get; set; }

        public string CompanyName { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Telephone { get; set; }
        public string FileNumber { get; set; }
        public string Type { get; set; }
    }

    public class Master
    { public string MasterID { get; set; } }

    public class EhopModel
    {
        public List<MastereHOPEligibility> CheckList { get; set; }
        public bool IsChecked { get; set; }
        public string MasterId { get; set; }
        public int TypeId { get; set; }
    }

    public class SubmissionVerfication
    {
        public string StreetNumber { get; set; }
        public string MasterID { get; set; }
        public string OccupanyNumber { get; set; }
        public string DateofIssue { get; set; }
        public string CorpFileNo { get; set; }
        public string OrgType { get; set; }
        public string BusinessOwner { get; set; }
        public string FienNumber { get; set; }
        public string TradeName { get; set; }
        public string DocType { get; set; }
        public decimal GrandTotal { get; set; }
        public string GrandTotals { get; set; }
        public bool IsCofo { get; set; }
        public bool IsHomeBased { get; set; }
        public bool IsSubmissionCofo { get; set; }
        public bool IsSubmissionHop { get; set; }
        public bool IsSubmissioneHop { get; set; }
        public string FEIN { get; set; }
        public bool IsFEIN { get; set; }
        public bool IsRaoFeeAdded { get; set; }
        public string HeadQName { get; set; }
        public string HeadQBName { get; set; }
        public string HeadQAddress { get; set; }
        public string HeadQAddressNumber { get; set; }
        public string HeadQStreetName { get; set; }
        public string HeadQStreetType { get; set; }
        public string HeadQAQuadrant { get; set; }
        public string HeadQCity { get; set; }
        public string HeadQState { get; set; }
        public string HeadQCountry { get; set; }
        public string HeadQZip { get; set; }
        public string HeadQEmail { get; set; }
        public string HeadQTelePhone { get; set; }
        public string HeadQFirstName { get; set; }
        public string HeadQMiddleName { get; set; }
        public string HeadQLastName { get; set; }

        public string PremiseBName { get; set; }
        public string PremiseName { get; set; }
        public string PremiseAddress { get; set; }
        public string PremiseAddressNumberSufix { get; set; }
        public string PremiseCity { get; set; }
        public string PremiseState { get; set; }
        public string PremiseCountry { get; set; }
        public string PremiseZip { get; set; }
        public string PremiseEmail { get; set; }
        public string PremiseTelePhone { get; set; }
        public string PremiseQuadrant { get; set; }
        public string PremiseUnitType { get; set; }
        public string PremiseUnit { get; set; }
        public string PremiseAddressNumber { get; set; }
        public string PremiseStreetType { get; set; }
        public string PremiseWard { get; set; }
        public string PremiseANC { get; set; }
        public string PremiseZone { get; set; }
        public string PremiseSsl { get; set; }
        public string PremiseStreetName { get; set; }
        public string MailingBName { get; set; }
        public string MailingAddressNumberSufix { get; set; }
        public string MailingName { get; set; }
        public string MailingAddress { get; set; }
        public string MailingCity { get; set; }
        public string MailingState { get; set; }
        public string MailingCountry { get; set; }
        public string MailingZip { get; set; }
        public string MailingEmail { get; set; }
        public string MailingTelePhone { get; set; }
        public string MailingQuadrant { get; set; }
        public string MailingUnitType { get; set; }
        public string MailingUnit { get; set; }

        public string MailingStreetName { get; set; }
        public string MailingStreetNumber { get; set; }
        public string MailingStreetType { get; set; }

        public string MailingFirstName { get; set; }
        public string MailingMiddleName { get; set; }
        public string MailingLastName { get; set; }
        public string AddressNumber { get; set; }
        public string AgentBName { get; set; }
        public string AgentName { get; set; }
        public string AgentUnit { get; set; }
        public string AgentAddress { get; set; }
        public string AgentCity { get; set; }
        public string AgentState { get; set; }
        public string AgentCountry { get; set; }
        public string AgentZip { get; set; }
        public string AgentEmail { get; set; }
        public string AgentAddressNumber { get; set; }
        public string AgentStreetName { get; set; }
        public string AgentStreetType { get; set; }
        public string AgentQuadrant { get; set; }
        public string AgentTelePhone { get; set; }
        public string AgentFirstName { get; set; }
        public string AgentMiddleName { get; set; }
        public string AgentLastName { get; set; }
        public bool IsRaoFeeApplied { get; set; }
        public decimal ApplicationFee { get; set; }
        public decimal CategoryLicenseFee { get; set; }
        public decimal EndorsementFee { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TechFee { get; set; }
        public decimal TotalFee { get; set; }
        public bool Isehop { get; set; }
        public List<DetailedCategoryList> ServiceCheckList { get; set; }
        public List<BblServiceDocuments> DocumentList { get; set; }
        public string LicenseNumber { get; set; }
    }

    public class PaymentDetailsModel
    {
        public string PaymentId { get; set; }
        public string MasterId { get; set; }
        public string OrderNumber { get; set; }
        public string TranscationId { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime PaymentDate { get; set; }
        public string ApproveBy { get; set; }
        public string Description { get; set; }
        public string FullAddress { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public string CardName { get; set; }
        public string CardExpMonth { get; set; }
        public string CardExpYear { get; set; }
        public string CvvNumber { get; set; }
        public string TotalAmount { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ContactNumber1 { get; set; }
        public string ContactNumber2 { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string BusinessName { get; set; }
        public string SubmissionLicense { get; set; }
        public string PaymentMailAddress { get; set; }
        public string Signature { get; set; }
        public bool IsAggree { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal TotalApplicationFee { get; set; }
        public decimal TotalLicenseFee { get; set; }
        public decimal TotalEndosementFee { get; set; }
        public decimal TotalTechFee { get; set; }
        public string PaymentType { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactMiddleName { get; set; }
        public string ContactLastName { get; set; }
        public string StreetNumber { get; set; }

        // public string StreetName { get; set; }
        public string StreetType { get; set; }

        public string Quadrant { get; set; }
        public string UnitNumber { get; set; }
        public string eHopPdfName { get; set; }
        public string SubmissionOrderPdfName { get; set; }
        public bool ApplicationTransactionStatus { get; set; }
    }

    public class ReceiptModel
    {
        public string FullName { get; set; }
        public string TransactionSuccess { get; set; }
        public string InnerHtml { get; set; }

        //Transaction
        public string EmailAddress { get; set; }

        public string MasterID { get; set; }
        public string PaymentID { get; set; }
        public string SubNumber { get; set; }
        public decimal AmountCharged { get; set; }
        public string ReceiptDate { get; set; }
        public string CardNumber { get; set; }
        public string TransactionId { get; set; }
        public bool IsEhopAllowed { get; set; }
        public string DocType { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal TotalApplicationFee { get; set; }
        public decimal ApplicationFee { get; set; }
        public decimal TotalLicenseFee { get; set; }
        public decimal TotalEndosementFee { get; set; }
        public decimal TotalTechFee { get; set; }
        public string ExceptedFinalCheckingDate { get; set; }
        public decimal CategoryLicenseFee { get; set; }
        public decimal EndorsementFee { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TechFee { get; set; }
        public decimal TotalFee { get; set; }
        public bool Isehop { get; set; }
        public bool IsSubmissionCofo { get; set; }
        public bool IsSubmissionHop { get; set; }
        public bool IsSubmissioneHop { get; set; }
        public decimal ExtraAmount { get; set; }
        public string Extradays { get; set; }
        public List<DetailedCategoryList> ServiceCheckList { get; set; }
        public List<BblServiceDocuments> DocumentList { get; set; }
        public string GrandTotals { get; set; }
        public string TotalLicenseFees { get; set; }
        public string TotalTechFees { get; set; }
        public string ApplicationSubmit { get; set; }
        public string LicenseDuration { get; set; }
        public string PaymentMailId { get; set; }
        public string UserId { get; set; }
        public string SubmitTypeFrom { get; set; }
        public bool IsBackgroundInvestigation { get; set; }
        public string FullUserName { get; set; }
        public string SubmissionApplicationStatus { get; set; }
        public string ApplicationStatus { get; set; }
    }

    public class RenewModel
    {
        public string MasterId { get; set; }
        public string EntityId { get; set; }
        public string LicenseNumber { get; set; }
        public string UserId { get; set; }
        public decimal GrandTotalAmount { get; set; }
        public string CorpNumber { get; set; }
        public bool IsCorp { get; set; }
        public string CorpStatus { get; set; }
        public string TaxStatus { get; set; }
        public string TaxNumber { get; set; }
        public string SubCategoryName { get; set; }
        public string CategoryName { get; set; }
        public string ActivityName { get; set; }
        public string Endoresement { get; set; }
        public string CategoryId { get; set; }
        public string ActivityId { get; set; }
        public string SubCategoryID { get; set; }
        public decimal LicenseAmount { get; set; }
        public decimal EndorsementFee { get; set; }
        public decimal ApplicationFee { get; set; }
        public decimal TechFee { get; set; }
        public decimal RAOFee { get; set; }
        public bool IsCorpRegistration { get; set; }
        public bool IsCleanHands { get; set; }
        public List<BblServiceDocuments> DocumentList { get; set; }
        public List<DetailedCategoryList> ServiceCheckList { get; set; }
        public string BblAddress { get; set; }
        public string BblCity { get; set; }
        public string BblState { get; set; }
        public string BblZip { get; set; }
        public decimal LapsedAmount { get; set; }
        public decimal ExpiredAmount { get; set; }
        public string Extradays { get; set; }
        public string RenewalLicenseCode { get; set; }
        public string LrenNumber { get; set; }
        public string UserBblAssociateId { get; set; }
        public int LicenseDuration { get; set; }
        public string RenewStatus { get; set; }
        public string InitalDocumet { get; set; }
        public string CurrentDate { get; set; }
        public string NameofLicense { get; set; }
        public string BusinessOwnerName { get; set; }
        public string SubmissionLicense { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactMiddleName { get; set; }
        public string ContactLastName { get; set; }
        public string App_Type { get; set; }
        public bool IsBblCorp { get; set; }
        public string BillingAddress1 { get; set; }
        public string BillingAddress2 { get; set; }
        public string BillingAddress3 { get; set; }
    }

    public class RenewQuestionsList
    {
        public string MasterId { get; set; }
        public string CategoryName { get; set; }
        public string SubmissionLicense { get; set; }
        public int SubmissionCategoryID { get; set; }
        public string CategoryTypeID { get; set; }
        public string Endorsement { get; set; }
        public string License { get; set; }
        public string CategoryCode { get; set; }
        public string LicenseName { get; set; }
        public string Type { get; set; }
        public bool IsCorpRegistration { get; set; }
        public bool IsCleanHands { get; set; }
    }

    public class PrimaryPhysicallocation
    {
        // Primary Category
        public string PrimaryID { get; set; }

        public string ActivityID { get; set; }
        public string Description { get; set; }
        public string Endorsement { get; set; }
        public string CategoryCode { get; set; }
        public string UnitOne { get; set; }
        public string UnitTwo { get; set; }
        public string App_Type { get; set; }

        //Category Physical location
        public string BusinessMustbeinDC { get; set; }

        public string CofORequired { get; set; }
        public string HOP_EHOPAllowed { get; set; }
        public string ExemptfromAllFees { get; set; }
        public string LicenseType { get; set; }
        public Nullable<bool> Status { get; set; }
        public bool IsSecondaryLicenseCategory { get; set; }
        public bool IsBackgroundInvestigation { get; set; }
        public bool IsSubCategory { get; set; }
        public string UserQuestion1 { get; set; }
        public string UserQuestion2 { get; set; }

        // category fee
        public string OSub_Category { get; set; }

        public string OSub_Description { get; set; }
        public string Fee_Code { get; set; }
        public Nullable<int> Start { get; set; }
        public Nullable<int> End { get; set; }
        public Nullable<decimal> License_Fee { get; set; }
        public Nullable<int> Tier { get; set; }
        public string OldUnitOne { get; set; }
        public string OldUnitTwo { get; set; }
        public string OldCategoryName { get; set; }

        public bool IsPDFShow { get; set; }
    }

    public class DocumentCheck
    {
        public List<BblServiceDocuments> DocumentList { get; set; }
        public string DocType { get; set; }
        public string MasterId { get; set; }
    }

    public partial class MasterCategoryDocumentModel
    {
        public int MasterCategoryDocId { get; set; }
        public string CategoryName { get; set; }
        public string InitialLicense { get; set; }
        public string PostLicensure { get; set; }
        public string Renewal { get; set; }
        public string Agency { get; set; }
        public string Agency_FullName { get; set; }
        public string Div { get; set; }
        public string DivisionFullName { get; set; }
        public string SupportingDocuments { get; set; }
        public string ShortDocName { get; set; }
        public string Description { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<bool> IsPrimaryCategoryDoc { get; set; }
        public Nullable<bool> IsSecondaryLicenseDoc { get; set; }
    }

    public class OSub_Category_FeesEntity
    {
        public string OSub_Category { get; set; }
        public string OSub_Description { get; set; }
        public string Fee_Code { get; set; }
        public Nullable<int> Start { get; set; }
        public Nullable<int> End { get; set; }
        public Nullable<decimal> License_Fee { get; set; }
        public Nullable<int> Tier { get; set; }
        public string App_Type { get; set; }
        public Nullable<bool> Status { get; set; }
    }

    public class InvoiceModel
    {
        public string GF_DES { get; set; }
        public int InvocieId { get; set; }
        public decimal GF_FEE { get; set; }
        public decimal GF_UNIT { get; set; }
    }

    public class CategoryQuestionModel
    {
        public int Id1 { get; set; }
        public int Id2 { get; set; }
        public string PrimaryCategoryName { get; set; }
        public string Quantity1 { get; set; }
        public string Quantity2 { get; set; }
        public string UserQuestion1 { get; set; }
        public string UserQuestion2 { get; set; }
    }

    public class Submissiontransfer
    {
        public string MasterId { get; set; }
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
        public string LicenseNumber { get; set; }
        public string CreatedBy { get; set; }
        public string ReasonForTransfer { get; set; }
    }

    public class SecondaryCategory
    {
        public string SecondaryId { get; set; }
        public string PrimaryId { get; set; }
        public string SecondaryLicenseCategory { get; set; }
        public string UnitOne { get; set; }
        public string UnitTwo { get; set; }
        public string Endorsement { get; set; }
        public bool IsSubCategory { get; set; }
        public Nullable<bool> Status { get; set; }
        public bool IsPrimaryStatus { get; set; }
    }

    public class AccelaDocument
    {
        public string LicenseNumber { get; set; }
        public string MasterId { get; set; }
        public string MasterCategoryDocId { get; set; }
        public string FileName { get; set; }
    }

    public class CategoryLookup
    {
        public int LookUpId { get; set; }
        public string NewCategoryName { get; set; }
        public string ExistingCategory { get; set; }
    }

    public class BusinessStructureLookUp
    {
        public int LookUpBusinessStructureId { get; set; }
        public string BusinessStructure { get; set; }
        public bool IsManualAddress { get; set; }
    }

    public class SubmissionData
    {
        public string Status { get; set; }
        public string MasterId { get; set; }
        public string TradeName { get; set; }
        public string BusinessStructure { get; set; }
        public bool IsCorporationDivision { get; set; }
        public bool IsCoporateRegistration { get; set; }
        public bool IsResidentAgent { get; set; }
        public bool IsIndividual { get; set; }
        public bool IsBusinessMustbeinDc { get; set; }
        public string AppType { get; set; }
        public bool IsFEIN { get; set; }
        public string DocSubmType { get; set; }
        public string PaymentId { get; set; }
        public string PaymentStatus { get; set; }
        public string CorporationStatus { get; set; }
        public string BusinessOwnerName { get; set; }
        public string CurrentYear { get; set; }
        public string CreatedDate { get; set; }
        public string SelectedMailType { get; set; }
        public string PremisesAddress { get; set; }
        public string BusinessName { get; set; }
        public bool IsCategorySelfCertification { get; set; }
    }

    public class AddressType
    {
        public List<StreetDetails> StreetList { get; set; }
        public IEnumerable<MasterCountry> CountryList { get; set; }
    }

    public class TaxRevenueData
    {
        public string MasterId { get; set; }
        public string FullAddress { get; set; }
        public string TaxNumber { get; set; }
        public string UserId { get; set; }
        public string UserBblAssociateId { get; set; }
        public string EntityId { get; set; }
        public string CreatedDate { get; set; }
        public string CurrentYear { get; set; }
        public string BusinessType { get; set; }
        public string TaxType { get; set; }
        public string SubmissionLicense { get; set; }
        public string BusinessOwner { get; set; }
        public string tradeName { get; set; }
    }

    public class AddressDetails
    {
        public string AddressID { get; set; }
        public string FullAddress { get; set; }
        public string AddressNumber { get; set; }
        public string AddressNumberSufix { get; set; }
        public string StreetName { get; set; }
        public string StreetType { get; set; }
        public string Quadrant { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Xcoord { get; set; }
        public string Ycoord { get; set; }
        public string Anc { get; set; }
        public string Ward { get; set; }
        public string Cluster { get; set; }
        public string ZipCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Vote_Prcnct { get; set; }
        public string UnitType { get; set; }
        public string UnitNumber { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Zone { get; set; }
        public string SMD { get; set; }
        public string SSL { get; set; }
    }

    public class EhopData
    {
        public string MasterID { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public string OccupationType { get; set; }
        public string CreatedDate { get; set; }
        public string Square { get; set; }
        public string Lot { get; set; }
        public string PermitNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string CreatedBy { get; set; }
        public string UserName { get; set; }
        public string EhopSignature { get; set; }
        public string DCLogo { get; set; }
    }

    public class BasicBusinessLicense
    {
        public string MasterId { get; set; }
        public string DateIssued { get; set; }
        public string Category { get; set; }
        public string License { get; set; }
        public string LicensePeriod { get; set; }
        public string BillingName { get; set; }
        public string BillingAddress { get; set; }
        public string PremisesName { get; set; }
        public string PremisesAddress { get; set; }
        public string AgentName { get; set; }
        public string AgentAddress { get; set; }
        public string OwnerName { get; set; }
        public string CorpName { get; set; }
        public string TradeName { get; set; }
        public string CofoHopNumber { get; set; }
        public string Ssl { get; set; }
        public string Zone { get; set; }
        public string Ward { get; set; }
        public string Anc { get; set; }
        public string PermNo { get; set; }
        public string Units { get; set; }

        //public string Endoresement { get; set; }
        //public string CategoryLicense { get; set; }
        public List<CategoryDetails> CategoryDetailsList { get; set; }

        public string Signature { get; set; }
        public string PrimaryCategoryName { get; set; }
        public string BillingCompanyName { get; set; }
        public string BillingAddress1 { get; set; }
        public string BillingAddress2 { get; set; }
        public string BillingAddress3 { get; set; }
        public string PremisesBusinessName { get; set; }
        public string PremisesAddress1 { get; set; }
        public string AgentBusinessName { get; set; }
        public string AgentAddress1 { get; set; }
        public string AgentAddress2 { get; set; }
        public string AgentAddress3 { get; set; }
        public string ReceiptLogo { get; set; }
        public string ApplicationStatus { get; set; }
    }

    public class CorporationStatus
    {
        public string OriginalCorpStatus { get; set; }
        public string ChangeCorpStatus { get; set; }
    }

    public class RenewBasicBusinessLicense
    {
        public string MasterId { get; set; }
        public string DateIssued { get; set; }
        public string Category { get; set; }
        public string License { get; set; }
        public string LicensePeriod { get; set; }
        public string BillingName { get; set; }
        public string BillingAddress { get; set; }
        public string PremisesName { get; set; }
        public string PremisesAddress { get; set; }
        public string AgentName { get; set; }
        public string AgentAddress { get; set; }
        public string OwnerName { get; set; }
        public string CorpName { get; set; }
        public string TradeName { get; set; }
        public string CofoHopNumber { get; set; }
        public string Ssl { get; set; }
        public string Zone { get; set; }
        public string Ward { get; set; }
        public string Anc { get; set; }
        public string PermNo { get; set; }
        public string Units { get; set; }

        //public string Endoresement { get; set; }
        //public string CategoryLicense { get; set; }
        public List<CategoryDetails> CategoryDetailsList { get; set; }

        public string ABL { get; set; }
        public decimal LicenseFee { get; set; }
        public decimal RaoFee { get; set; }
        public decimal ApplicationFee { get; set; }
        public decimal EndoresementFee { get; set; }
        public decimal LateFee { get; set; }
        public decimal PenaltyFee { get; set; }
        public decimal EnhancedFee { get; set; }
        public string ApplicationExpiry { get; set; }
        public decimal TotalFee { get; set; }
        public string LapsedDate { get; set; }
        public decimal LapsedFee { get; set; }
        public string ExpiryDate { get; set; }
        public decimal ExpiryFee { get; set; }
        public string Signature { get; set; }
        public string PrimaryCategoryName { get; set; }
        public string LicenseNumber { get; set; }
        public string BillingCompanyName { get; set; }
        public string BillingAddress1 { get; set; }
        public string BillingAddress2 { get; set; }
        public string BillingAddress3 { get; set; }
        public string PremisesBusinessName { get; set; }
        public string PremisesAddress1 { get; set; }
        public string AgentBusinessName { get; set; }
        public string AgentAddress1 { get; set; }
        public string AgentAddress2 { get; set; }
        public string AgentAddress3 { get; set; }
        public string ReceiptLogo { get; set; }
    }

    public class SubmissionGeneratedDocumentData
    {
        public string Filename { get; set; }
        public Byte[] FileBytes { get; set; }
    }

    public class CorporationDetails
    {
        public string FileNumber { get; set; }
    }

    public class BusinessLicense
    {
        public string MasterId { get; set; }
        public string LicenseNumber { get; set; }
        public string LicesnseType { get; set; }
        public string Status { get; set; }
        public string PaymentTransaction { get; set; }
        public string PaymentDate { get; set; }
        public string GrandTotal { get; set; }
        public string FullName { get; set; }
        public string CreatedDate { get; set; }
    }

    public class RenewalLicense
    {
        public string MasterId { get; set; }
        public string LicenseNumber { get; set; }
        public string CategoryName { get; set; }
        public string PaymentDate { get; set; }
        public string FullName { get; set; }
        public string CreatedDate { get; set; }
    }

    public class DocumentData
    {
        public string ApplicationNo { get; set; }
        public string FileName { get; set; }
    }

    public class PaymentTransactionDetails
    {
        public string PaymentId { get; set; }
        public string PaymentTransactionID { get; set; }
        public string PaymentTransactionDate { get; set; }
        public List<PaymentAddressDetails> PaymentAddressDetails { get; set; }
    }

    public class PortaContentErrorsModel
    {
        public string MessageId { get; set; }
        public string MessageType { get; set; }
        public string ShortName { get; set; }
        public string ErrrorMessage { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

    public class SubmissionCounter
    {
        public int Counter { get; set; }
        public int Sequence { get; set; }
        public int PhysicalYear { get; set; }
        public string Type { get; set; }
    }

    public class MailTemplateModel
    {
        public string EmailTemplateId { get; set; }
        public string Type { get; set; }
        public string Subject { get; set; }
        public Nullable<int> MailSentFailCount { get; set; }
        public string MailContent { get; set; }
        public Nullable<bool> IsMailSent { get; set; }
        public DateTime? MailCreatedDate { get; set; }
        public DateTime? MailSentDate { get; set; }
        public string UserId { get; set; }
        public string CustomApplicationId { get; set; }
    }

    public class Userdetails
    {
        public string UserName { get; set; }
        public bool IsLogedIn { get; set; }
        public string SessionId { get; set; }
    }
}