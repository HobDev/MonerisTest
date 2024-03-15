﻿


namespace MonerisTest
{
    public partial class BookingViewModel: ObservableObject, IQueryAttributable
    {

        [ObservableProperty]
        decimal totalAmount;

        [ObservableProperty]
        string? cardType;

        [ObservableProperty]
        string? maskedCardNumber;

        public BookingViewModel()
        {
            try
            {
                Customer customer = new()
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

                TotalAmount = 1;
                CardType = customer.CardType;
                MaskedCardNumber = customer.MaskedCardNumber;


            }
            catch (Exception ex)
            {

                Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            }
          
        }



        [RelayCommand]
        private static async Task Purchase()
        {
            try
            {
                await Shell.Current.GoToAsync("PaymentWebPage");
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
                   
                }
            }
        }
    }
}
