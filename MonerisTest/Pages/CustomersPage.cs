namespace MonerisTest.Pages;

public class CustomersPage : ContentPage
{

    CustomersViewModel viewModel;
	public CustomersPage(CustomersViewModel viewModel)
	{
		try
		{
            this.viewModel = viewModel;
            Content = new VerticalStackLayout
            {
                Children = 
                {
                   new CollectionView
                   {
                       ItemTemplate= new DataTemplate(()=> new Label
                       {
                          TextColor=Colors.Black ,
                       }.Bind(Label.TextProperty, nameof(Customer.Name))),

                   }.Bind(ItemsView.ItemsSourceProperty, nameof(viewModel.Customers))
                }
            
            };

			BindingContext = viewModel;

            Loaded += CustomersPage_Loaded;
        }
		catch (Exception ex)
		{

			Shell.Current.DisplayAlert("Error", ex.Message, "OK");
		}
		
	}

    private async void CustomersPage_Loaded(object? sender, EventArgs e)
    {
       await viewModel.Init();  
    }
}