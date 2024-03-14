

using System.Collections.ObjectModel;

namespace MonerisTest.ViewModels
{
    public partial class CustomersViewModel : ObservableObject
    {
        [ObservableProperty]
        List<Entity> customers;

        public CustomersViewModel()
        {

            try
            {

                Task.Run(Init);
            }

            catch (Exception ex)
            {
                Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }

        }

        async Task  Init()
        {
            try
            {
                customers= App.PaymentRepo.GetAll().Result;

                if(customers.Count==0)
                {
                    // Add two customers
                    customers.Add(
                        new Customer
                        {
                            Name = "John Doe",
                            Email = ""


                        });
                    customers.Add(new Customer
                    {
                        Name = "Jane Doe",
                        Email = ""

                    });
                }
            }
            catch (Exception ex)
            {
                Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
