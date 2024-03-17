

namespace MonerisTest.Models
{
    public class PaymentCard
    {
        public int PaymentCardId { get; set; }
        public string? PermanentToken { get; set; }
        public string? MaskedCardNumber { get; set; }
        public string? CardExpiryDate { get; set; }

        // the CustomerId and Customer properties relate back to the parent Customer object for the instance of the PaymentCard
        public int CustomerId { get; set; } 
        public Customer TheCustomer { get; set; }
    }
}
