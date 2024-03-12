
using MonerisTest.Models;
using MonerisTest.Services.Interfaces;

namespace MonerisTest.Services.Implementations
{
    public class SendReceiptService : ISendReceiptService
    {
        public async Task SendReceiptToCustomer(CardHolderTransactionRecordPurchase cardHolderTransactionRecord)
        {

            CardholderReceipt cardHolderReceipt = new CardholderReceipt()
            {

            };
        }

        public async Task SendReceiptToMerchant(CardHolderTransactionRecordPurchase cardHolderTransactionRecord)
        {
           MerchantCopyReceipt merchantCopyReceipt = new MerchantCopyReceipt()
           {

           };
        }
    }
}
