using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
   public interface IPaymentDetailsRepository
   {
       //IEnumerable<PaymentDetails> GetAll();
       string InsertPaymentDetails(PaymentDetailsModel pDetails);
       bool UpdatePaymentDetails(PaymentDetailsModel pDetails);
       ReceiptModel GetReceiptData(ReceiptModel RModel);
       IEnumerable<PaymentDetails> FindByPaymentID(PaymentDetails paymentDetails);
       PaymentTransactionDetails FindAddressByPaymentId(string masterid);
   }
}
