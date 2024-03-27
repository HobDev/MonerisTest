namespace MonerisTest.Models.Failure
{
    public partial class TransactionRecordOfFailedRefund : IRealmObject
    {
        public string CustomerId { get; set; } = System.Guid.NewGuid().ToString();
    }
}
