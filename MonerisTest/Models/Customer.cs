


using Realms;

namespace MonerisTest.Models
{
    public partial class Customer : IRealmObject
    {

        public int CustomerId { get; set; } 

        public string? Name { get; set; }
       

        public string? Email { get; set; }
       

        public string? PhoneNumber { get; set; }


        public string? Address { get; set; }
       
        // embedded objects
        // the PaymentCards property is a collection of PaymentCard and have a one-to-many parent-child relationship between Customer and PaymentCard
        public IList<PaymentCard> SavedPaymentCards { get;}

        // backlinks 
        [Backlink(nameof(PaymentReceipt.Purchaser))]
        public IQueryable<PaymentReceipt> PaymentReceipts { get; }

        [Backlink(nameof(TransactionRecordOfPurchase.Customer))]
        public IQueryable<TransactionRecordOfPurchase> TransactionRecordOfPurchases { get; }

        [Backlink(nameof(TransactionRecordOfConvenienceFee.Customer))]
        public IQueryable<TransactionRecordOfConvenienceFee> TransactionRecordOfConvenienceFees { get; }

        [Backlink(nameof(TransactionRecordOfRefund.Customer))]
        public IQueryable<TransactionRecordOfRefund> TransactionRecordOfRefunds { get; }
       
    }
}
