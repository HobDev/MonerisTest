

using MonerisTest.Pages;
using MonerisTest.Services.Interfaces;

namespace MonerisTest.Services.Implementations
{
    public class GetTempTokenService : IGetTempTokenService
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


        public GetTempTokenService()
        {

        }

        public  async Task<string> GetTempToken()
        {
            return null;
            await Shell.Current.GoToAsync(nameof(PaymentWebPage));
        }

       
    }
}
