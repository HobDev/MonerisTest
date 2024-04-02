

namespace MonerisTest.Services.Implementations
{
    public class ReceiptErrorMessageService : IReceiptErrorMessageService
    {

        public ReceiptErrorMessageService() { }

        public async Task<string?> GetErrorMessage(Receipt receipt)
        {

            string? responseMessage = null;
            string? iSOMessage = null;

            string complete = receipt.GetComplete();
            string timedOut = receipt.GetTimedOut();

            if (complete == "true" && timedOut == "false")
            {
                // the transaction was successfull. the responseCode will indicate if the transaction was approved or declined
                string responseCode = receipt.GetResponseCode();
                string iSOCode = receipt.GetISO();
                if(responseCode==null)
                {
                    return "null - transaction was not sent for authorization";
                }
                bool result = int.TryParse(responseCode, out int responseCodeInt);
               if (result)
                {
                    // responseCode from 0 to 49 indicates the transaction was approved. responseCode from 50 to 999 indicates the transaction was declined
                    if (responseCodeInt >= 50 )
                    {
                        responseMessage = await GetDeclinedResponseMessage(responseCode, iSOCode);
                        if (responseMessage == string.Empty)
                        {
                            responseMessage = await GetReferralResponseMessage(responseCode);
                            if (responseMessage == string.Empty)
                            {
                                responseMessage = await GetSystemErrorResponseMessage(responseCode);
                                if (responseMessage == string.Empty)
                                {
                                    responseMessage = await GetVisaResponseMessage(responseCode);
                                    if (responseMessage == string.Empty)
                                    {
                                        responseMessage = await GetMasterCardResponseMessage(responseCode);
                                        if (responseMessage == string.Empty)
                                        {
                                            responseMessage = await GetAmexResponseMessage(responseCode);
                                            if (responseMessage == string.Empty)
                                            {
                                                responseMessage = await GetCreditCardResponseMessage(responseCode);
                                                if (responseMessage == string.Empty)
                                                {
                                                    //  responseMessage = await GetSystemDeclineResponseMessage(responseCode);
                                                    if (responseMessage == string.Empty)
                                                    {
                                                        //  responseMessage = await GetAdminResponseMessage(responseCode);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (responseMessage== string.Empty)
                        {
                            responseMessage = receipt.GetMessage();
                        }
                        return $"ISO CODE: {iSOCode} , ISO MESSAGE: {iSOMessage}\nRESPONSE CODE: {responseCode} ---- RESPONSE MESSSAGE: {responseMessage}";
                    }
                    
                }
            }

            else
            {
                // the transaction was failed. the responseCode will indicate the reason of failure
                string responseCode = receipt.GetResponseCode();
                string iSOCode = receipt.GetISO();
                if (responseCode == null)
                {
                    return "null - transaction was not sent for authorization";
                }
               
                        responseMessage = await GetDeclinedResponseMessage(responseCode, iSOCode);
                        if(responseMessage== string.Empty)
                        {                        
                           responseMessage= await GetReferralResponseMessage(responseCode);
                            if(responseMessage == string.Empty)
                            {
                            responseMessage =await GetSystemErrorResponseMessage(responseCode);
                               if(responseMessage == string.Empty)
                               {
                                  responseMessage =await GetVisaResponseMessage(responseCode);
                                    if(responseMessage == string.Empty)
                                    {
                                        responseMessage =await  GetMasterCardResponseMessage(responseCode);
                                         if(responseMessage == string.Empty)
                                         {
                                              responseMessage = await GetAmexResponseMessage(responseCode);
                                               if(responseMessage == string.Empty)
                                               {
                                                   responseMessage = await GetCreditCardResponseMessage(responseCode);
                                                    if(responseMessage == string.Empty)
                                                    {
                                                      //  responseMessage = await GetSystemDeclineResponseMessage(responseCode);
                                                        if(responseMessage == string.Empty)
                                                        {
                                                          //  responseMessage = await GetAdminResponseMessage(responseCode);
                                                        }
                                                    }
                                               }
                                         }
                                    }
                               }
                            }
                        }

                       if (responseMessage == string.Empty)
                        {
                            responseMessage = receipt.GetMessage();
                        }

                return $"ISO CODE: {iSOCode} , ISO MESSAGE: {iSOMessage}\nRESPONSE CODE: {responseCode} ---- RESPONSE MESSSAGE: {responseMessage}";

            }

            return responseMessage;
        }

        //private async Task<string> GetAdminResponseMessage(string responseCode)
        //{
            
        //}

        //private async Task<string> GetSystemDeclineResponseMessage(string responseCode)
        //{
            
        //}

        private async Task<string> GetCreditCardResponseMessage(string responseCode)
        {
           if(responseCode=="408")
            {
                return "(CREDIT CARD - card use limited - refer to branch) declined";
            }
           else if(responseCode=="475")
            {
                 return "CREDIT CARD - invalid expiry date";
            }
           else if(responseCode=="476")
            {
                 return "CREDIT CARD - invalid expiry date";
                }


            return string.Empty;
        }

        private async Task<string> GetAmexResponseMessage(string responseCode)
        {
            if(responseCode == "426")
            {
                return "(AMEX - Denial 12) Call Amex 12";
            }
            if(responseCode == "427")
            {
                return "AMEX - invalid merchant";
            }
            if(responseCode == "429")
            {
                return "AMEX -  account error retry";
            }
            if(responseCode == "430")
            {
                return "AMEX - expired card";
            }
            if(responseCode == "431")
            {
                return "AMEX - call Amex";
            }
            if(responseCode == "434")
            {
                return "AMEX - call 03, Invalid CVD (CID)";
            }
            if(responseCode == "435")
            {
                return "AMEX - system down";
            }
            if(responseCode == "436")
            {
                return "AMEX - call 05";
            }
            if(responseCode == "437")
            {
                return "AMEX - declined";
            }
            if(responseCode == "438")
            {
                return "AMEX - declined";
            }
            if(responseCode == "439")
            {
                return "AMEX - service error";
            }
            if(responseCode == "440")
            {
                return "AMEX - call Amex";
            }
            if(responseCode == "441")
            {
                return "AMEX - amount error, retry";
            }


            return string.Empty;
        }

        private async Task<string> GetMasterCardResponseMessage(string reponseCode)
        {
            if(reponseCode == "416")
            {
                return "declined, use updated card";
            }
            if(reponseCode == "421")
            {
                return "card declined, do not retry";
            }
            if(reponseCode == "422")
            {
                return "stop payment, do not retry";
            }
            if(reponseCode == "481")
            {
                return "declined";
            }
            return string.Empty;
        }

        private async Task<string> GetVisaResponseMessage(string responseCode)
        {
           if(responseCode=="421")
            {
                return "card declined, do not retry";
            }
           else if(responseCode=="422")
            {
                return "card declined, do not retry stop payment";
            }
            else if(responseCode=="423")
            {
                return "(verification data failed) declined verification failed";
            }
           
            return string.Empty;
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

        private async Task<string> GetDeclinedResponseMessage(string responseCode, string iSOCode)
        {
            // iso codes which don't have a specific response code
            if (iSOCode == "48" || iSOCode == "53" || iSOCode == "78")
            {
                return "no savings account retry or cancel";
            }
            else if(iSOCode=="89")
            {
                return "(declined) system problem";
            }
            else if (iSOCode == "91")
            {
                return "(merchant link not logged on) unable to authorize";
            }

            // declined response codes
            if (responseCode == "050")
            {
                return "re-try system problem";
            }
            else if(responseCode == "051")
            {
                return "expired card";
            }
            else if(responseCode == "052")
            {
                return "Excess pin tries";
            }
            else if(responseCode == "053")
            {
                return "(no sharing) re-try edit error";
            }
            else if(responseCode == "054")
            {
                return "(no security module) re-try system problem";
            }
            else if(responseCode == "055")
            {
                return "(invalid transaction) card not supported";
            }
            else if(responseCode == "056")
            {
                return "card not supported";
            }
            else if(responseCode == "057")
            {
                return "(lost or stolen card) card use limited";
            }
            else if(responseCode == "058")
            {
                return "(invalid status) card use limited";
            }
            else if(responseCode == "059")
            {
                return "(restricted card) card use limited";
            }
            else if(responseCode == "060")
            {
                return "No chequing/savings account re-try or cancel";
            }
            else if(responseCode == "061")
            {
                return "(no pbf) card is not set up";
            }
            else if(responseCode == "062")
            {
                return "(pbf update error) re-try system problem";
            }
            else if(responseCode == "063")
            {
                return "(invalid authorization type) re-try system problem";
            }
            else if(responseCode == "064")
            {
                return "(bad track 2) re-try invalid card";
            }
            else if(responseCode == "065")
            {
                return "(adjustment not allowed) exceeds correction Limit";
            }
            else if(responseCode == "066")
            {
                return "(invalid credit card advance increment) re-try system problem";
            }
            else if(responseCode == "067")
            {
                return "(invalid transaction date) re-try edit error";
            }
            else if(responseCode == "068")
            {
                return "(PTLF error) re-try system problem";
            }
            else if(responseCode == "069")
            {
                return "re-try edit error";
            }
            else if(responseCode == "070")
            {
                return "invalid card";
            }
            else if(responseCode == "071")
            {
                return "invalid card";
            }
            else if(responseCode == "072")
            {
                return "(card on national NEG file) re-try system problem";
            }
            else if(responseCode == "073")
            {
                return "(invalid route service/destination) invalid card";
            }
            else if(responseCode == "074")
            {
                return "issuer not online";
            }   
            else if(responseCode == "075")
            {
                return "(invalid pan length) re-try invalid card";
            }
            else if(responseCode == "076")
            {
                return "Insufficient funds";
            }
            else if(responseCode == "077")
            {
                return "(pre-auth full) Limit exceeded";
            }
            else if(responseCode == "078")
            {
                return "(duplicate transaction/ request in progress) re-try system problem";
            }
            else if(responseCode == "079")
            {
                return "(maximum online refund reached) limit exceeded";
            }
            else if(responseCode == "080")
            {
                return "(maximum online refund reached) limit exceeded";
            }
           else if(responseCode == "081")
            {
                return "(maximum credit per refund reached) limit exceeded";
            }
           else if(responseCode == "082")
            {
                return "(number of times used exceeded) usage exceeded";
            }
           else if(responseCode == "083")
            {
                return "(maximum refund credit reached) limit exceeded";
            }
           else if(responseCode == "084")
            {
                return "duplicate transaction - authorization number has already been corrected by host";
            }
           else if(responseCode == "085")
            {
                return "(inquiry not allowed) card not supported";
            }
           else if(responseCode == "086")
            {
                return "(over floor limit) re-try system problem";
            }
           else if(responseCode == "087")
            {
                return "(maximum number of refund credit by retailer) over retailer limit";
            }
           else if(responseCode == "088")
            {
                return "(place call) re-try system problem";
            }
           else if(responseCode == "089")
            {
                return "(CAF status inactive or closed) card use limited";
            }
           else if(responseCode == "090")
            {
                return "(referral file full) re-try system problem";
            }
           else if(responseCode == "091")
            {
                return "(NEG file problem) re-try system problem";
            }
           else if(responseCode == "092")
            {
                return "(advance less than minimum) over retailer limit";
            }
           else if(responseCode == "093")
            {
                return "(delinquent) re-try system problem";
            }
           else if(responseCode == "094")
            {
                return "(over table limit) re-try system problem";
            }
           else if(responseCode == "095")
            {
                return "(amount over maximum / transaction amount limit exceeded) re-try system problem";
            }
           else if(responseCode == "096")
            {
                return "(pin required) re-try pin error";
            }
           else if(responseCode == "097")
            {
                return "(mod 10 check failure) re-try invalid card";
            }
           else if(responseCode == "098")
            {
                return "(force post) re-try system problem";
            }
           else if(responseCode == "099")
            {
                return "(bad PBF) re-try invalid card";
            }
          
          

           return string.Empty;
        }
    }
}
