

namespace MonerisTest.Helpers
{
    public class PaymentResponseHelpers
    {

        public async Task<string?> GetTransactionType(string transType)
        {
            string? transTypeValue = null;

            switch (transType)
            {
                case "0":
                    transTypeValue = "Purchase";
                    break;
                case "1":
                    transTypeValue = "Pre-Authorization";
                    break;
                case "2":
                    transTypeValue = "Completion";
                    break;
                case "4":
                    transTypeValue = "Refund";
                    break;
                case "11":
                    transTypeValue = "Void";
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


        public async  Task<string?> GetConvenienceFeeStatus(string convenienceFeeStatus)
        {

            string? convenienceFeeStatusValue = null;
            switch (convenienceFeeStatus)
            {
                case "1" :
                case "1F":
                    convenienceFeeStatusValue = "Completed 1st purchase transaction";
                    break;
                    case "2":
                    case "2F":
                    convenienceFeeStatusValue = "Completed 2nd purchase transaction";
                    break;
                    case "3":
                    convenienceFeeStatusValue = "Completed void transaction";
                    break;
                    case "4A":
                case "4D":
                    convenienceFeeStatusValue= "Completed refund transaction";
                    break;
                    case "7":
                    case "7F":
                    convenienceFeeStatusValue = "Completed merchant independent refund transaction";
                    break;
                    case "8":
                    case "8F":
                    convenienceFeeStatusValue = "Completed merchant refund transaction";
                    break;
                    case "9":
                    case "9F":
                    convenienceFeeStatusValue = "Completed 1st void transaction";
                    break;
                    case "10":
                    case "10F":
                    convenienceFeeStatusValue = "Completed 2nd void transaction";
                    break;
                    case "11A":
                    case "11D":
                    convenienceFeeStatusValue = "Completed refund transaction";
                    break;
            }

            return convenienceFeeStatusValue;
        }
    }
}
