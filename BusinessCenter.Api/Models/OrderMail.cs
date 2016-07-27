namespace BusinessCenter.Api.Models
{
    public class OrderMail
    {
       
            public string FullName { get; set; }
            public string TransactionSuccess { get; set; }
            public string InnerHtml { get; set; }
            //Transaction
            public string EmailAddress { get; set; }
            public string MasterID { get; set; }
            public string PaymentID { get; set; }
            public string SubNumber { get; set; }
            public decimal AmountCharged { get; set; }
            public string ReceiptDate { get; set; }
            public string CardNumber { get; set; }
            public string TransactionId { get; set; }
            public bool IsEhopAllowed { get; set; }
            public string DocType { get; set; }
            //public List<DetailedCategoryList> ServiceCheckList { get; set; }
            //public List<BblServiceDocuments> DocumentList { get; set; }

        
    }

    public class Ehopdetails
    {
        public byte[] EhopByteCode { get; set; }
        public string EhopFilename { get; set; }
    }
}