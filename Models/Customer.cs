﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Customer:User
    {
        public string AccountHolderName { get; set; }
        public string AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }   
        public string Email { get; set; }
        public double Balance { get; set; }


        public List<Transaction> Transactions = new List<Transaction>();

        public Customer() { }
        public Customer(string accountHolderName, string accountId, int balance, string bankName, string bankId, string userName, string password, string email)
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