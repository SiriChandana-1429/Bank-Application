using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Account
    {
        
        public string AccountId { get; set; }
        public double Balance { get; set; }

        public List<Transaction> Transactions = new List<Transaction>();

        
        public Account(string accountId,double balance)
        { 
            this.AccountId= accountId;
            this.Balance = balance;
        }

    }
}
