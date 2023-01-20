using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;



namespace BusinessLogic
{
    public class CustomerServices
    {

        public static int DepositAmount(int depositAmount, Customer currentHolder)
        {
            if (depositAmount <= 0) return 0;
            currentHolder.Balance = currentHolder.Balance + depositAmount;
            return 1;
        }
        public static int WithDrawAmount(int withDrawAmount, Customer currentHolder)
        {
            if (withDrawAmount > currentHolder.Balance)
            {
                return 0;

            }
            else
            {
                currentHolder.Balance -= withDrawAmount;
                return 1;
            }
        }
        

        public static int TransferFunds(Customer sender,Customer reciever,double amount,string transferType,Bank senderBankObject, Bank recieverBankObject)
        {
            if (!ValidateUser(recieverBankObject, reciever))
            {
                return 0;
            }
            double totalAmount;

            string transactionID;


            if (transferType == "1")
            {
                if (sender.BankId != reciever.BankId)
                {
                    totalAmount = (senderBankObject.RTGSChargesForOther * amount) + amount;
                }
                else
                {
                    totalAmount = (senderBankObject.RTGSChargesForSame * amount) + amount;
                }
            }
            else
            {
                if (sender.BankId != reciever.BankId)
                {
                    totalAmount = (senderBankObject.IMPSChargesForOther * amount) + amount;

                }
                else
                {
                    totalAmount = (senderBankObject.IMPSChargesForSame * amount) + amount;
                }
            }


            if (totalAmount > sender.Balance)
            {

                return 0;
            }
            else
            {

                if (senderBankObject.Currency != recieverBankObject.Currency)
                {
                    double senderExchangeValue = Bank.AcceptedCurrencies[senderBankObject.Currency] * amount;
                    double recieverExchangeValue = senderExchangeValue / Bank.AcceptedCurrencies[recieverBankObject.Currency];
                    transactionID = "TXN" + sender.BankId + sender.AccountId;
                    Transaction transfer = new Transaction(sender, reciever, amount, recieverExchangeValue,transactionID);
                    sender.Transactions.Add(transfer);
                    sender.Balance -= totalAmount;
                    reciever.Transactions.Add(transfer);
                    reciever.Balance += recieverExchangeValue;

                }

                else
                {
                    transactionID = "TXN" + sender.BankId + sender.AccountId;
                    Transaction transfer = new Transaction(sender, amount, reciever,transactionID);
                    sender.Transactions.Add(transfer);
                    sender.Balance -= totalAmount;
                    reciever.Transactions.Add(transfer) ;
                    reciever.Balance += amount;

                }
                return 1;
            }
        }


        public static Boolean ValidateUser(Bank checkBank,Customer checkCustomer)
        {
            foreach(Customer customer in checkBank.AllAccounts)
            {
                if(customer.AccountId.Equals(checkCustomer.AccountId))
                {
                    return true;
                }

            }
            return false;

        }







    }
}
