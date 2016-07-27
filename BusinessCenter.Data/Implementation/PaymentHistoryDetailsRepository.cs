using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Implementation
{
    public class PaymentHistoryDetailsRepository : GenericRepository<PaymentHistoryDetails>, IPaymentHistoryDetailsRepository
    {
        public PaymentHistoryDetailsRepository(IUnitOfWork context)
            : base(context)
        { 
        
        }
        /// <summary>
        /// This method is used to inset the payment details based in user inputs
        /// </summary>
        /// <param name="paymentModel"></param>
        /// <returns>Return bool value</returns>
        public bool InsertPaymentDetails(PaymentDetailsModel paymentModel)
        {
            try
            {
                var paymentHistoryDetails = new PaymentHistoryDetails
                {
                    PaymentHistoryId = Guid.NewGuid().ToString(),
                    PaymentId = paymentModel.PaymentId??"",
                    CardNumber = paymentModel.CardNumber??"",
                    PaymentAmount =Convert.ToDecimal( paymentModel.TotalAmount),
                    CardName = paymentModel.CardName??"",
                    CvvNumber = paymentModel.CvvNumber??"",
                    CardExpDate = (paymentModel.CardExpMonth ?? "").Trim() + "/" + (paymentModel.CardExpYear ?? "").Trim(),
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    PaymentDate=paymentModel.PaymentDate,
                    PaymentStatus=paymentModel.PaymentStatus??"",
                    PaymentTransactionId=paymentModel.TranscationId??""
                };

                Add(paymentHistoryDetails);
                Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
