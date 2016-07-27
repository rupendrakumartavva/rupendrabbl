using BusinessCenter.Data;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusinessCenter.Admin.Models
{
    public class ActivityModel
    {
        public string ActivityID { get; set; }

        // [Required(ErrorMessage = "Business Activity is required")]
        public string ActivityName { get; set; }

        public string APP_Type { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }

    public class SubmissiontoAccelaModel
    {
        public long SubmissiontoAccelaId { get; set; }
        public string LicenseNumber { get; set; }
        public Nullable<bool> ApplicationCompleted { get; set; }
        public Nullable<bool> ApplicationCreated { get; set; }
        public Nullable<bool> ApplicationFeeMatched { get; set; }
        public Nullable<bool> RenewalPaymentUpdated { get; set; }
        public Nullable<bool> RenewalFeeMatched { get; set; }
        public Nullable<bool> AllDocumentsUpdated { get; set; }
        public Nullable<bool> EhopCreated { get; set; }
        public string AccelaGeneratedID { get; set; }
        public Nullable<bool> ProcessCompleted { get; set; }
    }

    public class BusinessActivityViewModel
    {
        public ActivityModel Activity { get; set; }

        public List<ActivityModel> ActivityList { get; set; }

        public string CategoryType { get; set; }
    }

    public class PrimaryCategoryModel
    {
        // Primary Category
        public string PrimaryID { get; set; }

        public string ActivityID { get; set; }

        [Required(ErrorMessage = "Primary Category is required")]
        [Display(Name = "Primary Category")]
        //[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Primary Category accepts only alphabets")]
        //[StringLength(100, ErrorMessage = "Primary Category can be no longer than 100 characters", MinimumLength = 4)]
        // [RegularExpression(@"[a-zA-Z._^%$#!~@\s&:*()?/|,-]+$", ErrorMessage = "Primary Category accepts only alphabets and special characters")]
        [MinLength(4, ErrorMessage = "Primary Category must be at least 4 characters long")]
        [MaxLength(100, ErrorMessage = "Primary Category can be no longer than 100 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Endorsement is required")]
        [Display(Name = "Endorsement")]
        //  [RegularExpression(@"[a-zA-Z._^%$#!~@\s&:*()?/,-]+$", ErrorMessage = "Endorsement accepts only alphabets and special characters")]
        [MinLength(4, ErrorMessage = "Endorsement must be at least 4 characters long")]
        [MaxLength(100, ErrorMessage = "Endorsement  can be no longer than 240 characters")]
        public string Endorsement { get; set; }

        [Required(ErrorMessage = "Category Id is required")]
        [Display(Name = "Category Code")]

        //[RegularExpression("([0-9][0-9\\s]*)", ErrorMessage = "Category Id accepts only numbers ")]
        [RegularExpression("[0-9 ]+", ErrorMessage = "Category Id accepts only numbers")]

        [MinLength(4, ErrorMessage = "Category Id must be 4 digits")]
        [MaxLength(4, ErrorMessage = "Category Id must be 4 digits")]
        public string CategoryCode { get; set; }

        // [StringLength(100, ErrorMessage = "Primary Units A  can be no longer than 100 characters", MinimumLength = 4)]
        //  [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Primary Units A  accepts only alphabets")]
        [MinLength(4, ErrorMessage = "Primary Units A must be at least 4 characters long")]
        [MaxLength(25, ErrorMessage = "Primary Units A  can be no longer than 25 characters")]
        public string UnitOne { get; set; }

        // [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Primary Units B  accepts only alphabets")]
        [MinLength(4, ErrorMessage = "Primary Units B must be at least 4 characters long")]
        [MaxLength(25, ErrorMessage = "Primary Units B  can be no longer than 25 characters")]
        public string UnitTwo { get; set; }

        [Required(ErrorMessage = "Application Type is required")]
        [Display(Name = "Application Type")]
        public string App_Type { get; set; }

        //Category Physical location
        //[Required(ErrorMessage = "Please Select Business Must be in DC?")]
        public string BusinessMustbeinDC { get; set; }

        //[Required(ErrorMessage = "Please Select CofO Required?")]
        public string CofORequired { get; set; }

        //[Required(ErrorMessage = "Please Select HOP/EHOP Allowed?")]
        public string HOP_EHOPAllowed { get; set; }

        //[Required(ErrorMessage = "Please Select Exempt from All Fees?")]
        public string ExemptfromAllFees { get; set; }

        public string LicenseType { get; set; }
        public bool? Status { get; set; }

        public bool IsSecondaryLicenseCategory { get; set; }

        public bool IsSubCategory { get; set; }

        // [Required(ErrorMessage = "Please Select Secondary License Category Allowed?")]
        //[Required(ErrorMessage = "Please answer all Questions")]
        public string SecondaryLicenseCategory { get; set; }

        // [Required(ErrorMessage = "Sub category is required")]
        //[Required(ErrorMessage = "Please answer all Questions")]
        public string SubCategory { get; set; }

        public string IsBackgroundInvestigation { get; set; }

        public string IsPDFShow { get; set; }
        public string PId { get; set; }

        public int Id1 { get; set; }
        public int Id2 { get; set; }
        public string PrimaryCategoryName { get; set; }
        public string Quantity1 { get; set; }
        public string Quantity2 { get; set; }

        //  [RegularExpression(@"[a-zA-Z._^%$#!~@\s&?,-]+$", ErrorMessage = "Primary Units A Question accepts only alphabets and special characters")]
        [MinLength(10, ErrorMessage = "Primary Units A Question must be at least 10 characters long")]
        [MaxLength(240, ErrorMessage = "Primary Units A Question  can be no longer than 240 characters")]
        public string UserQuestion1 { get; set; }

        //  [RegularExpression(@"[a-zA-Z._^%$#!~@\s&?,-]+$", ErrorMessage = "Primary Units B Question accepts only alphabets and special characters")]
        [MinLength(10, ErrorMessage = "Primary Units B Question must be at least 10 characters long")]
        [MaxLength(240, ErrorMessage = "Primary Units B Question  can be no longer than 240 characters")]
        public string UserQuestion2 { get; set; }

        // category fee
        public string OSub_Category { get; set; }

        public string OSub_Description { get; set; }

        [Required(ErrorMessage = "Fee Code is required")]
        [Display(Name = "Fee Code")]
        public string Fee_Code { get; set; }

        public int Start { get; set; }

        public int End { get; set; }

        [Display(Name = "License Fee")]
        [RegularExpression(@"^\d+.\d{0,2}$", ErrorMessage = "License Fee accepts only numbers and decimals upto two(2) places")]
        //[RegularExpression(@"^\d+.\d{0,2}$", ErrorMessage = "License Fee accepts only numbers and upto two(2) Decimal places.")]
        //[StringLength(10, ErrorMessage = "License Fee must not be greater than 10 digits")]
        [Required(ErrorMessage = "License Fee is required")]
        [Range(0, 9999999999.99, ErrorMessage = "License Fee must not be greater than 10 digits")]
        //[DataType(DataType.Currency)]
        public Nullable<decimal> License_Fee { get; set; }

        [RegularExpression("([0-9][0-9]*)", ErrorMessage = "Tier accepts only numbers ")]
        //[StringLength(10, ErrorMessage = "Tier must not be greater than 100")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Tier cannot be less than one(1)")]
        public Nullable<int> Tier { get; set; }
    }

    public class SecondaryModel
    {
        public string Endorsement { get; set; }
        public string PrimaryId { get; set; }
        public string SecondaryId { get; set; }

        [Required(ErrorMessage = "SecondaryLicenseCategory field is required")]
        public string SecondaryLicenseCategory { get; set; }

        public string UnitOne { get; set; }
        public string UnitTwo { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<bool> IsSuperSubCategoryAllow { get; set; }
        public bool IsPrimaryStatus { get; set; }
    }

    public class SecondaryViewModel
    {
        public SecondaryModel Secondary { get; set; }
        public List<SecondaryModel> SecondaryList { get; set; }
    }

    public class SubCategoryModel
    {
        public string SubCatID { get; set; }
        public string SubCategoryName { get; set; }
        public string CustomCategoryName { get; set; }
        public Nullable<bool> Status { get; set; }
    }

    public class SubCategoryViewModel
    {
        public SubCategoryModel SubCategory { get; set; }
        public List<SubCategoryModel> SubCategoryList { get; set; }
    }

    public enum ApplicationType
    {
        Business,
        Individual
    }

    public enum FeeCode
    {
        C, H, HT, T, TA, S
    }

    public class DocumentsModel
    {
        public string MasterId { get; set; }
        public string LicenseNumber { get; set; }
        public string UserId { get; set; }

        public int MasterCategoryDocId { get; set; }

        [Required(ErrorMessage = "Category Name is required")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Initial License is required")]
        public string InitialLicense { get; set; }

        [Required(ErrorMessage = "Post Licensure is required")]
        public string PostLicensure { get; set; }

        [Required(ErrorMessage = "Renewal is required")]
        public string Renewal { get; set; }

        [Required(ErrorMessage = "The Agency field is required.")]
        //  [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Agency accepts only alphabets")]
        [MinLength(2, ErrorMessage = "Agency must be at least 2 characters long")]
        [MaxLength(50, ErrorMessage = "Agency can be no longer than 50 characters")]
        public string Agency { get; set; }

        [Required(ErrorMessage = "The Agency Full Name field is required.")]
        [MinLength(2, ErrorMessage = "Agency Full Name must be at least 2 characters long")]
        [MaxLength(240, ErrorMessage = "Agency Full Name can be no longer than 240 characters")]
        public string Agency_FullName { get; set; }

        [Required(ErrorMessage = "The Division field is required.")]
        //  [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Division accepts only alphabets")]
        [MinLength(2, ErrorMessage = "Division must be at least 2 characters long")]
        [MaxLength(50, ErrorMessage = "Division can be no longer than 50 characters")]
        public string Div { get; set; }

        [Required(ErrorMessage = "The Division Full Name field is required.")]
        [MinLength(2, ErrorMessage = "Division Full Name must be at least 2 characters long")]
        [MaxLength(240, ErrorMessage = "Division Full Name can be no longer than 240 characters")]
        public string DivisionFullName { get; set; }

        [Required(ErrorMessage = "The Supporting Documents field is required.")]
        [MinLength(2, ErrorMessage = "Supporting Documents must be at least 2 characters long")]
        [MaxLength(240, ErrorMessage = "Supporting Documents can be no longer than 240 characters")]
        public string SupportingDocuments { get; set; }

        [Required(ErrorMessage = "The Short Document Name field is required.")]
        [MinLength(2, ErrorMessage = "Short Document Name must be at least 2 characters long")]
        [MaxLength(100, ErrorMessage = "Short Document Name can be no longer than 100 characters")]
        public string ShortDocName { get; set; }

        [Required(ErrorMessage = "The Description field is required.")]
        [DataType(DataType.MultilineText)]
        [MinLength(2, ErrorMessage = "Description must be at least 2 characters long")]
        [MaxLength(1500, ErrorMessage = "Description can be no longer than 1500 characters")]
        public string Description { get; set; }

        public Nullable<bool> Status { get; set; }
        public string ActivityID { get; set; }

        public string PrimaryID { get; set; }

        public string SecondaryID { get; set; }

        [Required(ErrorMessage = "Allow for Primary License is required.")]
        public Nullable<bool> IsPrimaryCategoryDoc { get; set; }

        [Required(ErrorMessage = "Allow for Secondary License is required.")]
        public Nullable<bool> IsSecondaryLicenseDoc { get; set; }

        // public List<SubmissionVerfication> BblDocumentsList { get; set; }

        public SubmissionVerfication BblDocumentsList { get; set; }

        public ServiceChecklist Servicechecklist { get; set; }
    }

    public class DocumentsViewModel
    {
        public DocumentsModel Document { get; set; }
        public List<DocumentsModel> DocumentList { get; set; }
    }

    public class CategoryFeeModel
    {
        public string OSub_Category { get; set; }

        //[Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        //[StringLength(240, ErrorMessage = "Category should be less than 240 characters", MinimumLength = 3)]
        public string OSub_Description { get; set; }

        [Required(ErrorMessage = "Fee Code is required")]
        [Display(Name = "Fee Code")]
        //[StringLength(240, ErrorMessage = "Fee Code should be less than 240 characters", MinimumLength = 3)]
        public string Fee_Code { get; set; }

        [Required(ErrorMessage = "Start Range is required")]
        [Display(Name = "Start")]
        // [Range(0, int.MaxValue, ErrorMessage = "Start field accepts only Numbers.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Start field accepts only Numbers.")]
        //[StringLength(8, ErrorMessage = "Start should be less than 8 characters", MinimumLength = 3)]
        public Nullable<int> Start { get; set; }

        [Required(ErrorMessage = "End Range is required")]
        [Display(Name = "End")]
        [RegularExpression(@"^\d+$", ErrorMessage = "End field accepts only Numbers.")]
        //  [Range(0, int.MaxValue, ErrorMessage = "End field accepts only Numbers.")]

        //   [Range(0, 250, ErrorMessage = "Please enter a number between 0 and 250.")]
        //  [DataAnnotationsExtensions.Integer(ErrorMessage = "Please enter a valid number.")]
        //[StringLength(8, ErrorMessage = "End should be less than 8 characters", MinimumLength = 3)]
        public Nullable<int> End { get; set; }

        [Required(ErrorMessage = "License Fee is required")]
        [Display(Name = "License Fee")]
        //[RegularExpression(@"^\d+.\d{0,2}$", ErrorMessage = "License Fee accepts only numbers and upto two(2) Decimal places.")]
        //[StringLength(20, ErrorMessage = "License Fee should be less than 10 characters", MinimumLength = 3)]
        [RegularExpression(@"^\d+.\d{0,2}$", ErrorMessage = "License Fee accepts only numbers and upto two(2) Decimal places.")]
        public Nullable<decimal> License_Fee { get; set; }

        //[Required(ErrorMessage = "Tier is required")]
        [Display(Name = "Tier")]
        //[StringLength(8, ErrorMessage = "Tier should be less than 8 characters", MinimumLength = 3)]
        [RegularExpression(@"^\d+$", ErrorMessage = "Tier field accepts only Numbers.")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Tier cannot be less than one(1)")]
        public Nullable<int> Tier { get; set; }

        // [Required(ErrorMessage = "Application Type is required")]
        [Display(Name = "Application Type")]
        public string App_Type { get; set; }

        public string UnitOne { get; set; }

        public Nullable<bool> Status { get; set; }
    }

    public class CategoryFeeViewModel
    {
        public CategoryFeeModel CategoryFee { get; set; }
        public List<CategoryFeeModel> CategoryFeesList { get; set; }
    }

    public class SubmissionMasterViewModel
    {
        public SubmissionMasterMvcModel SubmissionMaster { get; set; }
        public List<SubmissionMasterMvcModel> SubmissionMasterList { get; set; }
    }

    public class SubmissionMasterMvcModel
    {
        public string MasterId { get; set; }
        public string SubmissionLicense { get; set; }
        public string UserID { get; set; }
        public string ActivityID { get; set; }
        public Nullable<decimal> ApplicationFee { get; set; }
        public Nullable<decimal> RAOFee { get; set; }
        public Nullable<bool> IseHOP { get; set; }
        public Nullable<decimal> eHOP { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> ExpirationDate { get; set; }
        public string ApprovedBy { get; set; }
        public string Description { get; set; }
        public string App_Type { get; set; }
        public string DocSubmType { get; set; }
        public string FEIN { get; set; }
        public Nullable<bool> IsFEIN { get; set; }
        public Nullable<bool> IsBusinessMustbeinDC { get; set; }
        public Nullable<bool> IsHomeBased { get; set; }
        public Nullable<bool> IsCofo { get; set; }
        public Nullable<bool> IsPhysicalLocationVerify { get; set; }
        public Nullable<decimal> GrandTotal { get; set; }
        public Nullable<bool> isCorporationDivision { get; set; }
        public string BusinessStructure { get; set; }
        public string TradeName { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> Updatedate { get; set; }
        public string UserSelectMailAddressType { get; set; }
    }

    public class PrimaryLicenseCategoryModel
    {
        // Primary Category
        public string PrimaryID { get; set; }

        public string ActivityID { get; set; }

        [Required(ErrorMessage = "Primary Category is required")]
        [Display(Name = "Primary Category")]
        //[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Primary Category accepts only alphabets")]
        //[StringLength(100, ErrorMessage = "Primary Category can be no longer than 100 characters", MinimumLength = 4)]
        // [RegularExpression(@"[a-zA-Z._^%$#!~@\s&,-]+$", ErrorMessage = "Primary Category accepts only alphabets and special characters")]
        [MinLength(4, ErrorMessage = "Primary Category must be at least 4 characters long")]
        [MaxLength(100, ErrorMessage = "Primary Category can be no longer than 100 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Endorsement is required")]
        [Display(Name = "Endorsement")]
        // [RegularExpression(@"[a-zA-Z._^%$#!~@\s&,-]+$", ErrorMessage = "Endorsement accepts only alphabets and special characters")]
        [MinLength(4, ErrorMessage = "Endorsement must be at least 4 characters long")]
        [MaxLength(100, ErrorMessage = "Endorsement  can be no longer than 240 characters")]
        public string Endorsement { get; set; }

        [Required(ErrorMessage = "Category Id is required")]
        [Display(Name = "Category Code")]
        [RegularExpression("([0-9][0-9]*)", ErrorMessage = "Category Id accepts only numbers ")]
        //[StringLength(10, ErrorMessage = "Category Code must be less than 10 digits")]
        [MinLength(4, ErrorMessage = "Category Id must be atleast 4 digits")]
        [MaxLength(10, ErrorMessage = "Category Id must be less than 10 digits")]
        public string CategoryCode { get; set; }

        // [StringLength(100, ErrorMessage = "Primary Units A  can be no longer than 100 characters", MinimumLength = 4)]
        // [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Primary Units A  accepts only alphabets")]
        [MinLength(4, ErrorMessage = "Primary Units A must be at least 4 characters long")]
        [MaxLength(25, ErrorMessage = "Primary Units A  can be no longer than 25 characters")]
        public string UnitOne { get; set; }

        //[StringLength(100, ErrorMessage = "Primary Units B  can be no longer than 100 characters", MinimumLength = 4)]
        // [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Primary Units B  accepts only alphabets")]
        [MinLength(4, ErrorMessage = "Primary Units B must be at least 4 characters long")]
        [MaxLength(25, ErrorMessage = "Primary Units B  can be no longer than 25 characters")]
        public string UnitTwo { get; set; }

        [Required(ErrorMessage = "Application Type is required")]
        [Display(Name = "Application Type")]
        public string App_Type { get; set; }

        //Category Physical location
        [Required(ErrorMessage = "Please Select Business Must be in DC?")]
        public string BusinessMustbeinDC { get; set; }

        [Required(ErrorMessage = "Please Select CofO Required?")]
        public string CofORequired { get; set; }

        [Required(ErrorMessage = "Please Select HOP/EHOP Allowed?")]
        public string HOP_EHOPAllowed { get; set; }

        [Required(ErrorMessage = "Please Select Exempt from All Fees?")]
        public string ExemptfromAllFees { get; set; }

        public string LicenseType { get; set; }
        public bool? Status { get; set; }

        [Required(ErrorMessage = "Please Select Secondary License Category Allowed?")]
        public bool IsSecondaryLicenseCategory { get; set; }

        [Required(ErrorMessage = "Sub category is required")]
        public bool IsSubCategory { get; set; }

        public string PId { get; set; }
        public bool IsBackgroundInvestigation { get; set; }

        public bool IsPDFShow { get; set; }

        public int Id1 { get; set; }
        public int Id2 { get; set; }
        public string PrimaryCategoryName { get; set; }
        public string Quantity1 { get; set; }
        public string Quantity2 { get; set; }

        //  [RegularExpression(@"[a-zA-Z._^%$#!~@\s&?,-]+$", ErrorMessage = "Primary Units A Question accepts only alphabets and special characters")]
        [MinLength(10, ErrorMessage = "Primary Units A Question must be at least 10 characters long")]
        [MaxLength(240, ErrorMessage = "Primary Units A Question  can be no longer than 240 characters")]
        public string UserQuestion1 { get; set; }

        // [RegularExpression(@"[a-zA-Z._^%$#!~@\s&?,-]+$", ErrorMessage = "Primary Units B Question accepts only alphabets and special characters")]
        [MinLength(10, ErrorMessage = "Primary Units B Question must be at least 10 characters long")]
        [MaxLength(240, ErrorMessage = "Primary Units B Question  can be no longer than 240 characters")]
        public string UserQuestion2 { get; set; }

        // category fee
        public string OSub_Category { get; set; }

        public string OSub_Description { get; set; }

        [Required(ErrorMessage = "Fee Code is required")]
        [Display(Name = "Fee Code")]
        public string Fee_Code { get; set; }

        public Nullable<int> Tier { get; set; }
    }

    public class BusinessLicenseCompare
    {
        public BblLicenseView BusinessView { get; set; }
        public BusinessLicenseFromTables BusinessApplication { get; set; }
    }

    public class BusinessLicenseFromTables
    {
        public string ApplicationUniqueID { get; set; }
        public string ApplicantName { get; set; }
        public string FullName { get; set; }
        public string OnlineAppLicenseStatus { get; set; }
        public string ApplicationLicenseNo { get; set; }
        public string ApplicationType { get; set; }
        public string App_Type { get; set; }
        public int LicensePeriod { get; set; }
        public decimal ApplicationFee { get; set; }
        public decimal EndorsementFee { get; set; }
        public decimal LicenseFee { get; set; }
        public decimal RAOFee { get; set; }
        public decimal ESFFee { get; set; }

        public decimal EHOPFees { get; set; }
        public decimal TotalAmount { get; set; }

        public string PaymentTransactionID { get; set; }
        public string PaymentTransactionDate { get; set; }
        public string CHSelfCertificateSignature { get; set; }

        public string CHSelfCertificateDate { get; set; }
        public string CHSelfCertificateType { get; set; }
        public string SelfCertificateOneFamily { get; set; }
        public string CertificateofOccupancyNumber { get; set; }
        // public string COIssueDate { get; set; }

        public string HomeOccupationNumber { get; set; }
        //public string HOIssueDate { get; set; }

        public string EHOPNumber { get; set; }
        public string EHOPIssueDate { get; set; }
        public string SEHOPAttestedBy { get; set; }

        public string EHOPOccupationType { get; set; }
        public string EHOPIssuanceStatus { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string OrganizationName { get; set; }
        public string FullAddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string CountryRegion { get; set; }
        public string Email { get; set; }
        public string BusinessOrganization { get; set; }

        public string TradeNameIfapplicable { get; set; }

        public string TaxIDNumber { get; set; }
        public string FEINSSN { get; set; }
        public string PrimStreetNumber { get; set; }

        public string PrimStreetName { get; set; }
        public string StreetType { get; set; }
        public string Quadrant { get; set; }

        public string UnitType { get; set; }
        public string SuiteUnitNumber { get; set; }
        public string PCity { get; set; }
        public string PState { get; set; }

        public string Country { get; set; }
        public string PZip { get; set; }
        public string Phone { get; set; }

        public string PremiseAddressVerified { get; set; }
        public string ParcelPremiseSuffix { get; set; }
        public string ParcelPremiseWard { get; set; }
        public string ParcelPremiseANC { get; set; }
        public string ParcelPremiseZone { get; set; }
        public string ParcelPremiseSsl { get; set; }
        public string BillingOrgBussName { get; set; }
        public string BillingContactFirstName { get; set; }
        public string BillingContactMiddleName { get; set; }
        public string BillingContactLastName { get; set; }
        public string BillingStreetNumber { get; set; }
        public string BillingStreetName { get; set; }
        public string BillingStreetType { get; set; }
        public string BillingQuadrant { get; set; }
        public string BillingSuiteUnitNumber { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingCountry { get; set; }
        public string BillingZip { get; set; }
        public string BillingPhone { get; set; }
        public string BillingEmail { get; set; }
        public string AgentOrgBussName { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactMiddleName { get; set; }
        public string ContactLastName { get; set; }
        public string StreetFullAddress { get; set; }
        public string StreetName { get; set; }
        public string Street_Type { get; set; }
        public string AgentQuadrant { get; set; }
        public string AgentUnitNumber { get; set; }
        public string ContactAgent_City { get; set; }
        public string ContactAgent_State { get; set; }
        public string ContactAgent_Country { get; set; }
        public string ContactAgent_ZipCode { get; set; }
        public string CorpTelephone { get; set; }
        public string ContactAgent_Email { get; set; }
        public string FileNumber { get; set; }
        public string CompanyName { get; set; }
        public string EmployeeName { get; set; }
        public string CompanyBussLicNumber { get; set; }
        public string DateofBirth { get; set; }
        public string PlaceofBirth { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string HairColor { get; set; }
        public string EyesColor { get; set; }
        public string DriversLicense { get; set; }
        public string StateofLicense { get; set; }
        public string IndividualExpirationDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpDatedDate { get; set; }
    }

    public class PortaContentModel
    {
        public string MessageId { get; set; }

        [Required(ErrorMessage = "Message Type is required")]
        public string MessageType { get; set; }

        [Required(ErrorMessage = "Short Name is required")]
        public string ShortName { get; set; }

        [Required(ErrorMessage = "Errror Message is required")]
        [DataType(DataType.MultilineText)]
        public string ErrrorMessage { get; set; }

        public Nullable<bool> IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

    public class TransferModel
    {
        public string SubmissionLicense { get; set; }

        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
        public string DateOfTransfer { get; set; }
        public string CreatedBy { get; set; }

        public string ReasonForTransfer { get; set; }
    }
}