using MonerisTest.ViewModels;

namespace MonerisTest.Pages;

public class CancellationPage : ContentPage
{
	public CancellationPage(CancellationViewModel viewModel)
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
}