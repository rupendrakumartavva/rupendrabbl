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
    public partial class DCBC_ENTITY_RESULTS_OPLA
    {
        [Key]
        public int DCBC_ENTITY_ID { get; set; }
        [Key]
        public bool REC_STATUS { get; set; }
        [Key]
        public System.DateTime LAST_UPDATE { get; set; }
        [Key]
        public string DCBC_ENTITY_SOURCE { get; set; }
        public string License_Type { get; set; }
        public string License_Board { get; set; }
        public string LICENSE_NUMBER { get; set; }
        public Nullable<System.DateTime> LICENSE_ISSUE_DATE { get; set; }
        public Nullable<System.DateTime> EXPIRATION_DATE { get; set; }
        [Key]
        public string LICENSE_STATUS { get; set; }
        public string Licensee_Name { get; set; }
        public string ADDRESS { get; set; }
        public string Employer_City_State_Zip { get; set; }
        public string Preferred_Address_Ind { get; set; }
        public string EMPLOYER_LICENSE_DESCRIPTION { get; set; }
        public string EMPLOYER_LICENSE_NUMBER { get; set; }
        public string Organization_EMPLOYER_NAME { get; set; }
        public Nullable<System.DateTime> EMPLOYER_LICENSE_ISSUE_DATE { get; set; }
        public Nullable<System.DateTime> EMPLOYER_LICENSE_EXPIRATION_DATE { get; set; }
        [Key]
        public string EMPLOYER_LICENSE_STATUS { get; set; }
        public string FULL_NAME { get; set; }
        public string OPLA_LICENSE_CATEGORY { get; set; }
        public string OPLA_SOURCE { get; set; }
    }
}
