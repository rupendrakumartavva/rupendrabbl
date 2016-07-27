using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Common
{
    public class SubmissionIndividualEntity
    {
        public int SubmindividualId { get; set; }
        public string MasterId { get; set; }
        public string CompanyBusinessLicense { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateofBirth { get; set; }
        public string City { get; set; }
        public string State_Province { get; set; }
        public string Country { get; set; }
        public string Height { get; set; }
        public string HeightIn { get; set; }

        public string CompanyName { get; set; }
        public string MiddleName { get; set; }

        public string Weight { get; set; }
        public string HairColor { get; set; }
        public string EyeColor { get; set; }
        public string IdentificationCard { get; set; }
        public string StateofIssuance { get; set; }

        public string ExpirationDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

   //SubmissionIndividualEntity

   public class SubmissionTaxRevenuEntity
   {
       public int SubmissionTaxRevenueId { get; set; }
       public string MasterId { get; set; }
       public string TaxRevenueFfin { get; set; }
       public string FullName { get; set; }
       public string BusinessOwnerRoles { get; set; }
       public string TaxRevenueType { get; set; }
       public string UserId { get; set; }
       public string UserBblAssociateId { get; set; }
       public string EntityId { get; set; }
       public DateTime CreatdedDate { get; set; }
       public string SubmissionLicense { get; set; }
       public bool IsIAgree { get; set; }

   }

   public partial class MastereHopEligibilityEntity
   {
       public int Id { get; set; }
       public string Name { get; set; }
       public bool GetChcekedItem { get; set; }
       public int TypeId { get; set; }
   }

    public class SubmissionMasterDetails
    {
       public  string MasterId { get; set; }
       public string UserName { get; set; }
       public string ActivityName { get; set; }
       public string App_Type { get; set; }
       public string SubmissionLicense { get; set; }
       public decimal GrandTotal { get; set; }
       public string Status { get; set; }
       public string ApplicationSubmitType { get; set; }

    }
    public class SubmissiontoAccelaEntity
    {
        public long SubmissiontoAccelaId { get; set; }
        public string LicenseNumber { get; set; }
        public bool? ApplicationCompleted { get; set; }
        public bool? ApplicationCreated { get; set; }
        public bool? ApplicationFeeMatched { get; set; }
        public bool? RenewalPaymentUpdated { get; set; }
        public bool? RenewalFeeMatched { get; set; }
        public bool? AllDocumentsUpdated { get; set; }
        public bool? EhopCreated { get; set; }
        public bool? ProcessCompleted { get; set; }
        public string AccelaGeneratedID { get; set; }
       
 
    }

    public class SubmissiontoAccelaDetails
    {
        public long SubmissiontoAccelaId { get; set; }
        public string LicenseNumber { get; set; }
        public bool? ApplicationCompleted { get; set; }
        public bool? ApplicationCreated { get; set; }
        public bool? ApplicationFeeMatched { get; set; }
        public bool? RenewalPaymentUpdated { get; set; }
        public bool? RenewalFeeMatched { get; set; }
        public bool? AllDocumentsUpdated { get; set; }
        public bool? EhopCreated { get; set; }
        public bool? ProcessCompleted { get; set; }
        public string AccelaGeneratedID { get; set; }
        public string MasterId { get; set; }

        public string FullName { get; set; }
        public string CreatedDate { get; set; }
        public string Type { get; set; }

    }

    public class SubmissionDocumentToAccelaEntity
    {
        public int SubmisionDocToAccelaId { get; set; }
        public string LicenseNumber { get; set; }
        public string MasterId { get; set; }
        public Nullable<int> MasterCategoryDocId { get; set; }
        public string FileName { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<bool> Status { get; set; }

    }



    public partial class SubmissionGeneratedDocumentEntity
    {

        public string SubmissionGeneratedDocumentId { get; set; }
        public string MasterId { get; set; }
        public string SubmissionLicenseNumber { get; set; }
        public string UserId { get; set; }
        public byte[] FileByteCode { get; set; }
        public string FileType { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string Gen_DocumentFrom { get; set; }
    }
    //SubmissiontoAccela
    public class SubmissionMasterDetailsViewModel
    {
        public string UserName { get; set; }
        public List<SubmissionMasterDetails> Draftlist;
        public List<SubmissionMasterDetails> UnderReviewlist;
        public List<SubmissionMasterDetails> Licenselist;
        public List<SubmissionMasterDetails> Renewlist;
        public int DraftlistCount { get; set; }
        public int UnderReviewlistCount { get; set; }
        public int LicenseCount { get; set; }
        public int RenewCount { get; set; }
        public string LicenseType { get; set; }
    }

    public class ChecklistModel
    {
        public string MasterId { get; set; }

    }

    public class TaxAndReneueInitailDisplay
    {
        public string FullAddress { get; set; }
        public string TradeName { get; set; }
        public string BussinessOwnerFullName { get; set; }
    }

    public class ApplicationReviewCounts
    {
        public int DraftlistCount { get; set; }
        public int UnderReviewlistCount { get; set; }
    }

    //public class AddressType
    //{
    //    public List<string> Street { get; set; }
    //    public IEnumerable<MasterCountry> Country { get; set; }
    //}
}
