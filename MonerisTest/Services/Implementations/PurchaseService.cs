

using Moneris;
using MonerisTest.Services.Interfaces;

namespace MonerisTest.Services.Implementations
{
    public class PurchaseService : IPurchaseService
    {
        //transaction object properties
        String store_id = AppConstants.STORE_ID;
        String api_token = AppConstants.API_TOKEN;  


        // connection object properties
        string order_id = "Test" + DateTime.Now.ToString("yyyyMMddhhmmss");
        string amount = "5.00";
        string pan = "4242424242424242";  // card number is called pan
        string expdate = "2612"; //YYMM format
        string crypt = "7";
        String processing_country_code = "CA";
        bool status_check = false;

        public async Task Purchase()
        {
            Purchase purchase = new Purchase();
            purchase.SetOrderId(order_id);
            purchase.SetAmount(amount);
            purchase.SetPan(pan);
            purchase.SetExpDate(expdate);
            purchase.SetCryptType(crypt);
            purchase.SetDynamicDescriptor("2134565");

            // Instantiate the transaction object and assign values to properties.
            HttpsPostRequest mpgReq = new HttpsPostRequest();
            mpgReq.SetProcCountryCode(processing_country_code);
            mpgReq.SetTestMode(true); //false for production transactions
            mpgReq.SetStoreId(store_id);
            mpgReq.SetApiToken(api_token);
            mpgReq.SetTransaction(purchase);
            mpgReq.SetStatusCheck(status_check);
            mpgReq.Send();



            // Instantiate connection object and assign values to properties,including the transaction object you just created.
            try
            {
                Receipt receipt = mpgReq.GetReceipt();
               
                string cardType = receipt.GetCardType();
                string transAmount = receipt.GetTransAmount();
                string txnNumber = receipt.GetTxnNumber();
                string receiptId = receipt.GetReceiptId();
                string transType = receipt.GetTransType();
                string referenceNum = receipt.GetReferenceNum();
                string responseCode = receipt.GetResponseCode();
                string iso = receipt.GetISO();
                string bankTotals = receipt.GetBankTotals();
                string message = receipt.GetMessage();
                string authCode = receipt.GetAuthCode();
                string complete = receipt.GetComplete();
                string transDate = receipt.GetTransDate();
                string transTime = receipt.GetTransTime();
                string ticket = receipt.GetTicket();
                string timedOut = receipt.GetTimedOut();
                string isVisaDebit = receipt.GetIsVisaDebit();
               
              
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
