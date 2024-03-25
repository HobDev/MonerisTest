namespace MonerisTest.Services.Implementations.Success
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
