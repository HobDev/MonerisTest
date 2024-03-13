


namespace MonerisTest
{
    public class AppShell : Shell
    {
        public AppShell()
        {
           
            Routing.RegisterRoute(nameof(BookingPage), typeof(BookingPage));
            Routing.RegisterRoute(nameof(CancellationPage), typeof(CancellationPage));
            Routing.RegisterRoute(nameof(CustomersPage), typeof(CustomersPage));
            Routing.RegisterRoute(nameof(PaymentWebPage), typeof(PaymentWebPage));

            Items.Add( new TabBar
            {
                Items =
                {
                    new Tab
                    {
                        Items =
                        {
                            new ShellContent
                            {
                                Title = "Customers",
                                ContentTemplate = new DataTemplate(typeof(CustomersPage))
                            }
                        }
                    }
                }
            });

            

        }
    }
}
