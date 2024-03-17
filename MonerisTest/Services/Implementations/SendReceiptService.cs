


namespace MonerisTest.Services.Implementations
{
    public class SendReceiptService : ISendReceiptService
    {

       
        public async Task SendReceipt(CardHolderTransactionRecordPurchase cardHolderTransactionRecord)
        {

            PaymentReceipt cardHolderReceipt = new()
            {

            };
        }

       
    }
}
