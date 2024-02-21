
namespace MonerisTest
{
    public class AppShell : Shell
    {
        public AppShell()
        {
            Routing.RegisterRoute(nameof(PaymentWebPage), typeof(PaymentWebPage));

           TabBar LoginTab = new TabBar
            {
                Items =
                {
                  new ShellContent {ContentTemplate=new DataTemplate(typeof(PaymentPage))}
                }
            };
            Items.Add(LoginTab);
        }
    }
}
