



namespace MonerisTest
{
    public partial class BookingViewModel: ObservableObject, IQueryAttributable
    {
        [ObservableProperty]
        Customer? purchaser;


        [ObservableProperty]
        string? customerName;

        [ObservableProperty]
        ObservableCollection<PaymentCard>? paymentCards;

        [ObservableProperty]
        PaymentCard? selectedCard;

        [ObservableProperty]
        decimal totalAmount;

        [ObservableProperty]
        string? cardType;

        [ObservableProperty]
        string? maskedCardNumber;

        Realm realm;

     
        private readonly IPurchaseService? purchaseService;
        private readonly IReceiptErrorMessageService? receiptErrorMessageService;
        private readonly ITransactionFailureService? transactionFailureService;
        private readonly ITransactionSuccessService? transactionSuccessService;


        public BookingViewModel( IPurchaseService purchaseService, IReceiptErrorMessageService receiptErrorMessageService, ITransactionFailureService transactionFailureService, ITransactionSuccessService transactionSuccessService)
        {
            try
            {
               
                this.purchaseService = purchaseService;
                this.receiptErrorMessageService = receiptErrorMessageService;
                this.transactionFailureService = transactionFailureService;
                this.transactionSuccessService = transactionSuccessService;
              
                realm= Realm.GetInstance();

                TotalAmount = 1.00M;
               
            }
            catch (Exception ex)
            {

                Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            }
          
        }


        [RelayCommand]
        public async Task CancelPayment()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(CustomersPage));
            }
            catch (Exception ex)
            {

                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }


        [RelayCommand]
        private  async Task Purchase()
        {
            try
            {
                if(SelectedCard!=null)
                {
                    if (purchaseService == null)
                    {
                        throw new Exception("Purchase Service is not available");
                    }
                    PurchaseData purchaseData = new PurchaseData
                           (
                               store_Id: AppConstants.STORE_ID,
                               api_Token: AppConstants.API_TOKEN,
                               token: SelectedCard.PermanentToken,
                               order_Id: Guid.NewGuid().ToString(),
                               amount: TotalAmount.ToString(),
                               cust_Id: purchaser.CustomerId
                           );
                    Receipt? receipt = await purchaseService.Purchase(purchaseData);
                    string? errorMessage = await receiptErrorMessageService?.GetErrorMessage(receipt);
                    if (errorMessage != null)
                    {
                        await transactionFailureService.SaveFailedTransactionData(purchaser.CustomerId, errorMessage, (int)TransactionType.Purchase);
                        await Shell.Current.DisplayAlert("Purchase failed", errorMessage, "OK");
                    }
                    else
                    {
                        string? transactionId = await transactionSuccessService.SaveSuccessfulTransactionData(receipt);
                        if (transactionId != null)
                        {

                            await Shell.Current.DisplayAlert("Payment Successful", " you will get the receipt in an email soon", "OK");
                            Dictionary<string, object> query = new Dictionary<string, object> { { "customerId", purchaser.CustomerId }, { "transactionId", transactionId } };
                            await Shell.Current.GoToAsync(nameof(TransactionDetailPage), query);
                        }
                    }
                } 
                else
                {
                    IDictionary<string, object> query= new Dictionary<string, object> { { "customerId", purchaser.CustomerId }, { "amount", TotalAmount.ToString()} };
                    await Shell.Current.GoToAsync(nameof(PaymentWebPage));
                }
                
            }
            catch (Exception ex)
            {

                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }

      

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            // This method is called when the page is navigated to with a query string.
            // The query string is parsed into a dictionary and passed to this method.
             if (query.TryGetValue("customerId", out object? value))
            {
                if (value is string  customerId)
                {
                    Purchaser = realm.All<Customer>().FirstOrDefault(c => c.CustomerId == customerId);
                    CustomerName = Purchaser?.Name;
                    PaymentCards = Purchaser?.SavedPaymentCards.ToObservableCollection();
                }
            }

            if (query.TryGetValue("errorMessage", out object? message))
            {
                if (message is string errorMessage)
                {
                    await Shell.Current.DisplayAlert("Error in creating temporary token", errorMessage, "Ok");
                }
            }

            
        }
    }
}
