
namespace MonerisTest.Services.Interfaces
{
    public interface ITransactionSuccessService
    {

        Task<string?> SaveSuccessfulTransactionData(Receipt receipt);
    }
}
