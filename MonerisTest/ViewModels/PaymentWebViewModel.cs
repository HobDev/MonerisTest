



using Moneris;

namespace MonerisTest.ViewModels
{
   public partial class PaymentWebViewModel: ObservableObject
    {
        [ObservableProperty]
        Customer? purchaser;

        [ObservableProperty]
        string? customerName;

        [ObservableProperty]
        bool saveCard;

        [ObservableProperty]
        decimal totalAmount;

        string? tempToken;
     
        string? permanentToken;


        //after getting temporary token the card verification is the first step in the payment process. The successful card verification returns an issuer ID. The issuer ID is use to save the permanent token in the Moneris account. The permanent token is used for current and future purchases.
        private readonly ICardVerificationService? cardVerificationService;
        private readonly IPurchaseService? purchaseService;
        private readonly IAddTokenService? addTokenService;
        private readonly IConvenienceFeeService? convenienceFeeService;

        private readonly PaymentContext? paymentContext;    
        

        public PaymentWebViewModel(ICardVerificationService cardVerificationService, IPurchaseService purchaseService, IAddTokenService addTokenService, PaymentContext paymentContext, IConvenienceFeeService convenienceFeeService)
        {
            try
            {
                this.cardVerificationService = cardVerificationService;
                this.purchaseService = purchaseService;
                this.addTokenService = addTokenService;
                this.convenienceFeeService = convenienceFeeService;


                this.paymentContext = paymentContext;

                CustomerName = Purchaser?.Name;

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

               Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
           
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            // This method is called when the page is navigated to with a query string.
            // The query string is parsed into a dictionary and passed to this method.
            if (query.ContainsKey("customerId"))
            {
                if (query["customerId"] is string customerId)
                {
                    Purchaser = paymentContext?.Customers.FirstOrDefault(c => c.CustomerId == customerId);
                }
            }
        }

        private async Task VerifyCard()
        {
            try
            {
                if(cardVerificationService==null )
                {
                    throw new Exception("Card Verification Service is not available");
                }
                else if(tempToken==null)
                {
                    throw new Exception("Temporary Token is not available");
                }   
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
                if(purchaseService==null)
                {
                    throw new Exception("Purchase Service is not available");
                }
                await purchaseService.Purchase(token);
                await convenienceFeeService?.ChargeConvenienceFee(1);
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
                    if(addTokenService==null)
                    {
                        throw new Exception("Add Token Service is not available");
                    }
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
