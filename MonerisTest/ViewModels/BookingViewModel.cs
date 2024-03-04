

using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MonerisTest.Services.Interfaces;

namespace MonerisTest
{
    public partial class BookingViewModel
    {
    
        string? tempToken;
        string? permanentToken;

        private readonly IGetTempTokenService getTempTokenService;
      
        private readonly IPurchaseService purchaseService;     
              
       

        public BookingViewModel(IPurchaseService purchaseService,IGetTempTokenService getTempTokenService)
        {

            this.getTempTokenService = getTempTokenService;
            this.purchaseService = purchaseService;          


            WeakReferenceMessenger.Default.Register<string>(this, (sender, message) =>
            {
                // Handle the message
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    string? tempToken = message;
                    if (tempToken != null)
                    {
                        await GetPermanentToken(); 
                    }
                });
            });

           
        }



        [RelayCommand]
        private async Task Purchase()
        {
          //  await purchaseService.Purchase();
          string? tempToken = await getTempTokenService.GetTempToken(); 
            
        }

       async Task GetPermanentToken()
        {
            if (tempToken != null)
            {
                permanentToken = await convertTempToPermanentTokenService.SaveTokenToVault(tempToken);
            }
          
        }


        


    }
}
