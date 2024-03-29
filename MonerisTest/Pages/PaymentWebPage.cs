
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
                    new ImageButton{ Source="close_black", HorizontalOptions=LayoutOptions.Start, WidthRequest=35, HeightRequest=35}.BindCommand(nameof(viewModel.CancelPaymentCommand)),

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

                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                         WeakReferenceMessenger.Default.Send(new ErrorMessage(message));
                    });
                }

              

                else if (!string.IsNullOrWhiteSpace(dataKey))
                {
                    
                    MainThread.BeginInvokeOnMainThread(() =>
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
                     errorMessage = "940 - Invalid profile id (on tokenization request)";               
                    break;

                case "941":
                     errorMessage = "941 - Error generating token";
                    break;

                case "942":
                    errorMessage = "942 - Invalid Profile ID, or source URL";
                    break;

                case "943":
                   errorMessage = "943 - Card data is invalid (not numeric, fails mod10, we will remove spaces)";
                    break;

                case "944":
                    errorMessage = "944 - Invalid expiration date (mmyy, must be current month or in the future)";
                    break;

                case "945":
                    errorMessage = "945 - Invalid CVD data (not 3-4 digits)";
                    break;
            }

            return errorMessage;
        }
    }
}