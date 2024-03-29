namespace MonerisTest.Services.Interfaces
{
    public interface ITransactionFailureService
    {
        Task SaveFailedTransactionData(string customerId, string errorMessage, int transactionType);
    }
}
