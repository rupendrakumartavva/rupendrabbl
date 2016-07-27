using System;
using System.Collections.Generic;
using BusinessCenter.Data;

namespace BusinessCenter.Tests.Setup
{
    public class PaymentCardDetailsData
    {
          private readonly List<PaymentCardDetails> _entities;
        public bool IsInitialized;

        public void AddPaymentDetailsEntity(PaymentCardDetails obj)
        {
            _entities.Add(obj);
        }

        public List<PaymentCardDetails> PaymentDetailsList
        {
            get { return _entities; }
        }

        public PaymentCardDetailsData()
        {
            IsInitialized = true;
            _entities = new List<PaymentCardDetails>();

            AddPaymentDetailsEntity(new PaymentCardDetails()
            {
                PaymentCardId=1,
                PaymentId = "34cd9f7d-988c-4cd9-81c9-d740255bbfc6",
                CardType = "2",
                CardNumber = "1111-1111-1111-1111",
                CardName = "bharath kumar",
                CardExpDate = "bharath.pbk@gmail.com",
                CvvNumber = "bharath",
                FullName = ""
               
                
            });
           
        }
    }
}