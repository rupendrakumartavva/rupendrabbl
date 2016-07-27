using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessCenter.Data.Implementation
{
    public class PaymentCardDetailsRepository : GenericRepository<PaymentCardDetails>, IPaymentCardDetailsRepository
    {
        public PaymentCardDetailsRepository(IUnitOfWork context)
            : base(context)
        {
        }
        /// <summary>
        /// This method to used to get all Payment Card Details.
        /// </summary>
        /// <returns>list of Payment Card Details</returns>
        public IEnumerable<PaymentCardDetails> GetPaymentDetails()
        {
            var paymentCard = GetAll().AsQueryable();
            return paymentCard;
        }
        /// <summary>
        /// This method is used to insert the Payment card Details into the table based on PaymentDetailsModel
        /// </summary>
        /// <param name="pcardDetails"></param>
        /// <returns>Retrun result in bool</returns>
        public bool InsertPaymentCardDetails(PaymentDetailsModel pcardDetails)
        {
            bool Status = false;
            try
            {
                var paymentCardDetails = FindBy(x => x.PaymentId.Trim() == pcardDetails.PaymentId).ToList();
                if (!paymentCardDetails.Any())
                {
                    PaymentCardDetails payDetails = new PaymentCardDetails();
                    payDetails.PaymentId = (pcardDetails.PaymentId ?? "").Trim();
                    payDetails.CardType = (pcardDetails.CardType ?? "").Trim();
                    payDetails.CardName = (pcardDetails.CardName ?? "").Trim();
                    payDetails.CardNumber = (pcardDetails.CardNumber ?? "").Trim();
                    payDetails.CardExpDate = (pcardDetails.CardExpMonth ?? "").Trim() +"/"+ (pcardDetails.CardExpYear ?? "").Trim();
                    payDetails.CvvNumber = (pcardDetails.CvvNumber ?? "").Trim();
                    payDetails.FullName = (pcardDetails.FullName ?? "").Trim();
                    Add(payDetails);
                    Save();
                    Status = true;
                }else
                {
                    var payDetails = paymentCardDetails.FirstOrDefault();
                   // payDetails.PaymentId = (pcardDetails.PaymentId ?? "").Trim();
                    payDetails.CardType = (pcardDetails.CardType ?? "").Trim();
                    payDetails.CardName = (pcardDetails.CardName ?? "").Trim();
                    payDetails.CardNumber = (pcardDetails.CardNumber ?? "").Trim();
                    payDetails.CardExpDate = (pcardDetails.CardExpMonth ?? "").Trim() + (pcardDetails.CardExpYear ?? "").Trim();
                    payDetails.CvvNumber = (pcardDetails.CvvNumber ?? "").Trim();
                    payDetails.FullName = (pcardDetails.FullName ?? "").Trim();
                    Update(payDetails, payDetails.PaymentCardId);
                    Save();
                    Status = true;
                }
            }
            catch (Exception )
            { Status = false; }
            return Status;
        }
        /// <summary>
        /// This method to retive specific Payment Card Details based on Payment Id.
        /// </summary>
        /// <param name="rModel"></param>
        /// <returns>Retrun Specific Payment Card Details</returns>
        public IEnumerable<PaymentCardDetails> FindByID(ReceiptModel rModel)
        {
            var paymentCardDetails = FindBy(x => x.PaymentId.Trim() == rModel.PaymentID);
            return paymentCardDetails;
        }
        //public virtual void SaveChanges()
        //{
        //    Context.SaveChanges();
        //}
    }
}
