

namespace MonerisTest.Services.Implementations
{
    public class PaymentResponseHelper : IPaymentResponseHelper
    {

        public async Task<string?> GetTransactionType(string transType)
        {
            string? transTypeValue = null;

            switch (transType)
            {
                case "00":
                    transTypeValue = "Purchase";
                    break;
                case "04":
                    transTypeValue = "Refund";
                    break;
              
            }

            return transTypeValue;
        }

        public async Task<string?> GetCardType(string cardType)
        {
            string? CardTypeValue = null;
            switch (cardType)
            {
                case "V":
                    CardTypeValue = "Visa";
                    break;
                case "M":
                    CardTypeValue = "MasterCard";
                    break;
                case "AX":
                    CardTypeValue = "American Express";
                    break;
                case "DC":
                    CardTypeValue = "Diners Club";
                    break;
                case "NO":
                    CardTypeValue = "Novus/Discover";
                    break;
                case "SE":
                    CardTypeValue = "Sears";
                    break;
                case "D":
                    CardTypeValue = "Debit";
                    break;
                case "C1":
                    CardTypeValue = "JCB";
                    break;
            }

            return CardTypeValue;
        }


       
    }
}
