



using System.Collections.ObjectModel;

namespace MonerisTest.ViewModels
{
    public partial class CustomersViewModel : ObservableObject
    {
        [ObservableProperty]
        List<Customer>? customers;


        private readonly PaymentContext? paymentContext;
      
        public CustomersViewModel(PaymentContext paymentContext)
        {

            try
            {
                
               this.paymentContext = paymentContext;
               
             
            }

            catch (Exception ex)
            {
                Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }

        }

       public  async Task  Init()
        {
            try
            {
                Customers = paymentContext?.Customers.ToList();

                if (Customers?.Count==0)
                {
                    // Add two customers

                    Customer customer1 = new Customer
                    {

                        Name = "Paramjit",
                        Email = "paramjit@someexample.com",
                        PhoneNumber = "+12345",
                        Address = "Whitby, Canada",
                    };

                    Customer customer2 = new Customer
                    {
                        Name = "Nithin",
                        Email = "nithin@daflo.com",
                        PhoneNumber = "+12345",
                        Address = "Whitby, Canada",
                    };

                var firstCustomer=   paymentContext?.Customers.Add(customer1);
                 var secondCustomer=   paymentContext?.Customers.Add(customer2);

                   await paymentContext.SaveChangesAsync();


                    Customers = paymentContext.Customers.ToList();

                }

             
            }
            catch (Exception ex)
            {
               await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }

       

        [RelayCommand]
        private async Task CustomerSelected(Customer customer)
        {
            try
            {
                await Shell.Current.GoToAsync($"{nameof(BookingPage)}", new Dictionary<string, object> { { "customerId", customer.CustomerId} });
            }
            catch (Exception ex)
            {

                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
