



using System.Collections.ObjectModel;

namespace MonerisTest.ViewModels
{
    public partial class CustomersViewModel : ObservableObject
    {
        [ObservableProperty]
        List<Customer>? customers;

      
        public CustomersViewModel()
        {

            try
            {
                
               
             
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
                using PaymentContext paymentContext = new PaymentContext();
                Customers=paymentContext.Customers.ToList();
             
              

                if (Customers?.Count==0)
                {
                    // Add two customers

                    Customer customer1 = new Customer
                    {

                        Name = "Paramjit",
                        Email = "paramjit@someexample.com"
                    };

                    Customer customer2 = new Customer
                    {
                        Name = "Nithin",
                        Email = "nithin@daflo.com"
                    };

                var firstCustomer=   paymentContext.Customers.Add(customer1);
                 var secondCustomer=   paymentContext.Customers.Add(customer2);

                   await paymentContext.SaveChangesAsync(); 
            
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
