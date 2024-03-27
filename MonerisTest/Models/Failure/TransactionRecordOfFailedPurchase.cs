

namespace MonerisTest.Models
{
    public partial class TransactionRecordOfFailedPurchase : IRealmObject
    {

        public string CustomerId { get; set; } = System.Guid.NewGuid().ToString();
    }
}
