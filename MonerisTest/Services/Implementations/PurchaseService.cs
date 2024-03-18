



namespace MonerisTest.Services.Implementations
{
    public class PurchaseService : IPurchaseService
    {
       
        public async Task<Receipt?> Purchase(PurchaseData purchaseData)
        {
            if(purchaseData==null)
            {
                throw new ArgumentNullException(nameof(purchaseData));
            }
            //transaction object properties
            String store_id = purchaseData.Store_Id;
            String api_token = purchaseData.Api_Token;


            string order_id = purchaseData.Order_Id;
            string amount = purchaseData.Amount;  
            string? cust_id = null;  
            if(purchaseData.Cust_Id!=null)
            {
                //if sent will be submitted, otherwise cust_id from profile will be used
                cust_id = purchaseData.Cust_Id;
            }   
            string token = purchaseData.Token;
           
            string crypt_type = "7";
            string descriptor = "my descriptor";
            bool status_check = false;
            String processing_country_code = "CA";


            ResPurchaseCC resPurchaseCC = new ResPurchaseCC();
            resPurchaseCC.SetDataKey(token);
            resPurchaseCC.SetOrderId(order_id);
            if(purchaseData.Cust_Id!=null)
            {
                resPurchaseCC.SetCustId(cust_id);
            }
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
