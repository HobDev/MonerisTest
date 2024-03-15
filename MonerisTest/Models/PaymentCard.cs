

namespace MonerisTest.Models
{
    public class PaymentCard
    {
        public string? CardToken { get; set; }
        public string? MaskedCardNumber { get; set; }
        public string? CardExpiryDate { get; set; }
        public string? CardType { get; set; }
        public string? CardHolderName { get; set; }
        public string? CardBankName { get; set; }
        public byte[]? CardLogo { get; set; }


        // the CustomerId and Customer properties relate back to the parent Customer object for the instance of the PaymentCard
        public int CustomerId { get; set; } 
        public Customer? Customer { get; set; }
    }
}
