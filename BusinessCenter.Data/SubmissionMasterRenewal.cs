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
    public partial class SubmissionMasterRenewal
    {
        [Key]
        public long RenewalSubmId { get; set; }
        public string MasterId { get; set; }
        public string SubmissionLicense { get; set; }
        public string CorpNumber { get; set; }
        public Nullable<bool> IsDcraCorpDivision { get; set; }
        public Nullable<decimal> LapsedFee { get; set; }
        public string Extradays { get; set; }
        public Nullable<bool> IsCorpDocRegistration { get; set; }
        public Nullable<decimal> ExpiredFee { get; set; }
    }
}