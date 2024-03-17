


namespace MonerisTest.Pages;

public class BookingPage : ContentPage
{
  
    enum CardColumns { CheckBoxColumn, CardInfoColumn }

    // pay for the booking by using Hosted tokenization. If the permanent token is saved use it to make a vault payment. If the permanent token is not saved, then first create a temporary token, and if customer allows convert it to a permanent token and then make a payment using the permanent token. If customer don't allow to save the permanent token, then make a payment using the temporary token.
    // charge convenience fee on every purchase. The convenience fee is charged by the merchant for providing the convenience of booking online. The purpose of the convenience fee is to cover the cost of the payment gateway and the cost of the convenience provided to the customer. The convenience fee is charged on the total amount of the booking.The convenience fee and the booking amount will be shown separately in the bank statement of the customer.
    public BookingPage(BookingViewModel viewModel)
    {
        try
        {

            Content = new VerticalStackLayout
            {
                Spacing = 20,

                Margin = new Thickness(20, 30, 20, 0),
                Children =
            {
                    new Label{FontAttributes=FontAttributes.Bold, TextDecorations= TextDecorations.Underline, TextColor=Colors.Black, FontSize=20, HorizontalOptions=LayoutOptions.Center}.Bind(Label.TextProperty, nameof(viewModel.CustomerName)).Margins(0,0,0,40),
                    new Label{Text="Saved Cards:", TextColor=Colors.Black}.Bind(Label.IsVisibleProperty, nameof(viewModel.PaymentCards),converter: new IsListNotNullOrEmptyConverter()),
                    new CollectionView
                    {
                       
                        ItemTemplate = new DataTemplate(() =>
                        {
                            
                            return new Grid
                            {
                                
                                ColumnDefinitions= Columns.Define
                                (
                                    (CardColumns.CheckBoxColumn,10),
                                    (CardColumns.CheckBoxColumn,Auto)
                                ),

                                Children=
                                {
                                 new CheckBox{}.Column(CardColumns.CheckBoxColumn),
                               new Label{}.Bind(Label.TextProperty, nameof(PaymentCard.MaskedCardNumber)).Column(CardColumns.CardInfoColumn),
                                }
                            };
                           
                           
                        }),
                      
                    }.Bind(ItemsView.ItemsSourceProperty, nameof(viewModel.PaymentCards)),
                new Label{FontSize=20,TextColor=Colors.Black, FontAttributes=FontAttributes.Bold, HorizontalOptions=LayoutOptions.Center}.Bind(Label.TextProperty, nameof(viewModel.TotalAmount), stringFormat:"Amount Payable ${0}"),
                new Button{Text="PurChase", Command= viewModel.PurchaseCommand}.Bind(Button.TextProperty,nameof(viewModel.TotalAmount), stringFormat: "${0} - Pay Now"),


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