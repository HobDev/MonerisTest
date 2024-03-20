

namespace MonerisTest.Services.Interfaces
{
    public interface IReceiptErrorMessageService
    {

        Task<string?> GetErrorMessage(Receipt receipt);
    }
}
