



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
                PurchaseData purchaseData = new PurchaseData
                        (
                            store_Id: AppConstants.STORE_ID,
                            api_Token: AppConstants.API_TOKEN,
                            token: token,
                            order_Id: Guid.NewGuid().ToString(),
                            amount: TotalAmount.ToString(),
                            cust_Id: null
                        );
                Receipt? receipt = await purchaseService.Purchase(purchaseData);
                await SavePurchaseData(receipt);
                await convenienceFeeService?.ChargeConvenienceFee(1);
            }
            catch (Exception ex)
            {

                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
          
        }

        private async Task SavePurchaseData(Receipt? receipt)
        {
            if (receipt == null)
            {
                return;
            }
            // Save the receipt data to the database
            string? dataKey = receipt.GetDataKey();
            string? receiptId = receipt.GetReceiptId();
            string? referenceNum = receipt.GetReferenceNum();
            string? responseCode = receipt.GetResponseCode();
            string? authCode = receipt.GetAuthCode();
            string? message = receipt.GetMessage();
            string? transDate = receipt.GetTransDate();
            string? transTime = receipt.GetTransTime();
            string? transType = receipt.GetTransType();
            string? Complete = receipt.GetComplete();
            string? transAmount = receipt.GetTransAmount();
            string? cardType = receipt.GetCardType();
            string? txnNumber = receipt.GetTxnNumber();
            string? timedOut = receipt.GetTimedOut();
            string? resSuccess = receipt.GetResSuccess();
            string? paymentType = receipt.GetPaymentType();
            string? isVisaDebit = receipt.GetIsVisaDebit();
            string? issuerId = receipt.GetIssuerId();

            string? cust_ID = receipt.GetResDataCustId();
            string? phone = receipt.GetResDataPhone();
            string? email = receipt.GetResDataEmail();
            string? note = receipt.GetResDataNote();
            string? masked_Pan = receipt.GetResDataMaskedPan();
            string? exp_Date = receipt.GetResDataExpdate();
            string? crypt_Type = receipt.GetResDataCryptType();
            string? avs_Street_Number = receipt.GetResDataAvsStreetNumber();
            string? avs_Street_Name = receipt.GetResDataAvsStreetName();
            string? avs_Zipcode = receipt.GetResDataAvsZipcode();
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
