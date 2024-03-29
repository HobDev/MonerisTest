

namespace MonerisTest.Models.Failure
{
    public partial class RecordOfFailedTransaction: IRealmObject
    {
        private RecordOfFailedTransaction()
        {
        }   

        public RecordOfFailedTransaction(string customerId, string errorMessage, int transactionType)
        {
            CustomerId = customerId;
            ErrorMessage = errorMessage;
            TransactionType = transactionType;
        }

        public string ErrorMessage { get; set; }

        public string CustomerId { get; set; }

        public int TransactionType { get; set; }

    }
}
