﻿


namespace MonerisTest.Services.Implementations
{
    public class AddTokenService : IAddTokenService
    {
        String store_id = AppConstants.STORE_ID;
        String api_token = AppConstants.API_TOKEN;

        
      

        public async Task<Receipt?> SaveTokenToVault(string issuerId, string tempToken)
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
               

                return receipt;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

return null;
        }
    }
}
