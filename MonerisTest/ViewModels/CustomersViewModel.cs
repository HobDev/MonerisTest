
namespace MonerisTest.ViewModels
{
    public partial class CustomersViewModel : ObservableObject
    {
        [ObservableProperty]
        List<Customer>? customers;


        Realm realm;
      
        public CustomersViewModel()
        {

            try
            {

               realm = Realm.GetInstance();


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
               
                Customers = realm?.All<Customer>().ToList();

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

                    realm?.Write(() =>
                    {
                        realm.Add(customer1);
                        realm.Add(customer2);
                    });


                    Customers = realm?.All<Customer>().ToList();
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
                Dictionary<string, object> query = new Dictionary<string, object> { { "customerId", customer.CustomerId } };
                await Shell.Current.GoToAsync($"{nameof(BookingPage)}", query);
            }
            catch (Exception ex)
            {

                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
