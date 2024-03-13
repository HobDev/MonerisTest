

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
                new Label{FontSize=30, FontAttributes=FontAttributes.Bold, HorizontalOptions=LayoutOptions.Center}.Bind(Label.TextProperty, nameof(viewModel.TotalAmount), stringFormat:"Amount Payable {0}"),
                new Label{FontSize=20, FontAttributes=FontAttributes.Bold, HorizontalOptions=LayoutOptions.Center}.Bind(Label.TextProperty, nameof(viewModel.CardType)),
                new Label{FontSize=20, FontAttributes=FontAttributes.Bold, HorizontalOptions=LayoutOptions.Center}.Bind(Label.TextProperty, nameof(viewModel.MaskedCardNumber)),
                new Button{Text="PurChase", Command= viewModel.PurchaseCommand}.Bind(Button.TextProperty,nameof(viewModel.TotalAmount), stringFormat: "${0} - Pay Now"),
            
               
            }
        };

        BindingContext = viewModel;
    }
}