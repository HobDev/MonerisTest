

namespace MonerisTest.Pages;

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