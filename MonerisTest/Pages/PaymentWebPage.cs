

namespace MonerisTest.Pages;

public class PaymentWebPage : ContentPage
{
    public PaymentWebPage(PaymentWebViewModel viewModel)
    {
        try
        {
            HybridWebView.HybridWebView hybridWebView = new()
            {
                HybridAssetRoot = "hybrid_root",
                MainFile = "index.html",
                JSInvokeTarget = new MyJSInvokeTarget(this)
            };

            CheckBox checkBox = new();
            checkBox.SetBinding(CheckBox.IsCheckedProperty, nameof(viewModel.SaveCard));

            Content = new VerticalStackLayout
            {
                Spacing = 20,
                Margin = new Thickness(20, 30, 20, 0),
                Children =
            {
                hybridWebView,

                new HorizontalStackLayout
                {
                    new Label{Text="Save Card for future purchases", TextColor= Colors.Black, VerticalOptions=LayoutOptions.Center},
                     checkBox,
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

   

    private sealed class MyJSInvokeTarget
    {
        private readonly PaymentWebPage _mainPage;

        public MyJSInvokeTarget(PaymentWebPage mainPage)
        {
            _mainPage = mainPage;
        }

        public static async void CallMeFromScript(string? dataKey, string? bin)
        {
            try
            {
                // Handle the message received from JavaScript

                if (dataKey != null)
                {
                    // await Shell.Current.GoToAsync($"//PaymentPage?DataKey={monerisTokenResponse.Data_key}");
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        WeakReferenceMessenger.Default.Send(new TokenMessage(dataKey));
                    });

                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
             
            }
           

        }
    }
}