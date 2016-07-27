using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayPal.Payments.DataObjects;


namespace BusinessCenter.Api.PayPalUtility
{
    public interface IPayPalHelper
    {
        PayPalTransaction Pay(PaymentModel payment);
    }
}
