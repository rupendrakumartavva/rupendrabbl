using System.Collections.Generic;
using BusinessCenter.Data;

namespace BusinessCenter.Tests.Setup
{
    public class PaymentAddressDetailsData
    {
        private readonly List<PaymentAddressDetails> _entities;
        public bool IsInitialized;

        public void AddPaymentAddressDetailsEntity(PaymentAddressDetails obj)
        {
            _entities.Add(obj);
        }

        public List<PaymentAddressDetails> PaymentAddressDetailsList
        {
            get { return _entities; }
        }

        public PaymentAddressDetailsData()
        {
            IsInitialized = true;
            _entities = new List<PaymentAddressDetails>();

            AddPaymentAddressDetailsEntity(new PaymentAddressDetails()
            {
                PaymentAddressId = 1,
                PaymentId = "34cd9f7d-988c-4cd9-81c9-d740255bbfc6",
                FullAddress = "12100 Wilshire Boulevard, Suite 1400",
                BusinessName = "APROSERVE CORPORATION",
                City = "Los Angeles",
                State = "bharath.pbk@gmail.com",
                Country = "",
                ContactNumber1 = "",
                ContactNumber2 = "",
                EmailAddress = "",
                Zip = "90025",
                StreetNumber = "12100 Wilshire Boulevard, Suite 1400",
                StreetName = "",
                StreetType = "",
                Quadrant = "",
                UnitNumber = "",
                ContactFirstName = "",
                ContactMiddleName = "",
                ContactLastName = ""
              
                
            });
           
        }
 
    }
}