



namespace MonerisTest.Services.Implementations
{
    public class PurchaseService : IPurchaseService
    {
        //transaction object properties
        String store_id = AppConstants.STORE_ID;
        String api_token = AppConstants.API_TOKEN;  


      

        public async Task Purchase(string token)
        {
            string order_id = "Test" + DateTime.Now.ToString("yyyyMMddhhmmss");
            string amount = "1.00";
            string convenience_fee = "1.00";    
            string cust_id = "customer1"; //if sent will be submitted, otherwise cust_id from profile will be used
            string crypt_type = "1";
            string descriptor = "my descriptor";
            bool status_check = false;
            String processing_country_code = "CA";


            ConvFeeInfo convFeeInfo = new ConvFeeInfo();
            convFeeInfo.SetConvenienceFee(convenience_fee);

            ResPurchaseCC resPurchaseCC = new ResPurchaseCC();
            resPurchaseCC.SetDataKey(token);
            resPurchaseCC.SetOrderId(order_id);
            resPurchaseCC.SetCustId(cust_id);
            resPurchaseCC.SetAmount(amount);
            resPurchaseCC.SetCryptType(crypt_type);
            resPurchaseCC.SetDynamicDescriptor(descriptor);
           
          

            HttpsPostRequest mpgReq = new HttpsPostRequest();
            mpgReq.SetProcCountryCode(processing_country_code);
            mpgReq.SetTestMode(true); //false or comment out this line for production transactions
            mpgReq.SetStoreId(store_id);
            mpgReq.SetApiToken(api_token);
            mpgReq.SetTransaction(resPurchaseCC);
            mpgReq.SetStatusCheck(status_check);
            mpgReq.Send();

            try
            {
                Receipt receipt = mpgReq.GetReceipt();

                string dataKey =  receipt.GetDataKey();
                string receiptId =  receipt.GetReceiptId();
                string referenceNum = receipt.GetReferenceNum();
                string responseCode =  receipt.GetResponseCode();
                string authCode =  receipt.GetAuthCode();
                string message =  receipt.GetMessage();
                string transDate =  receipt.GetTransDate();
                string transTime = receipt.GetTransTime();
                string transType = receipt.GetTransType();
                string Complete = receipt.GetComplete();
                string transAmount = receipt.GetTransAmount();
                string cardType = receipt.GetCardType();
                string txnNumber = receipt.GetTxnNumber();
                string timedOut = receipt.GetTimedOut();
                string resSuccess =  receipt.GetResSuccess();
                string paymentType = receipt.GetPaymentType();
                string isVisaDebit =  receipt.GetIsVisaDebit();
                string issuerId =  receipt.GetIssuerId();

                string cust_ID =  receipt.GetResDataCustId();
                string phone =  receipt.GetResDataPhone();
                string email = receipt.GetResDataEmail();
                string note =  receipt.GetResDataNote();
                string masked_Pan =  receipt.GetResDataMaskedPan();
                string exp_Date =  receipt.GetResDataExpdate();
                string crypt_Type =  receipt.GetResDataCryptType();
                string avs_Street_Number =  receipt.GetResDataAvsStreetNumber();
                string avs_Street_Name =  receipt.GetResDataAvsStreetName();
                string avs_Zipcode = receipt.GetResDataAvsZipcode();


                CardHolderTransactionRecordPurchase cardHolderTransactionRecordPurchase = new CardHolderTransactionRecordPurchase()
                {

                };
              
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
