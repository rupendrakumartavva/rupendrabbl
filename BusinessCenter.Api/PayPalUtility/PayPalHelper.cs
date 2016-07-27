using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using PayPal.Payments.Common.Utility;
using PayPal.Payments.DataObjects;
using PayPal.Payments.Transactions;



namespace BusinessCenter.Api.PayPalUtility
{
    public class PayPalHelper : IPayPalHelper
    {
        private static UserInfo UserInformation  
        {
            get
            {
                return new UserInfo(
                WebConfigurationManager.AppSettings["PayPal.User"],
                WebConfigurationManager.AppSettings["PayPal.Vendor"],
                WebConfigurationManager.AppSettings["PayPal.Partner"],
                WebConfigurationManager.AppSettings["PayPal.Password"]
                ); 
            }
        }
        private static PayflowConnectionData Connection
        {
            get
            {
                return new PayflowConnectionData(
                    WebConfigurationManager.AppSettings["PayPal.Host"]);
            }
        }

        public BillTo GetBilling(PaymentModel payment)
        {
            return new BillTo
            {
                Street = payment.BillingStreetAddress,
                City = payment.BillingCity,
                State = payment.BillingState,
                Zip = payment.BillingZip,
                BillToCountry = "USA",
                FirstName = payment.FirstName,
                LastName = payment.LastName
            };
        }
        public Invoice GetInvoice(PaymentModel payment)
        {
            var invoiceComment = String.Join(", ", payment.SitePermits);
            return new Invoice
            {
                Amt = new Currency(payment.Amount),
                PoNum = invoiceComment,
                BillTo = GetBilling(payment),
                Comment1 = WebConfigurationManager.AppSettings["PayPal.Environment"]
            };

        }

        public CardTender TenderCreditCard(PaymentModel payment)
        {
            var creditCard = new CreditCard(payment.CreditCardNumber, payment.ExpirationDate)
            {
                Cvv2 = payment.CardSecurityCode
            };

            return new CardTender(creditCard);
        }
        public PayPalTransaction Pay(PaymentModel payment)
        {
            if (WebConfigurationManager.AppSettings["PayPal.Environment"] == "Test")
            {
                return new PayPalTransaction
                {
                    Success = true,
                    Result = 0,
                    Id = "Test-Transaction",
                    Message = "Test Approval",
                    Time = DateTime.Now
                };   
            }
            var sale = new SaleTransaction(UserInformation, Connection, GetInvoice(payment), TenderCreditCard(payment), PayflowUtility.RequestId);
            // Submit the Transaction
            var transactionResponse = sale.SubmitTransaction().TransactionResponse;
            if (transactionResponse != null)
            {
                return new PayPalTransaction
                {
                    Success = transactionResponse.Result == 0,
                    Result = transactionResponse.Result,
                    Id = transactionResponse.Pnref,
                    Message = transactionResponse.RespMsg,
                    //Time = Convert.ToDateTime(transactionResponse.EndTime) --To get transaction time from response.
                    Time = DateTime.Now
                };
            }
            return new PayPalTransaction
            {
                Success = false,
                Result = 999,
                Message = "Unable to process your request. Please try again.",
                Time = DateTime.Now
            };
        }
    }

}