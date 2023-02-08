using DataAccessLayer;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class StaffServices
    {
        static BankDataBaseContext bankDbContext = new BankDataBaseContext();
        public static string CreateAccount(string bankName, string firstName, string lastName, string email, string password, string bankId, string staffId)

        {
            var currentStaff = bankDbContext.Users.Where(i => i.UserId.Equals(staffId));
            if (!currentStaff.Any())
            {
                return "0";
            }
            int balance = 200;

            string accountHolderName = firstName + " " + lastName;
            DateTime now = DateTime.Now;
            string accountId = accountHolderName.Substring(0, 3) + now.ToString("");
            now = DateTime.Now;
            string userId = now.ToString("");
            Customer newCustomer = new Customer(accountHolderName, accountId, bankName, bankId, userId, password, email);
            Account newAccount = new Account(accountId, balance);
            bankDbContext.Users.Add(newCustomer);
            bankDbContext.Accounts.Add(newAccount);
            return newAccount.AccountId;

        }

        public static int DeleteAccount(string accountID, string staffId)
        {
            var accountObject = bankDbContext.Accounts.Where(i => i.AccountId.Equals(accountID));
            if (!accountObject.Any())
            {
                return 0;
            }
            var staffObject = bankDbContext.Users.Where(i => i.UserId.Equals(staffId));
            if (!staffObject.Any())
            {
                return 0;
            }
            

            bankDbContext.Accounts.Remove(accountObject.ElementAt(0));
            return 1;


        }



    


        public static int AddNewCurrency(string newCurrency, int newCurrencyValue)
        {
            var currencyCheck = bankDbContext.AcceptedCurrencies.Where(i => i.Name.Equals(newCurrency));
            if (currencyCheck.Any())
            {
                return 0;
            }
           
            if (newCurrencyValue <= 0)
            {
                return 1;
            }
            AcceptedCurrency acceptedCurrency = new AcceptedCurrency();
            acceptedCurrency.Name = newCurrency;
            acceptedCurrency.Value=newCurrencyValue;
            bankDbContext.AcceptedCurrencies.Add(acceptedCurrency);
            return 2;
        }

        public static void UpdateChargesForSameBank(string bankId, float newRTGSCharges, float newIMPSCharges)
        {
            var bankObject = bankDbContext.Banks.Where(i => i.BankId.Equals(bankId));
            if(!bankObject.Any()) 
            { 
                return; 
            }
            bankObject.ElementAt(0).RTGSChargesForSame = newRTGSCharges;
            bankObject.ElementAt(0).IMPSChargesForSame = newIMPSCharges;
        }


        public static void UpdateChargesForOtherBank(string bankId, float newRTGSCharges, float newIMPSCharges)
        {
            var bankObject = bankDbContext.Banks.Where(i => i.BankId.Equals(bankId));
            if (!bankObject.Any())
            {
                return;
            }
            bankObject.ElementAt(0).RTGSChargesForOther = newRTGSCharges;
            bankObject.ElementAt(0).IMPSChargesForOther = newIMPSCharges;

        }
       
        public static int RevertTransaction(string customerId,string transactionID,string staffId,string bankId)
        {
            if (!ValidateCustomer(bankId, customerId) && !ValidateStaff(staffId, bankId)){
                return 0;
            }
            var transactionObject = bankDbContext.Transactions.Where(i => i.TransactionId.Equals(transactionID));
            if (!transactionObject.Any())
            {
                return 0;
            }
            else
            {
                var senderObject = bankDbContext.Accounts.Where(i => i.AccountId.Equals(transactionObject.ElementAt(0).SenderId));
                var recieverObject=bankDbContext.Accounts.Where(i=>i.AccountId.Equals(transactionObject.ElementAt(0).RecieverId));
                senderObject.ElementAt(0).Balance += transactionObject.ElementAt(0).SentAmount;
                recieverObject.ElementAt(0).Balance-=transactionObject.ElementAt(0).RecievedAmount;
                bankDbContext.Transactions.Remove(transactionObject.ElementAt(0));
                return 1;
            }
            

        }

        public static Boolean ValidateCustomer(string bankId,string accountId)
        {
            var StaffList = bankDbContext.Users.Where(i => (i.AccountId.Equals(accountId)) && (i.BankId.Equals(bankId)));
            if (StaffList.Any())
            {
                return true;
            }
            return false;
        }

        public static Boolean ValidateStaff(string staffId,string bankId)
        {
            var isStaff = bankDbContext.Users.Where(i =>( i.UserId.Equals(staffId)) && (i.BankId.Equals(bankId)));
            
            if(isStaff.Any())
            {
                return true;
            }
            return false;
        }


    }
}
