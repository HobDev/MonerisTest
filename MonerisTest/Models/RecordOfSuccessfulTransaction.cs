

namespace MonerisTest.Models
{
    public partial class RecordOfSuccessfulTransaction : IRealmObject
    {

        private RecordOfSuccessfulTransaction()
        {
        }

        public RecordOfSuccessfulTransaction( string transactionType, string orderNumber, DateTimeOffset transactionDate, string authorizationNumber, string referenceNumber, string responseCode, string iSOCode, string goodsDescription, string amount, string currencyCode, string cardHolderName, string cardHolderAddress, Customer buyer)
        {
           
            TransactionType = transactionType;
            OrderNumber = orderNumber;
            Transaction_DateTime = transactionDate;
            AuthorizationNumber = authorizationNumber;
            ReferenceNumber = referenceNumber;
            ResponseCode = responseCode;
            ISOCode = iSOCode;
            Goods_Description = goodsDescription;
            Amount = amount;
            Currency_Code = currencyCode;
            CardHolderName = cardHolderName;
            CardHolderAddress = cardHolderAddress;
            Buyer = buyer;

        }

        [PrimaryKey]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        // legal name of the merchant
        public string MerchantName { get; set; } = "Daflo Innovation Inc.";

        // postal address of the Merchant   
        public string StoreAddress { get; set; } = "117 Vanier Street\r\nWhitby\r\nL1R 3J8";

        // web address of the Merchant
        public string MerchantUrl { get; set; } = "https://www.dafloinnovations.com";

        // the type of transaction, e.g. purchase, refund, etc.
        public string TransactionType { get; set; }

        // optional, at merchant discretion
        public string OrderNumber { get; set; }

        public DateTimeOffset Transaction_DateTime { get; set; }

        public string ReferenceNumber { get; set; }

        // only for approved transactions
        public string AuthorizationNumber { get; set; }

        // code returned from the transaction   
        public string ResponseCode { get; set; }

        // code returned from the transaction
        public string ISOCode { get; set; }


        public string Goods_Description { get; set; }

        public string Amount { get; set; }

        public string Currency_Code { get; set; }

        public string CardHolderName { get; set; }

        public string CardHolderAddress { get; set; }

        // any restrictions on refunds and returns
        public string Restrictions { get; set; } = "Cancellation is only allowed 24 hours before the start of the Game";


        // backlink to the customer
        public Customer? Buyer { get; set; }
    }
}
