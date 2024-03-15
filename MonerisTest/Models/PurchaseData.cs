

namespace MonerisTest.Models
{
    public class PurchaseData
    {
        public PurchaseData(string store_Id, string api_Token, string token, string order_Id, string amount,  string? cust_Id)
        {
            Store_Id = store_Id;
            Api_Token = api_Token;
            Token = token;
            Order_Id = order_Id;
            Amount = amount;
            Cust_Id = cust_Id;
        }

       public String Store_Id { get; set;}
        public String Api_Token { get; set;}
        public string Token { get; set; }  
       public string Order_Id { get; set; }
       public string Amount { get; set; }

        //if sent will be submitted, otherwise cust_id from profile will be used
       public string? Cust_Id { get; set; }   
    }
}
