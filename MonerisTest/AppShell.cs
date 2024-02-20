
namespace MonerisTest
{
    public class AppShell : Shell
    {
        public AppShell()
        {

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
