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
    public partial class MasterRenewalStatusFee
    {
        [Key]
        public int RenewalStatusFeesId { get; set; }
        public string StatusType { get; set; }
        public Nullable<decimal> FeeAmount { get; set; }
        public Nullable<int> StartRange { get; set; }
        public Nullable<int> EndRange { get; set; }
    }
}