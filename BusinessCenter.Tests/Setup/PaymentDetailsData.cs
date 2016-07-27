using System;
using System.Collections.Generic;
using BusinessCenter.Data;

namespace BusinessCenter.Tests.Setup
{
    public class PaymentDetailsData
    {

        private readonly List<PaymentDetails> _entities;
        public bool IsInitialized;

        public void AddPaymentDetailsEntity(PaymentDetails obj)
        {
            _entities.Add(obj);
        }

        public List<PaymentDetails> PaymentDetailsList
        {
            get { return _entities; }
        }

        public PaymentDetailsData()
        {
            IsInitialized = true;
            _entities = new List<PaymentDetails>();

            AddPaymentDetailsEntity(new PaymentDetails()
            {
                PaymentId = "34cd9f7d-988c-4cd9-81c9-d740255bbfc6",
                MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97",
                Ordernumber = "DCRABBLORDER15953919",
                PaymentFrom = "submission",
                PaymentMailAddress = "bharath.pbk@gmail.com",
                Signature = "bharath",
                IsAggree = true,
                TranscationId = "Test-Transaction",
                PaymentStatus = "Test Approval",
                PaymentDate = Convert.ToDateTime("2015-12-15 07:52:14.063"),
                ApproveBy = "",
                Description = ""
                
            });
           
        }
    }
}