

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
                    ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Vertical)
                    {
                        ItemSpacing = 15
                    },
                    ItemTemplate = new DataTemplate(() =>
                    {
                        return new HorizontalStackLayout
                        {
                            Spacing = 15,
                            Children =
                            {
                                new Label { FontSize = 15, FontAttributes = FontAttributes.Bold, TextColor = Colors.Black }.Bind(Label.TextProperty,nameof(RecordOfSuccessfulTransaction.Transaction_DateTime), stringFormat:"Date: {0:yyyy MM dd }"),
                                new Label { FontSize = 15, FontAttributes = FontAttributes.Bold, TextColor = Colors.Black }.Bind(Label.TextProperty,nameof(RecordOfSuccessfulTransaction.Transaction_DateTime), stringFormat:"Time: {0:HH:MM:ss }"),
                            }
                        };

                    }),


                }.Bind(ItemsView.ItemsSourceProperty, nameof(viewModel.BookingInvoices)).Bind(SelectableItemsView.SelectedItemProperty, nameof(viewModel.SelectedTransaction)).Bind(SelectableItemsView.SelectionChangedCommandProperty,nameof(viewModel.TransactionSelectedCommand), source:viewModel) ;
                BindingContext = viewModel;
            }
            catch (Exception ex)
            {
                Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            }
        }
    }
}
