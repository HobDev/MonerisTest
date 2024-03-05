

using Moneris;
using MonerisTest.Services.Interfaces;

namespace MonerisTest.Services.Implementations
{
    public class CardVerificationService : ICardVerificationService
    {
        public async Task VerifyPaymentCard()
        {
            String store_id = AppConstants.STORE_ID;
            String api_token = AppConstants.API_TOKEN;

            string data_key = "V6F9PJKdXQj6vKiCMNrWbsyJ2";
            string order_id = "Test_P_033333_6";
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
            rescardverify.SetDataKey(data_key);
            rescardverify.SetOrderId(order_id);
            rescardverify.SetCustId(cust_id);
            //rescardverify.SetExpDate("1612"); //for use with Temp Tokens only
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
                Console.WriteLine("CardType = " + receipt.GetCardType());
                Console.WriteLine("TransAmount = " + receipt.GetTransAmount());
                Console.WriteLine("TxnNumber = " + receipt.GetTxnNumber());
                Console.WriteLine("ReceiptId = " + receipt.GetReceiptId());
                Console.WriteLine("TransType = " + receipt.GetTransType());
                Console.WriteLine("ReferenceNum = " + receipt.GetReferenceNum());
                Console.WriteLine("ResponseCode = " + receipt.GetResponseCode());
                Console.WriteLine("ISO = " + receipt.GetISO());
                Console.WriteLine("BankTotals = " + receipt.GetBankTotals());
                Console.WriteLine("Message = " + receipt.GetMessage());
                Console.WriteLine("AuthCode = " + receipt.GetAuthCode());
                Console.WriteLine("Complete = " + receipt.GetComplete());
                Console.WriteLine("TransDate = " + receipt.GetTransDate());
                Console.WriteLine("TransTime = " + receipt.GetTransTime());
                Console.WriteLine("Ticket = " + receipt.GetTicket());
                Console.WriteLine("TimedOut = " + receipt.GetTimedOut());
                Console.WriteLine("IsVisaDebit = " + receipt.GetIsVisaDebit());
                Console.WriteLine("IssuerId = " + receipt.GetIssuerId());
                Console.WriteLine("SourcePanLast4 = " + receipt.GetSourcePanLast4());
                if (get_nt_response)
                {
                    Console.WriteLine("\nNTResponseCode = " + receipt.GetNTResponseCode());
                    Console.WriteLine("NTMessage = " + receipt.GetNTMessage());
                    Console.WriteLine("NTUsed = " + receipt.GetNTUsed());
                    Console.WriteLine("NTTokenBin = " + receipt.GetNTTokenBin());
                    Console.WriteLine("NTTokenLast4 = " + receipt.GetNTTokenLast4());
                    Console.WriteLine("NTTokenExpDate = " + receipt.GetNTTokenExpDate());
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
        
