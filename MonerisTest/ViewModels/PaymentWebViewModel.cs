﻿

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


        //after getting temporary token the card verification is the first step in the payment process. The successful card verification returns an issuer ID. The issuer ID is use to save the permanent token in the Moneris account. The permanent token is used for current and future purchases.
        private readonly ICardVerificationService cardVerificationService;
        private readonly IPurchaseService purchaseService;
        private readonly IAddTokenService addTokenService;
        

        public PaymentWebViewModel(ICardVerificationService cardVerificationService, IPurchaseService purchaseService, IAddTokenService addTokenService)
        {
            this.cardVerificationService = cardVerificationService;
            this.purchaseService = purchaseService;
            this.addTokenService = addTokenService;
            

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
                           await VerifyCard();
                        }
                       else
                        {
                          await  CompletePurchase(tempToken);
                        }
                    }
                });
            });
        }

        private async Task VerifyCard()
        {
            string expDate = "2212";
            await cardVerificationService.VerifyPaymentCard(tempToken, expDate);
        }

        private async Task CompletePurchase(string token)
        {
           await  purchaseService.Purchase(token);
        }

        async Task GetPermanentToken()
        {
            if (tempToken != null)
            {
                permanentToken = await addTokenService.SaveTokenToVault(tempToken);
                if(permanentToken != null)
                {
                  await  CompletePurchase(permanentToken);
                }
                else
                {
                    await CompletePurchase(tempToken);
                }
            }

        }

    }
}
