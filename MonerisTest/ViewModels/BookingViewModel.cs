


namespace MonerisTest
{
    public partial class BookingViewModel: ObservableObject, IQueryAttributable
    {
        [ObservableProperty]
        Customer? purchaser;

        [ObservableProperty]
        List<PaymentCard>? paymentCards;

        [ObservableProperty]
        string? customerName;

        [ObservableProperty]
        decimal totalAmount;

        [ObservableProperty]
        string? cardType;

        [ObservableProperty]
        string? maskedCardNumber;

        private readonly PaymentContext? paymentContext;
        private readonly IPurchaseService? purchaseService;
        private readonly IConvenienceFeeService? convenienceFeeService;

        public BookingViewModel(PaymentContext  paymentContext, IPurchaseService purchaseService, IConvenienceFeeService convenienceFeeService)
        {
            try
            {
                this.paymentContext = paymentContext;
                this.purchaseService = purchaseService;
                this.convenienceFeeService = convenienceFeeService;

                CustomerName = Purchaser?.Name;
               

                TotalAmount = 1;
                CardType = Purchaser?.CardType;
                MaskedCardNumber = Purchaser?.MaskedCardNumber;


            }
            catch (Exception ex)
            {

                Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            }
          
        }



        [RelayCommand]
        private  async Task Purchase()
        {
            try
            {
                string? token = Purchaser?.CardToken;   
                if(token==null)
                {
                    await Shell.Current.GoToAsync($"{nameof(PaymentWebPage)}", new Dictionary<string, object> { { "customerId", Purchaser.CustomerId } });
                }
                else
                {
                    // Make a payment using the permanent token
                    await purchaseService.Purchase(token);
                    await convenienceFeeService.ChargeConvenienceFee(TotalAmount);    
                }
               
            }
            catch (Exception ex)
            {

               await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            }
           
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            // This method is called when the page is navigated to with a query string.
            // The query string is parsed into a dictionary and passed to this method.
             if (query.ContainsKey("customerId"))
            {
                if (query["customerId"] is string customerId)
                {
                    Purchaser = paymentContext?.Customers.FirstOrDefault(c => c.CustomerId == customerId); 
                    PaymentCards = Purchaser?.SavedPaymentCards;
                }
            }
        }
    }
}
