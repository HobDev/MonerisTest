
namespace MonerisTest
{
    public class AppShell : Shell
    {
        public AppShell()
        {
            Routing.RegisterRoute(nameof(PaymentWebPage), typeof(PaymentWebPage));


            // pay for the booking by using Hosted tokenization. If the permanent token is saved use it to make a vault payment. If the permanent token is not saved, then first create a temporary token, and if customer allows convert it to a permanent token and then make a payment using the permanent token. If customer don't allow to save the permanent token, then make a payment using the temporary token.
            Items.Add(new ShellContent
            {
                Title = "booking",
                ContentTemplate = new DataTemplate(typeof(PaymentPage))
            });

            // first initiate the refund and then cancel the booking. The refund is done if the payment is already settled by the payment gateway. Both the payment and the refund will be shown in the bank statement of the customer.
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

            // charge convenience fee on every purchase. The convenience fee is charged by the merchant for providing the convenience of making the payment online. The convenience fee is charged by the merchant for providing the convenience of booking online. The purpose of the convenience fee is to cover the cost of the payment gateway and the cost of the convenience provided to the customer. The convenience fee is charged on the total amount of the booking.The convenience fee and the booking amount will be shown separately in the bank statement of the customer.
            Items.Add(new ShellContent
            {
                Title = "Convenience Fee",
                ContentTemplate = new DataTemplate(typeof(PaymentWebPage))
            });
        }
    }
}
