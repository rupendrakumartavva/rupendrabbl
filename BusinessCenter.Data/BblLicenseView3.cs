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
    public partial class BblLicenseView3
    {
        [Key]
        public string Application_Unique_ID { get; set; }
        public string License_Number { get; set; }
        public string Full_Name { get; set; }
        public string Online_App_License_Status { get; set; }        
        public string Renewal_License_No_ { get; set; }
        public string Primary_Category { get; set; }
        public Nullable<int> License_Renewal_Period { get; set; }
        public Nullable<decimal> ApplicationFee { get; set; }
        public Nullable<decimal> Endorsement_Fee { get; set; }
        public Nullable<decimal> LicenseFee { get; set; }
        public Nullable<decimal> RAO_Fee { get; set; }
        public Nullable<decimal> ESFFee { get; set; }
        public Nullable<decimal> Penalty_Fee__Lapse_ { get; set; }
        public Nullable<decimal> Penalty_Fee__Expired_ { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<int> Expiration_Date { get; set; }
        public string Payment_Transaction_ID { get; set; }
        public Nullable<System.DateTime> Transaction_Payment_Date { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public Nullable<System.DateTime> UpDated_Date { get; set; }

        public string Org_Number { get; set; }
    }
}
