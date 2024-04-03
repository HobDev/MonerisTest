namespace MonerisTest.Pages;

public class CustomersPage : ContentPage
{

    enum CustomerRows
    {
       Top,
       Bottom
    }
    enum CustomerColumns
    {
        Left,
        Right
    }

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
                   new Label{Text="Customers",TextDecorations= TextDecorations.Underline,FontAttributes=FontAttributes.Bold, TextColor=Colors.Black, FontSize=20, HorizontalOptions=LayoutOptions.Center}.Margins(0,0,0,40),
                   new CollectionView
                   {
                       ItemsLayout= new LinearItemsLayout(ItemsLayoutOrientation.Vertical)
                       {
                           ItemSpacing=10
                       },



                       ItemTemplate= new DataTemplate(()=>
                       {
                         return  new Grid
                       {
                          RowDefinitions= Rows.Define(
                              (CustomerRows.Top, Auto),
                              (CustomerRows.Bottom, Auto)
                              ),

                          ColumnDefinitions= Columns.Define(
                              (CustomerColumns.Left, Star),
                              (CustomerColumns.Right, Star)
                              ),

                          ColumnSpacing=10,

                          Children=
                           {
                               new Label{ FontAttributes=FontAttributes.Bold, TextColor=Colors.Black, FontSize=25, HorizontalOptions=LayoutOptions.Center}.Bind(Label.TextProperty, nameof(Customer.Name)).Row(CustomerRows.Top).Column(CustomerColumns.Left).ColumnSpan(2),

                               new Button{Text="Initiate Payment", BackgroundColor=Colors.Blue, TextColor=Colors.White, FontSize=12, FontAttributes=FontAttributes.Bold}.BindCommand(nameof(viewModel.InitiatePaymentCommand), source:viewModel).Row(CustomerRows.Bottom).Column(CustomerColumns.Left),

                              new Button{Text="View Transactions", BackgroundColor=Colors.Blue, TextColor=Colors.White, FontSize=12, FontAttributes=FontAttributes.Bold}.BindCommand(nameof(viewModel.ViewTransactionsCommand), source:viewModel).Row(CustomerRows.Bottom).Column(CustomerColumns.Right),

                       },

                           };
                   }),

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