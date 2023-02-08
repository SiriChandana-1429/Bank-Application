using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Bank
    {
        public string? BankId { get; set; }
        public string BankName { get; set; }
        public string? Location { get; set; }

        public string? Currency;
        public float RTGSChargesForSame { get; set; } = 0f;
        public float RTGSChargesForOther { get; set; } = 0.02f;
        public float IMPSChargesForSame { get; set; } = 0.05f;
        public float IMPSChargesForOther { get; set; } = 0.06f;

        public ICollection<AcceptedCurrency> AcceptedCurrencies { get; set; }
        public ICollection<User> Users;
        public ICollection<Account> Accounts;
  
        
        public Bank() { }
        public Bank(string bankName, string bankLocation, string currency)
        {
            this.Location = bankLocation;
            this.Currency = currency;
            this.BankName = bankName;
            DateTime now = DateTime.Now;
            this.BankId = bankName.Substring(0, 3) + now.ToString("");


        }

       
        
    }
}
