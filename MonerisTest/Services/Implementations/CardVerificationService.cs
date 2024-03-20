

namespace MonerisTest.Services.Implementations
{
    public class CardVerificationService : ICardVerificationService
    {


        /// <summary>
        /// When using a temporary token (e.g., such as with Hosted Tokenization) and you intend to store the cardholder credentials, this transaction must be run prior to running the Vault Add Token transaction
        /// </summary>
        /// <param name="tempToken"></param>
        /// <returns></returns>
        public async Task<Receipt?> VerifyPaymentCard(string tempToken)
        {
            String store_id = AppConstants.STORE_ID;
            String api_token = AppConstants.API_TOKEN;

            string order_id = Guid.NewGuid().ToString();
            string crypt = "7";
            string processing_country_code = "CA";
            bool status_check = false;


            /*************** Credential on File *************************************/
            CofInfo cof = new CofInfo();
            cof.SetPaymentIndicator("U");
            cof.SetPaymentInformation("7");
           
            ResCardVerificationCC rescardverify = new ResCardVerificationCC();
            rescardverify.SetDataKey(tempToken);
            rescardverify.SetOrderId(order_id);
            rescardverify.SetCryptType(crypt);
            rescardverify.SetCofInfo(cof);
           
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
        
