
using System.Reflection.Metadata;

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
        decimal totalAmount;

        string? tempToken;
     
        string? permanentToken;

        Realm realm;


        //after getting temporary token the card verification is the first step in the payment process. The successful card verification returns an issuer ID. The issuer ID is use to save the permanent token in the Moneris account. The permanent token is used for current and future purchases.
        private readonly ICardVerificationService? cardVerificationService;
        private readonly IPurchaseService? purchaseService;
        private readonly IAddTokenService? addTokenService;
        private readonly IConvenienceFeeService? convenienceFeeService;

       

        public PaymentWebViewModel(ICardVerificationService cardVerificationService, IPurchaseService purchaseService, IAddTokenService addTokenService, IConvenienceFeeService convenienceFeeService)
        {
            try
            {
                this.cardVerificationService = cardVerificationService;
                this.purchaseService = purchaseService;
                this.addTokenService = addTokenService;
                this.convenienceFeeService = convenienceFeeService;

                realm= Realm.GetInstance();
              

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
            if (query.TryGetValue("customerId", out object? value))
            {
                if (value is int customerId)
                {
                    Purchaser = realm.All<Customer>().FirstOrDefault(c => c.CustomerId == customerId);
                    CustomerName = Purchaser?.Name;
                   
                }
            }
            if(query.TryGetValue("amount", out object? secondValue))
            {
                if(secondValue is decimal amount)
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
            string? resSuccess = receipt.GetResSuccess();
            string? paymentType = receipt.GetPaymentType();
            string? cust_ID = receipt.GetResCustId();
            string? phone = receipt.GetResPhone(); 
            string? email = receipt.GetResEmail();
            string? note = receipt.GetResNote();
            string? masked_Pan = receipt.GetResMaskedPan();
            string? exp_Date = receipt.GetResExpDate();
            string? crypt_Type = receipt.GetResCryptType();
            string? avs_Street_Number = receipt.GetResAvsStreetNumber();
            string? avs_Street_Name = receipt.GetResAvsStreetName();
            string? avs_Zipcode = receipt.GetResAvsZipcode();



            string? receiptId = receipt.GetReceiptId();
            string? referenceNum = receipt.GetReferenceNum();
            string? responseCode = receipt.GetResponseCode();
            string? authCode = receipt.GetAuthCode();
            string? cardBrand = receipt.GetCardBrand();
            string? brandName=receipt.GetCardBrandName();    
            string? isoCode = receipt.GetISO();
            string? message = receipt.GetMessage();
            string? transDate = receipt.GetTransDate();
            string? transTime = receipt.GetTransTime();
            string? transType = receipt.GetTransType();
            string? Complete = receipt.GetComplete();
            string? transAmount = receipt.GetTransAmount();
            string? cardType = receipt.GetCardType();
            string? txnNumber = receipt.GetTxnNumber();
            string? timedOut = receipt.GetTimedOut();

            string? CardTypeValue=string.Empty;
          switch (cardType)
            {
                case "V":
                    CardTypeValue = "Visa";
                    break;
                    case "M":
                    CardTypeValue = "MasterCard";
                    break;
                    case "AX": 
                    CardTypeValue = "American Express";
                    break;
                    case "DC":
                    CardTypeValue = "Diners Club";
                    break;
                    case "NO":
                    CardTypeValue = "Novus/Discover";
                      break;
                    case "SE":
                    CardTypeValue = "Sears";
                    break;
                    case "D":
                    CardTypeValue = "Debit";
                    break;
                    case "C1":
                    CardTypeValue = "JCB";
                    break;
            }

            string? transTypeValue = string.Empty;  

            switch(transType)
            {
                case "0":
                    transTypeValue = "Purchase";
                    break; 
                    case "1":
                    transTypeValue = "Pre-Authorization";
                    break;
                    case "2":
                    transTypeValue = "Completion";
                    break;
                    case "4":
                    transTypeValue = "Refund";
                    break;
                    case "11":
                    transTypeValue = "Void";
                    break;
            }
          
           
          


            decimal amount = decimal.TryParse(transAmount, out decimal TotalAmount) ? TotalAmount : 0;
            // convert string to datetime offset
            DateTimeOffset transDateOffset = DateTimeOffset.TryParse(transDate, out DateTimeOffset result) ? result : DateTimeOffset.Now;

            PaymentReceipt transactionReceipt = new PaymentReceipt
            (
               transactionType:transType,
            orderNumber:receiptId,
            transaction_DateTime:transDateOffset,
        authorizationNumber:authCode,
        referenceNumber:referenceNum,
        iSOCode:isoCode,
        responseCode:responseCode,
            goods_Description:string.Empty,
             amount:amount,
           currency_Code:string.Empty,
            cardHolderName:Purchaser?.Name,  
           cardHolderAddress:string.Empty,
             purchaser:Purchaser,

             // not part of the receipt
             transactionNumber:txnNumber,
             cardType:CardTypeValue
            );
         
           
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
                    permanentToken = receipt?.GetDataKey();
                    if (permanentToken != null)
                    {
                        await AddPermanentToken(receipt);
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

        private async Task AddPermanentToken(Receipt receipt)
        {
            string dataKey = receipt.GetDataKey();
            string responseCode = receipt.GetResponseCode();
            string message = receipt.GetMessage();
            string transDate = receipt.GetTransDate();
            string transTime = receipt.GetTransTime();
            string complete = receipt.GetComplete();
            string timedOut = receipt.GetTimedOut();
            string resSuccess = receipt.GetResSuccess();
            string paymentType = receipt.GetPaymentType();
            string cardType = receipt.GetCardType();
            string cust_ID = receipt.GetResDataCustId();
            string phone = receipt.GetResDataPhone();
            string email = receipt.GetResDataEmail();
            string note = receipt.GetResDataNote();
            string maskedPan = receipt.GetResDataMaskedPan();
            string exp_Date = receipt.GetResDataExpdate();
            string crypt_Type = receipt.GetResDataCryptType();
            string avs_Street_Number = receipt.GetResDataAvsStreetNumber();
            string avs_Street_Name = receipt.GetResDataAvsStreetName();
            string avs_Zipcode = receipt.GetResDataAvsZipcode();


            PaymentCard paymentCard = new PaymentCard
            {
                PermanentToken = dataKey,
                MaskedCardNumber = maskedPan,
                CardExpiryDate= exp_Date,
               
            };
            realm.Write(() =>
            {
                purchaser.SavedPaymentCards.Add(paymentCard);
            });
             
            
        }
    }
}
