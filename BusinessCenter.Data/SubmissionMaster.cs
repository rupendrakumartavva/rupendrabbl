//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BusinessCenter.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public partial class SubmissionMaster
    {
        [Key]
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
        public Nullable<int> LicenseDuration { get; set; }
        public string UserBblAssociateId { get; set; }
        public Nullable<bool> IsCategorySelfCertification { get; set; }
        public string BusinessOwnerName { get; set; }
        public string BusinessName { get; set; }
        public string PremisesAddress { get; set; }
        public Nullable<bool> IsRaoFee_Applied { get; set; }
    }
}
