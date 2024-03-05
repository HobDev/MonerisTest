

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using MonerisTest.Services.Interfaces;

namespace MonerisTest.ViewModels
{
   public partial class PaymentWebViewModel: ObservableObject
    {
        [ObservableProperty]
        bool saveCard;

       
        string? tempToken;

       
        string? permanentToken;


        private readonly IConvertTempToPermanentTokenService convertTempToPermanentTokenService;
        private readonly IPurchaseService purchaseService;

        public PaymentWebViewModel(IConvertTempToPermanentTokenService convertTempToPermanentTokenService, IPurchaseService purchaseService)
        {

            this.convertTempToPermanentTokenService = convertTempToPermanentTokenService;
            this.purchaseService = purchaseService;

            saveCard = false;

            WeakReferenceMessenger.Default.Register<string>(this, (sender, message) =>
            {
                // Handle the message
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    string? tempToken = message;
                    if (tempToken != null)
                    {
                       if(SaveCard)
                        {
                            await GetPermanentToken();
                        }
                       else
                        {
                          await  CompletePurchase(tempToken);
                        }
                    }
                });
            });
        }

        private async Task CompletePurchase(string token)
        {
            purchaseService.Purchase(token);
        }

        async Task GetPermanentToken()
        {
            if (tempToken != null)
            {
                permanentToken = await convertTempToPermanentTokenService.SaveTokenToVault(tempToken);
                if(permanentToken != null)
                {
                  await  CompletePurchase(permanentToken);
                }
            }

        }

    }
}
