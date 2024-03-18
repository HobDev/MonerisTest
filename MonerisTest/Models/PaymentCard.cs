

using Realms;

namespace MonerisTest.Models
{
    public partial class PaymentCard : IEmbeddedObject
    {
        public int PaymentCardId { get; set; }
        public string? PermanentToken { get; set; }
        public string? MaskedCardNumber { get; set; }
        public string? CardExpiryDate { get; set; }

      
    }
}
