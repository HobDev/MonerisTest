

namespace MonerisTest.Services.Interfaces
{
    public interface ICardVerificationService
    {
        Task<Receipt?> VerifyPaymentCard(string tempToken);
    }
}
