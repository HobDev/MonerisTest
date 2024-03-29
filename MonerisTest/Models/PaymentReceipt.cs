﻿
namespace MonerisTest.Models
{
    // the payment receipt for an e-commerce transaction is same for the merchant and customer in Canada
    public partial class PaymentReceipt: IRealmObject
    {
        public PaymentReceipt()
        {
        }

        public PaymentReceipt(string transactionType, string orderNumber, DateTimeOffset transaction_DateTime, string authorizationNumber, string referenceNumber, string iSOCode, string responseCode, string goods_Description, decimal amount, string currency_Code, string cardHolderName, string cardHolderAddress, Customer purchaser, string transactionNumber, string cardType)
        {
            TransactionType = transactionType;
            OrderNumber = orderNumber;
            Transaction_DateTime = transaction_DateTime;
            AuthorizationNumber = authorizationNumber;
            ReferenceNumber = referenceNumber;
            ISOCode = iSOCode;
            ResponseCode = responseCode;    
            Goods_Description = goods_Description;
            Amount = amount;
            Currency_Code = currency_Code;
            CardHolderName = cardHolderName;
            CardHolderAddress = cardHolderAddress;
            Purchaser = purchaser;
            TransactionNumber = transactionNumber;
            CardType = cardType;
        }

        [PrimaryKey]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        // legal name of the merchant
        public string MerchantName { get; set; }= "Daflo Innovation Inc.";

        // postal address of the Merchant   
        public string StroreAddress { get; set; } = "117 Vanier Street\r\nWhitby\r\nL1R 3J8";

        // web address of the Merchant
        public string MerchantUrl { get; set; } = "https://www.dafloinnovations.com";

        // the type of transaction, e.g. purchase, refund, etc.
        public string TransactionType { get; set; }

        // optional, at merchant discretion
        public string OrderNumber { get; set; }

        public DateTimeOffset Transaction_DateTime { get; set; }


        // only for approved transactions
        public string AuthorizationNumber { get; set; }


        // this data is used for reference the transactions on the host system. In the 17 digits reference number the first 8 digits are Terminal Id,  the second 3 digits are the shift number, the third 3 digits are the batch number and the fourth 3 digits are the transaction number with the batch
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
        public string Restrictions { get; set; } = "Cancellation is only allowed 24 hours before the start of the Game";    


        // backlink to the customer
        public Customer? Purchaser { get; set; }


        // not part of the receipt but useful for the merchant
        public string TransactionNumber { get; set; }

        public string CardType { get; set; }

        public string TransactionTypeValue { get; set; }


    }
}
