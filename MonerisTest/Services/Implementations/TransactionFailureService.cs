using MonerisTest.Models.Failure;

namespace MonerisTest.Services.Implementations
{
    public class TransactionFailureService : ITransactionFailureService
    {

        public async Task SaveFailedTransactionData(string customerId, string errorMessage, int transactionType)
        {

            RecordOfFailedTransaction recordOfFailedTransaction = new RecordOfFailedTransaction
            (
                customerId : customerId,
                errorMessage : errorMessage,
                transactionType : transactionType
            );

            Realm realm = Realm.GetInstance();
            // Save the failed card verification data to the database
            await realm.WriteAsync(() =>
            {
                realm.Add(recordOfFailedTransaction);
            });

        }
    }
}
