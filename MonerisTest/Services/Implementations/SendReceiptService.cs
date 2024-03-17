


namespace MonerisTest.Services.Implementations
{
    public class SendReceiptService : ISendReceiptService
    {
        public async Task SendReceiptToCustomer(CardHolderTransactionRecordPurchase cardHolderTransactionRecord)
        {

            PaymentReceipt cardHolderReceipt = new()
            {

            };
        }

        public async Task SendReceiptToMerchant(CardHolderTransactionRecordPurchase cardHolderTransactionRecord)
        {
            PaymentReceipt cardHolderReceipt = new()
            {

            };
        }
    }
}
