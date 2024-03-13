namespace MonerisTest.Pages;

public class CustomersPage : ContentPage
{
	public CustomersPage(CustomersViewModel viewModel)
	{
		try
		{
            Content = new VerticalStackLayout
            {
                Children = {
                new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "Welcome to .NET MAUI!"
                }
            }
            };
        }
		catch (Exception ex)
		{

			Shell.Current.DisplayAlert("Error", ex.Message, "OK");
		}
		
	}
}