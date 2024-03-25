

namespace MonerisTest.Models.Failure
{
    public partial class RecordOfFailedTokenization: IRealmObject
    {
        private RecordOfFailedTokenization()
        {
        }   

        public RecordOfFailedTokenization(string customerId, string errorMessage)
        {
            CustomerId = customerId;
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; set; }

        public string CustomerId { get; set; }

    }
}
