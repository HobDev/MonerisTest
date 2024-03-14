﻿

using SQLite;
using System.Collections.Specialized;
using System.ComponentModel;

namespace MonerisTest.Models
{
    public class Customer:Entity
    {

        public Customer()
        {
            
        }

        public int Id { get; set; }

        public string? Name { get; set; }
       

        public string? Email { get; set; }
       

        public string? PhoneNumber { get; set; }


        public string? Address { get; set; }
       

        public string? MaskedCardNumber { get; set; }
       

        public string? CardToken { get; set; }
        

        public string? CardExpiryDate { get; set; }
       

        public string? CardType { get; set; }
       

        public string? CardHolderName { get; set; }
       

        public string?  CardBankName { get; set; }
        

        public byte[]? CardLogo { get; set; }
       
    }
}
