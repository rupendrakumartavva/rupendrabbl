﻿using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
  public interface IPaymentAddressDetailsRepository
    {
      IEnumerable<PaymentAddressDetails> GetPaymentAddressDetails();
      bool InsertPaymentAddressDetails(PaymentDetailsModel pAddrModel);
      IEnumerable<PaymentAddressDetails> FindByPaymentId(string paymentId);
    }
}
