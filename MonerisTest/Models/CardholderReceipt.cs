﻿
namespace MonerisTest.Models
{
    public class CardholderReceipt
    {
        // legal name of the merchant
        public string MerchantName { get; set; }

        // postal address of the Merchant   
        public string StroreAddress { get; set; }

        // web address of the Merchant
        public string MerchantUrl { get; set; }

        // the type of transaction, e.g. purchase, refund, etc.
        public string TransactionType { get; set; }

        // optional, at merchant discretion
        public string OrderNumber { get; set; }

        public DateTimeOffset Transaction_DateTime { get; set; }


        // only for approved transactions
        public string AuthorizationNumber { get; set; }

        public string ReferenceNumber { get; set; }

        // code returned from the transaction
        public string ISOCode { get; set; }

        // code returned from the transaction   
        public string ResponseCode { get; set; }    

        public string Goods_Description { get; set; }

        public decimal Amount { get; set; }

        public string Currency_Code { get; set; }

        public string CardHolderName { get; set; }  

        public string CardHolderAddress { get; set; }   

        // any restrictions on refunds and returns
        public string Restrictions { get; set; }    


    }
}
