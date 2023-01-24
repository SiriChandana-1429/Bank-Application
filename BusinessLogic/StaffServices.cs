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
            Customer newCustomer = new Customer(accountHolderName, accountId, bankName, bankId, firstName, password, email);
            Account newAccount = new Account(accountId, balance);


            foreach (var currentBank in Admin.AllBanks)
            {

                if (currentStaff.BankId.Equals(currentBank.Value.BankId))
                {
                    currentBank.Value.AllUsers.Add(newCustomer);
                    currentBank.Value.Accounts.Add(newAccount);
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
                    var checkForValidAccountId = from acc in currentBank.Value.AllUsers
                                                 where acc.AccountId == accountID
                                                 select acc;
                    
                    if (checkForValidAccountId.Any())
                    {
                        Console.WriteLine("No such account found.");
                        return 0;
                        
                    }
                    foreach (Customer acc in checkForValidAccountId)
                    {
                        currentBank.Value.AllUsers.Remove(acc);
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
       
        public static int RevertTransaction(Account customer,string transactionID,User currentStaff,Bank currentBank)
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
                    transaction.Reciever.Transactions.Remove(transaction);
                    return 1;
                }
            }
            return 0;


        }

        public static Boolean ValidateCustomer(Bank bank,Account account)
        {
            var StaffList = bank.AllUsers.Where(i => i.TypeOfUser.Equals(UserType.Customer));
            foreach(Customer currentCustomer in StaffList)
            {
                if (account.AccountId.Equals(currentCustomer.AccountId)){
                    return true;
                }
            }
            return false;
        }

        public static Boolean ValidateStaff(User currentStaff,Bank currentBank)
        {
            var checkForStaff=currentBank.AllUsers.Where(i=>(i.Name.Equals(currentStaff.Name) && (i.TypeOfUser.Equals("staff"))));
            if(checkForStaff.Any())
            {
                return true;
            }
            return false;
        }


    }
}
