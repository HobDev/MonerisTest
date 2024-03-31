
namespace MonerisTest.Services.Interfaces
{
    public interface ITransactionSuccessService
    {

        Task SaveSuccessfulTransactionData(Receipt receipt);
    }
}
