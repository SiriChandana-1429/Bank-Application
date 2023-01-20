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


        public static string CreateAccount(string bankName, string firstName, string lastName, string email, string password, string bankId, Staff currentStaff)

        {
            int balance = 200;

            string accountHolderName = firstName + " " + lastName;
            DateTime now = DateTime.Now;
            string accountId = accountHolderName.Substring(0, 3) + now.ToString("");
            Customer newAccount = new Customer(accountHolderName, accountId, balance, bankName, bankId, firstName, password, email);


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

        public static int DeleteAccount(string accountID, Staff currentStaff)
        {

            foreach (var currentBank in Admin.AllBanks)
            {
                if (currentStaff.BankId == currentBank.Value.BankId)
                {
                    var checkForValidAccountId = from acc in currentBank.Value.AllAccounts
                                                 where acc.AccountId == accountID
                                                 select acc;
                    foreach (Customer acc in checkForValidAccountId)
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
        //public static void RevertTransaction(string senderAccountId, string recieverAccountId, string reciverBankId, string transactionId, Staff currentStaff)
        //{
        //    var checkForRecieverBank = from bank in Admin.AllBanks
        //                               where bank.Value.BankId == reciverBankId
        //                               select bank;
        //    foreach (var bank in checkForRecieverBank)
        //    {
        //        var checkForValidRecieverAccountId = from acc in bank.Value.AllAccounts
        //                                             where acc.AccountId == recieverAccountId
        //                                             select acc;
        //
        //        foreach (Customer acc in checkForValidRecieverAccountId)
        //        {
        //            var checkForValidTransactionId = from transaction in acc.Transactions
        //                                             where transaction.TransactionId == transactionId
        //                                             select transaction;
        //            foreach (Transaction transaction in checkForValidTransactionId)
        //            {
        //
        //                acc.Balance -= transaction.RecievedAmount;
        //
        //            }
        //        }
        //    }
        //
        //    foreach (var currentBank in Admin.AllBanks)
        //    {
        //        if (currentStaff.BankId == currentBank.Value.BankId)
        //        {
        //            var checkForValidSenderAccountId = from acc in currentBank.Value.AllAccounts
        //                                               where acc.AccountId == senderAccountId
        //                                               select acc;
        //
        //            foreach (Customer acc in checkForValidSenderAccountId)
        //            {
        //                var checkForValidTransactionId = from transaction in acc.Transactions
        //                                                 where transaction.TransactionId == transactionId
        //                                                 select transaction;
        //
        //                foreach (Transaction transaction in checkForValidTransactionId)
        //                {
        //                    acc.Balance += transaction.SentAmount;
        //
        //                }
        //            }
        //        }
        //    }
        //
        //
        //}

        public static int RevertTransaction(Customer customer,string transactionID,Staff currentStaff,Bank currentBank)
        {
            if (!ValidateCustomer(Admin.AllBanks[currentStaff.BankName],customer) && !ValidateStaff(currentStaff,currentBank)){
                return 0;
            }
            foreach(Transaction transaction in customer.Transactions)
            {
                if (transaction.TransactionId.Equals(transactionID))
                {
                    transaction.Sender.Balance += transaction.SentAmount;
                    transaction.Reciever.Balance -= transaction.RecievedAmount;
                    transaction.Sender.Transactions.Remove(transaction);
                    transaction.Sender.Transactions.Remove(transaction);
                    return 1;
                }
            }
            return 0;


        }

        public static Boolean ValidateCustomer(Bank bank,Customer customer)
        {
            foreach(Customer currentCustomer in bank.AllAccounts)
            {
                if (customer.AccountId.Equals(currentCustomer.AccountId)){
                    return true;
                }
            }
            return false;
        }

        public static Boolean ValidateStaff(Staff currentStaff,Bank currentBank)
        {
            var checkForStaff=currentBank.Staff.Where(i=>i.UserName.Equals(currentStaff.UserName));
            if(checkForStaff.Count() > 0)
            {
                return true;
            }
            return false;
        }






    }
}
