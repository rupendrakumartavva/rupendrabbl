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
    public partial class SubmissionMaster_ApplicationCheckList
    {
        [Key]
        public int SubMaster_ApplicationCheckListId { get; set; }
        public string MasterId { get; set; }
        public Nullable<bool> FEIN_SSN { get; set; }
        public Nullable<bool> IsSubmissionCofo { get; set; }
        public Nullable<bool> IsSubmissionHop { get; set; }
        public Nullable<bool> IsSubmissioneHop { get; set; }
        public Nullable<bool> IsCleanHandsVerify { get; set; }
        public Nullable<bool> IsCorporateRegistration { get; set; }
        public Nullable<bool> IsBHAddress { get; set; }
        public Nullable<bool> IsBPAddress { get; set; }
        public Nullable<bool> IsMailAddress { get; set; }
        public Nullable<bool> IsResidentAgent { get; set; }
        public Nullable<bool> IsDocForCleanHands { get; set; }
        public Nullable<bool> IsDocForCofo { get; set; }
        public Nullable<bool> IsDocForHop { get; set; }
        public Nullable<bool> IsDocForeHop { get; set; }
        public Nullable<bool> IsSelfCertification { get; set; }
    }
}