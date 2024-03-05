

using Moneris;
using MonerisTest.Services.Interfaces;

namespace MonerisTest.Services.Implementations
{
    public class AddTokenService : IAddTokenService
    {
        String store_id = AppConstants.STORE_ID;
        String api_token = AppConstants.API_TOKEN;

        
        string crypt_type = "7";
        string data_key_format = "0";
        string processing_country_code = "CA";
        bool status_check = false;

        public Task<string?> SaveTokenToVault(string tempToken)
        {
            string? permanentToken=null;
           
            CofInfo cof = new CofInfo();
            cof.SetIssuerId("168451306048014");
            ResAddToken resAddToken = new ResAddToken(tempToken, crypt_type);
            
            resAddToken.SetCofInfo(cof);
            //resAddToken.SetDataKeyFormat(data_key_format); //optional
            HttpsPostRequest mpgReq = new HttpsPostRequest();
            mpgReq.SetProcCountryCode(processing_country_code);
            mpgReq.SetTestMode(true); //false for production transactions
            mpgReq.SetStoreId(store_id);
            mpgReq.SetApiToken(api_token);
            mpgReq.SetTransaction(resAddToken);
            mpgReq.SetStatusCheck(status_check);
            mpgReq.Send();
            try
            {
                Receipt receipt = mpgReq.GetReceipt();
                string aataKey =  receipt.GetDataKey();
                string  responseCode  = receipt.GetResponseCode();
                string message =  receipt.GetMessage();
                string transDate =  receipt.GetTransDate();
                string transTime =  receipt.GetTransTime();
                string complete =  receipt.GetComplete();
                string timedOut =  receipt.GetTimedOut();
                string resSuccess =  receipt.GetResSuccess();
                string paymentType =  receipt.GetPaymentType();
                string cust_ID =  receipt.GetResDataCustId();
                string phone = receipt.GetResDataPhone();
                string email = receipt.GetResDataEmail();
                string note = receipt.GetResDataNote();
                string maskedPan =  receipt.GetResDataMaskedPan();
                string exp_Date = receipt.GetResDataExpdate();
                string crypt_Type =  receipt.GetResDataCryptType();
                string avs_Street_Number =  receipt.GetResDataAvsStreetNumber();
                string avs_Street_Name =  receipt.GetResDataAvsStreetName();
                string avs_Zipcode =  receipt.GetResDataAvsZipcode();
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

return Task.FromResult(permanentToken);
        }
    }
}
