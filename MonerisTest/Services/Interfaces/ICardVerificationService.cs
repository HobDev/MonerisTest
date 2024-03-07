

namespace MonerisTest.Services.Interfaces
{
    public interface ICardVerificationService
    {
        Task<string?> VerifyPaymentCard(string tempToken);
    }
}
