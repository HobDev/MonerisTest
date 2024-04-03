

namespace MonerisTest.Services.Interfaces
{
    public interface IPaymentResponseHelper
    {

        Task<string?> GetTransactionType(string transType);

        Task<string?> GetCardType(string cardType);

       
    }
}
