


namespace MonerisTest.Services.Implementations
{
    public class SendReceiptService : ISendReceiptService
    {
        public async Task SendReceiptToCustomer(CardHolderTransactionRecordPurchase cardHolderTransactionRecord)
        {

            CardholderReceipt cardHolderReceipt = new()
            {

            };
        }

        public async Task SendReceiptToMerchant(CardHolderTransactionRecordPurchase cardHolderTransactionRecord)
        {
           MerchantCopyReceipt merchantCopyReceipt = new()
           {

           };
        }
    }
}
