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
    public partial class TBL_ETL_Address_And_Parcel
    {
        [Key]
        public string L1_PARCEL_NBR { get; set; }
        public Nullable<int> L1_HSE_NBR_START { get; set; }
        public Nullable<int> L1_HSE_NBR_END { get; set; }
        public string L1_HSE_FRAC_NBR_START { get; set; }
        public string L1_HSE_FRAC_NBR_END { get; set; }
        public string L1_UNIT_START { get; set; }
        public string L1_UNIT_END { get; set; }
        public string L1_UNIT_TYPE { get; set; }
        public string L1_STR_NAME { get; set; }
        public string L1_STR_PREFIX { get; set; }
        public string L1_STR_SUFFIX { get; set; }
        public string L1_STR_SUFFIX_DIR { get; set; }
        public string L1_INSP_DISTRICT_PREFIX { get; set; }
        public string L1_INSP_DISTRICT { get; set; }
        public string L1_SITUS_NBRHD_PREFIX { get; set; }
        public string L1_SITUS_NBRHD { get; set; }
        public string L1_SITUS_CITY { get; set; }
        public string L1_SITUS_COUNTY { get; set; }
        public string L1_SITUS_STATE { get; set; }
        public string L1_SITUS_ZIP { get; set; }
        public string L1_SITUS_COUNTRY { get; set; }
        public string L1_ADDR_STATUS { get; set; }
        public string L1_ADDR_DESC { get; set; }
        public string L1_UDF1 { get; set; }
        public string L1_UDF2 { get; set; }
        public string L1_UDF3 { get; set; }
        public string L1_UDF4 { get; set; }
        public string L1_FULL_ADDRESS { get; set; }
    }
}