namespace MonerisTest.Pages;

public class CustomersPage : ContentPage
{

    CustomersViewModel? viewModel;
	public CustomersPage(CustomersViewModel viewModel)
	{
		try
		{
            this.viewModel = viewModel;
            Content = new VerticalStackLayout
            {
                Margin = new Thickness(20),
                Children =
                {
                   new Label{Text="Customers", TextColor=Colors.Black, FontSize=20, HorizontalOptions=LayoutOptions.Center}.Margins(0,0,0,20),
                   new CollectionView
                   {
                       ItemsLayout= new LinearItemsLayout(ItemsLayoutOrientation.Vertical)
                       {
                           ItemSpacing=10
                       },
                      
                       ItemTemplate= new DataTemplate(()=> new Button
                       {
                           TextColor=Colors.Black ,
                           BackgroundColor=Colors.LightGray,
                           FontSize=20,
                       }.Bind(Button.TextProperty, nameof(Customer.Name)).BindCommand(nameof(viewModel.CustomerSelectedCommand), source:viewModel)),


                   }.Bind(ItemsView.ItemsSourceProperty, nameof(viewModel.Customers)),   
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