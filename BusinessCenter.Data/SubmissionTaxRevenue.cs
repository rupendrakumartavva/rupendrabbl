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
    public partial class SubmissionTaxRevenue
    {
        [Key]
        public int SubmissionTaxRevenueId { get; set; }
        public string MasterId { get; set; }
        public string TaxRevenueNumber { get; set; }
        public string TaxRevenueType { get; set; }
        public Nullable<System.DateTime> CreatdedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string FullName { get; set; }
        public string BusinessOwnerRoles { get; set; }
        public Nullable<bool> IsIAgree { get; set; }
    }
}