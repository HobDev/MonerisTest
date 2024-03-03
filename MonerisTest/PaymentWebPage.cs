using CommunityToolkit.Mvvm.Messaging;
using HybridWebView;
using System.Text.Json.Serialization;

namespace MonerisTest;

public class PaymentWebPage : ContentPage
{
	public PaymentWebPage()
	{
       
		HybridWebView.HybridWebView hybridWebView =new HybridWebView.HybridWebView
		{
           HybridAssetRoot = "hybrid_root",
           MainFile = "index.html"
		   
        };
        hybridWebView.RawMessageReceived += HybridWebView_RawMessageReceived;

      
       hybridWebView.JSInvokeTarget = new MyJSInvokeTarget(this);

      
		Content = hybridWebView;    
	}

    private async void HybridWebView_RawMessageReceived(object? sender, HybridWebViewRawMessageReceivedEventArgs e)
    {
        // Handle the message received from JavaScript
        await Dispatcher.DispatchAsync(async () =>
        {
             string? monerisToken = e.Message;
            if (monerisToken != null)
            {
               // await Shell.Current.GoToAsync($"//PaymentPage?DataKey={monerisTokenResponse.Data_key}");
               
            }
        });
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
            // Handle the message received from JavaScript
           
                if (dataKey != null)
                {
                    // await Shell.Current.GoToAsync($"//PaymentPage?DataKey={monerisTokenResponse.Data_key}");
                    MainThread.BeginInvokeOnMainThread(async() =>
                    {
                        WeakReferenceMessenger.Default.Send(new TokenMessage(dataKey));
                    });

                }
         
        }
    }
}