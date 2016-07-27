using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessCenter.Data.Implementation
{
    public class PaymentAddressDetailsRepository : GenericRepository<PaymentAddressDetails>, IPaymentAddressDetailsRepository
    {
        public PaymentAddressDetailsRepository(IUnitOfWork context)
            : base(context)
        {
        }
        /// <summary>
        /// This method is used to retrive Payment Address Details 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PaymentAddressDetails> GetPaymentAddressDetails()
        {
            return GetAll().AsQueryable();
        }
        /// <summary>
        /// This method is used to Insert Payment Address Details based on user PaymentDetailsModel Details
        /// </summary>
        /// <param name="pAddrModel"></param>
        /// <returns></returns>
        public bool InsertPaymentAddressDetails(PaymentDetailsModel pAddrModel)
        {
            bool Status = false;
            try
            {
                var paymentAddressDetails = FindBy(x => x.PaymentId.Trim() == pAddrModel.PaymentId).ToList();
                if (!paymentAddressDetails.Any())
                {
                var payDetails = new PaymentAddressDetails
                {
                    PaymentId = (pAddrModel.PaymentId ??"").Trim(),
                    FullAddress = (pAddrModel.FullAddress ??"").Trim(),
                    ContactFirstName = (pAddrModel.ContactFirstName ??"").Trim(),
                    ContactMiddleName = (pAddrModel.ContactMiddleName ??"").Trim(),
                    ContactLastName = (pAddrModel.ContactLastName ??"").Trim(),
                    BusinessName = (pAddrModel.BusinessName ??"").Trim(),
                    StreetNumber = (pAddrModel.StreetNumber ??"").Trim(),
                    StreetName = (pAddrModel.StreetName ??"").Trim(),
                    StreetType = (pAddrModel.StreetType ??"").Trim(),
                    Quadrant = (pAddrModel.Quadrant ??"").Trim(),
                    UnitNumber = (pAddrModel.UnitNumber ??"").Trim(),
                    City = (pAddrModel.City ??"").Trim(),
                    State = (pAddrModel.State ??"").Trim(),
                    Country = (pAddrModel.Country ??"").Trim(),
                    ContactNumber1 = (pAddrModel.ContactNumber1 ??"").Trim(),
                    ContactNumber2 = (pAddrModel.ContactNumber2 ??"").Trim(),
                    EmailAddress = (pAddrModel.Email ??"").Trim(),
                    Zip = (pAddrModel.Zip ?? "").Trim()
                };
            
                 Add(payDetails);
                 Save();
             }else
             {
                 var payDetails = paymentAddressDetails.FirstOrDefault();
                 
                   // payDetails.PaymentId = (pAddrModel.PaymentId ?? "").Trim(),
                    payDetails.FullAddress = (pAddrModel.FullAddress ?? "").Trim();
                     payDetails.ContactFirstName = (pAddrModel.ContactFirstName ?? "").Trim();
                     payDetails.ContactMiddleName = (pAddrModel.ContactMiddleName ?? "").Trim();
                     payDetails.ContactLastName = (pAddrModel.ContactLastName ?? "").Trim();
                    payDetails. BusinessName = (pAddrModel.BusinessName ?? "").Trim();
                    payDetails. StreetNumber = (pAddrModel.StreetNumber ?? "").Trim();
                     payDetails.StreetName = (pAddrModel.StreetName ?? "").Trim();
                     payDetails.StreetType = (pAddrModel.StreetType ?? "").Trim();
                    payDetails.Quadrant = (pAddrModel.Quadrant ?? "").Trim();
                   payDetails.UnitNumber = (pAddrModel.UnitNumber ?? "").Trim();
                    payDetails.City = (pAddrModel.City ?? "").Trim();
                    payDetails.State = (pAddrModel.State ?? "").Trim();
                  payDetails.Country = (pAddrModel.Country ?? "").Trim();
                 payDetails.ContactNumber1 = (pAddrModel.ContactNumber1 ?? "").Trim();
                   payDetails.ContactNumber2 = (pAddrModel.ContactNumber2 ?? "").Trim();
                    payDetails.EmailAddress = (pAddrModel.Email ?? "").Trim();
                    payDetails.Zip = (pAddrModel.Zip ?? "").Trim();
                 
                 Update(payDetails,payDetails.PaymentAddressId);
                 Save();
             }
              
                Status = true;
            }
            catch (Exception)
            { Status = false; }
            return Status;
        }
        /// <summary>
        /// This method is used to get specific payment address details based on payment id
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns>Return payment address detail data</returns>
        public IEnumerable<PaymentAddressDetails> FindByPaymentId(string paymentId)
        {
            return FindBy(x=>x.PaymentId.Trim()==paymentId);
        }
    }
}