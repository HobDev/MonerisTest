

using System.Text.Json;
using System.Text.Json.Serialization;

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
                 new Label{FontAttributes=FontAttributes.Bold,TextDecorations= TextDecorations.Underline, TextColor=Colors.Black, FontSize=20, HorizontalOptions=LayoutOptions.Center}.Bind(Label.TextProperty, nameof(viewModel.CustomerName)).Margins(0,0,0,40),

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

        public static async void CallMeFromScript(string? dataKey, string? bin, string? errorMessage, object? responseCode)
        {
            // Handle the message received from JavaScript
            try
            {
                if(responseCode!=null)
                {
                  
                    if(responseCode is string)
                    {

                    }
                    else 
                    {
                        // convert object to stream
                       
                        var convertAgain= (JsonValueKind)responseCode;    

                        //var responseCodeArray =await System.Text.Json.JsonSerializer.DeserializeAsync<JsonValueKind.Array>(responseCode);
                        //foreach (var item in convertAgain)
                        //{
                        //    if (item is string)
                        //    {
                        //        await Shell.Current.DisplayAlert("Error", item.ToString(), "Ok");
                        //    }
                        //}
                    }   
                }
               
               else if (!string.IsNullOrWhiteSpace(errorMessage))
                {
                   
                        await Shell.Current.DisplayAlert("Error", errorMessage, "Ok");
                  
                }

               else if (!string.IsNullOrWhiteSpace(dataKey))
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