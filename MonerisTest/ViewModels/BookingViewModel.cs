

namespace MonerisTest
{
    public partial class BookingViewModel: ObservableObject
    {

        [ObservableProperty]
        decimal totalAmount;

        [ObservableProperty]
        string cardType;

        [ObservableProperty]
        string maskedCardNumber;

        public BookingViewModel()
        {

            Customer customer = new Customer()
            {
                Name = "John Doe",
                Email = "johndoe@example.com",
                PhoneNumber = "+12345",
                Address = "Whitby, Canada",
                MaskedCardNumber = "**** **** **** 4242",
                CardToken = "something",
                CardExpiryDate = "1224",
                CardType = "Visa Debit",
                CardHolderName = "John Doe",
                CardBankName = "Bank of Montreal"

            };
        }



        [RelayCommand]
        private async Task Purchase()
        {
          await Shell.Current.GoToAsync("PaymentWebPage");
        }

      

        


    }
}
