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
    public partial class SubmissionCofo_Hop_Ehop
    {
        [Key]
        public int SubCofoHopEhopId { get; set; }
        public string MasterId { get; set; }
        public string UserSelectType { get; set; }
        public string Number { get; set; }
        public Nullable<System.DateTime> DateOfIssuance { get; set; }
        public Nullable<bool> DoNotHaveCofo { get; set; }
        public Nullable<bool> IsUploadSupportDoc { get; set; }
        public Nullable<bool> IsValid { get; set; }
        public string OccupancyAddressValidate { get; set; }
        public Nullable<bool> IseHOPEligibility { get; set; }
        public string EHopEligibilityType { get; set; }
        public Nullable<bool> ConfirmeHOPEligibilityType { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}