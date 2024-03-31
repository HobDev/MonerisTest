

namespace MonerisTest.Services.Implementations
{
    public class TransactionSuccessService : ITransactionSuccessService
    {
        public async Task<string?> SaveSuccessfulTransactionData(Receipt receipt)
        {
            string TransactionType = receipt.GetTransType(); 
            string TransactionDate = receipt.GetTransDate();
            string TransactionTime = receipt.GetTransTime();    
            string AuthorizationNumber = receipt.GetAuthCode();
            string ReferenceNumber = receipt.GetReferenceNum();
            string ResponseCode = receipt.GetResponseCode();
            string ISOCode = receipt.GetISO();
            string GoodsDescription = "Goods Description";
            string Amount = receipt.GetTransAmount();
            string CurrencyCode = receipt.GetCurrencyCode();
            string CardHolderName = receipt.GetCardCardHolderName();
            string CardHolderAddress = "address";
            string CustomerId = receipt.GetResCustId();
             
            

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
            buyer: null
         
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
