using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class BankStaffServices
    {


        public static string CreateAccount(string bankName, string firstName, string lastName, string email, string password, string bankId, BankStaff currentStaff)

        {
            int balance = 200;

            string accountHolderName = firstName + " " + lastName;
            DateTime now = DateTime.Now;
            string accountId = accountHolderName.Substring(0, 3) + now.ToString("");
            AccountHolder newAccount = new AccountHolder(accountHolderName, accountId, balance, bankName, bankId, firstName, password, email);


            foreach (var currentBank in Admin.AllBanks)
            {

                if (currentStaff.BankId.Equals(currentBank.Value.BankId))
                {
                    currentBank.Value.AllAccounts.Add(newAccount);

                    return newAccount.AccountId;

                }

            }
            return "0";




        }

        public static int DeleteAccount(string accountID, BankStaff currentStaff)
        {

            foreach (var currentBank in Admin.AllBanks)
            {
                if (currentStaff.BankId == currentBank.Value.BankId)
                {
                    var checkForValidAccountId = from acc in currentBank.Value.AllAccounts
                                                 where acc.AccountId == accountID
                                                 select acc;
                    foreach (AccountHolder acc in checkForValidAccountId)
                    {
                        currentBank.Value.AllAccounts.Remove(acc);
                        return 1;


                    }



                }

            }
            return 0;

        }
        public static int AddNewCurrency(string newCurrency, int newCurrencyValue)
        {
            if (Bank.AcceptedCurrencies.ContainsKey(newCurrency)) return 0;
            if (newCurrencyValue <= 0) return 1;
            Bank.AcceptedCurrencies.Add(newCurrency, newCurrencyValue);
            return 2;
        }

        public static void UpdateChargesForSameBank(Bank currentBank, double newRTGSCharges, double newIMPSCharges)
        {
            currentBank.RTGSChargesForSame = newRTGSCharges;
            currentBank.IMPSChargesForSame = newIMPSCharges;
        }


        public static void UpdateChargesForOtherBank(Bank currentBank, double newRTGSCharges, double newIMPSCharges)
        {
            currentBank.RTGSChargesForOther = newRTGSCharges;
            currentBank.IMPSChargesForOther = newIMPSCharges;

        }
        public static void RevertTransaction(string senderAccountId, string recieverAccountId, string reciverBankId, string transactionId, BankStaff currentStaff)
        {
            var checkForRecieverBank = from bank in Admin.AllBanks
                                       where bank.Value.BankId == reciverBankId
                                       select bank;
            foreach (var bank in checkForRecieverBank)
            {
                var checkForValidRecieverAccountId = from acc in bank.Value.AllAccounts
                                                     where acc.AccountId == recieverAccountId
                                                     select acc;

                foreach (AccountHolder acc in checkForValidRecieverAccountId)
                {
                    var checkForValidTransactionId = from transaction in acc.Transactions
                                                     where transaction.TransactionId == transactionId
                                                     select transaction;
                    foreach (Transaction transaction in checkForValidTransactionId)
                    {

                        acc.Balance -= transaction.RecievedAmount;

                    }
                }
            }

            foreach (var currentBank in Admin.AllBanks)
            {
                if (currentStaff.BankId == currentBank.Value.BankId)
                {
                    var checkForValidSenderAccountId = from acc in currentBank.Value.AllAccounts
                                                       where acc.AccountId == senderAccountId
                                                       select acc;

                    foreach (AccountHolder acc in checkForValidSenderAccountId)
                    {
                        var checkForValidTransactionId = from transaction in acc.Transactions
                                                         where transaction.TransactionId == transactionId
                                                         select transaction;

                        foreach (Transaction transaction in checkForValidTransactionId)
                        {
                            acc.Balance += transaction.SentAmount;

                        }
                    }
                }
            }


        }
    }
}
