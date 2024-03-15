

namespace MonerisTest.Pages
{
    public class ReceiptsPage: ContentPage
    {

        public ReceiptsPage(ReceiptsViewModel viewModel)
        {
            try
            {
                Title = "Receipts";
                Content = new StackLayout
                {
                    Children =
                    {
                        new Label
                        {
                            Text = "Receipts",
                            FontSize = 30,
                            HorizontalOptions = LayoutOptions.Center
                        }
                    }
                };
                BindingContext = viewModel;
            }
            catch (Exception ex)
            {
                Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            }
        }
    }
}
