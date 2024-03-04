namespace MonerisTest.Pages;

public class BookingPage : ContentPage
{
    public BookingPage(BookingViewModel viewModel)
    {
        Content = new VerticalStackLayout
        {
            Spacing = 20,

            Margin = new Thickness(20, 30, 20, 0),
            Children =
            {
               
                new Button{Text="PurChase", Command= viewModel.PurchaseCommand},
            
               
            }
        };

        BindingContext = viewModel;
    }
}