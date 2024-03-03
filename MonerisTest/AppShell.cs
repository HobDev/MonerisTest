
namespace MonerisTest
{
    public class AppShell : Shell
    {
        public AppShell()
        {
            Routing.RegisterRoute(nameof(PaymentWebPage), typeof(PaymentWebPage));

          
            // pay for the booking
            Items.Add(new ShellContent
            {
                Title = "booking",
                ContentTemplate = new DataTemplate(typeof(PaymentPage))
            });

            // cancel the booking   
            Items.Add(new ShellContent
            {
                Title = "Cancellation",
                ContentTemplate = new DataTemplate(typeof(PaymentWebPage))
            });

            // refund the booking amount. the refund is done if the payment is already settled by the payment gateway. Both the payment and the refund will be shown in the bank statement of the customer.
            Items.Add(new ShellContent
            {
                Title = "Refund",
                ContentTemplate = new DataTemplate(typeof(PaymentWebPage))
            });

            // void the booking means cancel the payment after the payment is authorised but before the payment is settled  by the payment gateway. There is no record of the payment in the bank statement of the customer. Before initiating the void, check the status of the payment. If is it in the authorisation state, then only void can be initiated.
            Items.Add(new ShellContent
            {
                Title = "Void",
                ContentTemplate = new DataTemplate(typeof(PaymentWebPage))
            });

            // charge convenience fee   

            Items.Add(new ShellContent
            {
                Title = "Convenience Fee",
                ContentTemplate = new DataTemplate(typeof(PaymentWebPage))
            });
        }
    }
}
