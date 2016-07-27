using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Common
{
    public static class GenericEnums
    {
        /// <summary>
        /// This method is used to get the enum value .
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumDescription(Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
        /// <summary>
        ///Declare Search type Enums
        /// </summary>
        public enum SearchType
        {
            BusinessName = 1, LicenseNumber = 2, FirstName = 3, LastName = 4
        }
        /// <summary>
        ///Declare DocumentType Enums
        /// </summary>
        public enum DocumentTypes
        {
            [Description("ON")]
            OnLine = 1,
            [Description("IN")]
            InPerson = 2,
            [Description("CLEANHANDSCERT")]
            CleanHandsDoc=3,
            [Description("EHOP")]
            eHOP=4,
            [Description("HOP")]
            HOP=5,
            [Description("COFO")]
            COFO=6,
            [Description("NODOCS")]
            NoDocs=7,
            [Description("NO DOCS")]
            NO=8,
            [Description("NODOCUMENT")]
            NoDocument=9,
            [Description("N")]
            N=10,
            [Description("CORPRATION")]
            Corpration=11,
            [Description("NOPO")]
            NoPo = 12
        }
        /// <summary>
        ///Declare ApplicationSubmissionType Enums
        /// </summary>
        public enum ApplicationSubmissionType
        {
            [Description("S")]
            FromSubmission = 1,
            [Description("A")]
            FromAssociation = 2
        }
        /// <summary>
        ///Declare ApplicationValidateStatus enums
        /// </summary>
        public enum ApplicationValidateStatus
        {
            Draft,
            Underreview,
            Active,
            Expired,
            Lapsed,
            [Description("UnderReview")]
            UnderreviewSpace,
            ExpiringSoon
        }
        /// <summary>
        /// Declare ApplicationStatus Enums
        /// </summary>
        public enum ApplicationStatus
        {
            [Description("Draft Application In Progress")]
            Draft = 1,
            [Description("Under Review")]
            Underreview = 2,
            [Description("ACTIVE")]
            Active = 3,
            [Description("EXPIRED")]
            Expired = 4,
            [Description("Expiring Soon")]
            ExpiringSoon = 5,
            [Description("LAPSED")]
            Lapsed = 6,
            [Description("Unable to Be Renewed")]
            Renewalnotallowed = 7
        }
        /// <summary>
        /// Declare GeneralTypes Enums
        /// </summary>
        public enum GenaralTypes
        {
            [Description("EHOP")]
            Ehop = 1,
            [Description("Multiple")]
            Multiple = 2,
            [Description("RENEW")]
            Renew = 3,
            [Description("RENEWAL")]
            Renewal=4,
            [Description("I")]
            IndividualApp=5,
            [Description("B")]
            BusinessApp = 6,
            [Description("0")]
            InPersonNumber = 7,
            [Description("9")]
            OnlineNumber =8,
            [Description("IAPP")]
            FinalSubmissionStartsWithIndividual = 9,
            [Description("LAPP")]
            FinalSubmissionStartsWithBusiness = 10,
             [Description("DAPP")]
            FirstSubmissionStartsWith = 11,
            [Description("PRIMSES ADDRESS")]
            PrimsessAddress=12,
            [Description("NODATA")]
            NoData=13,
            [Description("CORPREG")]
            CorpRegistration=14,
            [Description("FALSE")]
            False=15,
            [Description("CORPAGENT")]
            CorpAgent=16,
            [Description("Y-CORPREG")]
            YCorpRegistration=17,
            [Description("Y-CORPAGENT")]
            YCorpAgent = 18,
            [Description("N-CORPAGENT")]
            NCorpAgent = 19,
            [Description("NEWMAIL")]
            NewMail=20,
            [Description("N-CORPREG")]
            NCorpRegistration = 21,
            [Description("TRUE")]
            True = 22,
            [Description("HQ ADDRESS")]
            HqAdderss=23,
            [Description("ACTIVE")]
            Active=24,
            [Description("N-CORPAGENTEMPTY")]
            NCorpAgentEmpty=25,
            [Description("ENDORSEMENT")]
            ENDORSEMENT=26,
            [Description("APPLICATION")]
            APPLICATION=27,
            [Description("RAO FEE")]
            RAOFEE=28,
            [Description("ALL")]
            ALL=29,
            [Description("BUSINESSNAME")]
            BUSINESSNAME=30,
            [Description("LICENSENUMBER")]
            LICENSENUMBER=31,
            [Description("FIRSTNAME")]
            FIRSTNAME=32,
            [Description("LASTNAME")]
            LASTNAME=33,
            [Description("ABRA")]
            ABRA=34,
            [Description("BBL")]
            BBL=35,
            [Description("CBE")]
            CBE=36,
            [Description("CORP")]
            CORP=37,
            [Description("OPLA")]
            OPLA=38,
            [Description("BILLED")]
            BILLED = 39,
              [Description("90")]
            MailalerttoExpiryDate = 40
        }
        /// <summary>
        /// Declare CategoryType Enums
        /// </summary>
        public enum CategoryTypes
        {
            [Description("BusinessActivity")]
            BusinessActivity = 1,
            [Description("PRIMARY")]
            PrimaryCategory = 2,
            [Description("Multiple")]
            Multiple = 3,
            [Description("RENEW")]
            Renew = 4,
            [Description("RENEWAL")]
            Renewal =5,
            [Description("SECONDARYCATEGORY")]
            SecondaryCategory = 6,
            [Description("SUBCATEGORY")]
            SubCategory = 7,
            [Description("PRIMARY")]
            Primary = 8
        }
        /// <summary>
        /// Declare CategoryList Enums
        /// </summary>
        public enum CategoryList
        {
            PRIMARY = 1,
            SECONDARYCATEGORY = 2,
            SUBCATEGORY = 3
        }
        /// <summary>
        /// Declare CategoryQuestions Enums
        /// </summary>
        public enum CategoryQuestions
        {
            [Description("Would you like a two (2) or four (4) year license?")]
            DurationOfLicense,
            [Description("Will this business be located in the District of Columbia?")]
            LocationMustBeInDc,
            [Description("Will this Business be Home based?")]
            IsHomeBased,
            [Description("Do you have a Home Occupancy Permit (HOP) from Office of Zoning?")]
            IsEhopAllowed,
            [Description("Is this business already registered with DCRA’s Corporations Division?")]
            IsDcraRegisteredInCorporation,
            [Description("What is your Business Structure ?")]
            BusinessStructure,
            [Description("What is the Trade Name?")]
            TradeName,
            [Description("Which Tax Identification Number is associated with your business: Federal Employer Identification Number (FEIN) or  Social Security Number (SSN)?")]
            TaxAndRevenueValidate,
            [Description("Home Occupancy Permit (HOP) Document")]
            HopDocument,
            [Description("Certificate of Occupancy (COfO) Document")]
            CofoDocument,
            [Description("Please enter the full name of the business owner (if applying for a business license) or full employee name( if applying for an individual license) as it will appear on the business license.  Cannot be a company or trade name, must be an individual.")]
            // [Description("Please enter the full name of the business owner:")]
            BusinessOwner,
             [Description("Please enter the full employee name:")]
            IndividualBusinessOwner
        }
        /// <summary>
        /// Decalre BusinessStructureOptions Enums
        /// </summary>
        public enum BusinessStructureOptions
        {
            [Description("Select One")]
            Select,
            [Description("Corporation (For Profit)")]
            CorpProfit,
            [Description("Corporation (Non-Profit)")]
            CorpNonProfit,
            [Description("Limited Liability Company (LLC)")]
            Llc,
            [Description("Limited Partnership (LP)")]
            Lp,
            [Description("Limited Liability Partnership (LLP)")]
            Llp,
            [Description("General Cooperative Association")]
            Gca,
            [Description("Limited Cooperative Association")]
            Lca,
            [Description("Statutory Trust")]
            StatutoryTrust,
            [Description("Sole Proprietorship")]
            SoleProprietorship,
            [Description("General Partnership")]
            GeneralPartnership,
            [Description("Joint Venture")]
            JointVenture
        }
        /// <summary>
        /// Declare CategoryAnswers Enums
        /// </summary>
        public enum CategoryAnswers
        {
            [Description("Textbox")]
            Textbox,
            [Description("RadioButton")]
            RadioButton,
            [Description("Dropdown")]
            Dropdown,
            [Description("Two (2) year")]
            TwoYear,
            [Description("Four (4) year")]
            FourYear,
            [Description("NA")]
            NA,
            [Description("YES")]
            YES,
            [Description("NO")]
            NO,
            [Description("Primary")]
            Primary,
            [Description("Secondary")]
            Secondary,
            [Description("I")]
            I,
            [Description("SOLICITOR")]
            SOLICITOR,
            [Description("FEIN")]
            FEIN,
            [Description("SSN")]
            SSN
        }
        /// <summary>
        /// Declare Category Enums
        /// </summary>
        public enum Category
        {
            [Description("APARTMENT")]
            APARTMENT,
            [Description("ONE FAMILY RENTAL")]
            FamilyRental,
            [Description("KITCHENS")]
            KITCHENS,
            [Description("CHARITABLE EXEMPT")]
            CharitableExempt,
            [Description("GENERAL BUSINESS LICENSES")]
            GeneralBusiness
        }
        /// <summary>
        /// Declare SubmissionGenerationDocumentType Enums
        /// </summary>
        public enum SubmissionGenerationDocumentType
        {

            [Description("EHOP")]
            eHOP,
            [Description("NODOCS")]
            Nodocs,
            [Description("RenewalNODOCS")]
            RenewalNodocs,
            [Description("_Receipt")]
            SubmitReceipt,
            [Description("SUBREC")]
            SubRec,
            [Description("SUBRENEW")]
            SubRenew,
            [Description("_RenewalReceipt")]
            RenewalReceipt,
             [Description("RENEW")]
             Renew,
             [Description("_REN")]
             RenewDocFileExtension,
             [Description("_Sub")]
             SubDocFileExtension
          
        }
        /// <summary>
        /// Declare DocumentType Enums
        /// </summary>
        public enum DocumentType
        {

            [Description("pdf")]
            Pdf,
            [Description("doc")]
            Doc,
            [Description("txt")]
            Txt

        }
        /// <summary>
        /// Declare SupportingDocumentList Enums
        /// </summary>
        public enum SupportingDocumentList
        {
            [Description("Business Tax Service Center")]
            DivisionName,
            [Description("Notice of BusinessTax Registration")]
            DocumentRequired,
            
        }
        /// <summary>
        /// Declare ManualMasterDocumentsId Enums
        /// </summary>
        public enum ManualMasterDocumentsId
        {
            [Description("-99991")]
            ManualEhopId,
            [Description("-99990")]
            ManualReceiptId,
            [Description("-99992")]
            ManualRenewalReceiptId,
        }
        /// <summary>
        /// Declare RenewalCheck Enums
        /// </summary>
        public enum RenewalCheck
        {
            [Description("ENDORSEMENT")]
            ENDORSEMENT,
            [Description("ENHANCED")]
            ENHANCED,
            [Description("APPLICATION")]
            APPLICATION,
            [Description("RAO")]
            RAO,
            [Description("PENALTY FEE (EXPIRED)")]
            PENALTYEXPIRED,
            [Description("PENALTY FEE (LAPSE)")]
            PENALTYLAPSE
        }
        /// <summary>
        /// Declare SubmissionCounter Enums
        /// </summary>
        public enum SubmissionCounter
        {
            [Description("DAPP")]
            DAPP = 1,
            [Description("LAPP_IAPP")]
            LAPP_IAPP = 2
        }
        /// <summary>
        /// Declare SubmissionNumberCounter Enums
        /// </summary>
        public enum SubmissionNumberCounter
        {
            [Description("899999")]
            DdppHeightNumber,
            [Description("99999")]
            LappHeightNumber,
            //
            [Description("000001")]
            InitialNumber,
            [Description("00001")]
            InitialNumberStartWith8,
            [Description("8")]
            Sequence8,
            [Description("9")]
            Sequence9
            //

        }
    }
}
