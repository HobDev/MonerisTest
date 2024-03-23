

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
                // the transaction was successfull. the responseCode will indicate if the transaction was approved or declined
                string responseCode = receipt.GetResponseCode();
                bool result = int.TryParse(responseCode, out int responseCodeInt);
               if (result)
                {
                    // responseCode from 0 to 49 indicates the transaction was approved. responseCode from 50 to 999 indicates the transaction was declined
                    if (responseCodeInt >= 50 && responseCodeInt <= 999)
                    {
                        errorMessage = await GetDeclinedResponseMessage(responseCode);
                        if (errorMessage == string.Empty)
                        {
                            errorMessage = await GetReferralResponseMessage(responseCode);
                            if (errorMessage == string.Empty)
                            {
                                errorMessage = await GetSystemErrorResponseMessage(responseCode);
                            }
                        }
                        if (string.IsNullOrWhiteSpace(errorMessage))
                        {
                            errorMessage = receipt.GetMessage();
                        }
                        if(errorMessage == null)
                        {
                            errorMessage = string.Empty;
                        }
                        return responseCode + " - " + errorMessage;
                    }
                }
            }

            else
            {
                // the transaction was failed. the responseCode will indicate the reason of failure
                string responseCode = receipt.GetResponseCode();
                bool result = int.TryParse(responseCode, out int responseCodeInt);
                if (result)
                {
                    
                        errorMessage = await GetDeclinedResponseMessage(responseCode);
                        if(errorMessage== string.Empty)
                        {                        
                           errorMessage= await GetReferralResponseMessage(responseCode);
                            if(errorMessage == string.Empty)
                        {
                            errorMessage =await GetSystemErrorResponseMessage(responseCode);
                            }   
                        }

                        if (string.IsNullOrWhiteSpace(errorMessage))
                        {
                            errorMessage = receipt.GetMessage();
                        }
                       if (errorMessage == null)
                        {
                            errorMessage = string.Empty;
                        }
                        return responseCode + " - " + errorMessage;
                
                }
            }

            return errorMessage;
        }

        private async Task<string> GetSystemErrorResponseMessage(string responseCode)
        {
            if (responseCode == "150")
            {
                return "re-try edit error";
            }
            else if (responseCode == "200")
            {
                return "re-try edit error";
            }
            else if (responseCode == "201")
            {
                return "re-try pin error";
            }
            else if (responseCode == "202")
            {
                return "(advance less than minimum) re-try edit error";
            }
            else if (responseCode == "203")
            {
                return "(admininstrative card needed) re-try system problem";
            }
            else if (responseCode == "204")
            {
                return "(amount over maximum) over retailer limit";
            }
            else if (responseCode == "205")
            {
                return "re-try edit error";
            }
            else if (responseCode == "206")
            {
                return "card is not set up";
            }
            else if (responseCode == "207")
            {
                return "(invalid transaction date) re-try edit error";
            }
            else if (responseCode == "208")
            {
                return "(invalid expiration date) re-try edit error";
            }
            else if (responseCode == "209")
            {
                return "(invalid transaction code) re-try edit error";
            }
            else if (responseCode == "210")
            {
                return "(PIN key sync error) re-try edit error";
            }
            else if (responseCode == "212")
            {
                return "(destination not available) issuer not online";
            }
            else if (responseCode == "251")
            {
                return "(error on cash amount) re-try edit error";
            }
            else if (responseCode == "252")
            {
                return "(debit not supported) card not supported";
            }
           

                return string.Empty;
        }

        private async Task<string> GetReferralResponseMessage(string responseCode)
        {
            if (responseCode == "100")
            {
                return "re-try system problem";
            }
            else if (responseCode == "101")
            {
                return "(place call) re-try system problem";
            }
            else if (responseCode == "102")
            {
                return "re-try system problem";
            }
            else if (responseCode == "103")
            {
                return "(NEG file problem) re-try system problem";
            }
            else if (responseCode == "104")
            {
                return "(CAF problem) re-try system problem";
            }
            else if (responseCode == "105")
            {
                return "(card not supported) invalid card";
            }
            else if (responseCode == "106")
            {
                return "(amount over maximum) over retailer limit";
            }
            else if (responseCode == "107")
            {
                return "(over daily limit) usage exceeded";
            }
            else if (responseCode == "108")
            {
                return "(CAF problem) re-try system problem";
            }
            else if (responseCode == "109")
            {
                return "(advance less than minimum) over retailer limit";
            }
            else if (responseCode == "110")
            {
                return "(number of times used exceeded) usage exceeded";
            }
            else if (responseCode == "111")
            {
                return "(delinquent) re-try system problem";
            }
            else if (responseCode == "112")
            {
                return "(over table limit) re-try system problem";
            }
            else if (responseCode == "113")
            {
                return "(timeout) re-try system problem";
            }
            else if (responseCode == "115")
            {
                return "(PTLF error) re-try system problem";
            }
            else if (responseCode == "121")
            {
                return "(administration file problem) re-try system problem";
            }
            else if (responseCode == "122")
            {
                return "(unable to validate pin: security module down) re-try edit error";
            }
          

             return string.Empty;
        }

        private async Task<string?> GetDeclinedResponseMessage(string responseCode)
        {

            // declined response codes
           if(responseCode == "50")
            {
                return "re-try system problem";
            }
            else if(responseCode == "51")
            {
                return "expired card";
            }
            else if(responseCode == "52")
            {
                return "Excess pin tries";
            }
            else if(responseCode == "53")
            {
                return "(no sharing) re-try edit error";
            }
            else if(responseCode == "54")
            {
                return "(no security module) re-try system problem";
            }
            else if(responseCode == "55")
            {
                return "(invalid transaction) card not supported";
            }
            else if(responseCode == "56")
            {
                return "card not supported";
            }
            else if(responseCode == "57")
            {
                return "(lost or stolen card) card use limited";
            }
            else if(responseCode == "58")
            {
                return "(invalid status) card use limited";
            }
            else if(responseCode == "59")
            {
                return "(restricted card) card use limited";
            }
            else if(responseCode == "60")
            {
                return "No chequing/savings account re-try or cancel";
            }
            else if(responseCode == "61")
            {
                return "(no pbf) card is not set up";
            }
            else if(responseCode == "62")
            {
                return "(pbf update error) re-try system problem";
            }
            else if(responseCode == "63")
            {
                return "(invalid authorization type) re-try system problem";
            }
            else if(responseCode == "64")
            {
                return "(bad track 2) re-try invalid card";
            }
            else if(responseCode == "65")
            {
                return "(adjustment not allowed) exceeds correction Limit";
            }
            else if(responseCode == "66")
            {
                return "(invalid credit card advance increment) re-try system problem";
            }
            else if(responseCode == "67")
            {
                return "(invalid transaction date) re-try edit error";
            }
            else if(responseCode == "68")
            {
                return "(PTLF error) re-try system problem";
            }
            else if(responseCode == "69")
            {
                return "re-try edit error";
            }
            else if(responseCode == "70")
            {
                return "invalid card";
            }
            else if(responseCode == "71")
            {
                return "invalid card";
            }
            else if(responseCode == "72")
            {
                return "(card on national NEG file) re-try system problem";
            }
            else if(responseCode == "73")
            {
                return "(invalid route service/destination) invalid card";
            }
            else if(responseCode == "74")
            {
                return "issuer not online";
            }   
            else if(responseCode == "75")
            {
                return "(invalid pan length) re-try invalid card";
            }
            else if(responseCode == "76")
            {
                return "Insufficient funds";
            }
            else if(responseCode == "77")
            {
                return "(pre-auth full) Limit exceeded";
            }
            else if(responseCode == "78")
            {
                return "(duplicate transaction/ request in progress) re-try system problem";
            }
            else if(responseCode == "79")
            {
                return "(maximum online refund reached) limit exceeded";
            }
            else if(responseCode == "80")
            {
                return "(maximum online refund reached) limit exceeded";
            }
           else if(responseCode == "81")
            {
                return "(maximum credit per refund reached) limit exceeded";
            }
           else if(responseCode == "82")
            {
                return "(number of times used exceeded) usage exceeded";
            }
           else if(responseCode == "83")
            {
                return "(maximum refund credit reached) limit exceeded";
            }
           else if(responseCode == "84")
            {
                return "duplicate transaction - authorization number has already been corrected by host";
            }
           else if(responseCode == "85")
            {
                return "(inquiry not allowed) card not supported";
            }
           else if(responseCode == "86")
            {
                return "(over floor limit) re-try system problem";
            }
           else if(responseCode == "87")
            {
                return "(maximum number of refund credit by retailer) over retailer limit";
            }
           else if(responseCode == "88")
            {
                return "(place call) re-try system problem";
            }
           else if(responseCode == "89")
            {
                return "(CAF status inactive or closed) card use limited";
            }
           else if(responseCode == "90")
            {
                return "(referral file full) re-try system problem";
            }
           else if(responseCode == "91")
            {
                return "(NEG file problem) re-try system problem";
            }
           else if(responseCode == "92")
            {
                return "(advance less than minimum) over retailer limit";
            }
           else if(responseCode == "93")
            {
                return "(delinquent) re-try system problem";
            }
           else if(responseCode == "94")
            {
                return "(over table limit) re-try system problem";
            }
           else if(responseCode == "95")
            {
                return "(amount over maximum / transaction amount limit exceeded) re-try system problem";
            }
           else if(responseCode == "96")
            {
                return "(pin required) re-try pin error";
            }
           else if(responseCode == "97")
            {
                return "(mod 10 check failure) re-try invalid card";
            }
           else if(responseCode == "98")
            {
                return "(force post) re-try system problem";
            }
           else if(responseCode == "99")
            {
                return "(bad PBF) re-try invalid card";
            }
          
          

           return string.Empty;
        }
    }
}
