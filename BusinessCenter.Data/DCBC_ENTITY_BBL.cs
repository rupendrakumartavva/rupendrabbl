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
    public partial class DCBC_ENTITY_BBL
    {
        [Key]
        public int DCBC_ENTITY_ID { get; set; }
       
        public bool REC_STATUS { get; set; }
       
        public System.DateTime LAST_UPDATE { get; set; }
       
        public string DCBC_ENTITY_SOURCE { get; set; }
       
        public string SERV_PROV_CODE { get; set; }
      
        public string B1_PER_ID1 { get; set; }
      
        public string B1_PER_ID2 { get; set; }
       
        public string B1_PER_ID3 { get; set; }
       
        public string B1_PER_GROUP { get; set; }
       
        public string B1_PER_TYPE { get; set; }
      
        public string B1_PER_SUB_TYPE { get; set; }
       
        public string B1_PER_CATEGORY { get; set; }
        public string B1_APPL_STATUS_ORIGINAL { get; set; }
        public string B1_APPL_STATUS { get; set; }
        public string B1_ALT_ID { get; set; }
        public string APPLICATION_CAP { get; set; }
        public string B1_APPL_STATUS_DATE { get; set; }
        public Nullable<System.DateTime> ISSUED_DATE { get; set; }
        public Nullable<int> B1_HSE_NBR_START { get; set; }
        public Nullable<int> B1_HSE_NBR_END { get; set; }
        public string B1_HSE_FRAC_NBR_START { get; set; }
        public string B1_HSE_FRAC_NBR_END { get; set; }
        public string B1_UNIT_START { get; set; }
        public string B1_STR_NAME { get; set; }
        public string B1_STR_SUFFIX { get; set; }
        public string B1_STR_SUFFIX_DIR { get; set; }
        public string B1_SITUS_CITY { get; set; }
        public string B1_SITUS_STATE { get; set; }
        public string B1_SITUS_ZIP { get; set; }
        public string FULL_ADDRESS { get; set; }
        public string B1_APP_TYPE_ALIAS { get; set; }
        public string SSL { get; set; }
       
        public string REC_FUL_NAM { get; set; }
        public string Contact_Business_Name { get; set; }
        public string Contact_FirstName { get; set; }
        public string Contact_MiddleName { get; set; }
        public string Contact_LastName { get; set; }
        public string Billing_Address1 { get; set; }
        public string Billing_Address2 { get; set; }
        public string Billing_Address3 { get; set; }
        public string Billing_CITY { get; set; }
        public string Billing_STATE { get; set; }
        public string Billing_ZIP { get; set; }
        public string OwnrApplicant_BUSINESS_NAME { get; set; }
        public string OwnrApplicant_FNAME { get; set; }
        public string OwnrApplicant_MNAME { get; set; }
        public string OwnrApplicant_LNAME { get; set; }
        public string OwnrApplicant_Address1 { get; set; }
        public string OwnrApplicant_Address2 { get; set; }
        public string OwnrApplicant_Address3 { get; set; }
        public string OwnrApplicant_CITY { get; set; }
        public string OwnrApplicant_STATE { get; set; }
        public string OwnrApplicant_ZIP { get; set; }
        public string RegAgent_BUSINESS_NAME { get; set; }
        public string RegAgent_FNAME { get; set; }
        public string RegAgent_MNAME { get; set; }
        public string RegAgent_LNAME { get; set; }
        public string RegAgent_Address1 { get; set; }
        public string RegAgent_Address2 { get; set; }
        public string RegAgent_Address3 { get; set; }
        public string RegAgent_CITY { get; set; }
        public string RegAgent_STATE { get; set; }
        public string RegAgent_ZIP { get; set; }
        public string Attr_TRADE_NAME { get; set; }
        public string Business_Org { get; set; }
        public string Expiration_Date { get; set; }
        public string License_Issued_Date { get; set; }
        public string Period_Start_Date { get; set; }
        public string License_Category { get; set; }
        public string License_Category_Full { get; set; }
        public string TaxNumber { get; set; }
        public string FEIN_SSN { get; set; }
        public string APPLICATION_CAP_STATUS { get; set; }
        public Nullable<System.DateTime> APPLICATION_CAP_STATUS_DATE { get; set; }
       
        public System.DateTime REC_DATE { get; set; }

        public string WARD { get; set; }

        public string ANC { get; set; }

        public string CofO_Number { get; set; }

        public string CofO_IssueDate { get; set; }

        public string H_O_P_Number { get; set; }
        public string H_O_P_Issue_Date { get; set; }

        public string E_HOP_Number { get; set; }
        public string E_HOP_Issue_Date { get; set; }
        public string ZONE { get; set; }

        public string Org_Number { get; set; }

       
        
        
    }
}
