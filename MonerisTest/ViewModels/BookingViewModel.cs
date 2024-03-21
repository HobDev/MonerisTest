


using CommunityToolkit.Maui.Core.Extensions;

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
        private readonly IConvenienceFeeService? convenienceFeeService;

        public BookingViewModel( IPurchaseService purchaseService, IConvenienceFeeService convenienceFeeService)
        {
            try
            {
               
                this.purchaseService = purchaseService;
                this.convenienceFeeService = convenienceFeeService;
                realm= Realm.GetInstance();

              

                SelectedCard = new PaymentCard
                {
                    CardExpiryDate = "12/2023",

                    MaskedCardNumber = "4242********4242",
                };
                PaymentCards = new ObservableCollection<PaymentCard>(); 
                PaymentCards.Add(SelectedCard);
                TotalAmount = 1.00M;
               
            }
            catch (Exception ex)
            {

                Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            }
          
        }



        [RelayCommand]
        private  async Task Purchase()
        {
            try
            {


                if (SelectedCard == null)
                {
                    await Shell.Current.GoToAsync($"{nameof(PaymentWebPage)}", new Dictionary<string, object> { { "customerId", Purchaser.CustomerId }, { "amount", TotalAmount } });
                }
                else
                {
                    // Make a payment using the permanent token
                    string? token = SelectedCard.PermanentToken;
                    if (!string.IsNullOrWhiteSpace(token))
                    {
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
                        await convenienceFeeService.ChargeConvenienceFee(TotalAmount);
                    }

                }

            }
            catch (Exception ex)
            {

               await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            }
           
        }

        private async Task SavePurchaseData(Receipt receipt)
        {
            if(receipt==null)
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

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            // This method is called when the page is navigated to with a query string.
            // The query string is parsed into a dictionary and passed to this method.
             if (query.TryGetValue("customerId", out object? value))
            {
                if (value is int customerId)
                {
                    Purchaser = realm.All<Customer>().FirstOrDefault(c => c.CustomerId == customerId);
                    CustomerName = Purchaser?.Name;
                    PaymentCards = Purchaser?.SavedPaymentCards.ToObservableCollection();
                    SelectedCard = PaymentCards?.FirstOrDefault();
                    

                }
            }
        }
    }
}
