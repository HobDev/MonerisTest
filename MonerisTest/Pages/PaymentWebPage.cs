

using Moneris;
using MonerisTest.Messages;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
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

        public  async void CallMeFromScript(string? dataKey, string? bin, string? errorMessage, JsonElement? responseCode)
        {
            // Handle the message received from JavaScript
            try
            {

                if (!string.IsNullOrWhiteSpace(errorMessage))
                {
                        // convert jsonElement to string[]
                        string[]? responseCodeArray = responseCode.Value.EnumerateArray().Select(x => x.GetString()).ToArray();

                            int count = 0;
                            string message = string.Empty;
                            foreach (var item in responseCodeArray)
                                {                                
                                    if (!string.IsNullOrWhiteSpace(item))
                                    {
                            string errorText = await GetErrorMessage(item);
                                    if (count == 0)
                                        {
                                            message = errorText;
                                            count++;
                                        }
                                        else
                                        {
                                            message = message + "\n\n" + errorText;
                                        }


                                    }
                            }

                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        WeakReferenceMessenger.Default.Send(new ErrorMessage(message));
                    });
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

        private static async Task<string> GetErrorMessage(string item)
        {
            string errorMessage = string.Empty; 
            
          switch(item)
            {
                case "940":
                     errorMessage = "Invalid profile id (on tokenization request)";               
                    break;

                case "941":
                     errorMessage = "Error generating token";
                    break;

                case "942":
                    errorMessage = "Invalid Profile ID, or source URL";
                    break;

                case "943":
                   errorMessage = "Card data is invalid (not numeric, fails mod10, we will remove spaces)";
                    break;

                case "944":
                    errorMessage = "Invalid expiration date (mmyy, must be current month or in the future)";
                    break;

                case "945":
                    errorMessage = "Invalid CVD data (not 3-4 digits)";
                    break;
            }

            return errorMessage;
        }
    }
}