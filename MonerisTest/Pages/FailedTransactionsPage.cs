namespace MonerisTest.Pages;

public class FailedTransactionsPage : ContentPage
{
	public FailedTransactionsPage(FailedTransactionsViewModel viewModel)
	{
		Content = new VerticalStackLayout
		{
			Children = {
				new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "Welcome to .NET MAUI!"
				}
			}
		};

		BindingContext = viewModel;
	}
}