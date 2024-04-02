

namespace MonerisTest.Services.Implementations
{
    public class TransactionSuccessService : ITransactionSuccessService
    {

        private readonly IPaymentResponseHelper? paymentResponseHelper;

        public TransactionSuccessService(IPaymentResponseHelper paymentResponseHelper) 
        {
            this.paymentResponseHelper = paymentResponseHelper;
        }

        public async Task<string?> SaveSuccessfulTransactionData(Receipt receipt, Customer customer)
        {

            string transType = receipt.GetTransType(); 
            string TransactionType = await paymentResponseHelper.GetTransactionType(transType) ?? string.Empty;

            string TransactionDate = receipt.GetTransDate();
            string TransactionTime = receipt.GetTransTime();    
            string AuthorizationNumber = receipt.GetAuthCode();
            string ReferenceNumber = receipt.GetReferenceNum();
            string ResponseCode = receipt.GetResponseCode();
            string ISOCode = receipt.GetISO();
            string GoodsDescription = "Goods Description";
            string Amount = receipt.GetTransAmount();
            string CurrencyCode = "CAD";
            string CardHolderName = customer.Name ?? string.Empty;
            string CardHolderAddress = customer.Address ?? string.Empty;
            string CustomerId = customer.CustomerId;
             
            

            RecordOfSuccessfulTransaction recordOfSucessfulTransaction = new RecordOfSuccessfulTransaction
                (       
            transactionType: TransactionType,
            orderNumber: string.Empty,
            transactionDate: DateTimeOffset.Parse(TransactionDate + " " + TransactionTime),
            authorizationNumber: AuthorizationNumber,
            referenceNumber: ReferenceNumber,
            responseCode: ResponseCode,
            iSOCode: ISOCode,
            goodsDescription: GoodsDescription,
            amount: Amount,
            currencyCode: CurrencyCode,
            cardHolderName: CardHolderName,
            cardHolderAddress: CardHolderAddress,
            buyer: customer
         
                );


            //save to database
            Realm realm = Realm.GetInstance();
            await realm.WriteAsync(() =>
            {
                realm.Add(recordOfSucessfulTransaction);
            });

            return recordOfSucessfulTransaction.Id;
             
        }
    }
}
