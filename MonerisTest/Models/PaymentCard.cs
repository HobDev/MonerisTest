

namespace MonerisTest.Models
{
    public partial class PaymentCard : IEmbeddedObject
    {

        private PaymentCard()
        {
        }

        public PaymentCard(string permanentToken, string maskedCardNumber, string cardExpiryDate)
        {
            PermanentToken = permanentToken;
            MaskedCardNumber = maskedCardNumber;
            CardExpiryDate = cardExpiryDate;
        }

        public string PermanentToken { get; set; }
        public string MaskedCardNumber { get; set; }
        public string CardExpiryDate { get; set; }

      
    }
}
