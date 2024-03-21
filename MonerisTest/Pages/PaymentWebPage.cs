

using System.Collections.Generic;
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

        public static async void CallMeFromScript(string? dataKey, string? bin, string? errorMessage, JsonElement? respData)
        {
            // Handle the message received from JavaScript
            try
            {

                if (!string.IsNullOrWhiteSpace(errorMessage))
                {
                   
                    if (respData?.ValueKind == JsonValueKind.Object)
                    {                  
                            // UTF-8 encoding of responseCode
                            byte[]? responseDataBytes = Encoding.UTF8.GetBytes(respData?.GetRawText());

                            // responseCodeBytes to MemoryStream    
                            using MemoryStream responseDataStream = new(responseDataBytes);

                            //Convert string to object
                            dynamic? myDeserializedClass = await System.Text.Json.JsonSerializer.DeserializeAsync<dynamic>(responseDataStream);

                        // convert myDeserializedClass.responseCode to stream
                        using MemoryStream responseCodeStream = new(Encoding.UTF8.GetBytes(myDeserializedClass.responseCode));

                        MonerisHostedTokenizationResponse? monerisTokenResponse = await System.Text.Json.JsonSerializer.DeserializeAsync<MonerisHostedTokenizationResponse>(responseCodeStream);    

                        

                        // split the responseCodeValue by comma
                     
                                
                              //  string[]? responseCodeArray = responseCodeValue.Split(','); 
                             

                                //if responseCodeArray is not null
                                //if (responseCodeArray != null)
                                //{
                                //    //if responseCodeArray is not empty
                                //    if (responseCodeArray.Length > 0)
                                //    {

                                //        foreach (var item in responseCodeArray)
                                //        {
                                //            string message = string.Empty;
                                //            int count = 0;
                                //            if (!string.IsNullOrWhiteSpace(item))
                                //            {
                                //                if (count == 0)
                                //                {
                                //                    message = item;
                                //                    count++;
                                //                }
                                //                else
                                //                {
                                //                    message = message + "," + item;
                                //                }


                                //            }

                                //        }
                                //    }
                                //}
                            }

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