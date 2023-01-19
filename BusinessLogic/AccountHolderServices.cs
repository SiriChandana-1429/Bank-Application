using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLogic
{
    public class AccountHolderServices
    {

        public static int DepositAmount(int depositAmount, AccountHolder currentHolder)
        {
            if (depositAmount <= 0) return 0;
            currentHolder.Balance = currentHolder.Balance + depositAmount;
            return 1;
        }
        public static int WithDrawAmount(int withDrawAmount, AccountHolder currentHolder)
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
        public static int TransferFunds(int amount, string transferAccount, string transferBankId, AccountHolder currentHolder, string transferType, Bank senderBankObject, Bank recieverBankObject)
        {
            double totalAmount;


            if (transferType == "1")
            {
                if (currentHolder.BankId != transferBankId)
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
                if (currentHolder.BankId != transferBankId)
                {
                    totalAmount = (senderBankObject.IMPSChargesForOther * amount) + amount;

                }
                else
                {
                    totalAmount = (senderBankObject.IMPSChargesForSame * amount) + amount;
                }
            }


            if (totalAmount > currentHolder.Balance)
            {

                return 0;
            }
            else
            {

                if (senderBankObject.Currency != recieverBankObject.Currency)
                {
                    double senderExchangeValue = Bank.AcceptedCurrencies[senderBankObject.Currency] * amount;
                    double recieverExchangeValue = senderExchangeValue / Bank.AcceptedCurrencies[recieverBankObject.Currency];
                    Transaction transfer = new Transaction(currentHolder.AccountId, amount, recieverExchangeValue, currentHolder.BankId, transferAccount, transferBankId);
                    currentHolder.Transactions.Add(transfer);

                    currentHolder.Balance -= totalAmount;

                }


                else
                {
                    Transaction transfer = new Transaction(currentHolder.AccountId, amount, amount, currentHolder.BankId, transferAccount, transferBankId);
                    currentHolder.Transactions.Add(transfer);
                    currentHolder.Balance -= totalAmount;
                }
                return 1;
            }
        }







    }
}
