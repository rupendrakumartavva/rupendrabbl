using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessCenter.Api.PayPalUtility
{
    public class PaymentModel
    {
        public string BillToCountry { get; set; }
        public string BillToPhone2 { get; set; }
        public string BillToStreet2 { get; set; }
        public string City { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string FirstName { get; set; }
        public string HomePhone { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string PhoneNum { get; set; }
        public string State { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        public string BillingStreetAddress { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingZip { get; set; }
        public string SitePermits { get; set; }
        public decimal Amount { get; set; }
        public string CreditCardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string CardSecurityCode { get; set; }
    }
}