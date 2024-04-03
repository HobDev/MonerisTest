

namespace MonerisTest.Pages
{
    public class TransactionsPage: ContentPage
    {

        public TransactionsPage(TransactionsViewModel viewModel)
        {
            try
            {
                Title = viewModel.CustomerName;
                Content = new CollectionView
                {
                    SelectionMode = SelectionMode.Single,
                    ItemTemplate = new DataTemplate(() =>
                    {
                        return new VerticalStackLayout
                        {
                            Children=
                            {
                                new Label { FontSize = 15, FontAttributes = FontAttributes.Bold, TextColor = Colors.Black }.Bind(Label.TextProperty,nameof(RecordOfSuccessfulTransaction.Transaction_DateTime), stringFormat:"Date: {0:YYYY MM dd }"),
                                new Label { FontSize = 15, FontAttributes = FontAttributes.Bold, TextColor = Colors.Black }.Bind(Label.TextProperty,nameof(RecordOfSuccessfulTransaction.Transaction_DateTime), stringFormat:"Time: {0:HH:MM:SS }"),
                            }
                        };
                        
                    }),

                   
                }.Bind(ItemsView.ItemsSourceProperty, nameof(viewModel.BookingInvoices));
                BindingContext = viewModel;
            }
            catch (Exception ex)
            {
                Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            }
        }
    }
}
