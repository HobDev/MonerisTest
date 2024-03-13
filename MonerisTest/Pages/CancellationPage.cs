

namespace MonerisTest.Pages;

// first initiate the refund and then cancel the booking. The refund is done if the payment is already settled by the payment gateway. Both the payment and the refund will be shown in the bank statement of the customer.
public class CancellationPage : ContentPage
{
	public CancellationPage(CancellationViewModel viewModel)
	{
        try
        {
            Content = new VerticalStackLayout
            {
                Spacing = 20,

                Margin = new Thickness(20, 30, 20, 0),
                Children =
            {

                new Button{Text="Refund", Command=viewModel.RefundCommand},

            }
            };

            BindingContext = viewModel;
        }
        catch (Exception ex)
        {

            Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }

        
    }
}