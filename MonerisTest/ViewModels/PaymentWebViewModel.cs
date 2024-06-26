﻿

namespace MonerisTest.ViewModels
{
    public partial class PaymentWebViewModel: ObservableObject, IQueryAttributable
    {
        [ObservableProperty]
        Customer? purchaser;

        [ObservableProperty]
        string? customerName;

        [ObservableProperty]
        bool saveCard;

        [ObservableProperty]
        string totalAmount;

        string? tempToken;
     
        string? permanentToken;

        Realm realm;


        //after getting temporary token the card verification is the first step in the payment process. The successful card verification returns an issuer ID. The issuer ID is use to save the permanent token in the Moneris account. The permanent token is used for current and future purchases.
        private readonly ICardVerificationService? cardVerificationService;
        private readonly IPurchaseService? purchaseService;
        private readonly IAddTokenService? addTokenService;
        private readonly IReceiptErrorMessageService? receiptErrorMessageService;
        private readonly ITransactionFailureService? transactionFailureService;
        private readonly ITransactionSuccessService? transactionSuccessService;
       
       

        public PaymentWebViewModel(ICardVerificationService cardVerificationService, IPurchaseService purchaseService, IAddTokenService addTokenService,  IReceiptErrorMessageService receiptErrorMessageService, ITransactionFailureService transactionFailureService, ITransactionSuccessService transactionSuccessService)
        {
            try
            {
                this.cardVerificationService = cardVerificationService;
                this.purchaseService = purchaseService;
                this.addTokenService = addTokenService;
                this.receiptErrorMessageService = receiptErrorMessageService;
                this.transactionFailureService = transactionFailureService;
                this.transactionSuccessService = transactionSuccessService;
               

                realm= Realm.GetInstance();

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

                WeakReferenceMessenger.Default.Register<ErrorMessage>(this, (sender, message) =>
                {
                    // Handle the message
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                       
                        if (!string.IsNullOrWhiteSpace(message.Value))
                        {

                            Dictionary<string, object> errorDictionary = new Dictionary<string, object> {{ "customerId", Purchaser.CustomerId }, { "errorMessage", message.Value } };

                            await transactionFailureService.SaveFailedTransactionData(Purchaser.CustomerId, message.Value, (int)TransactionType.Tokenization);
                            await Shell.Current.GoToAsync(nameof(BookingPage), errorDictionary);
                         
                        }
                    });
                });


            }
            catch (Exception ex)
            {

               Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
           
        }

        [RelayCommand]
        public async Task CancelPayment()
        {
            try
            {
                Dictionary<string, object> query = new Dictionary<string, object> { { "customerId", Purchaser.CustomerId } };
                await Shell.Current.GoToAsync($"{nameof(BookingPage)}", query);
            }
            catch (Exception ex)
            {

                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            // This method is called when the page is navigated to with a query string.
            // The query string is parsed into a dictionary and passed to this method.
            if (query.TryGetValue("customerId", out object? value))
            {
                if (value is string customerId)
                {
                    Purchaser = realm.All<Customer>().FirstOrDefault(c => c.CustomerId == customerId);
                    CustomerName = Purchaser?.Name;
                   
                }
            }
            if(query.TryGetValue("amount", out object? secondValue))
            {
                if(secondValue is string amount)
                {
                    TotalAmount = amount;
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

              Receipt? receipt= await cardVerificationService.VerifyPaymentCard(tempToken);

                string? errorMessage = await receiptErrorMessageService?.GetErrorMessage(receipt);
                if (errorMessage != null)
                {
                    await transactionFailureService.SaveFailedTransactionData(Purchaser.CustomerId,errorMessage, (int)TransactionType.CardVerification);
                    await Shell.Current.DisplayAlert("Card Verification Failed", errorMessage, "OK");
                }
                else
                {
                    string? issuerId = receipt?.GetIssuerId();
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
                           cust_Id: Purchaser.CustomerId
                       );
                Receipt? receipt = await purchaseService.Purchase(purchaseData);
                string? errorMessage = await receiptErrorMessageService?.GetErrorMessage(receipt);
                if (errorMessage != null)
                {
                    await transactionFailureService.SaveFailedTransactionData(Purchaser.CustomerId, errorMessage, (int)TransactionType.Purchase);
                    await Shell.Current.DisplayAlert("Purchase failed", errorMessage, "OK");
                }
                else
                {
                  string? transactionId=  await transactionSuccessService.SaveSuccessfulTransactionData(receipt, Purchaser);
                    if(transactionId!=null)
                    {
                       
                        await Shell.Current.DisplayAlert("Payment Successful", " you will get the receipt in an email soon", "OK");
                       Dictionary<string, object> query= new Dictionary<string, object> { { "customerId", Purchaser.CustomerId }, { "transactionId", transactionId } };
                        await Shell.Current.GoToAsync(nameof(TransactionDetailPage), query);
                    }
                
                  
                }
               
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
                     Receipt? receipt= await addTokenService.SaveTokenToVault(issuerId, tempToken);
                    string? errorMessage = await receiptErrorMessageService?.GetErrorMessage(receipt);
                    if (errorMessage != null)
                    {
                        await transactionFailureService.SaveFailedTransactionData(Purchaser.CustomerId, errorMessage, (int)TransactionType.PermanentToken);
                        await CompletePurchase(tempToken);
                    }
                    else
                    {
                        permanentToken = receipt?.GetDataKey();
                        if (permanentToken != null)
                        {
                            await AddPermanentToken(receipt);
                        }
                        else
                        {
                            await CompletePurchase(tempToken);
                        }
                    }
                   
                }
            }
            catch (Exception ex)
            {

                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
           

        }

        private async Task AddPermanentToken(Receipt receipt)
        {
            string dataKey = receipt.GetDataKey();
            string maskedPan = receipt.GetResDataMaskedPan();
            string exp_Date = receipt.GetResDataExpdate();

            PaymentCard paymentCard = new PaymentCard
            (
                permanentToken: dataKey,
                maskedCardNumber : maskedPan,
                cardExpiryDate: exp_Date
               
            );
            realm.Write(() =>
            {
                Purchaser.SavedPaymentCards.Add(paymentCard);
            });


            await CompletePurchase(dataKey);
        }
    }
}
