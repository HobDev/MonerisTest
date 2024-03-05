
using static UIKit.UIGestureRecognizer;
using System.Transactions;
using MonerisTest.Services.Interfaces;
using Moneris;

namespace MonerisTest.Services.Implementations
{
    public class CardVerificationService : ICardVerificationService
    {

        String store_id = AppConstants.STORE_ID;
        String api_token = AppConstants.API_TOKEN;

       
      


        /// <summary>
        /// When using a temporary token (e.g., such as with Hosted Tokenization) and you intend to store the cardholder credentials, this transaction must be run prior to running the Vault Add Token transaction
        /// </summary>
        /// <param name="tempToken"></param>
        /// <returns></returns>
        public async Task VerifyPaymentCard(string tempToken, string expDate)
        {
           
            string order_id = Guid.NewGuid().ToString();
            string cust_id = "Customer1";
            string crypt = "7";
            string processing_country_code = "CA";
            bool status_check = false;
            /*************** Address Verification Service **********************/
            AvsInfo avsCheck = new AvsInfo();
            avsCheck.SetAvsStreetNumber("212");
            avsCheck.SetAvsStreetName("Payton Street");
            avsCheck.SetAvsZipCode("M1M1M1");
            /****************** Card Validation Digits *************************/
            CvdInfo cvdCheck = new CvdInfo();
            cvdCheck.SetCvdIndicator("1");
            cvdCheck.SetCvdValue("099");
            /*************** Credential on File *************************************/
            CofInfo cof = new CofInfo();
            cof.SetPaymentIndicator("U");
            cof.SetPaymentInformation("2");
            // cof.SetIssuerId("12345678901234");
            ResCardVerificationCC rescardverify = new ResCardVerificationCC();
            rescardverify.SetDataKey(tempToken);
            rescardverify.SetOrderId(order_id);
            rescardverify.SetCustId(cust_id);
            rescardverify.SetExpDate(expDate); //for use with Temp Tokens only
            rescardverify.SetCryptType(crypt);
            rescardverify.SetAvsInfo(avsCheck);
            rescardverify.SetCvdInfo(cvdCheck);
            rescardverify.SetCofInfo(cof);
            //NT Response Option
            bool get_nt_response = true;
            rescardverify.SetGetNtResponse(get_nt_response);
            HttpsPostRequest mpgReq = new HttpsPostRequest();
            mpgReq.SetProcCountryCode(processing_country_code);
            mpgReq.SetTestMode(true); //false for production transactions
            mpgReq.SetStoreId(store_id);
            mpgReq.SetApiToken(api_token);
            mpgReq.SetTransaction(rescardverify);
            mpgReq.SetStatusCheck(status_check);
            mpgReq.Send();
            try
            {
                Receipt receipt = mpgReq.GetReceipt();
                string cardType =  receipt.GetCardType();
                string transAmount =  receipt.GetTransAmount();
                string txnNumber =  receipt.GetTxnNumber();
                string receiptId =  receipt.GetReceiptId();
                string transType = receipt.GetTransType();
                string referenceNum =  receipt.GetReferenceNum();
                string responseCode = receipt.GetResponseCode();
                string iSO =  receipt.GetISO();
                string bankTotals = receipt.GetBankTotals();
                string message =  receipt.GetMessage();
                string authCode =  receipt.GetAuthCode();
                string complete =  receipt.GetComplete();
                string transDate = receipt.GetTransDate();
                string transTime =  receipt.GetTransTime();
                string ticket =  receipt.GetTicket();
                string timedOut =  receipt.GetTimedOut();
                string isVisaDebit =  receipt.GetIsVisaDebit();
                string issuerId = receipt.GetIssuerId();
                string sourcePanLast4 =  receipt.GetSourcePanLast4();
                if (get_nt_response)
                {
                    string nTResponseCode =  receipt.GetNTResponseCode();
                    string nTMessage =  receipt.GetNTMessage();
                    string nTUsed =  receipt.GetNTUsed();
                    string nTTokenBin =  receipt.GetNTTokenBin();
                    string nTTokenLast4 = receipt.GetNTTokenLast4();
                    string nTTokenExpDate =  receipt.GetNTTokenExpDate();
                }
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    } // end TestResCardVerificationCC
}
        
