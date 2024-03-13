﻿



namespace MonerisTest.ViewModels
{
   public partial class PaymentWebViewModel: ObservableObject
    {
        [ObservableProperty]
        bool saveCard;
   
        string? tempToken;
     
        string? permanentToken;


        //after getting temporary token the card verification is the first step in the payment process. The successful card verification returns an issuer ID. The issuer ID is use to save the permanent token in the Moneris account. The permanent token is used for current and future purchases.
        private readonly ICardVerificationService cardVerificationService;
        private readonly IPurchaseService purchaseService;
        private readonly IAddTokenService addTokenService;
        

        public PaymentWebViewModel(ICardVerificationService cardVerificationService, IPurchaseService purchaseService, IAddTokenService addTokenService)
        {
            try
            {
                this.cardVerificationService = cardVerificationService;
                this.purchaseService = purchaseService;
                this.addTokenService = addTokenService;


                saveCard = false;

                WeakReferenceMessenger.Default.Register<TokenMessage>(this, (sender, message) =>
                {
                    // Handle the message
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        tempToken = message.Value;
                        if (tempToken != null)
                        {

                            await VerifyCard();

                        }
                    });
                });
            }
            catch (Exception ex)
            {

               
            }
           
        }

        private async Task VerifyCard()
        {
            try
            {
                string? issuerId = await cardVerificationService.VerifyPaymentCard(tempToken);
                if (issuerId != null)
                {
                    if (SaveCard)
                    {
                        await GetPermanentToken(issuerId);
                    }
                    else
                    {
                        await CompletePurchase(tempToken);
                    }
                }
            }
            catch (Exception ex)
            {

               await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        
        }

        private async Task CompletePurchase(string token)
        {
            try
            {
                await purchaseService.Purchase(token);
            }
            catch (Exception ex)
            {

                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
          
        }

        async Task GetPermanentToken(string issuerId)
        {
            try
            {
                if (tempToken != null)
                {
                    permanentToken = await addTokenService.SaveTokenToVault(issuerId, tempToken);
                    if (permanentToken != null)
                    {
                        await CompletePurchase(permanentToken);
                    }
                    else
                    {
                        await CompletePurchase(tempToken);
                    }
                }
            }
            catch (Exception ex)
            {

                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
           

        }

    }
}
