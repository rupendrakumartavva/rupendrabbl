using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BusinessCenter.Api.PayPalUtility
{
    public class PayPalTransaction
    {
        /*The outcome of the attempted transaction. True means the transaction was approved.*/
        public bool Success { get; set; }

        public int Result { get; set; }
        /*The response message returned with the transaction result.*/
        public string Message { get; set; }

        /*PayPal transaction ID of the payment; returned by the PayPal processor.
          Character length and limitations: 17-character string*/
        [DisplayName("Transaction Id")]
        public string Id { get; set; }

        //Time of the transaction.
        [DisplayName("Receipt Date")]
        public DateTime Time { get; set; }
    }

   
}