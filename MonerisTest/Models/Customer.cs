

namespace MonerisTest.Models
{
    public partial class Customer : IRealmObject
    {

        public string CustomerId { get; set; } = System.Guid.NewGuid().ToString();

        public string? Name { get; set; }
       

        public string? Email { get; set; }
       

        public string? PhoneNumber { get; set; }


        public string? Address { get; set; }
       
        // embedded objects
        // the PaymentCards property is a collection of PaymentCard and have a one-to-many parent-child relationship between Customer and PaymentCard
        public IList<PaymentCard> SavedPaymentCards { get;}

        // backlinks 

        [Backlink(nameof(RecordOfSuccessfulTransaction.Buyer))]
        public IQueryable<RecordOfSuccessfulTransaction> RecordOfScuccessfulTransactions { get; }


       
       
    }
}
