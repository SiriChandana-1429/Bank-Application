using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Bank
    {
        public string? BankName;
        public string? Location;
        public string? BankId;

        public double RTGSChargesForSame = 0;
        public double RTGSChargesForOther = 0.02;
        public double IMPSChargesForSame = 0.05;
        public double IMPSChargesForOther = 0.06;
        public static Dictionary<string, int> AcceptedCurrencies = new Dictionary<string, int>
        {
            {"INR", 1 } ,
            {"USD",80 },
            {"EUR",90 }
        };


        public string? Currency;
        public Bank(string bankName, string bankLocation, string currency)
        {
            this.Location = bankLocation;
            this.Currency = currency;
            this.BankName = bankName;
            DateTime now = DateTime.Now;
            this.BankId = bankName.Substring(0, 3) + now.ToString("");


        }

        public List<AccountHolder> AllAccounts = new List<AccountHolder>();
        public List<BankStaff> Staff = new List<BankStaff>();
    }
}
