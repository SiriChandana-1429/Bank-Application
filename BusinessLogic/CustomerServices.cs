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
            var currentAccount = bankDBContext.Accounts.Where(i=>i.AccountId.Equals(currentAccountId));
            if(!currentAccount.Any() )
            {
                return 0;
            }
            if (depositAmount <= 0)
            {
                return 0;
            }
            currentAccount.ElementAt(0).Balance = currentAccount.ElementAt(0).Balance + depositAmount;
            return 1;
        }
        public static int WithDrawAmount(int withDrawAmount, string currentAccountId)
        {
            Bank bank = new Bank();
            var currentAccount = bank.Accounts.Where(i => i.AccountId.Equals(currentAccountId));
            if (!currentAccount.Any())
            {
                return 0;
            }

            if (withDrawAmount > currentAccount.ElementAt(0).Balance)
            {
                return 0;

            }
            else
            {
                currentAccount.ElementAt(0).Balance -= withDrawAmount;
                return 1;
            }
        }
        

        public static int TransferFunds(string senderId,string recieverId,float amount,string transferType,string senderBankId, string recieverBankId)
        {
            if(amount<= 0)
            {
                return 0;
            }
            if (!ValidateUser(recieverBankId, recieverId))
            {
                return 0;
            }
            float totalAmount;

            string transactionID;

            var senderBankObject = bankDBContext.Banks.Where(i => i.BankId.Equals(senderBankId));
            if(!senderBankObject.Any())
            {
                return 0;
            }

            var recieverBankObject = bankDBContext.Banks.Where(i => i.BankId.Equals(recieverBankId));
            if (!recieverBankObject.Any())
            {
                return 0;
            }
            if (transferType == "1")
            {
                if (senderBankObject.ElementAt(0).BankId != recieverBankObject.ElementAt(0).BankId)
                {
                    totalAmount = (senderBankObject.ElementAt(0).RTGSChargesForOther * amount) + amount;
                }
                else
                {
                    totalAmount = (senderBankObject.ElementAt(0).RTGSChargesForSame * amount) + amount;
                }
            }
            else
            {
                if (senderBankObject.ElementAt(0).BankId != recieverBankObject.ElementAt(0).BankId)
                {
                    totalAmount = (senderBankObject.ElementAt(0).IMPSChargesForOther * amount) + amount;

                }
                else
                {
                    totalAmount = (senderBankObject.ElementAt(0).IMPSChargesForSame * amount) + amount;
                }
            }

            var senderObject = bankDBContext.Accounts.Where(i => i.AccountId.Equals(senderId));
            var recieverObject = bankDBContext.Accounts.Where(i => i.AccountId.Equals(recieverId));
            if (!senderObject.Any() || !recieverObject.Any())
            {
                return 0;
            }
            if (totalAmount > senderObject.ElementAt(0).Balance)
            {

                return 0;
            }
            else
            {

                if (senderBankObject.ElementAt(0).Currency != recieverBankObject.ElementAt(0).Currency)
                {
                    var senderAcceptedCurrency = bankDBContext.AcceptedCurrencies.Where(i => i.Name.Equals(senderBankObject.ElementAt(0).Currency));
                    if (!senderAcceptedCurrency.Any())
                    {
                        return 0;
                    }
                    float senderExchangeValue = senderAcceptedCurrency.ElementAt(0).Value * amount;
                    var recieverAcceptedCurrency = bankDBContext.AcceptedCurrencies.Where(i => i.Name.Equals(recieverBankObject.ElementAt(0).Currency));
                    if (!recieverAcceptedCurrency.Any())
                    {
                        return 0;
                    }
                    float recieverExchangeValue = senderExchangeValue / recieverAcceptedCurrency.ElementAt(0).Value;
                    transactionID = "TXN" + senderBankObject.ElementAt(0).BankId + senderObject.ElementAt(0).AccountId;
                    Transaction transfer = new Transaction(senderObject.ElementAt(0).AccountId, recieverObject.ElementAt(0).AccountId, amount, recieverExchangeValue,transactionID);
                    bankDBContext.Transactions.Add(transfer);
                    senderObject.ElementAt(0).Balance -= totalAmount;
                    recieverObject.ElementAt(0).Transactions.Add(transfer);
                    recieverObject.ElementAt(0).Balance += recieverExchangeValue;

                }

                else
                {
                    transactionID = "TXN" + senderBankObject.ElementAt(0).BankId + senderObject.ElementAt(0).AccountId;
                    Transaction transfer = new Transaction(senderObject.ElementAt(0).AccountId, amount, recieverObject.ElementAt(0).AccountId,transactionID);
                    bankDBContext.Transactions.Add(transfer);
                    senderObject.ElementAt(0).Balance -= totalAmount;
                    recieverObject.ElementAt(0).Balance += amount;

                }
                return 1;
            }
        }


        public static Boolean ValidateUser(string bankId,string customerId)
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
