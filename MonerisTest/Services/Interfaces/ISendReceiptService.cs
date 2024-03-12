

using MonerisTest.Models;

namespace MonerisTest.Services.Interfaces
{
    public interface ISendReceiptService
    {

        Task SendReceiptToCustomer(CardHolderTransactionRecordPurchase cardHolderTransactionRecord);

        Task SendReceiptToMerchant(CardHolderTransactionRecordPurchase cardHolderTransactionRecord);
    }
}
