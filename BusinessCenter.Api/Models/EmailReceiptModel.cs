using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BusinessCenter.Api.App_Start;
using BusinessCenter.Data.Model;
using BusinessCenter.Service.Interface;
using BusinessCenter.Api.Common;
using PayPal.Payments.Communication;
using PayPal.Payments.Common.Utility;
using System.Collections.Specialized;
using System.Configuration;
using System.EnterpriseServices.Internal;
using System.IO;
using System.Web;
using BusinessCenter.Api.PayPalUtility;
using BusinessCenter.Email;
using RazorEngine;

namespace BusinessCenter.Api.Models
{
    public class EmailReceiptModel
    {
        public decimal AmountCharged { get; set; }
        public decimal ApplicationFee { get; set; }
        public string ApplicationSubmit { get; set; }
        public string CardNumber { get; set; }
        public decimal CategoryLicenseFee { get; set; }
        public string DocType { get; set; }
        public List<EmailBblServiceDocuments> DocumentList { get; set; }
        public string EmailAddress { get; set; }
        public decimal EndorsementFee { get; set; }
        public string ExceptedFinalCheckingDate { get; set; }
        public decimal ExtraAmount { get; set; }
        public string Extradays { get; set; }
        public string FullName { get; set; }
        public decimal GrandTotal { get; set; }
        public string GrandTotals { get; set; }
        public string InnerHtml { get; set; }
        public bool Isehop { get; set; }
        public bool IsEhopAllowed { get; set; }
        public bool IsSubmissionCofo { get; set; }
        public bool IsSubmissioneHop { get; set; }
        public bool IsSubmissionHop { get; set; }
        public string MasterID { get; set; }
        public string PaymentID { get; set; }
        public string ReceiptDate { get; set; }
        public List<EmailDetailedCategoryList> ServiceCheckList { get; set; }
        public string SubNumber { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TechFee { get; set; }
        public decimal TotalApplicationFee { get; set; }
        public decimal TotalEndosementFee { get; set; }
        public decimal TotalFee { get; set; }
        public decimal TotalLicenseFee { get; set; }
        public string TotalLicenseFees { get; set; }
        public decimal TotalTechFee { get; set; }
        public string TotalTechFees { get; set; }
        public string TransactionId { get; set; }
        public string TransactionSuccess { get; set; }

        public string SiteUrl { get; set; }
        public string LicenseDuration { get; set; }
        public bool IsBackgroundInvestigation { get; set; }

        public string ApplicationStatus { get; set; }
    }

    public class EmailDetailedCategoryList
    {
     

        public decimal ApplicationFee { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryId { get; set; }
        public decimal CategoryLicenseFee { get; set; }
        public string Endorsement { get; set; }
        public decimal EndorsementFee { get; set; }
        public bool IsRaoFeeApplied { get; set; }
        public string LicenseCategory { get; set; }
        public string LicenseDuration { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TechFee { get; set; }
        public decimal TotalFee { get; set; }
        public decimal LapsedFee { get; set; }
        public decimal ExpiryFee { get; set; }
        public string Units { get; set; }
    }

    public class EmailBblServiceDocuments
    {

        public int Sno { get; set; }
        public string Agency { get; set; }
        public string ApprovedBy { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryID { get; set; }
        public string CheckListType { get; set; }
        public string Description { get; set; }
        public string Div { get; set; }
        public string Division { get; set; }
        public string DocRequired { get; set; }
        public string DocStatus { get; set; }
        public string Endorsement { get; set; }
        public string FileLocation { get; set; }
        public string FileName { get; set; }
        public bool IsUpload { get; set; }
        public string License { get; set; }
        public string LicenseName { get; set; }
        public string MasterId { get; set; }
        public string ShortName { get; set; }
        public int SubmissionCategoryID { get; set; }
        public string SubmissionId { get; set; }
        public string UploadFileName { get; set; }
    }
}