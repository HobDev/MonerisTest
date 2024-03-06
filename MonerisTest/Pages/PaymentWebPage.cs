using CommunityToolkit.Mvvm.Messaging;
using HybridWebView;
using MonerisTest.ViewModels;
using System.Text.Json.Serialization;

namespace MonerisTest.Pages;

public class PaymentWebPage : ContentPage
{
    public PaymentWebPage(PaymentWebViewModel viewModel)
    {
        try
        {
            HybridWebView.HybridWebView hybridWebView = new HybridWebView.HybridWebView
            {
                HybridAssetRoot = "hybrid_root",
                MainFile = "index.html"

            };


            hybridWebView.JSInvokeTarget = new MyJSInvokeTarget(this);

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
                     new CheckBox{},
                }

            }
            };



            BindingContext = viewModel;
        }
        catch (Exception ex)
        {

           
        }
        
    }

   

    private sealed class MyJSInvokeTarget
    {
        private PaymentWebPage _mainPage;

        public MyJSInvokeTarget(PaymentWebPage mainPage)
        {
            _mainPage = mainPage;
        }

        public async void CallMeFromScript(string? dataKey, string? bin)
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

             
            }
           

        }
    }
}