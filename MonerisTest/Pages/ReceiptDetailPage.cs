namespace MonerisTest.Pages;

public class ReceiptDetailPage : ContentPage
{
	public ReceiptDetailPage(ReceiptDetailViewModel viewModel)
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