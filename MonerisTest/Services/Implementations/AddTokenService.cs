

using Moneris;
using MonerisTest.Services.Interfaces;


namespace MonerisTest.Services.Implementations
{
    public class AddTokenService : IAddTokenService
    {
        String store_id = AppConstants.STORE_ID;
        String api_token = AppConstants.API_TOKEN;

        
      

        public Task<string?> SaveTokenToVault(string issuerId, string tempToken)
        {

            string crypt_type = "7";
            bool status_check = false;

            CofInfo cof = new CofInfo();
            cof.SetIssuerId(issuerId);

            ResAddToken resAddToken = new ResAddToken(tempToken,crypt_type );
            resAddToken.SetCofInfo(cof);
            //resAddToken.SetDataKeyFormat(data_key_format); //optional

            HttpsPostRequest mpgReq = new HttpsPostRequest();
            mpgReq.SetTestMode(true); //false or comment out this line for production transactions
            mpgReq.SetStoreId(store_id);
            mpgReq.SetApiToken(api_token);
            mpgReq.SetTransaction(resAddToken);
            mpgReq.SetStatusCheck(status_check);
            mpgReq.Send();

            try
            {
                Receipt receipt = mpgReq.GetReceipt();
                string dataKey =  receipt.GetDataKey();
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

                return Task.FromResult(dataKey);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

return null;
        }
    }
}
