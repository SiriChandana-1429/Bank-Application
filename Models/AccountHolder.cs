using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Models
{
    public class AccountHolder
    {
        public string AccountHolderName;
        public string AccountId;
        public string UserName;
        public string BankName;
        public string BankId;
        public string FirstName;
        public string LastName;
        public string Email;
        public string Password;
        public double Balance;


        public List<Transaction> Transactions = new List<Transaction>();

        public AccountHolder() { }
        public AccountHolder(string accountHolderName, string accountId, int balance, string bankName, string bankId, string userName, string password, string email)
        {
            this.AccountHolderName = accountHolderName;
            this.AccountId = accountId;
            this.Balance = balance;
            this.BankName = bankName;
            this.BankId = bankId;
            this.UserName = userName;
            this.Password = password;
            this.Email = email;
        }






    }
}
