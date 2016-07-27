using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
   public interface IPaymentCardDetailsRepository
    {
       IEnumerable<PaymentCardDetails> GetPaymentDetails();
       bool InsertPaymentCardDetails(PaymentDetailsModel pcardDetails);
       IEnumerable<PaymentCardDetails> FindByID(ReceiptModel RModel);
    }
}
