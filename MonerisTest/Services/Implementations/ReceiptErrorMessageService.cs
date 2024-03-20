

using Moneris;

namespace MonerisTest.Services.Implementations
{
    public class ReceiptErrorMessageService : IReceiptErrorMessageService
    {

        public ReceiptErrorMessageService() { }

        public async Task<string?> GetErrorMessage(Receipt receipt)
        {

            string? errorMessage = null;

            string complete = receipt.GetComplete();
            string timedOut = receipt.GetTimedOut();

            if (complete == "true" && timedOut == "false")
            {
                string responseCode = receipt.GetResponseCode();
                bool result = int.TryParse(responseCode, out int responseCodeInt);
               if (result)
                {
                    if (responseCodeInt >= 50 && responseCodeInt <= 999)
                    {
                        return receipt.GetMessage();
                    }
                }
            }

            return errorMessage;
        }
    }
}
