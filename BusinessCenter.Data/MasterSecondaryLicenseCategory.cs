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
    public partial class MasterSecondaryLicenseCategory
    {
        [Key]
        public string SecondaryID { get; set; }
        public string PrimaryID { get; set; }
        public string SecondaryLicenseCategory { get; set; }
        public string UnitOne { get; set; }
        public string UnitTwo { get; set; }
        public string Endorsement { get; set; }
        public Nullable<bool> IsSubCategory { get; set; }
        public Nullable<bool> Status { get; set; }
    }
}