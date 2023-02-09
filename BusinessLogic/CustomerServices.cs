using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Models;




namespace BusinessLogic
{

    public class CustomerServices
    {
        static BankDataBaseContext bankDBContext = new BankDataBaseContext();
        public static int DepositAmount(int depositAmount, string currentAccountId)
        {
            List<Account> currentAccount = bankDBContext.Accounts.Where(i => i.AccountId.Equals(currentAccountId)).ToList();
            if (!(currentAccount.Count() > 0))
            {
                return 0;
            }
            if (depositAmount <= 0)
            {
                return 0;
            }
            currentAccount[0].Balance = currentAccount[0].Balance + depositAmount;
            bankDBContext.SaveChanges();
            return 1;
        }
        public static int WithDrawAmount(int withDrawAmount, string currentAccountId)
        {
            Bank bank = new Bank();
            List<Account> currentAccount = bank.Accounts.Where(i => i.AccountId.Equals(currentAccountId)).ToList();
            if (!(currentAccount.Count() > 0))
            {
                return 0;
            }

            if (withDrawAmount > currentAccount[0].Balance)
            {
                return 0;

            }
            else
            {
                currentAccount[0].Balance -= withDrawAmount;
                bankDBContext.SaveChanges();
                return 1;
            }
        }


        public static int TransferFunds(string senderId, string recieverId, float amount, string transferType, string senderBankId, string recieverBankId)
        {
            if (amount <= 0)
            {
                return 0;
            }
            if (!ValidateUser(recieverBankId, recieverId))
            {
                return 0;
            }
            float totalAmount;

            string transactionID;

            List<Bank> senderBankObject = bankDBContext.Banks.Where(i => i.BankId.Equals(senderBankId)).ToList();
            if (!senderBankObject.Any())
            {
                return 0;
            }

            List<Bank> recieverBankObject = bankDBContext.Banks.Where(i => i.BankId.Equals(recieverBankId)).ToList();
            if (!recieverBankObject.Any())
            {
                return 0;
            }
            if (transferType == "1")
            {
                if (senderBankObject[0].BankId != recieverBankObject[0].BankId)
                {
                    totalAmount = (senderBankObject[0].RTGSChargesForOther * amount) + amount;
                }
                else
                {
                    totalAmount = (senderBankObject[0].RTGSChargesForSame * amount) + amount;
                }
            }
            else
            {
                if (senderBankObject[0].BankId != recieverBankObject[0].BankId)
                {
                    totalAmount = (senderBankObject[0].IMPSChargesForOther * amount) + amount;

                }
                else
                {
                    totalAmount = (senderBankObject[0].IMPSChargesForSame * amount) + amount;
                }
            }

            List<Account> senderObject = bankDBContext.Accounts.Where(i => i.AccountId.Equals(senderId)).ToList();
            List<Account> recieverObject = bankDBContext.Accounts.Where(i => i.AccountId.Equals(recieverId)).ToList();
            if (!(senderObject.Count() > 0) || !(recieverObject.Count()>0))
            {
                return 0;
            }
            if (totalAmount > senderObject[0].Balance)
            {

                return 0;
            }
            else
            {

                if (senderBankObject[0].Currency != recieverBankObject[0].Currency)
                {
                    List<AcceptedCurrency> senderAcceptedCurrency = bankDBContext.AcceptedCurrencies.Where(i => i.Name.Equals(senderBankObject[0].Currency)).ToList();
                    if (!(senderAcceptedCurrency.Count()>0))
                    {
                        return 0;
                    }
                    float senderExchangeValue = senderAcceptedCurrency[0].Value * amount;
                    List<AcceptedCurrency> recieverAcceptedCurrency = bankDBContext.AcceptedCurrencies.Where(i => i.Name.Equals(recieverBankObject[0].Currency)).ToList();
                    if (!(recieverAcceptedCurrency.Count()>0))
                    {
                        return 0;
                    }
                    float recieverExchangeValue = senderExchangeValue / recieverAcceptedCurrency[0].Value;
                    transactionID = "TXN" + senderBankObject[0].BankId + senderObject[0].AccountId;
                    Transaction transfer = new Transaction(senderObject[0].AccountId, recieverObject[0].AccountId, amount, recieverExchangeValue, transactionID);
                    bankDBContext.Transactions.Add(transfer);
                    bankDBContext.SaveChanges();
                    senderObject[0].Balance -= totalAmount;
                    recieverObject[0].Transactions.Add(transfer);
                    recieverObject[0].Balance += recieverExchangeValue;

                }

                else
                {
                    transactionID = "TXN" + senderBankObject[0].BankId + senderObject[0].AccountId;
                    Transaction transfer = new Transaction(senderObject[0].AccountId, amount, recieverObject[0].AccountId, transactionID);
                    bankDBContext.Transactions.Add(transfer);
                    bankDBContext.SaveChanges();
                    senderObject[0].Balance -= totalAmount;
                    recieverObject[0].Balance += amount;

                }
                return 1;
            }
        }


        public static Boolean ValidateUser(string bankId, string customerId)
        {
            var isUser = bankDBContext.Users.Where(i => i.AccountId.Equals(customerId));
            if (!isUser.Any())
            {
                return false;
            }

            return true;

        }







    }
}
