


namespace MonerisTest.Services.Implementations
{
    public class SendReceiptService : ISendReceiptService
    {

       
        public async Task SendReceipt(TransactionRecordOfPurchase cardHolderTransactionRecord)
        {

            PaymentReceipt cardHolderReceipt = new()
            {

            };
        }

       
    }
}
