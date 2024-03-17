


namespace MonerisTest.Models
{
    public class Customer
    {


        public int CustomerId { get; set; } 

        public string? Name { get; set; }
       

        public string? Email { get; set; }
       

        public string? PhoneNumber { get; set; }


        public string? Address { get; set; }
       

        // the PaymentCards property is a collection of PaymentCard and have a one-to-many parent-child relationship between Customer and PaymentCard
        public List<PaymentCard>? SavedPaymentCards { get; set; }    = new List<PaymentCard>();
       
    }
}
