

namespace MonerisTest.Services.Interfaces.Failure
{
    public interface ICardVerificationFailure
    {
        Task SaveFailedCardVerificationData(Receipt receipt);
    }
}
