

namespace MonerisTest.Services.Interfaces
{
    public interface IPurchaseService
    {
        Task<Receipt?> Purchase(PurchaseData purchaseData);
    }
}
